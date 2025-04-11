using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Kernel.Sketches;
using SkiaSharp;
using StormPC.Core.Services.Dashboard;
using StormPC.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StormPC.ViewModels.Dashboard;

/// <summary>
/// ViewModel cho báo cáo doanh thu
/// </summary>
public partial class RevenueReportViewModel : ObservableObject
{
    private readonly IRevenueReportService _revenueReportService;
    private readonly IActivityLogService _activityLogService;

    [ObservableProperty]
    private DateTime _startDate = DateTime.Now.AddMonths(-3);

    [ObservableProperty]
    private DateTime _endDate = DateTime.Now;

    [ObservableProperty]
    private RevenueData? _revenueData;

    [ObservableProperty]
    private ISeries[]? _revenueSeries;

    [ObservableProperty]
    private ISeries[]? _categoryRevenueSeries;

    [ObservableProperty]
    private ISeries[]? _paymentMethodSeries;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis>? _xAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis>? _yAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis>? _categoryYAxes;

    /// <summary>
    /// Định dạng giá trị tiền tệ để hiển thị trên biểu đồ
    /// </summary>
    private string FormatCurrency(double value)
    {
        if (value >= 1_000_000_000) // Tỷ
        {
            return $"{value / 1_000_000_000:N1} Tỷ";
        }
        else // Triệu
        {
            return $"{value / 1_000_000:N0} Tr";
        }
    }

    public RevenueReportViewModel(IRevenueReportService revenueReportService, IActivityLogService activityLogService)
    {
        _revenueReportService = revenueReportService;
        _activityLogService = activityLogService;
        LoadDataAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Làm mới dữ liệu báo cáo doanh thu
    /// </summary>
    [RelayCommand]
    private async Task RefreshAsync()
    {
        try
        {
            await _activityLogService.LogActivityAsync(
                "Revenue Report",
                "Refresh",
                "Đang làm mới dữ liệu báo cáo doanh thu",
                "Info",
                "Admin"
            );

            await LoadDataAsync();

            await _activityLogService.LogActivityAsync(
                "Revenue Report",
                "Refresh",
                "Làm mới dữ liệu báo cáo doanh thu thành công",
                "Success",
                "Admin"
            );
        }
        catch (Exception ex)
        {
            await _activityLogService.LogActivityAsync(
                "Revenue Report",
                "Refresh",
                $"Lỗi khi làm mới dữ liệu: {ex.Message}",
                "Error",
                "Admin"
            );
        }
    }

    /// <summary>
    /// Tải dữ liệu báo cáo doanh thu
    /// </summary>
    private async Task LoadDataAsync()
    {
        try
        {
            await _activityLogService.LogActivityAsync(
                "Revenue Report",
                "Load Data",
                "Đang tải dữ liệu báo cáo doanh thu",
                "Info",
                "Admin"
            );

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

            await _activityLogService.LogActivityAsync(
                "Revenue Report",
                "Load Data",
                $"Tải thành công dữ liệu báo cáo doanh thu từ {StartDate:dd/MM/yyyy} đến {EndDate:dd/MM/yyyy}",
                "Success",
                "Admin"
            );
        }
        catch (Exception ex)
        {
            await _activityLogService.LogActivityAsync(
                "Revenue Report",
                "Load Data",
                $"Lỗi khi tải dữ liệu báo cáo: {ex.Message}",
                "Error",
                "Admin"
            );
        }
    }
}