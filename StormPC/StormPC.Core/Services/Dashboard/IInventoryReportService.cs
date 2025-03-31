using StormPC.Core.Models.Products;

namespace StormPC.Core.Services.Dashboard;

public interface IInventoryReportService
{
    Task<IEnumerable<Category>> GetCategories();
    Task<IEnumerable<Brand>> GetBrands();
    Task<InventoryReportData> GetInventoryData(DateTime startDate, DateTime endDate);

    // Thêm phân tích doanh số và doanh thu
    Task<IEnumerable<TopSellingProduct>> GetTopSellingProducts(DateTime startDate, DateTime endDate, int limit = 5);
    Task<(int OrderCount, decimal TotalRevenue)> GetDailySummary(DateTime date);
}

public class InventoryReportData
{
    // Tổng quan
    public int TotalProducts { get; set; }
    public int TotalStock { get; set; }
    public decimal TotalValue { get; set; }
    public int LowStockProducts { get; set; }
    public decimal AverageStockValue { get; set; }
    public decimal StockTurnoverRate { get; set; }

    // Phân tích theo danh mục
    public IEnumerable<CategoryAnalysis> CategoryAnalytics { get; set; }
    
    // Phân tích theo thương hiệu
    public IEnumerable<BrandAnalysis> BrandAnalytics { get; set; }
    
    // Phân tích theo thời gian
    public IEnumerable<StockTrend> StockTrends { get; set; }
    
    // Sản phẩm tồn kho lâu
    public IEnumerable<AgedInventory> AgedInventories { get; set; }
    
    // Sản phẩm sắp hết hàng
    public IEnumerable<LowStockItem> LowStockItems { get; set; }
    
    // Sản phẩm bán chạy cần nhập thêm
    public IEnumerable<RestockSuggestion> RestockSuggestions { get; set; }
}

public class CategoryAnalysis
{
    public string CategoryName { get; set; }
    public int TotalProducts { get; set; }
    public int TotalStock { get; set; }
    public decimal TotalValue { get; set; }
    public decimal PercentageOfTotal { get; set; }
    public decimal AveragePrice { get; set; }
    public int LowStockCount { get; set; }
}

public class BrandAnalysis 
{
    public string BrandName { get; set; }
    public int TotalProducts { get; set; }
    public int TotalStock { get; set; }
    public decimal TotalValue { get; set; }
    public decimal PercentageOfTotal { get; set; }
    public decimal AveragePrice { get; set; }
    public int LowStockCount { get; set; }
}

public class StockTrend
{
    public DateTime Date { get; set; }
    public int TotalStock { get; set; }
    public decimal TotalValue { get; set; }
    public int NewProducts { get; set; }
    public int SoldProducts { get; set; }
}

public class AgedInventory
{
    public string SKU { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public decimal StockValue { get; set; }
    public int DaysInStock { get; set; }
    public DateTime LastSoldDate { get; set; }
}

public class RestockSuggestion
{
    public string SKU { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public int CurrentStock { get; set; }
    public int AverageMonthlySales { get; set; }
    public int SuggestedReorderQuantity { get; set; }
    public decimal EstimatedValue { get; set; }
}

public class TopSellingProduct
{
    public string SKU { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public int QuantitySold { get; set; }
    public decimal Revenue { get; set; }
} 