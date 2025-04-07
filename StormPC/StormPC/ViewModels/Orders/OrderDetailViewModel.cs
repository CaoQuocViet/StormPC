using CommunityToolkit.Mvvm.ComponentModel;
using StormPC.Core.Models.Orders.Dtos;
using StormPC.Core.Services.Orders;
using System.Diagnostics;
using StormPC.Core.Contracts.Services;
using System;
using System.Threading.Tasks;

namespace StormPC.ViewModels.Orders;

public partial class OrderDetailViewModel : ObservableObject
{
    private readonly IOrderDetailService _orderDetailService;
    private readonly OrderListViewModel _orderListViewModel;
    private readonly IActivityLogService _activityLogService;

    [ObservableProperty]
    private OrderDetailDto? _orderDetail;

    [ObservableProperty]
    private bool _isLoading;

    public OrderDetailViewModel(IOrderDetailService orderDetailService, OrderListViewModel orderListViewModel, IActivityLogService activityLogService)
    {
        _orderDetailService = orderDetailService;
        _orderListViewModel = orderListViewModel;
        _activityLogService = activityLogService;
        Debug.WriteLine("OrderDetailViewModel constructed");
    }

    public async Task InitializeAsync()
    {
        Debug.WriteLine("InitializeAsync called");
        await LoadLatestOrderAsync();
    }

    private async Task LoadLatestOrderAsync()
    {
        if (IsLoading)
        {
            Debug.WriteLine("LoadLatestOrderAsync skipped - already loading");
            return;
        }

        try
        {
            IsLoading = true;
            Debug.WriteLine("LoadLatestOrderAsync started...");
            await _activityLogService.LogActivityAsync(
                "Orders",
                "Load Latest Order",
                "Đang tải đơn hàng mới nhất",
                "Info",
                "Admin"
            );

            OrderDetail = await _orderDetailService.GetLatestOrderDetailAsync();

            if (OrderDetail != null)
            {
                Debug.WriteLine($"Order loaded successfully. OrderID: {OrderDetail.OrderID}");
                Debug.WriteLine($"Items count: {OrderDetail.Items.Count}");
                await _activityLogService.LogActivityAsync(
                    "Orders",
                    "Load Latest Order",
                    $"Tải thành công đơn hàng ID: {OrderDetail.OrderID} với {OrderDetail.Items.Count} sản phẩm",
                    "Success",
                    "Admin"
                );
            }
            else
            {
                Debug.WriteLine("No order found");
                await _activityLogService.LogActivityAsync(
                    "Orders",
                    "Load Latest Order",
                    "Không tìm thấy đơn hàng nào",
                    "Info",
                    "Admin"
                );
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading order: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            await _activityLogService.LogActivityAsync(
                "Orders",
                "Load Latest Order",
                $"Lỗi khi tải đơn hàng: {ex.Message}",
                "Error",
                "Admin"
            );
        }
        finally
        {
            IsLoading = false;
            Debug.WriteLine("LoadLatestOrderAsync completed");
        }
    }

    public async Task LoadOrderByIdAsync(int orderId)
    {
        if (IsLoading)
        {
            Debug.WriteLine($"LoadOrderByIdAsync skipped - already loading. OrderID: {orderId}");
            return;
        }

        try
        {
            IsLoading = true;
            Debug.WriteLine($"LoadOrderByIdAsync started for OrderID: {orderId}");
            await _activityLogService.LogActivityAsync(
                "Orders",
                "Load Order",
                $"Đang tải đơn hàng ID: {orderId}",
                "Info",
                "Admin"
            );

            OrderDetail = await _orderDetailService.GetOrderDetailByIdAsync(orderId);

            if (OrderDetail != null)
            {
                Debug.WriteLine($"Order loaded successfully. OrderID: {OrderDetail.OrderID}");
                Debug.WriteLine($"Items count: {OrderDetail.Items.Count}");
                await _activityLogService.LogActivityAsync(
                    "Orders",
                    "Load Order",
                    $"Tải thành công đơn hàng ID: {orderId} với {OrderDetail.Items.Count} sản phẩm",
                    "Success",
                    "Admin"
                );
            }
            else
            {
                Debug.WriteLine($"No order found with ID: {orderId}");
                await _activityLogService.LogActivityAsync(
                    "Orders",
                    "Load Order",
                    $"Không tìm thấy đơn hàng ID: {orderId}",
                    "Error",
                    "Admin"
                );
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading order: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            await _activityLogService.LogActivityAsync(
                "Orders",
                "Load Order",
                $"Lỗi khi tải đơn hàng: {ex.Message}",
                "Error",
                "Admin"
            );
        }
        finally
        {
            IsLoading = false;
            Debug.WriteLine($"LoadOrderByIdAsync completed for OrderID: {orderId}");
        }
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        return await _orderListViewModel.DeleteOrderAsync(orderId);
    }
} 