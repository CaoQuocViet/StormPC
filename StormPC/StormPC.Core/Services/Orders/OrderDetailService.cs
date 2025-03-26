using Microsoft.EntityFrameworkCore;
using StormPC.Core.Infrastructure.Database.Contexts;
using StormPC.Core.Models.Orders.Dtos;

namespace StormPC.Core.Services.Orders;

public interface IOrderDetailService
{
    Task<OrderDetailDto?> GetLatestOrderDetailAsync();
    Task<OrderDetailDto?> GetOrderDetailByIdAsync(int orderId);
}

public class OrderDetailService : IOrderDetailService
{
    private readonly StormPCDbContext _dbContext;

    public OrderDetailService(StormPCDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderDetailDto?> GetLatestOrderDetailAsync()
    {
        var latestOrder = await _dbContext.Orders
            .AsNoTracking()
            .Include(o => o.Customer)
            .Include(o => o.Status)
            .Include(o => o.PaymentMethod)
            .Include(o => o.ShipCity)
            .OrderByDescending(o => o.OrderDate)
            .FirstOrDefaultAsync(o => !o.IsDeleted);

        if (latestOrder == null)
            return null;

        var orderItems = await _dbContext.OrderItems
            .AsNoTracking()
            .Where(oi => oi.OrderID == latestOrder.OrderID)
            .Join(_dbContext.LaptopSpecs,
                oi => oi.VariantID,
                ls => ls.VariantID,
                (oi, ls) => new { OrderItem = oi, LaptopSpec = ls })
            .Join(_dbContext.Laptops,
                x => x.LaptopSpec.LaptopID,
                l => l.LaptopID,
                (x, l) => new OrderItemDto
                {
                    OrderItemID = x.OrderItem.OrderID,
                    VariantID = x.OrderItem.VariantID,
                    ModelName = l.ModelName,
                    Quantity = x.OrderItem.Quantity,
                    UnitPrice = x.OrderItem.UnitPrice,
                    FormattedUnitPrice = x.OrderItem.UnitPrice.ToString("N0") + " đ",
                    Subtotal = x.OrderItem.Quantity * x.OrderItem.UnitPrice,
                    FormattedSubtotal = (x.OrderItem.Quantity * x.OrderItem.UnitPrice).ToString("N0") + " đ"
                })
            .ToListAsync();

        return new OrderDetailDto
        {
            OrderID = latestOrder.OrderID,
            CustomerName = latestOrder.Customer?.FullName ?? "Unknown",
            StatusName = latestOrder.Status?.StatusName ?? "Unknown",
            StatusColor = GetStatusColor(latestOrder.Status?.StatusName ?? "Unknown"),
            PaymentMethod = latestOrder.PaymentMethod?.MethodName ?? "Unknown",
            TotalAmount = latestOrder.TotalAmount,
            FormattedTotalAmount = string.Format("{0:N0} VNĐ", latestOrder.TotalAmount),
            ShippingAddress = latestOrder.ShippingAddress ?? string.Empty,
            ShippingCity = latestOrder.ShipCity?.CityName ?? string.Empty,
            OrderDate = latestOrder.OrderDate,
            FormattedOrderDate = latestOrder.OrderDate.ToString("dd/MM/yyyy HH:mm"),
            Items = orderItems
        };
    }

    public async Task<OrderDetailDto?> GetOrderDetailByIdAsync(int orderId)
    {
        var orderData = await _dbContext.Orders
            .AsNoTracking()
            .Include(o => o.Customer)
            .Include(o => o.Status)
            .Include(o => o.PaymentMethod)
            .Include(o => o.ShipCity)
            .FirstOrDefaultAsync(o => o.OrderID == orderId && !o.IsDeleted);

        if (orderData == null)
            return null;

        var orderItems = await _dbContext.OrderItems
            .AsNoTracking()
            .Where(oi => oi.OrderID == orderId)
            .Join(_dbContext.LaptopSpecs,
                oi => oi.VariantID,
                ls => ls.VariantID,
                (oi, ls) => new { OrderItem = oi, LaptopSpec = ls })
            .Join(_dbContext.Laptops,
                x => x.LaptopSpec.LaptopID,
                l => l.LaptopID,
                (x, l) => new OrderItemDto
                {
                    OrderItemID = x.OrderItem.OrderID,
                    VariantID = x.OrderItem.VariantID,
                    ModelName = l.ModelName,
                    Quantity = x.OrderItem.Quantity,
                    UnitPrice = x.OrderItem.UnitPrice,
                    FormattedUnitPrice = x.OrderItem.UnitPrice.ToString("N0") + " đ",
                    Subtotal = x.OrderItem.Quantity * x.OrderItem.UnitPrice,
                    FormattedSubtotal = (x.OrderItem.Quantity * x.OrderItem.UnitPrice).ToString("N0") + " đ"
                })
            .ToListAsync();

        return new OrderDetailDto
        {
            OrderID = orderData.OrderID,
            CustomerName = orderData.Customer?.FullName ?? "Unknown",
            StatusName = orderData.Status?.StatusName ?? "Unknown",
            StatusColor = GetStatusColor(orderData.Status?.StatusName ?? "Unknown"),
            PaymentMethod = orderData.PaymentMethod?.MethodName ?? "Unknown",
            TotalAmount = orderData.TotalAmount,
            FormattedTotalAmount = string.Format("{0:N0} VNĐ", orderData.TotalAmount),
            ShippingAddress = orderData.ShippingAddress ?? string.Empty,
            ShippingCity = orderData.ShipCity?.CityName ?? string.Empty,
            OrderDate = orderData.OrderDate,
            FormattedOrderDate = orderData.OrderDate.ToString("dd/MM/yyyy HH:mm"),
            Items = orderItems
        };
    }

    private static string GetStatusColor(string status)
    {
        return status.ToLower() switch
        {
            "pending" => "#FFA500",  // Orange
            "processing" => "#1E90FF", // Blue
            "shipped" => "#32CD32",   // Green
            "delivered" => "#008000",  // Dark Green
            "cancelled" => "#FF0000",  // Red
            "refunded" => "#808080",   // Gray
            _ => "#000000"            // Black
        };
    }
} 