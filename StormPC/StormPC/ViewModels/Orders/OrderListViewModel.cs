using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StormPC.Core.Models.Orders.Dtos;
using StormPC.Core.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.ComponentModel;

namespace StormPC.ViewModels.Orders;

public partial class OrderListViewModel : ObservableObject, IPaginatedViewModel
{
    private readonly StormPCDbContext _dbContext;
    private List<OrderDisplayDto> _allOrders;
    private ObservableCollection<OrderDisplayDto> _orders;
    private bool _isLoading;
    [ObservableProperty]
    private string _searchText = string.Empty;
    private int _currentPage = 1;
    private int _pageSize = 10;
    private int _totalItems;

    // Sorting properties
    private List<string> _sortProperties = new();
    private List<ListSortDirection> _sortDirections = new();

    public ObservableCollection<OrderDisplayDto> Orders
    {
        get => _orders;
        set => SetProperty(ref _orders, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    public int CurrentPage
    {
        get => _currentPage;
        set
        {
            if (SetProperty(ref _currentPage, value))
            {
                LoadPage(value);
            }
        }
    }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (SetProperty(ref _pageSize, value))
            {
                FilterAndPaginateOrders();
            }
        }
    }

    public int TotalPages => (_totalItems + PageSize - 1) / PageSize;

    public OrderListViewModel(StormPCDbContext dbContext)
    {
        _dbContext = dbContext;
        _orders = new ObservableCollection<OrderDisplayDto>();
        _allOrders = new List<OrderDisplayDto>();
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

            _allOrders = orders;
            FilterAndPaginateOrders();
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

    public void UpdateSorting(List<string> properties, List<ListSortDirection> directions)
    {
        _sortProperties = properties;
        _sortDirections = directions;
        FilterAndPaginateOrders();
    }

    private void FilterAndPaginateOrders()
    {
        var filteredOrders = string.IsNullOrWhiteSpace(SearchText)
            ? _allOrders
            : _allOrders.Where(o =>
                o.OrderID.ToString().Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                o.CustomerName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                o.StatusName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase)
            ).ToList();

        // Apply sorting if any sort properties are defined
        if (_sortProperties.Any())
        {
            filteredOrders = Core.Helpers.DataGridSortHelper.ApplySort(
                filteredOrders,
                _sortProperties,
                _sortDirections
            ).ToList();
        }

        _totalItems = filteredOrders.Count;
        CurrentPage = 1;
        LoadPage(1);
    }

    public void LoadPage(int page)
    {
        if (_allOrders == null) return;

        var filteredOrders = string.IsNullOrWhiteSpace(SearchText)
            ? _allOrders
            : _allOrders.Where(o =>
                o.OrderID.ToString().Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                o.CustomerName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                o.StatusName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase)
            ).ToList();

        // Apply sorting if any sort properties are defined
        if (_sortProperties.Any())
        {
            filteredOrders = Core.Helpers.DataGridSortHelper.ApplySort(
                filteredOrders,
                _sortProperties,
                _sortDirections
            ).ToList();
        }

        _totalItems = filteredOrders.Count;

        var pagedOrders = filteredOrders
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        Orders = new ObservableCollection<OrderDisplayDto>(pagedOrders);
        OnPropertyChanged(nameof(TotalPages));
    }

    partial void OnSearchTextChanged(string value)
    {
        FilterAndPaginateOrders();
    }

    public async Task LoadOrdersAsync()
    {
        try
        {
            IsLoading = true;
            await LoadOrders();
        }
        finally
        {
            IsLoading = false;
        }
    }
} 