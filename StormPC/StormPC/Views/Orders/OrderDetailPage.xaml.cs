using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using StormPC.ViewModels.Orders;

namespace StormPC.Views.Orders;

public sealed partial class OrderDetailPage : Page
{
    public OrderDetailViewModel ViewModel { get; }

    public OrderDetailPage()
    {
        ViewModel = App.GetService<OrderDetailViewModel>();
        InitializeComponent();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.InitializeAsync();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        if (e.Parameter is int orderId)
        {
            await ViewModel.LoadOrderByIdAsync(orderId);
        }
    }
} 