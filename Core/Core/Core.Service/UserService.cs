using Core.Domain.Entities;
using Core.Repository;

namespace Core.Service;

public class UserService(UserRepository userRepository) : IUserService
{
    public async Task<User> CreateUser(User user)
    {
        await userRepository.AddAsync(user);
        return user;
    }
}