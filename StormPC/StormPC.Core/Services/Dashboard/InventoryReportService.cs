using Microsoft.EntityFrameworkCore;
using StormPC.Core.Infrastructure.Database.Contexts;
using StormPC.Core.Models.Products;

namespace StormPC.Core.Services.Dashboard;

public class InventoryReportService : IInventoryReportService
{
    private readonly StormPCDbContext _context;

    public InventoryReportService(StormPCDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _context.Categories
            .Where(c => !c.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Brand>> GetBrands()
    {
        return await _context.Brands
            .ToListAsync();
    }

    public async Task<InventoryReportData> GetInventoryData(DateTime startDate, DateTime endDate)
    {
        // Lấy thông tin tồn kho hiện tại
        var currentInventory = await _context.LaptopSpecs
            .Include(ls => ls.Laptop)
            .ThenInclude(l => l.Category)
            .Include(ls => ls.Laptop)
            .ThenInclude(l => l.Brand)
            .Where(ls => !ls.Laptop.IsDeleted)
            .ToListAsync();

        // Lấy thông tin đơn hàng trong khoảng thời gian
        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => !o.IsDeleted && o.OrderDate >= startDate && o.OrderDate <= endDate)
            .ToListAsync();

        // Tính toán các chỉ số tổng quan
        var totalProducts = currentInventory.Count;
        var totalStock = currentInventory.Sum(ls => ls.StockQuantity);
        var totalValue = currentInventory.Sum(ls => ls.StockQuantity * ls.ImportPrice);
        var averageStockValue = totalStock > 0 ? totalValue / totalStock : 0;

        // Tính tỷ lệ quay vòng hàng tồn kho
        var totalSold = orders.SelectMany(o => o.OrderItems).Sum(oi => oi.Quantity);
        var stockTurnoverRate = totalStock > 0 ? (decimal)totalSold / totalStock : 0;

        // Phân tích theo danh mục
        var categoryAnalytics = currentInventory
            .GroupBy(ls => ls.Laptop.Category)
            .Select(g => new CategoryAnalysis
            {
                CategoryName = g.Key.CategoryName,
                TotalProducts = g.Count(),
                TotalStock = g.Sum(ls => ls.StockQuantity),
                TotalValue = g.Sum(ls => ls.StockQuantity * ls.ImportPrice),
                PercentageOfTotal = totalValue > 0 ? g.Sum(ls => ls.StockQuantity * ls.ImportPrice) / totalValue * 100 : 0,
                AveragePrice = g.Sum(ls => ls.ImportPrice) / g.Count(),
                LowStockCount = g.Count(ls => ls.StockQuantity < 5)
            })
            .ToList();

        // Phân tích theo thương hiệu
        var brandAnalytics = currentInventory
            .GroupBy(ls => ls.Laptop.Brand)
            .Select(g => new BrandAnalysis
            {
                BrandName = g.Key.BrandName,
                TotalProducts = g.Count(),
                TotalStock = g.Sum(ls => ls.StockQuantity),
                TotalValue = g.Sum(ls => ls.StockQuantity * ls.ImportPrice),
                PercentageOfTotal = totalValue > 0 ? g.Sum(ls => ls.StockQuantity * ls.ImportPrice) / totalValue * 100 : 0,
                AveragePrice = g.Sum(ls => ls.ImportPrice) / g.Count(),
                LowStockCount = g.Count(ls => ls.StockQuantity < 5)
            })
            .ToList();

        // Phân tích xu hướng tồn kho theo ngày
        var stockTrends = orders
            .GroupBy(o => o.OrderDate.Date)
            .Select(g => new StockTrend
            {
                Date = g.Key,
                TotalStock = currentInventory.Sum(ls => ls.StockQuantity),
                TotalValue = currentInventory.Sum(ls => ls.StockQuantity * ls.ImportPrice),
                SoldProducts = g.SelectMany(o => o.OrderItems).Sum(oi => oi.Quantity),
                NewProducts = 0 // Cần thêm bảng nhập hàng để tính chính xác
            })
            .OrderBy(st => st.Date)
            .ToList();

        // Phân tích hàng tồn kho lâu
        var lastOrderDates = orders
            .SelectMany(o => o.OrderItems)
            .GroupBy(oi => oi.VariantID)
            .ToDictionary(g => g.Key, g => g.Max(oi => oi.Order.OrderDate));

        var agedInventories = currentInventory
            .Select(ls => new AgedInventory
            {
                SKU = ls.Laptop.LaptopID,
                ModelName = ls.Laptop.ModelName,
                CategoryName = ls.Laptop.Category.CategoryName,
                BrandName = ls.Laptop.Brand.BrandName,
                StockQuantity = ls.StockQuantity,
                StockValue = ls.StockQuantity * ls.ImportPrice,
                LastSoldDate = lastOrderDates.ContainsKey(ls.VariantID) ? lastOrderDates[ls.VariantID] : DateTime.MinValue,
                DaysInStock = (int)(DateTime.Now - (lastOrderDates.ContainsKey(ls.VariantID) ? lastOrderDates[ls.VariantID] : DateTime.MinValue)).TotalDays
            })
            .Where(ai => ai.DaysInStock > 30) // Hàng tồn trên 30 ngày
            .OrderByDescending(ai => ai.DaysInStock)
            .ToList();

        // Sản phẩm sắp hết hàng
        var lowStockItems = currentInventory
            .Where(ls => ls.StockQuantity < 5)
            .Select(ls => new LowStockItem
            {
                SKU = ls.Laptop.LaptopID,
                ModelName = ls.Laptop.ModelName,
                StockQuantity = ls.StockQuantity,
                Price = ls.ImportPrice
            })
            .ToList();

        // Đề xuất nhập hàng dựa trên doanh số
        var monthlySales = orders
            .SelectMany(o => o.OrderItems)
            .GroupBy(oi => oi.VariantID)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(oi => oi.Quantity) / Math.Max(1, (endDate - startDate).Days / 30)
            );

        var restockSuggestions = currentInventory
            .Where(ls => monthlySales.ContainsKey(ls.VariantID) && ls.StockQuantity < monthlySales[ls.VariantID] * 2) // Tồn kho < 2 tháng doanh số
            .Select(ls => new RestockSuggestion
            {
                SKU = ls.Laptop.LaptopID,
                ModelName = ls.Laptop.ModelName,
                CategoryName = ls.Laptop.Category.CategoryName,
                BrandName = ls.Laptop.Brand.BrandName,
                CurrentStock = ls.StockQuantity,
                AverageMonthlySales = (int)monthlySales[ls.VariantID],
                SuggestedReorderQuantity = (int)(monthlySales[ls.VariantID] * 3 - ls.StockQuantity), // Nhập đủ 3 tháng doanh số
                EstimatedValue = (monthlySales[ls.VariantID] * 3 - ls.StockQuantity) * ls.ImportPrice
            })
            .OrderByDescending(rs => rs.AverageMonthlySales)
            .ToList();

        return new InventoryReportData
        {
            TotalProducts = totalProducts,
            TotalStock = totalStock,
            TotalValue = totalValue,
            AverageStockValue = averageStockValue,
            StockTurnoverRate = stockTurnoverRate,
            LowStockProducts = lowStockItems.Count,
            CategoryAnalytics = categoryAnalytics,
            BrandAnalytics = brandAnalytics,
            StockTrends = stockTrends,
            AgedInventories = agedInventories,
            LowStockItems = lowStockItems,
            RestockSuggestions = restockSuggestions
        };
    }
} 