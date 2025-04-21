using CommunityToolkit.Mvvm.ComponentModel;
using StormPC.Core.Models.Orders.Dtos;
using StormPC.Core.Services.Orders;
using System.Diagnostics;
using StormPC.Core.Contracts.Services;
using System;
using System.Threading.Tasks;
using StormPC.Core.Helpers;

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
        Debug.WriteLine("OrderDetailViewModel đã được khởi tạo");
    }

    public async Task InitializeAsync()
    {
        Debug.WriteLine("Đã gọi hàm InitializeAsync");
        await LoadLatestOrderAsync();
    }

    private async Task LoadLatestOrderAsync()
    {
        if (IsLoading)
        {
            Debug.WriteLine("LoadLatestOrderAsync bỏ qua - đang tải");
            return;
        }

        try
        {
            IsLoading = true;
            Debug.WriteLine("LoadLatestOrderAsync bắt đầu...");
            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Tải đơn hàng mới nhất",
                "Đang tải đơn hàng mới nhất",
                "Info",
                GetUserName.GetCurrentUsername()
            );

            OrderDetail = await _orderDetailService.GetLatestOrderDetailAsync();

            if (OrderDetail != null)
            {
                Debug.WriteLine($"Tải đơn hàng thành công. OrderID: {OrderDetail.OrderID}");
                Debug.WriteLine($"Số lượng sản phẩm: {OrderDetail.Items.Count}");
                await _activityLogService.LogActivityAsync(
                    "Đơn hàng",
                    "Tải đơn hàng mới nhất",
                    $"Tải thành công đơn hàng ID: {OrderDetail.OrderID} với {OrderDetail.Items.Count} sản phẩm",
                    "Success",
                    GetUserName.GetCurrentUsername()
                );
            }
            else
            {
                Debug.WriteLine("Không tìm thấy đơn hàng");
                await _activityLogService.LogActivityAsync(
                    "Đơn hàng",
                    "Tải đơn hàng mới nhất",
                    "Không tìm thấy đơn hàng nào",
                    "Info",
                    GetUserName.GetCurrentUsername()
                );
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Lỗi khi tải đơn hàng: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Tải đơn hàng mới nhất",
                $"Lỗi khi tải đơn hàng: {ex.Message}",
                "Error",
                GetUserName.GetCurrentUsername()
            );
        }
        finally
        {
            IsLoading = false;
            Debug.WriteLine("LoadLatestOrderAsync hoàn thành");
        }
    }

    public async Task LoadOrderByIdAsync(int orderId)
    {
        if (IsLoading)
        {
            Debug.WriteLine($"LoadOrderByIdAsync bỏ qua - đang tải. OrderID: {orderId}");
            return;
        }

        try
        {
            IsLoading = true;
            Debug.WriteLine($"LoadOrderByIdAsync bắt đầu cho OrderID: {orderId}");
            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Tải đơn hàng",
                $"Đang tải đơn hàng ID: {orderId}",
                "Info",
                GetUserName.GetCurrentUsername()
            );

            OrderDetail = await _orderDetailService.GetOrderDetailByIdAsync(orderId);

            if (OrderDetail != null)
            {
                Debug.WriteLine($"Tải đơn hàng thành công. OrderID: {OrderDetail.OrderID}");
                Debug.WriteLine($"Số lượng sản phẩm: {OrderDetail.Items.Count}");
                await _activityLogService.LogActivityAsync(
                    "Đơn hàng",
                    "Tải đơn hàng",
                    $"Tải thành công đơn hàng ID: {orderId} với {OrderDetail.Items.Count} sản phẩm",
                    "Success",
                    GetUserName.GetCurrentUsername()
                );
            }
            else
            {
                Debug.WriteLine($"Không tìm thấy đơn hàng có ID: {orderId}");
                await _activityLogService.LogActivityAsync(
                    "Đơn hàng",
                    "Tải đơn hàng",
                    $"Không tìm thấy đơn hàng ID: {orderId}",
                    "Error",
                    GetUserName.GetCurrentUsername()
                );
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Lỗi khi tải đơn hàng: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            await _activityLogService.LogActivityAsync(
                "Đơn hàng",
                "Tải đơn hàng",
                $"Lỗi khi tải đơn hàng: {ex.Message}",
                "Error",
                GetUserName.GetCurrentUsername()
            );
        }
        finally
        {
            IsLoading = false;
            Debug.WriteLine($"LoadOrderByIdAsync hoàn thành cho OrderID: {orderId}");
        }
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        return await _orderListViewModel.DeleteOrderAsync(orderId);
    }
}