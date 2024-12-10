using System.ComponentModel.DataAnnotations;

namespace AmazonKiller2000.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    [Required]
    public List<int> Items { get; set; } = [];

    [Required]
    public int TotalPrice { get; set; }
    [Required]
    public bool IsPaid { get; set; }
    [Required]
    public int CustomerId { get; set; }
}