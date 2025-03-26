using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using CommunityToolkit.WinUI.UI.Controls;
using StormPC.ViewModels.Orders;
using StormPC.Core.Models.Orders.Dtos;
using StormPC.Contracts;

namespace StormPC.Views.Orders;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class OrderListPage : Page
{
    public OrderListViewModel ViewModel { get; }
    private readonly INavigationService _navigationService;

    public OrderListPage()
    {
        InitializeComponent();
        ViewModel = App.GetService<OrderListViewModel>();
        _navigationService = App.GetService<INavigationService>();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.InitializeAsync();
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var dataGrid = sender as DataGrid;
        if (dataGrid?.SelectedItem is OrderDisplayDto selectedOrder)
        {
            _navigationService.NavigateTo("StormPC.ViewModels.Orders.OrderDetailViewModel", selectedOrder.OrderID);
        }
    }
} 