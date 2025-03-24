using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StormPC.Core.Models.Customers;

namespace StormPC.Core.Models.Orders;

[Table("Orders")]
public class Order
{
    [Key]
    public int OrderID { get; set; }

    public int CustomerID { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    public int StatusID { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    public int PaymentMethodID { get; set; }

    [Required]
    [MaxLength(3)]
    public string ShipCityCode { get; set; } = null!;

    [Required]
    public string ShippingAddress { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string ShippingCity { get; set; } = null!;

    [MaxLength(10)]
    public string? ShippingPostalCode { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    // Navigation properties
    public Customer Customer { get; set; } = null!;
    public OrderStatus Status { get; set; } = null!;
    public PaymentMethod PaymentMethod { get; set; } = null!;
    public City ShipCity { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
} 