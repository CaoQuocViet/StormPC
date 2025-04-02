using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Kernel.Sketches;
using SkiaSharp;
using StormPC.Core.Models.Products;
using StormPC.Core.Services.Dashboard;
using System.Collections.ObjectModel;
using StormPC.Helpers;
using System.Diagnostics;

namespace StormPC.ViewModels.Dashboard;

public partial class InventoryReportViewModel : ObservableObject
{
    private readonly IInventoryReportService _inventoryReportService;

    [ObservableProperty]
    private DateTimeOffset _startDate = DateTimeOffset.Now.AddMonths(-3);

    [ObservableProperty]
    private DateTimeOffset _endDate = DateTimeOffset.Now;

    [ObservableProperty]
    private ObservableCollection<Category>? _categories;

    [ObservableProperty]
    private ObservableCollection<Brand>? _brands;

    [ObservableProperty]
    private Category? _selectedCategory;

    [ObservableProperty]
    private Brand? _selectedBrand;

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
    private ObservableCollection<LowStockItem>? _lowStockItems;

    [ObservableProperty]
    private ISeries[]? _stockTrendSeries;

    [ObservableProperty]
    private ISeries[]? _categoryDistributionSeries;

    [ObservableProperty]
    private ISeries[]? _brandDistributionSeries;

    [ObservableProperty]
    private ISeries[]? _stockAgingSeries;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis>? _stockTrendXAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis>? _stockTrendYAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis>? _categoryXAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis>? _categoryYAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis>? _brandXAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis>? _brandYAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis>? _agingXAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis>? _agingYAxes;

    [ObservableProperty]
    private ObservableCollection<CategoryAnalysis>? _categoryAnalytics;

    [ObservableProperty]
    private ObservableCollection<BrandAnalysis>? _brandAnalytics;

    [ObservableProperty]
    private ObservableCollection<AgedInventory> _agedInventories = new();

    [ObservableProperty]
    private ObservableCollection<RestockSuggestion> _restockSuggestions = new();

    [ObservableProperty]
    private ISeries[]? _stockDistributionSeries;

    [ObservableProperty]
    private ISeries[]? _stockValueSeries;

    [ObservableProperty]
    private ISeries[]? _stockHeatSeries;

    [ObservableProperty]
    private List<IPolarAxis>? _angleAxes;

    [ObservableProperty]
    private List<IPolarAxis>? _radiusAxes;

    [ObservableProperty]
    private List<ICartesianAxis>? _valueXAxes;

    [ObservableProperty]
    private List<ICartesianAxis>? _valueYAxes;

    [ObservableProperty]
    private List<ICartesianAxis>? _heatXAxes;

    [ObservableProperty]
    private List<ICartesianAxis>? _heatYAxes;

    // Phân trang cho bảng thời gian tồn kho
    private int _agedInventoriesCurrentPage = 1;
    private readonly int _agedInventoriesItemsPerPage = 15;
    private int _restockSuggestionsCurrentPage = 1;
    private readonly int _restockSuggestionsItemsPerPage = 15;

    [ObservableProperty]
    private int _todayOrderCount;

    [ObservableProperty]
    private decimal _todayRevenue;

    [ObservableProperty]
    private ObservableCollection<TopSellingProduct>? _topSellingProducts;

    [ObservableProperty]
    private ISeries[]? _topSellersSeries;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis>? _topSellersXAxes;

    [ObservableProperty]
    private IEnumerable<ICartesianAxis>? _topSellersYAxes;

