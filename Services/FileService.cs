using System.Text.Json;
using fileFolder.DTOs;
using fileFolder.Models;
using fileFolder.Repositories;


namespace fileFolder.Services;

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

    public async Task CreateFolder(Dictionary<string, object> folder, Guid parentId)
    {
        foreach (var key in folder.Keys)
        {
           
            if (folder[key] is IFormFile)
            {
                AppFile file = new((IFormFile)folder[key])
                {
                    ParentId = parentId
                };
                await CreateFile(file);
            }
            else
            {
                AppFile newFolder = new()
                {
                    Name = key,
                    ParentId = parentId,
                    Mime = "folder"
                };
                var res = await CreateFile(newFolder);

                await CreateFolder((Dictionary<string, object>)folder[key], newFolder.Id);
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