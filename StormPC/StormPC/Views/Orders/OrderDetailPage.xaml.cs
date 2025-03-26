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
        InitializeComponent();
        ViewModel = App.GetService<OrderDetailViewModel>();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        // Only load latest order if we didn't navigate with a specific order ID
        if (Frame.BackStack.Count == 0)
        {
            await ViewModel.InitializeAsync();
        }
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        // If we have a parameter and it's an integer (OrderID)
        if (e.Parameter is int orderId)
        {
            await ViewModel.LoadOrderByIdAsync(orderId);
        }
    }
} 