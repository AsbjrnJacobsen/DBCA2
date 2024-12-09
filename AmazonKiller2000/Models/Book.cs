using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmazonKiller2000.Models;

public class Book
{
    [Key]
    public int ISBN { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public int AuthorId { get; set; }
    [JsonInclude]
    public virtual Author? Author { get; set; }
    [Required]
    public int StockLevel { get; set; }
    [Required]
    public int Price { get; set; }
    
}
