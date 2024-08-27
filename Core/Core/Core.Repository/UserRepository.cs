using Core.Domain.Entities;
using Core.Infrastructure;

namespace Core.Repository;

public class UserRepository()
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) : this()
    {
        _context = context;
    }
    
    public User? FindById(int id)
    {
        return _context.Users.FirstOrDefault(user => user.Id == id);
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}