using fileManagement.Data;
using fileManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace fileManagement.Repositories;

public class UserRepository (AppDbContext _context) : IUserRepository
{
    public async Task<User> CreateUser(User user)
    {
        var res = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        var folder = new AppFile()
        {
            Left = 1,
            Right = 2,
            Name = "root",
            Mime = "folder",
            UserId = res.Entity.Id
        };
        await _context.Files.AddAsync(folder);
        await _context.SaveChangesAsync();

        return res.Entity;
    }

    public Task DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if(user == null)
        {
            throw new Exception("User not found");
        }

        return user;
    }

    public async Task<User?> GetUser(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if(user != null)
        {
            return user;
        }
        else
        {
            return null;
        }
    }

    public Task<User> UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}