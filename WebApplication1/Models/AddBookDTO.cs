using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class AddBookDTO
{
    [Required]
    [MaxLength(100)]
    public string BookTitle { get; set; }
    [Required]
    [MaxLength(100)]
    public string EditionTitle { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int PublishingHouseId { get; set; }
    [Required]
    public DateTime ReleaseDate { get; set; }   
}