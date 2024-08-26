using Risk.Domain.Entities;
using Risk.Infrastructure;

namespace Risk.Repository;

public class LoanRepository(ApplicationDbContext context)
{
    public Loan? Find(int id)
    {
        return context.Loans.FirstOrDefault(loan => loan.Id == id);
    }
}
