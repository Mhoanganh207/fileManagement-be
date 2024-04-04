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

    public Task<AppFile> CreateFile(AppFile file)
    {
        return _fileRepository.CreateFile(file);
    }

    public Task<AppFile> CreateFolder(AppFile folder)
    {
        throw new NotImplementedException();
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