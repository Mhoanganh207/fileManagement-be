
using fileManagement.Models;

namespace fileManagement.DTOs;


public class UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Token { get; set; }

    public UserResponse(User user, string token)
    {
        Id = user.Id;
        Username = user.Name;
        Token = token;
    }
}

