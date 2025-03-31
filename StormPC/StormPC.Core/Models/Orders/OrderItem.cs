using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StormPC.Core.Models.Products;

namespace StormPC.Core.Models.Orders;

[Table("OrderItems")]
public class OrderItem
{
    public int OrderID { get; set; }
    public int VariantID { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    // Thuộc tính navigation
    public Order Order { get; set; } = null!;
    public LaptopSpec LaptopSpec { get; set; } = null!;
} 