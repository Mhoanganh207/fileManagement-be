
using System.Security.Claims;
using fileManagement.DTOs;
using fileManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;




namespace fileManagement.Controllers;


[ApiController]
[Route("api/folder")]
public class FolderController : ControllerBase
{


    private readonly IFileService _fileService;

    private readonly IAuthService _authService;

    public FolderController(IFileService fileService, IAuthService authService)
    {
        _fileService = fileService;
        _authService = authService;
    }



    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateFolder(string parentId, Folder folder)
    {
        var email = User.FindFirst(ClaimTypes.Name)?.Value;
        if (email == null)
        {
            return Unauthorized();
        }
        var user = await _authService.GetUserByEmail(email);

        if (user == null)
        {
            return Unauthorized();
        }

        if (folder == null || folder.Files.Count() == 0)
        {
            return BadRequest("No files were uploaded.");
        }


        Dictionary<string, object> treeFile = [];
        foreach (var file in folder.Files)
        {

            string[] path = file.FileName.Split("/");
            Dictionary<string, object> currentNode = treeFile;
            foreach (var folderName in path)
            {

                if (!treeFile.ContainsKey(folderName))
                {
                    currentNode[folderName] = new Dictionary<string, object>();
                }

                if (folderName == path.Last())
                {
                    currentNode[folderName] = new FormFile(file.OpenReadStream(), 0, file.Length, file.Name, folderName)
                    {
                        Headers = file.Headers,
                        ContentType = file.ContentType
                    };
                }
                else
                {
                    currentNode = (Dictionary<string, object>)currentNode[folderName];
                }
            }
        }
        await _fileService.CreateFolder(treeFile, Guid.Parse(parentId),user.Id);


        return Ok("Files uploaded successfully.");

    }

}