namespace StormPC.Core.Models.Products.Dtos;

public class CategoryDisplayDto
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } = null!;
    public string? Description { get; set; }
    public int ProductCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string FormattedCreatedAt => CreatedAt.ToString("dd/MM/yyyy HH:mm");
    public string FormattedUpdatedAt => UpdatedAt?.ToString("dd/MM/yyyy HH:mm") ?? "--";
} 