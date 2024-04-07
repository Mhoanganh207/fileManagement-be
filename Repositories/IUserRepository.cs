using fileManagement.Models;

namespace fileManagement.Repositories;

public interface IUserRepository
{
    Task<User> GetUser(Guid id);
    Task<User?> GetUser(string username);
    Task<User> CreateUser(User user);
    Task<User> UpdateUser(User user);
    Task DeleteUser(Guid id);
}