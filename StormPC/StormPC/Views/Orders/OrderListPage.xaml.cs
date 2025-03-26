using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Controls;
using StormPC.Core.Models.Orders.Dtos;
using StormPC.ViewModels.Orders;

namespace StormPC.Views.Orders;

public sealed partial class OrderListPage : Page
{
    public OrderListViewModel ViewModel { get; }

    public OrderListPage()
    {
        ViewModel = App.GetService<OrderListViewModel>();
        InitializeComponent();
    }

    private async void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        await ViewModel.InitializeAsync();
    }

    private void DataGrid_DoubleTapped(object sender, Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
    {
        if (sender is DataGrid dataGrid && dataGrid.SelectedItem is OrderDisplayDto selectedOrder)
        {
            Frame.Navigate(typeof(OrderDetailPage), selectedOrder.OrderID);
        }
    }
} 