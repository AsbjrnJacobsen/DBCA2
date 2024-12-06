using System.ComponentModel.DataAnnotations;

namespace AmazonKiller2000.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public int Telephone { get; set; }
    
}