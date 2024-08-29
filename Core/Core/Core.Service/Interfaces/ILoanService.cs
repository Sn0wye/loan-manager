using Core.Domain.Entities;

namespace Core.Service;

public interface ILoanService
{
    Task<LoanApplication> ApplyForLoan(User user, double loanAmount, int term);
}