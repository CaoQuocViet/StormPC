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
    [MaxLength(255)]
    public string FullName { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [MaxLength(255)]
    [Phone]
    public string? Phone { get; set; }

    [Required]
    public string Address { get; set; } = null!;

    [Required]
    [Column("CityCode")]
    public int CityId { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    // Navigation properties
    public City City { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = new List<Order>();
} 