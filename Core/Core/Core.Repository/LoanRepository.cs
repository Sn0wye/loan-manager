using Core.Domain.Entities;
using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository;

public class LoanRepository : ILoanRepository
{
    private readonly ApplicationDbContext _context;

    public LoanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Loan?> FindAsync(int id)
    {
        return await _context.Loans.FirstOrDefaultAsync(loan => loan.Id == id);
    }

    public async Task<Loan> AddAsync(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
        await _context.SaveChangesAsync();
        return loan;
    }
}