using Core.Domain.Entities;

namespace Core.Repository.Interfaces;

public interface IUserRepository
{
    Task<User?> FindByIdAsync(int id);
    Task<User?> FindByDocumentAsync(string document);
    Task<User?> FindByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}