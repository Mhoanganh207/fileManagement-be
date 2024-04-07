using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace fileManagement.Models;

public class AppFile
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public int Left { get; set; }
    public int Right { get; set; }

    public Guid ParentId { get; set; }

    public required string Mime { get; set; }

    public long Size { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }


    [JsonIgnore]
    public Guid UserId { get; set; }

    [JsonIgnore]
    public User User { get; set; }


    public AppFile()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }
    public AppFile(IFormFile file)
    {
        Name = file.FileName;
        Size = file.Length;
        Mime = file.ContentType;
        Left = 0;
        Right = 0;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

}
