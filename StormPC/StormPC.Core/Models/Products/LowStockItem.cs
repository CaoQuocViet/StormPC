namespace StormPC.Core.Models.Products;

public class LowStockItem
{
    public string SKU { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public decimal Price { get; set; }
} 