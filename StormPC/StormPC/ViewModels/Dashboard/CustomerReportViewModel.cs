using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using StormPC.Core.Services.Dashboard;
using System.Collections.ObjectModel;
using LiveChartsCore.Drawing;
using LiveChartsCore.Measure;
using LiveChartsCore.Defaults;

namespace StormPC.ViewModels.Dashboard;

public class BrandInfo : ObservableValue
{
    public BrandInfo(string name, decimal value, int orders, SolidColorPaint paint)
    {
        Name = name;
        Paint = paint;
        Orders = orders;
        Value = orders; // Sử dụng số đơn hàng trực tiếp
    }

    public string Name { get; set; }
    public SolidColorPaint Paint { get; set; }
    public int Orders { get; set; }
}

public partial class CustomerReportViewModel : ObservableObject
{
    private readonly ICustomerReportService _customerReportService;
    private BrandInfo[] _brandData = Array.Empty<BrandInfo>();
    
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

    [ObservableProperty]
    private ICartesianAxis[] _brandXAxes = new ICartesianAxis[]
    {
        new Axis
        {
            SeparatorsPaint = new SolidColorPaint(new SKColor(220, 220, 220)),
            LabelsPaint = new SolidColorPaint
            {
                Color = SKColors.DarkGray,
                SKFontStyle = new SKFontStyle(SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright)
            },
            Position = AxisPosition.Start
        }
    };

    [ObservableProperty]
    private ICartesianAxis[] _brandYAxes = new ICartesianAxis[] 
    { 
        new Axis 
        { 
            IsVisible = false,
            ShowSeparatorLines = false
        } 
    };

    [ObservableProperty]
    private ICartesianAxis[] _xAxes = new ICartesianAxis[]
    {
        new Axis
        {
            Labeler = value => DateTime.FromOADate(value).ToString("MM/yyyy"),
            LabelsPaint = new SolidColorPaint
            {
                Color = SKColors.DarkBlue,
                SKFontStyle = new SKFontStyle(SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright)
            }
        }
    };

    [ObservableProperty]
    private ICartesianAxis[] _yAxes = new ICartesianAxis[]
    {
        new Axis
        {
            Name = "Giá trị đơn hàng (VNĐ)",
            Labeler = value => value.ToString("N0"),
            LabelsPaint = new SolidColorPaint
            {
                Color = SKColors.Blue,
                SKFontStyle = new SKFontStyle(SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright)
            }
        }
    };

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
                var orderedTrends = trends.OrderBy(t => t.Date).ToList();
                var dates = orderedTrends.Select(t => t.Date.ToOADate()).ToArray();
                
                var averageOrderValues = orderedTrends
                    .Select((t, i) => new ObservablePoint(dates[i], (double)t.AverageOrderValue))
                    .ToList();

                var orderCounts = orderedTrends
                    .Select((t, i) => new ObservablePoint(dates[i], (double)t.OrderCount))
                    .ToList();

                PurchaseTrendsSeries = new ISeries[]
                {
                    new LineSeries<ObservablePoint>
                    {
                        Name = "Giá trị đơn hàng trung bình",
                        Values = averageOrderValues,
                        Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 },
                        GeometrySize = 8,
                        GeometryStroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 },
                        Fill = null,
                        LineSmoothness = 0.5
                    },
                    new LineSeries<ObservablePoint>
                    {
                        Name = "Số lượng đơn hàng",
                        Values = orderCounts,
                        Stroke = new SolidColorPaint(SKColors.Orange) { StrokeThickness = 2 },
                        GeometrySize = 8,
                        GeometryStroke = new SolidColorPaint(SKColors.Orange) { StrokeThickness = 2 },
                        Fill = null,
                        LineSmoothness = 0.5
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

            // Load customer preferences (brand preferences)
            var preferences = await _customerReportService.GetCustomerPreferencesAsync();
            if (preferences.Any())
            {
                var brandPreferences = preferences
                    .OrderByDescending(p => p.TotalOrders)
                    .ToList();

                var maxOrders = brandPreferences.Max(p => p.TotalOrders);

                // Màu sắc cho các thương hiệu
                var brandColors = new[]
                {
                    new SKColor(33, 150, 243),   // Blue
                    new SKColor(244, 67, 54),    // Red
                    new SKColor(76, 175, 80),    // Green
                    new SKColor(156, 39, 176),   // Purple
                    new SKColor(255, 193, 7),    // Amber
                    new SKColor(233, 30, 99),    // Pink
                    new SKColor(0, 150, 136),    // Teal
                    new SKColor(121, 85, 72)     // Brown
                };

                // Tạo dữ liệu thương hiệu
                _brandData = brandPreferences
                    .Select((p, i) => new BrandInfo(
                        p.BrandName,
                        p.TotalRevenue,
                        p.TotalOrders,
                        new SolidColorPaint(brandColors[i % brandColors.Length])
                    ))
                    .ToArray();

                var rowSeries = new RowSeries<BrandInfo>
                {
                    Values = _brandData,
                    DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                    DataLabelsSize = 14,
                    DataLabelsPosition = DataLabelsPosition.End,
                    DataLabelsFormatter = point => 
                        $"{point.Model!.Name} ({point.Model!.Orders} đơn)",
                    MaxBarWidth = 50,
                    Padding = 5,
                    YToolTipLabelFormatter = point => point.Model!.Name,
                    XToolTipLabelFormatter = point => $"{point.Model!.Orders} đơn"
                };

                rowSeries.PointMeasured += (point) =>
                {
                    if (point.Visual is null) return;
                    point.Visual.Fill = point.Model!.Paint;
                };

                _brandXAxes[0].MinLimit = 0;
                _brandXAxes[0].MaxLimit = maxOrders;
                _brandXAxes[0].Labeler = value => ((int)value).ToString();
                _brandXAxes[0].TextSize = 12;
                _brandXAxes[0].MinStep = 1;
                _brandXAxes[0].ForceStepToMin = true;
                _brandXAxes[0].SeparatorsPaint = new SolidColorPaint(new SKColor(220, 220, 220));

                _brandYAxes[0].ShowSeparatorLines = false;
                _brandYAxes[0].IsVisible = false;

                CustomerPreferenceSeries = new[] { rowSeries };
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