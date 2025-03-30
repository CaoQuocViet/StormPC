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
            new LineSeries<ObservableValue>
            {
                Name = "Tổng tồn kho",
                Values = trends.Select(t => new ObservableValue(t.TotalStock)).ToArray(),
                Fill = new SolidColorPaint(SKColors.Blue.WithAlpha(90)),
                Stroke = new SolidColorPaint(SKColors.Blue, 2)
            },
            new LineSeries<ObservableValue>
            {
                Name = "Sản phẩm bán ra",
                Values = trends.Select(t => new ObservableValue(t.SoldProducts)).ToArray(),
                Fill = null,
                Stroke = new SolidColorPaint(SKColors.Red, 2)
            }
        };

        StockTrendXAxes = new[]
        {
            new Axis
            {
                Name = "Thời gian",
                NamePaint = new SolidColorPaint(SKColors.Gray),
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 12,
                Labels = trends.Select(t => t.Date.ToString("dd/MM")).ToArray(),
                LabelsRotation = 45
            }
        };

        StockTrendYAxes = new[]
        {
            new Axis
            {
                Name = "Số lượng",
                NamePaint = new SolidColorPaint(SKColors.Gray),
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 12
            }
        };
    }

    private void UpdateCategoryDistributionChart(IEnumerable<CategoryAnalysis> categories)
    {
        CategoryDistributionSeries = new ISeries[]
        {
            new ColumnSeries<decimal>
            {
                Name = "Giá trị tồn kho",
                Values = categories.Select(c => c.TotalValue).ToArray(),
                Fill = new SolidColorPaint(SKColors.Blue.WithAlpha(90))
            }
        };

        CategoryXAxes = new[]
        {
            new Axis
            {
                Name = "Danh mục",
                NamePaint = new SolidColorPaint(SKColors.Gray),
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 12,
                LabelsRotation = 45
            }
        };

        CategoryYAxes = new[]
        {
            new Axis
            {
                Name = "Giá trị",
                NamePaint = new SolidColorPaint(SKColors.Gray),
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 12
            }
        };
    }

    private void UpdateBrandDistributionChart(IEnumerable<BrandAnalysis> brands)
    {
        BrandDistributionSeries = new ISeries[]
        {
            new ColumnSeries<decimal>
            {
                Name = "Giá trị tồn kho",
                Values = brands.Select(b => b.TotalValue).ToArray(),
                Fill = new SolidColorPaint(SKColors.Green.WithAlpha(90))
            }
        };

        BrandXAxes = new[]
        {
            new Axis
            {
                Name = "Thương hiệu",
                NamePaint = new SolidColorPaint(SKColors.Gray),
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 12,
                LabelsRotation = 45
            }
        };

        BrandYAxes = new[]
        {
            new Axis
            {
                Name = "Giá trị",
                NamePaint = new SolidColorPaint(SKColors.Gray),
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 12
            }
        };
    }

    private void UpdateStockAgingChart(IEnumerable<AgedInventory> aging)
    {
        var ageGroups = aging
            .GroupBy(a => a.DaysInStock switch
            {
                <= 30 => "0-30 ngày",
                <= 60 => "31-60 ngày",
                <= 90 => "61-90 ngày",
                _ => "Trên 90 ngày"
            })
            .OrderBy(g => g.Key)
            .ToList();

        StockAgingSeries = new ISeries[]
        {
            new ColumnSeries<decimal>
            {
                Name = "Giá trị tồn kho",
                Values = ageGroups.Select(g => g.Sum(a => a.StockValue)).ToArray(),
                Fill = new SolidColorPaint(SKColors.Orange.WithAlpha(90))
            }
        };

        AgingXAxes = new[]
        {
            new Axis
            {
                Name = "Thời gian tồn kho",
                NamePaint = new SolidColorPaint(SKColors.Gray),
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 12,
                LabelsRotation = 45
            }
        };

        AgingYAxes = new[]
        {
            new Axis
            {
                Name = "Giá trị",
                NamePaint = new SolidColorPaint(SKColors.Gray),
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                TextSize = 12
            }
        };
    }
} 