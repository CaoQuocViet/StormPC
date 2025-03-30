using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel.Sketches;
using SkiaSharp;
using StormPC.Core.Models.Products;
using StormPC.Core.Services.Dashboard;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using StormPC.Helpers;

namespace StormPC.ViewModels.Dashboard;

public partial class InventoryReportViewModel : ObservableObject
{
    private readonly IInventoryReportService _inventoryReportService;

    [ObservableProperty]
    private DateTimeOffset _startDate = DateTimeOffset.Now.AddMonths(-3);

    [ObservableProperty]
    private DateTimeOffset _endDate = DateTimeOffset.Now;

    [ObservableProperty]
    private ObservableCollection<Category> _categories;

    [ObservableProperty]
    private ObservableCollection<Brand> _brands;

    [ObservableProperty]
    private Category _selectedCategory;

    [ObservableProperty]
    private Brand _selectedBrand;

    [ObservableProperty]
    private int _totalProducts;

    [ObservableProperty]
    private int _totalStock;

    [ObservableProperty]
    private decimal _totalValue;

    [ObservableProperty]
    private decimal _averageStockValue;

    [ObservableProperty]
    private decimal _stockTurnoverRate;

    [ObservableProperty]
    private int _lowStockProducts;

    [ObservableProperty]
    private ObservableCollection<LowStockItem> _lowStockItems;

    [ObservableProperty]
    private ISeries[] _stockTrendSeries;

    [ObservableProperty]
    private ISeries[] _categoryDistributionSeries;

    [ObservableProperty]
    private ISeries[] _brandDistributionSeries;

    [ObservableProperty]
    private ISeries[] _stockAgingSeries;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis> _stockTrendXAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis> _stockTrendYAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis> _categoryXAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis> _categoryYAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis> _brandXAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis> _brandYAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis> _agingXAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis> _agingYAxes;

    [ObservableProperty]
    private ObservableCollection<CategoryAnalysis> _categoryAnalytics;

    [ObservableProperty]
    private ObservableCollection<BrandAnalysis> _brandAnalytics;

    [ObservableProperty]
    private ObservableCollection<AgedInventory> _agedInventories;

    [ObservableProperty]
    private ObservableCollection<RestockSuggestion> _restockSuggestions;

    [ObservableProperty]
    private ISeries[] _stockDistributionSeries;

    [ObservableProperty]
    private ISeries[] _stockValueSeries;

    [ObservableProperty]
    private ISeries[] _stockHeatSeries;

    [ObservableProperty]
    private List<IPolarAxis> _angleAxes;

    [ObservableProperty]
    private List<IPolarAxis> _radiusAxes;

    [ObservableProperty]
    private List<ICartesianAxis> _valueXAxes;

    [ObservableProperty]
    private List<ICartesianAxis> _valueYAxes;

    [ObservableProperty]
    private List<ICartesianAxis> _heatXAxes;

    [ObservableProperty]
    private List<ICartesianAxis> _heatYAxes;

    public InventoryReportViewModel(IInventoryReportService inventoryReportService)
    {
        _inventoryReportService = inventoryReportService;
        Initialize();
    }

    private async void Initialize()
    {
        Categories = new ObservableCollection<Category>(await _inventoryReportService.GetCategories());
        Brands = new ObservableCollection<Brand>(await _inventoryReportService.GetBrands());
        await LoadData();
    }

    [RelayCommand]
    private async Task Refresh()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        // Convert local time to UTC for PostgreSQL
        var startUtc = StartDate.UtcDateTime;
        var endUtc = EndDate.UtcDateTime;
        
        var data = await _inventoryReportService.GetInventoryData(startUtc, endUtc);

        // Update KPIs
        TotalProducts = data.TotalProducts;
        TotalStock = data.TotalStock;
        TotalValue = data.TotalValue;
        AverageStockValue = data.AverageStockValue;
        StockTurnoverRate = data.StockTurnoverRate;
        LowStockProducts = data.LowStockProducts;

        // Update Data Tables
        CategoryAnalytics = new ObservableCollection<CategoryAnalysis>(data.CategoryAnalytics);
        BrandAnalytics = new ObservableCollection<BrandAnalysis>(data.BrandAnalytics);
        AgedInventories = new ObservableCollection<AgedInventory>(data.AgedInventories);
        LowStockItems = new ObservableCollection<LowStockItem>(data.LowStockItems);
        RestockSuggestions = new ObservableCollection<RestockSuggestion>(data.RestockSuggestions);

