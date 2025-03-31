using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StormPC.Core.Models.Products;

[Table("Laptops")]
public class Laptop
{
    [Key]
    public int LaptopID { get; set; }

    public int BrandID { get; set; }

    public int CategoryID { get; set; }

    public string? Picture { get; set; }

    [Required]
    [MaxLength(200)]
    public string ModelName { get; set; } = null!;

    [Column(TypeName = "decimal(3,1)")]
    public decimal? ScreenSize { get; set; }

    [MaxLength(50)]
    public string? OperatingSystem { get; set; }

    public int? ReleaseYear { get; set; }

    public string? Description { get; set; }

    [Required]
    public decimal Discount { get; set; }

    public DateTime? DiscountStartDate { get; set; }

    public DateTime? DiscountEndDate { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    // Thuộc tính navigation
    public Brand Brand { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public ICollection<LaptopSpec> Specs { get; set; } = new List<LaptopSpec>();
} 