using System.Text.Json;
using fileManagement.Models;
using fileManagement.Repositories;



namespace fileManagement.Services;

public class FileService : IFileService
{

    public readonly IFileRepository _fileRepository;


    public FileService(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<AppFile> CreateFile(AppFile file)
    {
        var parent = _fileRepository.GetFile(file.ParentId).Result;
        file.Left = parent.Left + 1;
        file.Right = parent.Left + 2;
        return await _fileRepository.CreateFile(file);
    }

    public async Task CreateFolder(Dictionary<string, object> folder, Guid parentId,Guid userId)
    {
        foreach (var key in folder.Keys)
        {
           
            if (folder[key] is IFormFile)
            {

                var file = (IFormFile)folder[key];
                var newFile = new AppFile(file)
                {
                    ParentId = parentId,
                    Name = file.FileName,
                    Mime = file.ContentType,
                    UserId = userId
                };
                await CreateFile(newFile);
            }
            else
            {
                AppFile newFolder = new()
                {
                    Name = key,
                    ParentId = parentId,
                    Mime = "folder",
                    UserId  = userId
                };
                var res = await CreateFile(newFolder);

                await CreateFolder((Dictionary<string, object>)folder[key], newFolder.Id,userId);
            }
        }

        return;
    }

   

    public Task DeleteFile(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<AppFile> GetFile(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AppFile>> GetFiles(Guid? parentId)
    {
        throw new NotImplementedException();
    }

    public Task<AppFile> UpdateFile(AppFile file)
    {
        throw new NotImplementedException();
    }
}