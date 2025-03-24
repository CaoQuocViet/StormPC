using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StormPC.Core.Models.Products;

[Table("Categories")]
public class Category
{
    [Key]
    public int CategoryID { get; set; }

    [Required]
    [MaxLength(50)]
    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    public bool IsDeleted { get; set; }

    [Required]
    [Column("createdAt")]
    public DateTime CreatedAt { get; set; }

    [Column("updatedAt")]
    public DateTime? UpdatedAt { get; set; }

    // Navigation property
    public ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();

    [NotMapped]
    public int ProductCount => Laptops?.Count(l => !l.IsDeleted) ?? 0;
} 