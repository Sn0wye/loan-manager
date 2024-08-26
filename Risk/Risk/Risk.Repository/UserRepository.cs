using Risk.Domain.Entities;
using Risk.Infrastructure;

namespace Risk.Repository;

public class UserRepository(ApplicationDbContext context)
{
    public User? Find(int id)
    {
        return context.Users.FirstOrDefault(user => user.Id == id);
    }
}