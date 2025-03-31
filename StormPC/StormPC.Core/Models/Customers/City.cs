using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StormPC.Core.Models.Orders;

namespace StormPC.Core.Models.Customers;

[Table("Cities")]
public class City
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("CityCode")]
    public string CityCode { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    [Column("CityName")]
    public string CityName { get; set; } = null!;

    // Navigation properties
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
    public ICollection<Order> ShippingOrders { get; set; } = new List<Order>();
} 