using fileFolder.Models;

namespace fileFolder.Repositories;

public interface IFileRepository
{
    Task<AppFile> CreateFoleder(AppFile folder);
    Task<AppFile> GetFile(Guid id);
    Task<IEnumerable<AppFile>> GetFiles(Guid? parentId);
    Task<AppFile> CreateFile(AppFile file);
    Task<AppFile> UpdateFile(AppFile file);
    Task DeleteFile(Guid id);
}
