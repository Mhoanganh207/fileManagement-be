using fileFolder.Models;
using fileFolder.Services;
using Microsoft.AspNetCore.Mvc;

namespace fileFolder.Controllers;


[ApiController]
[Route("api/file")]
public class FileController : ControllerBase
{

    public readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFiles(Guid? parentId)
    {
        throw new NotImplementedException();
    }


    [HttpPost]
    public async Task<IActionResult> CreateFile(string parentId, IFormFile file)
    {
        AppFile newFile = new(file)
        {
            ParentId = Guid.Parse(parentId)
        };
        var result = await _fileService.CreateFile(newFile);
        return Ok(result);

    }


}