using Core.Domain.Entities;
using Core.Exceptions;
using Core.Repository;

namespace Core.Service;

public class UserService(UserRepository userRepository) : IUserService
{
    public async Task<User> CreateUser(User user)
    {
        var existingUserByDocument = await userRepository.FindByDocumentAsync(user.Document);
        var existingUserByEmail = await userRepository.FindByEmailAsync(user.Email);

        var validationErrors = new List<string>();

        if (existingUserByDocument != null) validationErrors.Add("The document is already taken.");
        if (existingUserByEmail != null) validationErrors.Add("The email is already taken.");

        if (validationErrors.Any()) throw new ValidationException(validationErrors);

        await userRepository.AddAsync(user);
        return user;
    }
}