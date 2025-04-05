using StormPC.Core.Models.Customers;
using StormPC.Core.Models.Orders;
using StormPC.Core.Models.Products;
using StormPC.Core.Models.Customers.Dtos;
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
    Task<IEnumerable<CustomerDisplayDto>> GetAllCustomersAsync();
    Task<CustomerDisplayDto?> GetCustomerByIdAsync(int id);
    Task<bool> AddCustomerAsync(Customer customer);
    Task<bool> UpdateCustomerAsync(Customer customer);
    Task<bool> DeleteCustomerAsync(int id);
    Task<bool> DeleteMultipleCustomersAsync(List<int> ids);
    Task<bool> CanDeleteCustomerAsync(int id);
    Task<IEnumerable<City>> GetAllCitiesAsync();
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