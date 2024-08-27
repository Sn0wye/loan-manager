using API.Model.Request;
using API.Model.Response;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("loan")]
public class LoanController
{
    private readonly LoanService _loanService;
    private readonly UserService _userService;

    public LoanController(LoanService loanService, UserService userService)
    {
        _loanService = loanService;
        _userService = userService;
    }

    [HttpPost("/apply")]
    public async Task<ActionResult<ApplyForLoanResponse>> ApplyForLoan([FromBody] ApplyForLoanRequest request)
    {
        var user = new User(
            request.Name,
            request.Email,
            request.Age,
            request.Document,
            request.AnnualIncome
        );

        var createdUser = _userService.CreateUser(user);

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

            return new ApplyForLoanResponse(
                application.Loan.Status,
                message,
                application.Loan.Amount,
                application.Loan.Term,
                null
            );
        }

        message = application.Loan.Status == LoanStatus.APPROVED
            ? "Loan approved, but we have a better option for you!"
            : "Loan rejected, but we have a better option for you!";

        return new ApplyForLoanResponse(
            application.Loan.Status,
            message,
            application.Loan.Amount,
            application.Loan.Term,
            application.SuggestedLoan
        );
    }
}