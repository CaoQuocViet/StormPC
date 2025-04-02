using System;

namespace StormPC.Core.Services.Dashboard;

public class RevenueData
{
    public decimal TotalRevenue { get; set; }
    public decimal TotalProfit { get; set; }
    public int TotalOrders { get; set; }
    public decimal AverageOrderValue { get; set; }
}

public class CategoryRevenueData
{
    public string CategoryName { get; set; } = string.Empty;
    public decimal Revenue { get; set; }
    public decimal Profit { get; set; }
}

public class DailyRevenueData
{
    public DateTime Date { get; set; }
    public decimal Revenue { get; set; }
    public decimal Profit { get; set; }
}

public class PaymentMethodData
{
    public string MethodName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int Count { get; set; }
} 