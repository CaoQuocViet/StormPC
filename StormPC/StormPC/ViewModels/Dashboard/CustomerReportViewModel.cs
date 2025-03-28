using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using StormPC.Core.Services.Dashboard;
using System.Collections.ObjectModel;

namespace StormPC.ViewModels.Dashboard;

public partial class CustomerReportViewModel : ObservableObject
{
    private readonly ICustomerReportService _customerReportService;
    
    [ObservableProperty]
    private bool _isLoading;
    
    [ObservableProperty]
    private DateTime _startDate = DateTime.Now.AddMonths(-6);
    
    [ObservableProperty]
    private DateTime _endDate = DateTime.Now;

    [ObservableProperty]
    private CustomerSegmentationData _segmentationData = new();

    [ObservableProperty]
    private ObservableCollection<TopCustomerData> _topCustomers = new();

    [ObservableProperty]
    private ISeries[] _purchaseTrendsSeries = Array.Empty<ISeries>();

    [ObservableProperty]
    private ISeries[] _customerSegmentationSeries = Array.Empty<ISeries>();

    [ObservableProperty]
    private ISeries[] _customerPreferenceSeries = Array.Empty<ISeries>();

    public CustomerReportViewModel(ICustomerReportService customerReportService)
    {
        _customerReportService = customerReportService;
    }

    [RelayCommand]
    public async Task LoadDataAsync()
    {
        if (IsLoading) return;
        
        try
        {
            IsLoading = true;

            // Load segmentation data
            var segData = await _customerReportService.GetCustomerSegmentationDataAsync();
            SegmentationData = segData;

            // Load top customers
            var topCustomersList = await _customerReportService.GetTopCustomersAsync();
            TopCustomers = new ObservableCollection<TopCustomerData>(topCustomersList);

            // Load purchase trends
            var trends = await _customerReportService.GetPurchaseTrendsAsync(StartDate, EndDate);
            if (trends.Any())
            {
                PurchaseTrendsSeries = new ISeries[]
                {
                    new LineSeries<decimal>
                    {
                        Name = "Doanh thu",
                        Values = trends.Select(t => t.TotalAmount).ToArray(),
                        Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 },
                        GeometrySize = 8,
                        GeometryStroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 }
                    },
                    new LineSeries<int>
                    {
                        Name = "Số lượng đơn hàng",
                        Values = trends.Select(t => t.OrderCount).ToArray(),
                        Stroke = new SolidColorPaint(SKColors.Orange) { StrokeThickness = 2 },
                        GeometrySize = 8,
                        GeometryStroke = new SolidColorPaint(SKColors.Orange) { StrokeThickness = 2 }
                    }
                };
            }

            // Load customer segmentation
            if (SegmentationData.TotalCustomers > 0)
            {
                CustomerSegmentationSeries = new ISeries[]
                {
                    new PieSeries<int>
                    {
                        Name = "Khách hàng Platinum",
                        Values = new[] { SegmentationData.PlatinumCustomers },
                        Fill = new SolidColorPaint(SKColors.Gold)
                    },
                    new PieSeries<int>
                    {
                        Name = "Khách hàng Gold",
                        Values = new[] { SegmentationData.GoldCustomers },
                        Fill = new SolidColorPaint(SKColors.Orange)
                    },
                    new PieSeries<int>
                    {
                        Name = "Khách hàng Silver",
                        Values = new[] { SegmentationData.SilverCustomers },
                        Fill = new SolidColorPaint(SKColors.Silver)
                    },
                    new PieSeries<int>
                    {
                        Name = "Khách hàng Bronze",
                        Values = new[] { SegmentationData.BronzeCustomers },
                        Fill = new SolidColorPaint(SKColors.SaddleBrown)
                    }
                };
            }

            // Load customer preferences
            var preferences = await _customerReportService.GetCustomerPreferencesAsync();
            if (preferences.Any())
            {
                CustomerPreferenceSeries = new ISeries[]
                {
                    new ColumnSeries<int>
                    {
                        Name = "Số đơn hàng",
                        Values = preferences.Select(p => p.TotalOrders).ToArray(),
                        Fill = new SolidColorPaint(SKColors.Blue)
                    },
                    new ColumnSeries<decimal>
                    {
                        Name = "Doanh thu",
                        Values = preferences.Select(p => p.TotalRevenue).ToArray(),
                        Fill = new SolidColorPaint(SKColors.Purple)
                    }
                };
            }
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadDataAsync();
    }

    partial void OnStartDateChanged(DateTime value)
    {
        LoadDataCommand.Execute(null);
    }

    partial void OnEndDateChanged(DateTime value)
    {
        LoadDataCommand.Execute(null);
    }
} 