using Core.Domain.Entities;
using Core.Repository;

namespace Core.Service;

public class UserService(UserRepository userRepository)
{
    public User CreateUser(User user)
    {
        userRepository.Add(user);
        return user;
    }
}