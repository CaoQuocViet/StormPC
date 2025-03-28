using Microsoft.UI.Xaml.Controls;
using StormPC.ViewModels.Dashboard;

namespace StormPC.Views.Dashboard;

public sealed partial class CustomerReportPage : Page
{
    public CustomerReportViewModel ViewModel { get; }

    public CustomerReportPage()
    {
        ViewModel = App.GetService<CustomerReportViewModel>();
        InitializeComponent();
    }

    private async void CustomerReportPage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (ViewModel != null)
        {
            await ViewModel.LoadDataAsync();
        }
    }
} 