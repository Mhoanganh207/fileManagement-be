using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fileFolder.Models;

public class AppFile
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }

    public int Left { get; set; }
    public int Right { get; set; }

    public Guid ParentId { get; set; }

    public string Mime { get; set; }

    public long Size { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }


    public AppFile()
    {
       
    }
    public AppFile(IFormFile file){
        Name = file.FileName;
        Size = file.Length;
        Mime = file.ContentType;
        Left = 0;
        Right = 0;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

}
