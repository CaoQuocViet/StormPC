using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StormPC.Core.Models.Orders.Dtos;
using StormPC.Core.Models.Orders;
using StormPC.Core.Models.Customers;
using StormPC.Core.Models.Products;
using StormPC.Core.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.ComponentModel;
using System;
using Microsoft.UI.Xaml.Controls;
using StormPC.Core.Contracts.Services;

namespace StormPC.ViewModels.Orders;

public partial class OrderListViewModel : ObservableObject, IPaginatedViewModel
{
    private readonly StormPCDbContext _dbContext;
    private readonly IActivityLogService _activityLogService;
    private List<OrderDisplayDto> _allOrders;
    private ObservableCollection<OrderDisplayDto> _orders;
    private bool _isLoading;
    [ObservableProperty]
    private string searchText = string.Empty;
    private int _currentPage = 1;
    private int _pageSize = 10;
    private int _totalItems;

    // Thuộc tính sắp xếp
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

    public OrderListViewModel(StormPCDbContext dbContext, IActivityLogService activityLogService)
    {
        _dbContext = dbContext;
        _activityLogService = activityLogService;
        _orders = new ObservableCollection<OrderDisplayDto>();
        _allOrders = new List<OrderDisplayDto>();
        Debug.WriteLine("OrderListViewModel được khởi tạo với dbContext");
    }

    public async Task InitializeAsync()
    {
        Debug.WriteLine("InitializeAsync được gọi");
        await LoadOrders();
    }

