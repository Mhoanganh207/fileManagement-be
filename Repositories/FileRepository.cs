using fileFolder.Data;
using fileFolder.Models;
using Microsoft.EntityFrameworkCore;

namespace fileFolder.Repositories;

public class FileRepository : IFileRepository
{

    public readonly AppDbContext _context;

    public FileRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<AppFile> CreateFile(AppFile file)
    {
        var parent = await _context.Files.FirstOrDefaultAsync(f => f.Id.ToString() == file.ParentId.ToString());
        if (parent == null)
        {
            throw new Exception("Parent not found");
        }
        file.Left = parent.Left + 1;
        file.Right = parent.Left + 2;
        var result = await _context.Files.AddAsync(file);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public Task CreateFoleder(AppFile folder)
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