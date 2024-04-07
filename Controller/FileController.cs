using System.Security.Claims;
using fileManagement.Models;
using fileManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fileManagement.Controllers;


[ApiController]
[Route("api/file")]
public class FileController : ControllerBase
{

    private readonly IFileService _fileService;

    private readonly IAuthService _authService;



    public FileController(IFileService fileService, IAuthService authService)
    {
        _fileService = fileService;
        _authService = authService;
    }

    // [Authorize]
    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetFiles(Guid? parentId)
    // {
    //     throw new NotImplementedException();
    // }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateFile(string parentId, IFormFile file)
    {
        var email =  User.FindFirst(ClaimTypes.Name)?.Value;
        if(email == null){
            return Unauthorized();
        }
        var user = await _authService.GetUserByEmail(email);


        if(user == null){
            return Unauthorized();
        }
        AppFile newFile = new(file)
        {
            ParentId = Guid.Parse(parentId),
            Name = file.FileName,
            Mime = file.ContentType,
            UserId = user.Id
        };
        var result = await _fileService.CreateFile(newFile);
        return Ok(result);
    }


}