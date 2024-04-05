using fileFolder.Data;
using fileFolder.DTOs;
using fileFolder.Models;
using Microsoft.EntityFrameworkCore;

namespace fileFolder.Repositories;

public class FileRepository(AppDbContext context) : IFileRepository
{

    public readonly AppDbContext _context = context;

    public async Task<AppFile> CreateFile(AppFile file)
    {
        var result = await _context.Files.AddAsync(file);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task CreateFoleder(Folder folder)
    {
        

        
    }

    public Task DeleteFile(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<AppFile> GetFile(Guid id)
    {
        var file = await _context.Files.FindAsync(id);

        if(file == null)
        {
            throw new Exception("File not found");
        }

        return file;
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