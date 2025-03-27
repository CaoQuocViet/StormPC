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

    // Thông tin phiên bản rẻ nhất
    public decimal LowestPrice { get; set; }
    public string CPU { get; set; } = "N/A";
    public string GPU { get; set; } = "N/A";
    public int RAM { get; set; }
    public int Storage { get; set; }
    public string StorageType { get; set; } = "N/A";

    public string FormattedPrice { get; set; } = string.Empty;
    public string FormattedDiscount { get; set; } = string.Empty;
    public int OptionsCount { get; set; }
} 