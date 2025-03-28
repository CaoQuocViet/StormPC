using StormPC.Core.Models.Customers;
using StormPC.Core.Models.Orders;
using StormPC.Core.Models.Products;
using StormPC.Core.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormPC.Core.Services.Dashboard;

public interface ICustomerReportService
{
    Task<CustomerSegmentationData> GetCustomerSegmentationDataAsync();
    Task<IEnumerable<TopCustomerData>> GetTopCustomersAsync(int count = 10);
    Task<IEnumerable<CustomerPurchaseData>> GetPurchaseTrendsAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<CustomerPreferenceData>> GetCustomerPreferencesAsync();
}

public class CustomerReportService : ICustomerReportService
{
    private readonly StormPCDbContext _context;

    public CustomerReportService(StormPCDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerSegmentationData> GetCustomerSegmentationDataAsync()
    {
        try
        {
            // Debug: Kiểm tra tất cả customers
            var allCustomers = await _context.Customers.ToListAsync();
            Console.WriteLine($"Debug: Total customers in database: {allCustomers.Count}");

            // Debug: Kiểm tra customers không bị xóa
            var activeCustomers = await _context.Customers
                .Where(c => !c.IsDeleted)
                .ToListAsync();
            Console.WriteLine($"Debug: Active customers: {activeCustomers.Count}");

            // Debug: Kiểm tra orders
            var orders = await _context.Orders
                .Where(o => !o.IsDeleted)
                .ToListAsync();
            Console.WriteLine($"Debug: Total orders: {orders.Count}");

            // Lấy tổng chi tiêu của từng khách hàng
            var customerSpending = await _context.Customers
                .Where(c => !c.IsDeleted)
                .Select(c => new
                {
                    CustomerId = c.CustomerID,
                    CustomerName = c.FullName,
                    TotalSpent = _context.Orders
                        .Where(o => o.CustomerID == c.CustomerID && !o.IsDeleted)
                        .Sum(o => o.TotalAmount)
                })
                .ToListAsync();

            Console.WriteLine("Debug: Customer spending details:");
            foreach (var customer in customerSpending)
            {
                Console.WriteLine($"Customer {customer.CustomerName} (ID: {customer.CustomerId}) - Total spent: {customer.TotalSpent}");
            }

            var totalCustomers = customerSpending.Count;
            
            if (totalCustomers == 0)
            {
                Console.WriteLine("Debug: No active customers found!");
                return new CustomerSegmentationData
                {
                    TotalCustomers = 0,
                    PlatinumCustomers = 0,
                    GoldCustomers = 0,
                    SilverCustomers = 0,
                    BronzeCustomers = 0
                };
            }

            // Sắp xếp khách hàng theo chi tiêu
            var sortedCustomers = customerSpending
                .OrderByDescending(x => x.TotalSpent)
                .ToList();

            // Phân khúc khách hàng dựa trên chi tiêu
            var platinumCount = Math.Max(1, (int)(totalCustomers * 0.1)); // Ít nhất 1 khách hàng Platinum
            var goldCount = Math.Max(1, (int)(totalCustomers * 0.2));     // Ít nhất 1 khách hàng Gold
            var silverCount = Math.Max(1, (int)(totalCustomers * 0.3));   // Ít nhất 1 khách hàng Silver
            var bronzeCount = Math.Max(1, totalCustomers - (platinumCount + goldCount + silverCount)); // Còn lại

            Console.WriteLine("Debug: Customer segmentation details:");
            Console.WriteLine($"Total Customers: {totalCustomers}");
            Console.WriteLine($"Platinum ({platinumCount}): {string.Join(", ", sortedCustomers.Take(platinumCount).Select(c => $"{c.CustomerName} ({c.TotalSpent:C})"))}");
            Console.WriteLine($"Gold ({goldCount}): {string.Join(", ", sortedCustomers.Skip(platinumCount).Take(goldCount).Select(c => $"{c.CustomerName} ({c.TotalSpent:C})"))}");
            Console.WriteLine($"Silver ({silverCount}): {string.Join(", ", sortedCustomers.Skip(platinumCount + goldCount).Take(silverCount).Select(c => $"{c.CustomerName} ({c.TotalSpent:C})"))}");
            Console.WriteLine($"Bronze ({bronzeCount}): {string.Join(", ", sortedCustomers.Skip(platinumCount + goldCount + silverCount).Take(bronzeCount).Select(c => $"{c.CustomerName} ({c.TotalSpent:C})"))}");

            return new CustomerSegmentationData
            {
                TotalCustomers = totalCustomers,
                PlatinumCustomers = platinumCount,
                GoldCustomers = goldCount,
                SilverCustomers = silverCount,
                BronzeCustomers = bronzeCount
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetCustomerSegmentationDataAsync: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<IEnumerable<TopCustomerData>> GetTopCustomersAsync(int count = 10)
    {
        return await _context.Customers
            .Where(c => !c.IsDeleted)
            .Select(c => new TopCustomerData
            {
                CustomerId = c.CustomerID,
                CustomerName = c.FullName,
                TotalOrders = c.Orders.Count(o => !o.IsDeleted),
                TotalSpent = c.Orders.Where(o => !o.IsDeleted).Sum(o => o.TotalAmount),
                AverageOrderValue = c.Orders.Any(o => !o.IsDeleted) 
                    ? c.Orders.Where(o => !o.IsDeleted).Average(o => o.TotalAmount) 
                    : 0
            })
            .Where(c => c.TotalOrders > 0)
            .OrderByDescending(c => c.TotalSpent)
            .Take(count)
            .ToListAsync();
    }

    public async Task<IEnumerable<CustomerPurchaseData>> GetPurchaseTrendsAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            var utcStartDate = startDate.ToUniversalTime();
            var utcEndDate = endDate.ToUniversalTime();

            // Lấy dữ liệu theo tháng
            var trends = await _context.Orders
                .Where(o => !o.IsDeleted && o.OrderDate >= utcStartDate && o.OrderDate <= utcEndDate)
                .GroupBy(o => new { 
                    Year = o.OrderDate.Year, 
                    Month = o.OrderDate.Month 
                })
                .Select(g => new CustomerPurchaseData
                {
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                    OrderCount = g.Count(),
                    TotalAmount = g.Sum(o => o.TotalAmount),
                    AverageOrderValue = g.Average(o => o.TotalAmount)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            Console.WriteLine($"Debug: Found {trends.Count} monthly trends");
            foreach (var trend in trends)
            {
                Console.WriteLine($"Debug: {trend.Date:MM/yyyy} - Orders: {trend.OrderCount}, Amount: {trend.TotalAmount}");
            }

            return trends;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetPurchaseTrendsAsync: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }

    public async Task<IEnumerable<CustomerPreferenceData>> GetCustomerPreferencesAsync()
    {
        try
        {
            // Query để lấy thống kê theo thương hiệu
            var preferences = await (
                from oi in _context.OrderItems
                join o in _context.Orders on oi.OrderID equals o.OrderID
                join ls in _context.Set<LaptopSpec>() on oi.VariantID equals ls.VariantID
                join l in _context.Set<Laptop>() on ls.LaptopID equals l.LaptopID
                join b in _context.Set<Brand>() on l.BrandID equals b.BrandID
                where !o.IsDeleted && !l.IsDeleted
                group new { oi, o } by new { b.BrandID, b.BrandName } into g
                select new CustomerPreferenceData
                {
                    BrandId = g.Key.BrandID,
                    BrandName = g.Key.BrandName,
                    TotalOrders = g.Select(x => x.o.OrderID).Distinct().Count(),
                    TotalRevenue = g.Sum(x => x.oi.UnitPrice * x.oi.Quantity),
                    TotalQuantity = g.Sum(x => x.oi.Quantity)
                })
                .OrderByDescending(p => p.TotalRevenue)
                .ToListAsync();

            Console.WriteLine($"Debug: Found {preferences.Count} brand preferences");
            foreach (var pref in preferences)
            {
                Console.WriteLine($"Debug: Brand {pref.BrandName} - Orders: {pref.TotalOrders}, Revenue: {pref.TotalRevenue}");
            }

            // Tính phần trăm
            if (preferences.Any())
            {
                var totalOrders = preferences.Sum(p => p.TotalOrders);
                var totalRevenue = preferences.Sum(p => p.TotalRevenue);

                foreach (var pref in preferences)
                {
                    pref.OrderPercentage = totalOrders > 0 ? (double)pref.TotalOrders * 100.0 / totalOrders : 0;
                    pref.RevenuePercentage = totalRevenue > 0 ? (double)pref.TotalRevenue * 100.0 / Convert.ToDouble(totalRevenue) : 0;
                }
            }

            return preferences;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetCustomerPreferencesAsync: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            throw;
        }
    }
}

public class CustomerSegmentationData
{
    public int TotalCustomers { get; set; }
    public int PlatinumCustomers { get; set; }
    public int GoldCustomers { get; set; }
    public int SilverCustomers { get; set; }
    public int BronzeCustomers { get; set; }
}

public class TopCustomerData
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int TotalOrders { get; set; }
    public decimal TotalSpent { get; set; }
    public decimal AverageOrderValue { get; set; }
}

public class CustomerPurchaseData
{
    public DateTime Date { get; set; }
    public int OrderCount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AverageOrderValue { get; set; }
}

public class CustomerPreferenceData
{
    public int BrandId { get; set; }
    public string BrandName { get; set; } = string.Empty;
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalQuantity { get; set; }
    public double OrderPercentage { get; set; }
    public double RevenuePercentage { get; set; }
} 