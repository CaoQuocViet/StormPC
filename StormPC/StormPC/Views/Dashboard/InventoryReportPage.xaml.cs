using Microsoft.UI.Xaml.Controls;
using StormPC.ViewModels.Dashboard;

namespace StormPC.Views.Dashboard;

public sealed partial class InventoryReportPage : Page
{
    public InventoryReportViewModel ViewModel { get; }

    public InventoryReportPage()
    {
        ViewModel = App.GetService<InventoryReportViewModel>();
        this.InitializeComponent();
    }
} 