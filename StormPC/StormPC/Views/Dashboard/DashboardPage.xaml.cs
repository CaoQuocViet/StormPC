using Microsoft.UI.Xaml.Controls;
using StormPC.Contracts.Services;
using StormPC.ViewModels.Dashboard;

namespace StormPC.Views.Dashboard;

public sealed partial class DashboardPage : Page
{
    private readonly INavigationService _navigationService;

    public DashboardViewModel ViewModel
    {
        get;
    }

    public DashboardPage()
    {
        ViewModel = App.GetService<DashboardViewModel>();
        _navigationService = App.GetService<INavigationService>();
        InitializeComponent();

        // Navigate to default page
        _navigationService.NavigateTo(typeof(InventoryReportViewModel).FullName!);
    }
} 