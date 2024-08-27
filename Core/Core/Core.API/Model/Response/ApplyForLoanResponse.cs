using Core.Domain.Entities;
using Core.Domain.Enums;

namespace API.Model.Response;

public class ApplyForLoanResponse(LoanStatus status, string message, double amount, int term, Loan? loan)
{
    public Loan? SuggestedLoan = loan;
    public LoanStatus Status { get; set; } = status;
    public string Message { get; set; } = message;
    public double Amount { get; set; } = amount;
    public int Term { get; set; } = term;
}