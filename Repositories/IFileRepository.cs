

using fileManagement.DTOs;
using fileManagement.Models;

namespace fileManagement.Repositories;

public interface IFileRepository
{
    Task<AppFile> GetFile(Guid id);
    Task<IEnumerable<AppFile>> GetFiles(Guid? parentId);
    Task<AppFile> CreateFile(AppFile file);
    Task<AppFile> UpdateFile(AppFile file);
    Task DeleteFile(Guid id);
}
