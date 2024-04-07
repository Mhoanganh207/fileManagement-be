

using fileManagement.Models;

namespace fileManagement.Services;

public interface IFileService
{
    Task<AppFile> CreateFile(AppFile file);
    Task CreateFolder(Dictionary<string,object> folder,Guid parentId, Guid userId);
    Task<AppFile> GetFile(Guid id);
    Task<IEnumerable<AppFile>> GetFiles(Guid? parentId);
    Task<AppFile> UpdateFile(AppFile file);
    Task DeleteFile(Guid id);
}
