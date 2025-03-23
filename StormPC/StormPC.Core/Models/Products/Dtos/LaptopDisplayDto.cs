namespace StormPC.Core.Models.Products.Dtos;

public class LaptopDisplayDto
{
    public string LaptopID { get; set; } = null!;
    public string ModelName { get; set; } = null!;
    public string BrandName { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string? Picture { get; set; }
    public decimal? ScreenSize { get; set; }
    public string? OperatingSystem { get; set; }
    public int? ReleaseYear { get; set; }
    public decimal Discount { get; set; }
    public DateTime? DiscountEndDate { get; set; }

    // Cheapest spec information
    public decimal LowestPrice { get; set; }
    public string CPU { get; set; } = null!;
    public string? GPU { get; set; }
    public int RAM { get; set; }
    public int Storage { get; set; }
    public string StorageType { get; set; } = null!;
} 