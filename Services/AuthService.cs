using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using fileManagement.DTOs;
using fileManagement.Models;
using fileManagement.Repositories;
using Microsoft.IdentityModel.Tokens;


namespace fileManagement.Services;

public class AuthService : IAuthService
{

    private readonly IConfiguration _config;

    private readonly string _key;

    private readonly IUserRepository _userRepository;

    public AuthService(IConfiguration config, IUserRepository userRepository)
    {
        _config = config;
        _key = _config.GetSection("SecretKey")?.Value ?? string.Empty;
        _userRepository = userRepository;
    }

    public string GenerateToken(string email)
    {
        var clamis = new[]
        {
            new Claim(ClaimTypes.Name, email),
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(clamis),
            Expires = DateTime.Now.AddSeconds(86400),
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return jwt;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
       var user = await _userRepository.GetUser(email);
       if(user == null){
        return null;
       }
       return user;
    }

    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public async Task<bool> LoginUser(UserLoginRequest req)
    {
        var user = await _userRepository.GetUser(req.Email);
        if (user == null)
        {
            return false;
        }
        if (VerifyPassword(req.Password, user.Password))
        {
            Console.WriteLine(1);
            return true;
        }
        return false;

    }

    public async Task<User> RegisterUser(User user)
    {
        return await _userRepository.CreateUser(user);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return HashPassword(password) == hashedPassword;
    }
}