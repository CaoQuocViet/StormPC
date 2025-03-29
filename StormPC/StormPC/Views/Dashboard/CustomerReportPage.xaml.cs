using Microsoft.UI.Xaml.Controls;
using StormPC.ViewModels.Dashboard;
using LiveChartsCore.SkiaSharpView.WinUI;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

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

        // Configure tooltips for CartesianChart
        if (this.FindName("PurchaseTrendsChart") is CartesianChart chart)
        {
            chart.TooltipTextSize = 14;
            chart.TooltipPosition = LiveChartsCore.Measure.TooltipPosition.Top;
            chart.TooltipBackgroundPaint = new SolidColorPaint(new SKColor(30, 30, 30));
            chart.TooltipTextPaint = new SolidColorPaint(SKColors.White);
        }
    }
} 