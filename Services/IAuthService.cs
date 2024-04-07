

using fileManagement.DTOs;
using fileManagement.Models;

namespace fileManagement.Services;

public interface IAuthService{

    public string GenerateToken(string email);

    public string HashPassword(string password);

    public bool VerifyPassword(string password, string hashedPassword);


    public Task<User?> GetUserByEmail(string email);

    public  Task<User> RegisterUser(User user);

    public Task<bool> LoginUser(UserLoginRequest user);
}