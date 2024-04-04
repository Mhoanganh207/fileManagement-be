using fileFolder.Models;
using Microsoft.EntityFrameworkCore;

namespace fileFolder.Data;

public class AppDbContext : DbContext

{


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    

    public  DbSet<AppFile> Files { get; set; }
}
