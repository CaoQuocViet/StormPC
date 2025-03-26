using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StormPC.Core.Models.Orders.Dtos;
using StormPC.Core.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace StormPC.ViewModels.Orders;

public partial class OrderListViewModel : ObservableObject
{
    private readonly StormPCDbContext _dbContext;

    [ObservableProperty]
    private ObservableCollection<OrderDisplayDto> _orders = new();

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _searchText = string.Empty;

    public OrderListViewModel(StormPCDbContext dbContext)
    {
        _dbContext = dbContext;
        Debug.WriteLine("OrderListViewModel constructed with dbContext");
    }

    public async Task InitializeAsync()
    {
        Debug.WriteLine("InitializeAsync called");
        await LoadOrders();
    }

    [RelayCommand]
    private async Task LoadOrders()
    {
        if (IsLoading)
        {
            Debug.WriteLine("LoadOrders skipped - already loading");
            return;
        }

        try
        {
            IsLoading = true;
            Debug.WriteLine("LoadOrders started...");

            // Kiểm tra kết nối database
            Debug.WriteLine("Checking database connection...");
            if (_dbContext.Database == null)
            {
                Debug.WriteLine("Database context is null!");
                return;
            }

            // Query từng bảng riêng lẻ để kiểm tra
            Debug.WriteLine("Checking individual tables...");
            var ordersExist = await _dbContext.Orders.AnyAsync();
            Debug.WriteLine($"Orders table has data: {ordersExist}");

            var customersExist = await _dbContext.Customers.AnyAsync();
            Debug.WriteLine($"Customers table has data: {customersExist}");

            var statusesExist = await _dbContext.OrderStatuses.AnyAsync();
            Debug.WriteLine($"OrderStatuses table has data: {statusesExist}");

            var paymentMethodsExist = await _dbContext.PaymentMethods.AnyAsync();
            Debug.WriteLine($"PaymentMethods table has data: {paymentMethodsExist}");

            // Thực hiện query chính với logging
            Debug.WriteLine("Executing main query...");
            var query = await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.Customer)
                .Include(o => o.Status)
                .Include(o => o.PaymentMethod)
                .Where(o => !o.IsDeleted)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new
                {
                    o.OrderID,
                    CustomerName = o.Customer != null ? o.Customer.FullName : "Unknown",
                    StatusName = o.Status != null ? o.Status.StatusName : "Unknown",
                    PaymentMethod = o.PaymentMethod != null ? o.PaymentMethod.MethodName : "Unknown",
                    o.TotalAmount,
                    o.ShippingAddress,
                    o.ShippingCity,
                    o.OrderDate
                })
                .ToListAsync();

            Debug.WriteLine($"Query executed. Found {query.Count} orders");

            var orders = query.Select(o => new OrderDisplayDto
            {
                OrderID = o.OrderID,
                CustomerName = o.CustomerName,
                StatusName = o.StatusName,
                StatusColor = GetStatusColor(o.StatusName),
                PaymentMethod = o.PaymentMethod,
                TotalAmount = o.TotalAmount,
                FormattedTotalAmount = string.Format("{0:N0} VNĐ", o.TotalAmount),
                ShippingAddress = o.ShippingAddress,
                ShippingCity = o.ShippingCity,
                OrderDate = o.OrderDate,
                FormattedOrderDate = o.OrderDate.ToString("dd/MM/yyyy HH:mm")
            }).ToList();

            // Kiểm tra từng order
            foreach (var order in orders)
            {
                Debug.WriteLine($"Order {order.OrderID}: Customer={order.CustomerName}, Status={order.StatusName}, Payment={order.PaymentMethod}");
            }

            Orders = new ObservableCollection<OrderDisplayDto>(orders);
            Debug.WriteLine($"Orders loaded successfully. Collection count: {Orders.Count}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading orders: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            if (ex.InnerException != null)
            {
                Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                Debug.WriteLine($"Inner stack trace: {ex.InnerException.StackTrace}");
            }
        }
        finally
        {
            IsLoading = false;
            Debug.WriteLine("LoadOrders completed");
        }
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

    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            LoadOrdersCommand.Execute(null);
            return;
        }

        var searchText = value.ToLower();
        var filteredOrders = Orders
            .Where(o => o.CustomerName.ToLower().Contains(searchText) ||
                       o.OrderID.ToString().Contains(searchText) ||
                       o.StatusName.ToLower().Contains(searchText))
            .ToList();

        Orders = new ObservableCollection<OrderDisplayDto>(filteredOrders);
    }
} 