using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using StormPC.Core.Infrastructure.Database.Contexts;

namespace StormPC.Core.Services.Dashboard;

public interface IRevenueReportService
{
    Task<RevenueData> GetRevenueDataAsync(DateTime startDate, DateTime endDate);
    Task<List<CategoryRevenueData>> GetCategoryRevenueAsync(DateTime startDate, DateTime endDate);
    Task<List<DailyRevenueData>> GetDailyRevenueAsync(DateTime startDate, DateTime endDate);
    Task<List<PaymentMethodData>> GetPaymentMethodDataAsync(DateTime startDate, DateTime endDate);
}

public class RevenueReportService : IRevenueReportService
{
    private readonly StormPCDbContext _context;

    public RevenueReportService(StormPCDbContext context)
    {
        _context = context;
    }

    public async Task<RevenueData> GetRevenueDataAsync(DateTime startDate, DateTime endDate)
    {
        var utcStartDate = startDate.ToUniversalTime();
        var utcEndDate = endDate.ToUniversalTime();

        // Get orders within date range
        var ordersData = await (
            from o in _context.Orders
            join oi in _context.OrderItems on o.OrderID equals oi.OrderID
            join ls in _context.LaptopSpecs on oi.VariantID equals ls.VariantID
            where !o.IsDeleted && o.OrderDate >= utcStartDate && o.OrderDate <= utcEndDate
            group new { o, oi, ls } by 1 into g
            select new
            {
                TotalRevenue = g.Sum(x => x.oi.UnitPrice * x.oi.Quantity),
                TotalCost = g.Sum(x => x.ls.ImportPrice * x.oi.Quantity),
                TotalOrders = g.Select(x => x.o.OrderID).Distinct().Count()
            })
            .FirstOrDefaultAsync();

        if (ordersData == null)
        {
            return new RevenueData
            {
                TotalRevenue = 0,
                TotalProfit = 0,
                TotalOrders = 0,
                AverageOrderValue = 0
            };
        }

        return new RevenueData
        {
            TotalRevenue = ordersData.TotalRevenue,
            TotalProfit = ordersData.TotalRevenue - ordersData.TotalCost,
            TotalOrders = ordersData.TotalOrders,
            AverageOrderValue = ordersData.TotalOrders > 0 
                ? ordersData.TotalRevenue / ordersData.TotalOrders 
                : 0
        };
    }

    public async Task<List<CategoryRevenueData>> GetCategoryRevenueAsync(DateTime startDate, DateTime endDate)
    {
        var utcStartDate = startDate.ToUniversalTime();
        var utcEndDate = endDate.ToUniversalTime();

        // Get all categories first
        var categories = await _context.Categories
            .Where(c => !c.IsDeleted)
            .ToListAsync();

        var categoryRevenues = await (
            from o in _context.Orders
            join oi in _context.OrderItems on o.OrderID equals oi.OrderID
            join ls in _context.LaptopSpecs on oi.VariantID equals ls.VariantID
            join l in _context.Laptops on ls.LaptopID equals l.LaptopID
            where !o.IsDeleted && !l.IsDeleted
                && o.OrderDate >= utcStartDate && o.OrderDate <= utcEndDate
            group new { oi, ls } by l.CategoryID into g
            select new
            {
                CategoryID = g.Key,
                Revenue = g.Sum(x => x.oi.UnitPrice * x.oi.Quantity),
                Cost = g.Sum(x => x.ls.ImportPrice * x.oi.Quantity)
            })
            .ToListAsync();

        // Combine with all categories to ensure we have data for each category
        var result = categories.Select(c => new CategoryRevenueData
        {
            CategoryName = c.CategoryName,
            Revenue = categoryRevenues
                .FirstOrDefault(r => r.CategoryID == c.CategoryID)?.Revenue ?? 0,
            Profit = categoryRevenues
                .Where(r => r.CategoryID == c.CategoryID)
                .Select(r => r.Revenue - r.Cost)
                .FirstOrDefault()
        }).ToList();

        return result;
    }

    public async Task<List<DailyRevenueData>> GetDailyRevenueAsync(DateTime startDate, DateTime endDate)
    {
        var utcStartDate = startDate.ToUniversalTime();
        var utcEndDate = endDate.ToUniversalTime();

        var dailyData = await (
            from o in _context.Orders
            join oi in _context.OrderItems on o.OrderID equals oi.OrderID
            join ls in _context.LaptopSpecs on oi.VariantID equals ls.VariantID
            where !o.IsDeleted && o.OrderDate >= utcStartDate && o.OrderDate <= utcEndDate
            group new { oi, ls } by o.OrderDate.Date into g
            select new DailyRevenueData
            {
                Date = g.Key,
                Revenue = g.Sum(x => x.oi.UnitPrice * x.oi.Quantity),
                Profit = g.Sum(x => x.oi.UnitPrice * x.oi.Quantity - x.ls.ImportPrice * x.oi.Quantity)
            })
            .OrderBy(x => x.Date)
            .ToListAsync();

        // Fill in missing dates with zero values
        var result = new List<DailyRevenueData>();
        for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
        {
            var dayData = dailyData.FirstOrDefault(x => x.Date.Date == date.Date) ?? 
                new DailyRevenueData { Date = date, Revenue = 0, Profit = 0 };
            result.Add(dayData);
        }

        return result;
    }

    public async Task<List<PaymentMethodData>> GetPaymentMethodDataAsync(DateTime startDate, DateTime endDate)
    {
        var utcStartDate = startDate.ToUniversalTime();
        var utcEndDate = endDate.ToUniversalTime();

        return await (
            from o in _context.Orders
            join pm in _context.PaymentMethods on o.PaymentMethodID equals pm.PaymentMethodID
            where !o.IsDeleted && o.OrderDate >= utcStartDate && o.OrderDate <= utcEndDate
            group o by new { pm.PaymentMethodID, pm.MethodName } into g
            select new PaymentMethodData
            {
                MethodName = g.Key.MethodName,
                Amount = g.Sum(o => o.TotalAmount),
                Count = g.Count()
            })
            .ToListAsync();
    }
} 