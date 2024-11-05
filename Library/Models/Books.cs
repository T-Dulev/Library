using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Books
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string? Title { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string? Genre { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string? Author { get; set; }

    [Required]
    [Column(TypeName = "int")]
    public int AvailableCopies { get; set; }
}