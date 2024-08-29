using Core.Domain.Entities;
using Core.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository;

public class UserRepository()
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) : this()
    {
        _context = context;
    }

    public async Task<User?> FindByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}