using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Kernel.Sketches;
using SkiaSharp;
using StormPC.Core.Services.Dashboard;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StormPC.ViewModels.Dashboard;

public partial class RevenueReportViewModel : ObservableObject
{
    private readonly IRevenueReportService _revenueReportService;

    [ObservableProperty]
    private DateTime _startDate = DateTime.Now.AddDays(-30);

    [ObservableProperty]
    private DateTime _endDate = DateTime.Now;

    [ObservableProperty]
    private RevenueData _revenueData;

    [ObservableProperty]
    private ISeries[] _revenueSeries;

    [ObservableProperty]
    private ISeries[] _categoryRevenueSeries;

    [ObservableProperty]
    private ISeries[] _paymentMethodSeries;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis> _xAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis> _yAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis> _categoryYAxes;

    private string FormatCurrency(double value)
    {
        if (value >= 1_000_000_000) // Tỷ
        {
            return $"{value / 1_000_000_000:N1} Tỷ VNĐ";
        }
        else // Triệu
        {
            return $"{value / 1_000_000:N0} Tr VNĐ";
        }
    }

    public RevenueReportViewModel(IRevenueReportService revenueReportService)
    {
        _revenueReportService = revenueReportService;
        LoadDataAsync().ConfigureAwait(false);
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        RevenueData = await _revenueReportService.GetRevenueDataAsync(StartDate, EndDate);

        var dailyData = await _revenueReportService.GetDailyRevenueAsync(StartDate, EndDate);
        var revenueValues = new List<DateTimePoint>();
        var profitValues = new List<DateTimePoint>();

        foreach (var data in dailyData)
        {
            revenueValues.Add(new DateTimePoint(data.Date, (double)data.Revenue));
            profitValues.Add(new DateTimePoint(data.Date, (double)data.Profit));
        }

        RevenueSeries = new ISeries[]
        {
            new LineSeries<DateTimePoint>
            {
                Name = "Doanh thu",
                Values = revenueValues,
                Fill = null,
                GeometrySize = 0,
                LineSmoothness = 0.5
            },
            new LineSeries<DateTimePoint>
            {
                Name = "Lợi nhuận",
                Values = profitValues,
                Fill = null,
                GeometrySize = 0,
                LineSmoothness = 0.5
            }
        };

        XAxes = new[]
        {
            new Axis
            {
                Labeler = value => new DateTime((long)value).ToString("dd/MM"),
                UnitWidth = TimeSpan.FromDays(1).Ticks
            }
        };

        YAxes = new[]
        {
            new Axis
            {
                Labeler = value => FormatCurrency(value)
            }
        };

        var categoryData = await _revenueReportService.GetCategoryRevenueAsync(StartDate, EndDate);
        var categoryValues = new List<ColumnSeries<decimal>>();
        foreach (var category in categoryData)
        {
            categoryValues.Add(new ColumnSeries<decimal>
            {
                Name = $"{category.CategoryName} ({FormatCurrency((double)category.Revenue)})",
                Values = new[] { category.Revenue }
            });
        }
        CategoryRevenueSeries = categoryValues.ToArray();

        CategoryYAxes = new[]
        {
            new Axis
            {
                Labeler = value => FormatCurrency((double)value)
            }
        };

        var paymentData = await _revenueReportService.GetPaymentMethodDataAsync(StartDate, EndDate);
        var paymentValues = new List<PieSeries<double>>();
        foreach (var payment in paymentData)
        {
            paymentValues.Add(new PieSeries<double>
            {
                Name = $"{payment.MethodName} ({FormatCurrency((double)payment.Amount)})",
                Values = new[] { (double)payment.Amount },
                InnerRadius = 100
            });
        }
        PaymentMethodSeries = paymentValues.ToArray();
    }
} 