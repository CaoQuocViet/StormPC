using Microsoft.UI.Xaml.Controls;
using StormPC.ViewModels.Dashboard;

namespace StormPC.Views.Dashboard;

public sealed partial class RevenueReportPage : Page
{
    public RevenueReportViewModel ViewModel { get; }

    public RevenueReportPage()
    {
        ViewModel = App.GetService<RevenueReportViewModel>();
        this.InitializeComponent();
    }
} 