        // Update Charts
        UpdateStockTrendChart(data.StockTrends);
        UpdateCategoryDistributionChart(data.CategoryAnalytics);
        UpdateBrandDistributionChart(data.BrandAnalytics);
        UpdateStockAgingChart(data.AgedInventories);
    }

    private void UpdateStockTrendChart(IEnumerable<StockTrend> trends)
    {
        StockTrendSeries = new ISeries[]
        {
            new LineSeries<double>
            {
                Name = "Tồn kho",
                Values = trends.Select(t => (double)t.TotalStock).ToArray(),
                Fill = null,
                GeometryFill = new SolidColorPaint(SKColors.DodgerBlue),
                GeometryStroke = new SolidColorPaint(SKColors.DodgerBlue) { StrokeThickness = 2 },
                Stroke = new SolidColorPaint(SKColors.DodgerBlue) { StrokeThickness = 2 },
                DataLabelsPaint = new SolidColorPaint(SKColors.DodgerBlue),
                DataLabelsSize = 12,
                DataLabelsFormatter = point => NumberFormatConverter.Format((double)point.Model)
            },
            new LineSeries<double>
            {
                Name = "Đã bán",
                Values = trends.Select(t => (double)t.SoldProducts).ToArray(),
                Fill = null,
                GeometryFill = new SolidColorPaint(SKColors.OrangeRed),
                GeometryStroke = new SolidColorPaint(SKColors.OrangeRed) { StrokeThickness = 2 },
                Stroke = new SolidColorPaint(SKColors.OrangeRed) { StrokeThickness = 2 },
                DataLabelsPaint = new SolidColorPaint(SKColors.OrangeRed),
                DataLabelsSize = 12,
                DataLabelsFormatter = point => NumberFormatConverter.Format((double)point.Model)
            }
        };

        StockTrendXAxes = new[]
        {
            new Axis
            {
                Name = "Thời gian",
                Labels = trends.Select(t => t.Date.ToString("dd/MM")).ToArray(),
                LabelsRotation = 45,
                TextSize = 10,
                LabelsPaint = new SolidColorPaint(SKColors.Gray)
            }
        };

        StockTrendYAxes = new[]
        {
            new Axis
            {
                Name = "Số lượng",
                TextSize = 10,
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                Labeler = value => NumberFormatConverter.Format(value)
            }
        };
    }

    private void UpdateCategoryDistributionChart(IEnumerable<CategoryAnalysis> categories)
    {
        CategoryDistributionSeries = new ISeries[]
        {
            new ColumnSeries<double>
            {
                Name = "Giá trị tồn kho",
                Values = categories.Select(c => (double)c.TotalValue).ToArray(),
                Fill = new SolidColorPaint(SKColors.DodgerBlue.WithAlpha(200)),
                Stroke = null,
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsSize = 12,
                DataLabelsFormatter = point => CurrencyConverter.Format((double)point.Model)
            }
        };

        CategoryXAxes = new[]
        {
            new Axis
            {
                Name = "Danh mục",
                Labels = categories.Select(c => c.CategoryName).ToArray(),
                LabelsRotation = 45,
                TextSize = 10,
                LabelsPaint = new SolidColorPaint(SKColors.Gray)
            }
        };

        CategoryYAxes = new[]
        {
            new Axis
            {
                Name = "Giá trị (VNĐ)",
                TextSize = 10,
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                Labeler = value => CurrencyConverter.Format(value)
            }
        };
    }

    private void UpdateBrandDistributionChart(IEnumerable<BrandAnalysis> brands)
    {
        BrandDistributionSeries = new ISeries[]
        {
            new ColumnSeries<double>
            {
                Name = "Giá trị tồn kho",
                Values = brands.Select(b => (double)b.TotalValue).ToArray(),
                Fill = new SolidColorPaint(SKColors.ForestGreen.WithAlpha(200)),
                Stroke = null,
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsSize = 12,
                DataLabelsFormatter = point => CurrencyConverter.Format((double)point.Model)
            }
        };

        BrandXAxes = new[]
        {
            new Axis
            {
                Name = "Thương hiệu",
                Labels = brands.Select(b => b.BrandName).ToArray(),
                LabelsRotation = 45,
                TextSize = 10,
                LabelsPaint = new SolidColorPaint(SKColors.Gray)
            }
        };

        BrandYAxes = new[]
        {
            new Axis
            {
                Name = "Giá trị (VNĐ)",
                TextSize = 10,
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                Labeler = value => CurrencyConverter.Format(value)
            }
        };
    }

    private void UpdateStockAgingChart(IEnumerable<AgedInventory> agedInventories)
    {
        var ageGroups = agedInventories
            .GroupBy(i => GetAgeGroup(i.DaysInStock))
            .OrderBy(g => g.Key)
            .ToList();

        StockAgingSeries = new ISeries[]
        {
            new ColumnSeries<double>
            {
                Name = "Giá trị tồn kho",
                Values = ageGroups.Select(g => (double)g.Sum(i => i.StockValue)).ToArray(),
                Fill = new SolidColorPaint(SKColors.Orange.WithAlpha(200)),
                Stroke = null,
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsSize = 12,
                DataLabelsFormatter = point => CurrencyConverter.Format((double)point.Model)
            }
        };

        AgingXAxes = new[]
        {
            new Axis
            {
                Name = "Thời gian tồn kho",
                Labels = ageGroups.Select(g => g.Key).ToArray(),
                TextSize = 10,
                LabelsPaint = new SolidColorPaint(SKColors.Gray)
            }
        };

        AgingYAxes = new[]
        {
            new Axis
            {
                Name = "Giá trị (VNĐ)",
                TextSize = 10,
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                Labeler = value => CurrencyConverter.Format(value)
            }
        };
    }

    private string GetAgeGroup(int days)
    {
        if (days <= 30) return "≤ 30 ngày";
        if (days <= 60) return "31-60 ngày";
        if (days <= 90) return "61-90 ngày";
        if (days <= 180) return "91-180 ngày";
        return "> 180 ngày";
    }
} 