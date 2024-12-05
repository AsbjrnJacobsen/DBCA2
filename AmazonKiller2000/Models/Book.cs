using System.ComponentModel.DataAnnotations;

namespace AmazonKiller2000.Models;

public class Book
{
    [Key]
    public int ISBN { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    public int StockLevel { get; set; }
}
