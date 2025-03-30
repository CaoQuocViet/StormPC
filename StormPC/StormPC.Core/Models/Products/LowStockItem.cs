namespace StormPC.Core.Models.Products;

public class LowStockItem
{
    public string SKU { get; set; }
    public string ModelName { get; set; }
    public int StockQuantity { get; set; }
    public decimal Price { get; set; }
} 