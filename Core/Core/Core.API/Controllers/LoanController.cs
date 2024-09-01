using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.DTO.Response;
using Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("loan")]
public class LoanController : ControllerBase
{
    private readonly ILoanService _loanService;
    private readonly IUserService _userService;

    public LoanController(ILoanService loanService, IUserService userService)
    {
        _loanService = loanService;
        _userService = userService;
    }

    [HttpPost("/apply")]
    public async Task<ActionResult<ApplyForLoanResponse>> ApplyForLoan([FromBody] ApplyForLoanRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Age = request.Age,
            Document = request.Document,
            YearlyIncome = request.YearlyIncome
        };

        var createdUser = await _userService.CreateUser(user);

        var application = await _loanService.ApplyForLoan(
            createdUser,
            request.LoanAmount,
            request.Term
        );

        string? message;
        if (application.SuggestedLoan is null)
        {
            message = application.Loan.Status == LoanStatus.APPROVED
                ? "Loan approved :)"
                : "Loan rejected, unfortunately we don't have a better option for you at the moment :(";

            return new ApplyForLoanResponse
            {
                Message = message,
                Status = application.Loan.Status,
                Amount = application.Loan.Amount,
                Term = application.Loan.Term,
                SuggestedLoan = null
            };
        }

        message = application.Loan.Status == LoanStatus.APPROVED
            ? "Loan approved, but we have a better option for you!"
            : "Loan rejected, but we have a better option for you!";

        return new ApplyForLoanResponse
        {
            Message = message,
            Status = application.Loan.Status,
            Amount = application.Loan.Amount,
            Term = application.Loan.Term,
            SuggestedLoan = application.SuggestedLoan
        };
    }
}