    [RelayCommand]
    private async Task LoadOrders()
    {
        try
        {
            await _activityLogService.LogActivityAsync("Đơn hàng", "Tải danh sách", "Bắt đầu tải danh sách đơn hàng", "Info", "Admin");
            IsLoading = true;
            Debug.WriteLine("Bắt đầu LoadOrders...");
            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Tải đơn hàng",
                "Đang tải danh sách đơn hàng",
                "Info",
                "Admin"
            );

            // Kiểm tra kết nối database
            Debug.WriteLine("Kiểm tra kết nối database...");
            if (_dbContext.Database == null)
            {
                Debug.WriteLine("Database context là null!");
                await _activityLogService.LogActivityAsync(
                    "Đơn hàng",
                    "Tải đơn hàng",
                    "Lỗi kết nối database - Database context is null",
                    "Error",
                    "Admin"
                );
                return;
            }

            // Query từng bảng riêng lẻ để kiểm tra
            Debug.WriteLine("Đang kiểm tra từng bảng...");
            var ordersExist = await _dbContext.Orders.AnyAsync();
            Debug.WriteLine($"Bảng Orders có dữ liệu: {ordersExist}");

            var customersExist = await _dbContext.Customers.AnyAsync();
            Debug.WriteLine($"Bảng Customers có dữ liệu: {customersExist}");

            var statusesExist = await _dbContext.OrderStatuses.AnyAsync();
            Debug.WriteLine($"Bảng OrderStatuses có dữ liệu: {statusesExist}");

            var paymentMethodsExist = await _dbContext.PaymentMethods.AnyAsync();
            Debug.WriteLine($"Bảng PaymentMethods có dữ liệu: {paymentMethodsExist}");

            // Thực hiện query chính với logging
            Debug.WriteLine("Thực hiện truy vấn chính...");
            var query = await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.Customer)
                .Include(o => o.Status)
                .Include(o => o.PaymentMethod)
                .Include(o => o.ShipCity)
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
                    CityName = o.ShipCity != null ? o.ShipCity.CityName : "Unknown",  // Use CityName from Cities table
                    o.OrderDate
                })
                .ToListAsync();

            Debug.WriteLine($"Truy vấn hoàn tất. Tìm thấy {query.Count} đơn hàng");

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
                ShippingCity = o.CityName,
                OrderDate = o.OrderDate,
                FormattedOrderDate = o.OrderDate.ToString("dd/MM/yyyy HH:mm")
            }).ToList();

            // Kiểm tra từng đơn hàng
            foreach (var order in orders)
            {
                Debug.WriteLine($"Đơn hàng {order.OrderID}: Khách hàng={order.CustomerName}, Trạng thái={order.StatusName}");
            }

            _allOrders = orders;
            FilterAndPaginateOrders();

            await _activityLogService.LogActivityAsync("Đơn hàng", "Tải danh sách", $"Đã tải thành công {Orders.Count} đơn hàng", "Success", "Admin");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Lỗi khi tải đơn hàng: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            await _activityLogService.LogActivityAsync("Đơn hàng", "Tải danh sách", $"Lỗi: {ex.Message}", "Error", "Admin");
            if (ex.InnerException != null)
            {
                Debug.WriteLine($"Lỗi bên trong: {ex.InnerException.Message}");
                Debug.WriteLine($"Stack trace lỗi bên trong: {ex.InnerException.StackTrace}");
                await _activityLogService.LogActivityAsync(
                    "Đơn hàng",
                    "Tải đơn hàng",
                    $"Lỗi bên trong: {ex.InnerException.Message}",
                    "Error",
                    "Admin"
                );
            }
        }
        finally
        {
            IsLoading = false;
            Debug.WriteLine("LoadOrders hoàn thành");
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
                o.CustomerName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                o.StatusName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                o.ShippingCity.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
            ).ToList();

        // Áp dụng sắp xếp dựa trên thuộc tính sắp xếp hiện tại
        filteredOrders = ApplySorting(filteredOrders);

        _totalItems = filteredOrders.Count;
        LoadPage(1); // Đặt lại về trang đầu tiên khi lọc
    }

    public void LoadPage(int page)
    {
        if (_allOrders == null) return;

        var filteredOrders = string.IsNullOrWhiteSpace(SearchText)
            ? _allOrders
            : _allOrders.Where(o =>
                o.CustomerName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                o.StatusName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                o.ShippingCity.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
            ).ToList();

        // Áp dụng sắp xếp
        filteredOrders = ApplySorting(filteredOrders);

        _totalItems = filteredOrders.Count;

        var pagedOrders = filteredOrders
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        Orders = new ObservableCollection<OrderDisplayDto>(pagedOrders);
        OnPropertyChanged(nameof(TotalPages));
    }

    private List<OrderDisplayDto> ApplySorting(List<OrderDisplayDto> orders)
    {
        // Áp dụng sắp xếp nếu có thuộc tính sắp xếp được định nghĩa
        if (_sortProperties.Any())
        {
            orders = Core.Helpers.DataGridSortHelper.ApplySort(
                orders,
                _sortProperties,
                _sortDirections
            ).ToList();
        }
        return orders;
    }

    partial void OnSearchTextChanged(string value)
    {
        FilterAndPaginateOrders();
    }

    public async Task LoadOrdersAsync()
    {
        try
        {
            await _activityLogService.LogActivityAsync("Đơn hàng", "Tải danh sách", "Bắt đầu tải danh sách đơn hàng", "Info", "Admin");
            IsLoading = true;
            await LoadOrders();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        try
        {
            IsLoading = true;
            Debug.WriteLine($"Đang cố gắng xóa đơn hàng {orderId}");
            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Xóa đơn hàng",
                $"Đang xóa đơn hàng ID: {orderId}",
                "Info",
                "Admin"
            );

            // Tải đơn hàng với trạng thái
            var order = await _dbContext.Orders
                .Include(o => o.Status)
                .FirstOrDefaultAsync(o => o.OrderID == orderId && !o.IsDeleted);

            if (order == null)
            {
                Debug.WriteLine($"Không tìm thấy đơn hàng {orderId}");
                await _activityLogService.LogActivityAsync(
                    "Đơn hàng",
                    "Xóa đơn hàng",
                    $"Xóa đơn hàng thất bại - Không tìm thấy đơn hàng ID: {orderId}",
                    "Error",
                    "Admin"
                );
                return false;
            }

            // Kiểm tra trạng thái
            if (order.Status?.StatusName != "Cancelled")
            {
                Debug.WriteLine($"Không thể xóa đơn hàng {orderId} - trạng thái chưa phải là Đã hủy");
                await _activityLogService.LogActivityAsync(
                    "Đơn hàng",
                    "Xóa đơn hàng",
                    $"Xóa đơn hàng thất bại - Đơn hàng ID: {orderId} chưa bị hủy",
                    "Error",
                    "Admin"
                );
                return false;
            }

            order.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            Debug.WriteLine($"Đơn hàng {orderId} đã được đánh dấu xóa");

            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Xóa đơn hàng",
                $"Xóa đơn hàng thành công ID: {orderId}",
                "Success",
                "Admin"
            );

            // Làm mới danh sách đơn hàng
            await LoadOrders();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Lỗi khi xóa đơn hàng: {ex.Message}");
            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Xóa đơn hàng",
                $"Lỗi khi xóa đơn hàng: {ex.Message}",
                "Error",
                "Admin"
            );
            throw;
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task<OrderDialogViewModel> CreateNewOrderDialogViewModel()
    {
        var viewModel = new OrderDialogViewModel();
        await LoadDialogData(viewModel);
        
        // Đặt trạng thái mặc định là Đang chờ cho đơn hàng mới
        viewModel.SelectedStatus = viewModel.OrderStatuses.FirstOrDefault(s => s.StatusName.ToLower() == "pending");
        
        return viewModel;
    }

    public async Task<OrderDialogViewModel> CreateEditOrderDialogViewModel(int? orderId = null)
    {
        var viewModel = new OrderDialogViewModel();
        
        // Tải dữ liệu
        await LoadDialogData(viewModel);

        if (orderId.HasValue)
        {
            // Tải đơn hàng hiện có
            var existingOrder = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.LaptopSpec)
                .ThenInclude(ls => ls.Laptop)
                .FirstOrDefaultAsync(o => o.OrderID == orderId.Value);

            if (existingOrder != null)
            {
                viewModel.IsNewOrder = false;  // Đặt thành false khi chỉnh sửa
                // Thiết lập các giá trị đã chọn từ đơn hàng hiện có
                viewModel.SelectedCustomer = viewModel.Customers.FirstOrDefault(c => c.CustomerID == existingOrder.CustomerID);
                
                var orderItem = existingOrder.OrderItems.FirstOrDefault();
                if (orderItem != null)
                {
                    viewModel.SelectedLaptop = viewModel.Laptops.FirstOrDefault(l => l.LaptopID == orderItem.LaptopSpec.LaptopID);
                    viewModel.SelectedLaptopSpec = viewModel.FilteredLaptopSpecs.FirstOrDefault(ls => ls.VariantID == orderItem.VariantID);
                    viewModel.Quantity = orderItem.Quantity;
                }

                viewModel.SelectedPaymentMethod = viewModel.PaymentMethods.FirstOrDefault(pm => pm.PaymentMethodID == existingOrder.PaymentMethodID);
                viewModel.ShippingAddress = existingOrder.ShippingAddress ?? string.Empty;
                viewModel.ShippingPostalCode = existingOrder.ShippingPostalCode ?? string.Empty;
                viewModel.SelectedCity = viewModel.Cities.FirstOrDefault(c => c.Id == existingOrder.ShipCityId);
                viewModel.SelectedStatus = viewModel.OrderStatuses.FirstOrDefault(s => s.StatusID == existingOrder.StatusID);
            }
        }
        else
        {
            viewModel.IsNewOrder = true;  // Đặt thành true cho đơn hàng mới
            // Đặt trạng thái mặc định là Đang chờ (StatusID = 1)
            viewModel.SelectedStatus = viewModel.OrderStatuses.FirstOrDefault(s => s.StatusID == 1);
        }

        return viewModel;
    }

    private async Task LoadDialogData(OrderDialogViewModel viewModel)
    {
        // Tải danh sách khách hàng
        var customers = await _dbContext.Customers
            .Where(c => !c.IsDeleted)
            .OrderBy(c => c.FullName)
            .ToListAsync();
        viewModel.Customers = new ObservableCollection<Customer>(customers);

        // Tải danh sách laptop
        var laptops = await _dbContext.Laptops
            .Where(l => !l.IsDeleted)
            .OrderBy(l => l.ModelName)
            .ToListAsync();
        viewModel.Laptops = new ObservableCollection<Laptop>(laptops);

        // Tải thông số kỹ thuật laptop
        var laptopSpecs = await _dbContext.LaptopSpecs
            .Include(ls => ls.Laptop)
            .Where(ls => !ls.IsDeleted && ls.StockQuantity > 0)
            .OrderBy(ls => ls.Laptop.ModelName)
            .ToListAsync();
        viewModel.LaptopSpecs = new ObservableCollection<LaptopSpec>(laptopSpecs);

        // Tải phương thức thanh toán
        var paymentMethods = await _dbContext.PaymentMethods.ToListAsync();
        viewModel.PaymentMethods = new ObservableCollection<PaymentMethod>(paymentMethods);

        // Tải danh sách thành phố
        var cities = await _dbContext.Cities.OrderBy(c => c.CityName).ToListAsync();
        viewModel.Cities = new ObservableCollection<City>(cities);

        // Tải trạng thái đơn hàng
        var statuses = await _dbContext.OrderStatuses.ToListAsync();
        viewModel.OrderStatuses = new ObservableCollection<OrderStatus>(statuses);
    }

    private static async Task ShowErrorDialog(string title, string content)
    {
        var dialog = new ContentDialog
        {
            Title = title,
            Content = content,
            CloseButtonText = "Đóng",
            XamlRoot = App.MainWindow.Content.XamlRoot
        };
        
        // Đảm bảo hộp thoại lỗi xuất hiện trên hộp thoại chính
        dialog.DefaultButton = ContentDialogButton.Close;

        await dialog.ShowAsync(ContentDialogPlacement.InPlace);
    }

    public async Task AddOrderAsync(OrderDialogViewModel dialogViewModel)
    {
        try
        {
            await _activityLogService.LogActivityAsync("Đơn hàng", "Thêm mới", "Bắt đầu thêm đơn hàng mới", "Info", "Admin");
            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Thêm đơn hàng",
                "Đang thêm đơn hàng mới",
                "Info",
                "Admin"
            );

            if (!ValidateOrderData(dialogViewModel, out string errorMessage))
            {
                await ShowErrorDialog("Error", errorMessage);
                await _activityLogService.LogActivityAsync(
                    "Đơn hàng",
                    "Thêm đơn hàng",
                    $"Thêm đơn hàng thất bại - {errorMessage}",
                    "Error",
                    "Admin"
                );
                return;
            }

            var maxOrderId = await _dbContext.Orders.MaxAsync(o => (int?)o.OrderID) ?? 0;
            var newOrderId = maxOrderId + 1;

            var order = new Order
            {
                OrderID = newOrderId,
                CustomerID = dialogViewModel.SelectedCustomer.CustomerID,
                StatusID = dialogViewModel.SelectedStatus.StatusID,
                PaymentMethodID = dialogViewModel.SelectedPaymentMethod.PaymentMethodID,
                OrderDate = DateTime.UtcNow,
                ShippingAddress = dialogViewModel.ShippingAddress,
                ShipCityId = dialogViewModel.SelectedCity.Id,  // Sử dụng City.Id
                ShippingCity = dialogViewModel.SelectedCity.CityName,  // Sử dụng CityName
                ShippingPostalCode = dialogViewModel.ShippingPostalCode,  // Thêm mã bưu điện
                TotalAmount = dialogViewModel.SelectedLaptopSpec.Price * dialogViewModel.Quantity,
                IsDeleted = false
            };

            var orderItem = new OrderItem
            {
                OrderID = newOrderId,  // Đặt OrderID cho OrderItem
                VariantID = dialogViewModel.SelectedLaptopSpec.VariantID,
                Quantity = dialogViewModel.Quantity,
                UnitPrice = dialogViewModel.SelectedLaptopSpec.Price
            };

            order.OrderItems = new List<OrderItem> { orderItem };

            // Tắt tự động phát hiện thay đổi để ngăn EF Core cố gắng tự động tăng
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            
            try
            {
                _dbContext.Orders.Add(order);

                // Cập nhật số lượng tồn kho
                var laptopSpec = await _dbContext.LaptopSpecs.FindAsync(orderItem.VariantID);
                if (laptopSpec != null)
                {
                    laptopSpec.StockQuantity -= orderItem.Quantity;
                }

                await _dbContext.SaveChangesAsync();

                await _activityLogService.LogActivityAsync("Đơn hàng", "Thêm mới", "Đã thêm đơn hàng thành công", "Success", "Admin");
                await _activityLogService.LogActivityAsync(
                    "Đơn hàng",
                    "Thêm đơn hàng",
                    $"Thêm đơn hàng thành công ID: {newOrderId}",
                    "Success",
                    "Admin"
                );
            }
            finally
            {
                _dbContext.ChangeTracker.AutoDetectChangesEnabled = true;
            }

            await LoadOrders();
        }
        catch (DbUpdateException ex)
        {
            var message = "Không thể thêm đơn hàng. ";
            if (ex.InnerException != null)
            {
                message += "Chi tiết lỗi: " + ex.InnerException.Message;
            }
            await ShowErrorDialog("Error", message);
            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Thêm đơn hàng",
                $"Lỗi khi thêm đơn hàng: {ex.Message}",
                "Error",
                "Admin"
            );
            return;
        }
        catch (Exception ex)
        {
            await ShowErrorDialog("Error", "Đã xảy ra lỗi khi thêm đơn hàng: " + ex.Message);
            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Thêm đơn hàng",
                $"Lỗi khi thêm đơn hàng: {ex.Message}",
                "Error",
                "Admin"
            );
            return;
        }
    }

    private bool ValidateOrderData(OrderDialogViewModel dialogViewModel, out string errorMessage)
    {
        errorMessage = string.Empty;

        if (dialogViewModel.SelectedCustomer == null)
        {
            errorMessage = "Vui lòng chọn khách hàng";
            return false;
        }
        if (dialogViewModel.SelectedLaptop == null)
        {
            errorMessage = "Vui lòng chọn laptop";
            return false;
        }
        if (dialogViewModel.SelectedLaptopSpec == null)
        {
            errorMessage = "Vui lòng chọn cấu hình laptop";
            return false;
        }
        if (dialogViewModel.SelectedPaymentMethod == null)
        {
            errorMessage = "Vui lòng chọn phương thức thanh toán";
            return false;
        }
        if (dialogViewModel.SelectedCity == null)
        {
            errorMessage = "Vui lòng chọn thành phố";
            return false;
        }
        if (dialogViewModel.SelectedStatus == null)
        {
            errorMessage = "Vui lòng chọn trạng thái";
            return false;
        }
        if (string.IsNullOrWhiteSpace(dialogViewModel.ShippingAddress))
        {
            errorMessage = "Vui lòng nhập địa chỉ giao hàng";
            return false;
        }
        if (dialogViewModel.Quantity <= 0)
        {
            errorMessage = "Số lượng phải lớn hơn 0";
            return false;
        }

        // Kiểm tra có đủ hàng tồn kho không
        if (dialogViewModel.SelectedLaptopSpec.StockQuantity < dialogViewModel.Quantity)
        {
            errorMessage = $"Số lượng tồn kho không đủ. Chỉ còn {dialogViewModel.SelectedLaptopSpec.StockQuantity} sản phẩm";
            return false;
        }

        if (string.IsNullOrWhiteSpace(dialogViewModel.ShippingPostalCode))
        {
            errorMessage = "Vui lòng nhập mã bưu điện";
            return false;
        }

        return true;
    }

    public async Task UpdateOrderAsync(int orderId, OrderDialogViewModel dialogViewModel)
    {
        try
        {
            await _activityLogService.LogActivityAsync("Đơn hàng", "Chỉnh sửa", "Bắt đầu chỉnh sửa đơn hàng", "Info", "Admin");
            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Cập nhật đơn hàng",
                $"Đang cập nhật đơn hàng ID: {orderId}",
                "Info",
                "Admin"
            );

            if (!ValidateOrderData(dialogViewModel, out string errorMessage))
            {
                await ShowErrorDialog("Error", errorMessage);
                await _activityLogService.LogActivityAsync(
                    "Đơn hàng",
                    "Cập nhật đơn hàng",
                    $"Cập nhật đơn hàng thất bại - {errorMessage}",
                    "Error",
                    "Admin"
                );
                return;
            }

            var order = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderID == orderId);

            if (order == null)
            {
                await ShowErrorDialog("Error", "Không tìm thấy đơn hàng");
                await _activityLogService.LogActivityAsync(
                    "Đơn hàng",
                    "Cập nhật đơn hàng",
                    $"Cập nhật thất bại - Không tìm thấy đơn hàng ID: {orderId}",
                    "Error",
                    "Admin"
                );
                return;
            }

            // Lưu số lượng cũ để điều chỉnh kho
            var oldOrderItem = order.OrderItems.FirstOrDefault();
            var oldQuantity = oldOrderItem?.Quantity ?? 0;
            var oldVariantId = oldOrderItem?.VariantID ?? 0;

            // Cập nhật thông tin đơn hàng
            order.CustomerID = dialogViewModel.SelectedCustomer.CustomerID;
            order.StatusID = dialogViewModel.SelectedStatus.StatusID;
            order.PaymentMethodID = dialogViewModel.SelectedPaymentMethod.PaymentMethodID;
            order.ShippingAddress = dialogViewModel.ShippingAddress;
            order.ShipCityId = dialogViewModel.SelectedCity.Id;  // Sử dụng City.Id
            order.ShippingCity = dialogViewModel.SelectedCity.CityName;  // Sử dụng CityName
            order.ShippingPostalCode = dialogViewModel.ShippingPostalCode;  // Thêm mã bưu điện
            order.TotalAmount = dialogViewModel.SelectedLaptopSpec.Price * dialogViewModel.Quantity;

            // Cập nhật hoặc tạo mục đơn hàng
            if (oldOrderItem != null)
            {
                oldOrderItem.VariantID = dialogViewModel.SelectedLaptopSpec.VariantID;
                oldOrderItem.Quantity = dialogViewModel.Quantity;
                oldOrderItem.UnitPrice = dialogViewModel.SelectedLaptopSpec.Price;
            }
            else
            {
                order.OrderItems.Add(new OrderItem
                {
                    VariantID = dialogViewModel.SelectedLaptopSpec.VariantID,
                    Quantity = dialogViewModel.Quantity,
                    UnitPrice = dialogViewModel.SelectedLaptopSpec.Price
                });
            }

            await _dbContext.SaveChangesAsync();

            // Cập nhật số lượng tồn kho
            if (oldVariantId > 0)
            {
                var oldLaptopSpec = await _dbContext.LaptopSpecs.FindAsync(oldVariantId);
                if (oldLaptopSpec != null)
                {
                    oldLaptopSpec.StockQuantity += oldQuantity;
                }
            }

            var newLaptopSpec = await _dbContext.LaptopSpecs.FindAsync(dialogViewModel.SelectedLaptopSpec.VariantID);
            if (newLaptopSpec != null)
            {
                newLaptopSpec.StockQuantity -= dialogViewModel.Quantity;
            }

            await _dbContext.SaveChangesAsync();

            await _activityLogService.LogActivityAsync("Đơn hàng", "Chỉnh sửa", "Đã cập nhật đơn hàng thành công", "Success", "Admin");
            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Cập nhật đơn hàng",
                $"Cập nhật thành công đơn hàng ID: {orderId}",
                "Success",
                "Admin"
            );

            await LoadOrders();
        }
        catch (Exception ex)
        {
            await ShowErrorDialog("Error", "Đã xảy ra lỗi khi cập nhật đơn hàng: " + ex.Message);
            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Cập nhật đơn hàng",
                $"Lỗi khi cập nhật đơn hàng: {ex.Message}",
                "Error",
                "Admin"
            );
            return;
        }
    }
}