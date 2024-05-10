namespace WebApplication1.Models;

public class BookEditionDTO
{
    public int Id { get; set; }
    public string BookTitle { get; set; }
    public string EditionTitle { get; set; }
    public string PublishingHouseName { get; set; }
    public DateTime ReleaseDate { get; set; }
}