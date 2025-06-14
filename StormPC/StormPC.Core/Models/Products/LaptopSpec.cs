using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StormPC.Core.Models.Orders;

namespace StormPC.Core.Models.Products;

[Table("LaptopSpecs")]
public class LaptopSpec
{
    [Key]
    public int VariantID { get; set; }

    [Required]
    [MaxLength(50)]
    public string SKU { get; set; } = null!;

    public int LaptopID { get; set; }

    [Required]
    [MaxLength(100)]
    public string CPU { get; set; } = null!;

    [MaxLength(100)]
    public string? GPU { get; set; }

    [Required]
    public int RAM { get; set; }

    [Required]
    public int Storage { get; set; }

    [Required]
    [MaxLength(20)]
    public string StorageType { get; set; } = null!;

    [MaxLength(50)]
    public string? Color { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal ImportPrice { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public int StockQuantity { get; set; }

    [Column("createdAt")]
    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; } = false;

    // Thuộc tính navigation
    public Laptop Laptop { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
} 