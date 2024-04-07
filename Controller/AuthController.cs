using fileManagement.DTOs;
using fileManagement.Models;
using fileManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace fileManagement.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase{
    

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService){
        _authService = authService;
    }

    [HttpPost("/register")]
    public async Task<IActionResult> RegisterUser(UserRegisterRequest req){
        var user =  new User
        {
            Name = req.Name,
            Email = req.Email,
            Password = _authService.HashPassword(req.Password),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        var result = await _authService.RegisterUser(user);
        var accessToken = _authService.GenerateToken(user.Email);
        return Ok(new UserResponse(user,accessToken));
    }

    [HttpPost("/login")]
    public async Task<IActionResult> LoginUser(UserLoginRequest req){
        var result = await _authService.LoginUser(req);
        if(!result){
            return Unauthorized();
        }
        var accessToken = _authService.GenerateToken(req.Email);
        return Ok(new {
            accessToken = accessToken
        });
    }
}