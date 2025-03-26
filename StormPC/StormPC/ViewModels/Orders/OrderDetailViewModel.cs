using CommunityToolkit.Mvvm.ComponentModel;
using StormPC.Core.Models.Orders.Dtos;
using StormPC.Core.Services.Orders;
using System.Diagnostics;

namespace StormPC.ViewModels.Orders;

public partial class OrderDetailViewModel : ObservableObject
{
    private readonly IOrderDetailService _orderDetailService;

    [ObservableProperty]
    private OrderDetailDto? _orderDetail;

    [ObservableProperty]
    private bool _isLoading;

    public OrderDetailViewModel(IOrderDetailService orderDetailService)
    {
        _orderDetailService = orderDetailService;
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

            OrderDetail = await _orderDetailService.GetLatestOrderDetailAsync();

            if (OrderDetail != null)
            {
                Debug.WriteLine($"Order loaded successfully. OrderID: {OrderDetail.OrderID}");
                Debug.WriteLine($"Items count: {OrderDetail.Items.Count}");
            }
            else
            {
                Debug.WriteLine("No order found");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading order: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
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

            OrderDetail = await _orderDetailService.GetOrderDetailByIdAsync(orderId);

            if (OrderDetail != null)
            {
                Debug.WriteLine($"Order loaded successfully. OrderID: {OrderDetail.OrderID}");
                Debug.WriteLine($"Items count: {OrderDetail.Items.Count}");
            }
            else
            {
                Debug.WriteLine($"No order found with ID: {orderId}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading order: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
        }
        finally
        {
            IsLoading = false;
            Debug.WriteLine($"LoadOrderByIdAsync completed for OrderID: {orderId}");
        }
    }
} 