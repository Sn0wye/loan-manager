using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Core.DTO.Response;

public class ApplyForLoanResponse
{
    public LoanStatus Status { get; set; }
    public required string Message { get; set; }
    public double Amount { get; set; }
    public int Term { get; set; }
    public Loan? SuggestedLoan { get; set; }
}