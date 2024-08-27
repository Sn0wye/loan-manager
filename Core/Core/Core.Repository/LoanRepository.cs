using Core.Domain.Entities;
using Core.Infrastructure;

namespace Core.Repository;

public class LoanRepository()
{
    private readonly ApplicationDbContext _context;

    public LoanRepository(ApplicationDbContext context) : this()
    {
        _context = context;
    }

    public Loan? Find(int id)
    {
        return _context.Loans.FirstOrDefault(loan => loan.Id == id);
    }

    public void Add(Loan loan)
    {
        _context.Loans.Add(loan);
        _context.SaveChanges();
    }
}