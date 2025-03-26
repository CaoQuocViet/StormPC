using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using StormPC.ViewModels.Orders;

namespace StormPC.Views.Orders;

public sealed partial class OrderListPage : Page
{
    public OrderListViewModel ViewModel { get; }

    public OrderListPage()
    {
        ViewModel = App.GetService<OrderListViewModel>();
        this.InitializeComponent();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.InitializeAsync();
    }
} 