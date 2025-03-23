using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StormPC.Core.Models.Orders;

namespace StormPC.Core.Models.Customers;

[Table("Customers")]
public class Customer
{
    [Key]
    public int CustomerID { get; set; }

    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [MaxLength(20)]
    [Phone]
    public string? Phone { get; set; }

    [Required]
    public string Address { get; set; } = null!;

    [Required]
    [MaxLength(3)]
    public string CityCode { get; set; } = null!;

    // Navigation properties
    public City City { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = new List<Order>();
} 