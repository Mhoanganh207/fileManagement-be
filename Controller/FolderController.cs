
using fileFolder.DTOs;
using fileFolder.Services;
using Microsoft.AspNetCore.Mvc;




namespace fileFolder.Controllers;


[ApiController]
[Route("api/folder")]
public class FolderController : ControllerBase
{


    private readonly IFileService _fileService;

    public FolderController(IFileService fileService)
    {
        _fileService = fileService;
    }




    [HttpPost]
    public async Task<IActionResult> CreateFolder(string parentId, Folder folder)
    {

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
        await _fileService.CreateFolder(treeFile, Guid.Parse(parentId));


        return Ok("Files uploaded successfully.");

    }

}