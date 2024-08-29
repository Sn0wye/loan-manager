using Core.Domain.Entities;

namespace Core.Service;

public interface IUserService
{
    Task<User> CreateUser(User user);
}