    public int AgedInventoriesCurrentPage
    {
        get => _agedInventoriesCurrentPage;
        set
        {
            SetProperty(ref _agedInventoriesCurrentPage, value);
            OnPropertyChanged(nameof(AgedInventoriesPagedItems));
            OnPropertyChanged(nameof(CanGoToPreviousAgedInventoriesPage));
            OnPropertyChanged(nameof(CanGoToNextAgedInventoriesPage));
            (PreviousAgedInventoriesPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
            (NextAgedInventoriesPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
        }
    }

    public int AgedInventoriesTotalPages => AgedInventories?.Count > 0 
        ? (int)Math.Ceiling(AgedInventories.Count / (double)_agedInventoriesItemsPerPage) 
        : 1;

    public IEnumerable<AgedInventory> AgedInventoriesPagedItems =>
        AgedInventories?.Count > 0
            ? AgedInventories
                .Skip((AgedInventoriesCurrentPage - 1) * _agedInventoriesItemsPerPage)
                .Take(_agedInventoriesItemsPerPage)
            : Enumerable.Empty<AgedInventory>();

    public int RestockSuggestionsCurrentPage
    {
        get => _restockSuggestionsCurrentPage;
        set
        {
            SetProperty(ref _restockSuggestionsCurrentPage, value);
            OnPropertyChanged(nameof(RestockSuggestionsPagedItems));
            OnPropertyChanged(nameof(CanGoToPreviousRestockSuggestionsPage));
            OnPropertyChanged(nameof(CanGoToNextRestockSuggestionsPage));
            (PreviousRestockSuggestionsPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
            (NextRestockSuggestionsPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
        }
    }

    public int RestockSuggestionsTotalPages => RestockSuggestions?.Count > 0
        ? (int)Math.Ceiling(RestockSuggestions.Count / (double)_restockSuggestionsItemsPerPage)
        : 1;

    public IEnumerable<RestockSuggestion> RestockSuggestionsPagedItems =>
        RestockSuggestions?.Count > 0
            ? RestockSuggestions
                .Skip((RestockSuggestionsCurrentPage - 1) * _restockSuggestionsItemsPerPage)
                .Take(_restockSuggestionsItemsPerPage)
            : Enumerable.Empty<RestockSuggestion>();

    public bool CanGoToPreviousAgedInventoriesPage => AgedInventoriesCurrentPage > 1;
    public bool CanGoToNextAgedInventoriesPage => AgedInventoriesCurrentPage < AgedInventoriesTotalPages;
    public bool CanGoToPreviousRestockSuggestionsPage => RestockSuggestionsCurrentPage > 1;
    public bool CanGoToNextRestockSuggestionsPage => RestockSuggestionsCurrentPage < RestockSuggestionsTotalPages;

    public IRelayCommand PreviousAgedInventoriesPageCommand { get; }
    public IRelayCommand NextAgedInventoriesPageCommand { get; }
    public IRelayCommand PreviousRestockSuggestionsPageCommand { get; }
    public IRelayCommand NextRestockSuggestionsPageCommand { get; }

    public InventoryReportViewModel(IInventoryReportService inventoryReportService)
    {
        _inventoryReportService = inventoryReportService;
        
        // Initialize commands
        PreviousAgedInventoriesPageCommand = new RelayCommand(
            () => {
                AgedInventoriesCurrentPage--;
                (PreviousAgedInventoriesPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
                (NextAgedInventoriesPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
            },
            () => CanGoToPreviousAgedInventoriesPage);
            
        NextAgedInventoriesPageCommand = new RelayCommand(
            () => {
                AgedInventoriesCurrentPage++;
                (PreviousAgedInventoriesPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
                (NextAgedInventoriesPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
            },
            () => CanGoToNextAgedInventoriesPage);
            
        PreviousRestockSuggestionsPageCommand = new RelayCommand(
            () => {
                RestockSuggestionsCurrentPage--;
                (PreviousRestockSuggestionsPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
                (NextRestockSuggestionsPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
            },
            () => CanGoToPreviousRestockSuggestionsPage);
            
        NextRestockSuggestionsPageCommand = new RelayCommand(
            () => {
                RestockSuggestionsCurrentPage++;
                (PreviousRestockSuggestionsPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
                (NextRestockSuggestionsPageCommand as RelayCommand)?.NotifyCanExecuteChanged();
            },
            () => CanGoToNextRestockSuggestionsPage);

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
        try
        {
            // Convert local time to UTC for PostgreSQL
            var startUtc = StartDate.UtcDateTime;
            var endUtc = EndDate.UtcDateTime;
            var today = DateTime.UtcNow.Date;
            
            var data = await _inventoryReportService.GetInventoryData(startUtc, endUtc);
            
            // Update KPIs
            TotalProducts = data.TotalProducts;
            TotalStock = data.TotalStock;
            TotalValue = data.TotalValue;
            AverageStockValue = data.AverageStockValue;
            StockTurnoverRate = data.StockTurnoverRate;
            LowStockProducts = data.LowStockProducts;

            // Get today's summary
            var dailySummary = await _inventoryReportService.GetDailySummary(today);
            TodayOrderCount = dailySummary.OrderCount;
            TodayRevenue = dailySummary.TotalRevenue;

            // Get top selling products
            var topSellers = await _inventoryReportService.GetTopSellingProducts(startUtc, endUtc);
            TopSellingProducts = new ObservableCollection<TopSellingProduct>(topSellers);
            UpdateTopSellersChart(topSellers);

            // Update with pagination
            AgedInventories = new ObservableCollection<AgedInventory>(data.AgedInventories ?? Enumerable.Empty<AgedInventory>());
            RestockSuggestions = new ObservableCollection<RestockSuggestion>(data.RestockSuggestions ?? Enumerable.Empty<RestockSuggestion>());

            // Reset pagination
            AgedInventoriesCurrentPage = 1;
            RestockSuggestionsCurrentPage = 1;

            // Update UI
            OnPropertyChanged(nameof(AgedInventoriesPagedItems));
            OnPropertyChanged(nameof(RestockSuggestionsPagedItems));
            OnPropertyChanged(nameof(AgedInventoriesTotalPages));
            OnPropertyChanged(nameof(RestockSuggestionsTotalPages));
            OnPropertyChanged(nameof(CanGoToPreviousAgedInventoriesPage));
            OnPropertyChanged(nameof(CanGoToNextAgedInventoriesPage));
            OnPropertyChanged(nameof(CanGoToPreviousRestockSuggestionsPage));
            OnPropertyChanged(nameof(CanGoToNextRestockSuggestionsPage));

            // Update Charts
            UpdateStockTrendChart(data.StockTrends ?? Enumerable.Empty<StockTrend>());
            UpdateCategoryDistributionChart(data.CategoryAnalytics ?? Enumerable.Empty<CategoryAnalysis>());
            UpdateBrandDistributionChart(data.BrandAnalytics ?? Enumerable.Empty<BrandAnalysis>());
            UpdateStockAgingChart(data.AgedInventories ?? Enumerable.Empty<AgedInventory>());
        }
        catch (Exception ex)
        {
            // Initialize empty collections in case of error
            AgedInventories = new ObservableCollection<AgedInventory>();
            RestockSuggestions = new ObservableCollection<RestockSuggestion>();
            
            // Reset pagination
            AgedInventoriesCurrentPage = 1;
            RestockSuggestionsCurrentPage = 1;
            
            // Update UI
            OnPropertyChanged(nameof(AgedInventoriesPagedItems));
            OnPropertyChanged(nameof(RestockSuggestionsPagedItems));
            OnPropertyChanged(nameof(AgedInventoriesTotalPages));
            OnPropertyChanged(nameof(RestockSuggestionsTotalPages));
            OnPropertyChanged(nameof(CanGoToPreviousAgedInventoriesPage));
            OnPropertyChanged(nameof(CanGoToNextAgedInventoriesPage));
            OnPropertyChanged(nameof(CanGoToPreviousRestockSuggestionsPage));
            OnPropertyChanged(nameof(CanGoToNextRestockSuggestionsPage));
            
            // Log the error
            Debug.WriteLine($"Error loading inventory data: {ex.Message}");
        }
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

    private void UpdateTopSellersChart(IEnumerable<TopSellingProduct> products)
    {
        TopSellersSeries = new ISeries[]
        {
            new StackedColumnSeries<double>
            {
                Name = "Số lượng bán",
                Values = products.Select(p => (double)p.QuantitySold).ToArray(),
                Fill = new SolidColorPaint(SKColors.DodgerBlue.WithAlpha(200)),
                Stroke = null,
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsSize = 12,
                DataLabelsFormatter = point => NumberFormatConverter.Format((double)point.Model)
            },
            new StackedColumnSeries<double>
            {
                Name = "Doanh thu",
                Values = products.Select(p => (double)p.Revenue).ToArray(),
                Fill = new SolidColorPaint(SKColors.ForestGreen.WithAlpha(200)),
                Stroke = null,
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsSize = 12,
                DataLabelsFormatter = point => CurrencyConverter.Format((double)point.Model)
            }
        };

        TopSellersXAxes = new[]
        {
            new Axis
            {
                Name = "Sản phẩm",
                Labels = products.Select(p => p.ModelName).ToArray(),
                LabelsRotation = 45,
                TextSize = 10,
                LabelsPaint = new SolidColorPaint(SKColors.Gray)
            }
        };

        TopSellersYAxes = new[]
        {
            new Axis
            {
                Name = "Số lượng / Doanh thu (VNĐ)",
                TextSize = 10,
                LabelsPaint = new SolidColorPaint(SKColors.Gray),
                Labeler = value => value >= 1000000 ? CurrencyConverter.Format(value) : NumberFormatConverter.Format(value)
            }
        };
    }
} 