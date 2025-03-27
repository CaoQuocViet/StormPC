using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StormPC.Core.Models.Products;

[Table("Brands")]
public class Brand
{
    [Key]
    public int BrandID { get; set; }

    [Required]
    [MaxLength(100)]
    public string BrandName { get; set; } = null!;

    // Thuộc tính navigation
    public ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();
} 