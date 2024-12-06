using System.ComponentModel.DataAnnotations;

namespace AmazonKiller2000.Models;

public class Author
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    
}