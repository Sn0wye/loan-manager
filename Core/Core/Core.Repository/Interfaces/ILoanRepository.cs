using Core.Domain.Entities;

namespace Core.Repository;

public interface ILoanRepository
{
    Task<Loan?> FindAsync(int id);
    Task<Loan> AddAsync(Loan loan);
}