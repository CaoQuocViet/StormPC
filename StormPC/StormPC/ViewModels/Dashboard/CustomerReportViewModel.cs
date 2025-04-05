using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using StormPC.Core.Services.Dashboard;
using StormPC.Core.Models.Customers.Dtos;
using StormPC.Core.Models.Customers;
using StormPC.Core.Infrastructure.Database.Contexts;
using System.Collections.ObjectModel;
using LiveChartsCore.Drawing;
using LiveChartsCore.Measure;
using LiveChartsCore.Defaults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

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

public partial class CustomerReportViewModel : ObservableObject, IPaginatedViewModel
{
    private readonly ICustomerReportService _customerReportService;
    private readonly StormPCDbContext _dbContext;
    private BrandInfo[] _brandData = Array.Empty<BrandInfo>();
    private List<CustomerDisplayDto> _allCustomers;
    private ObservableCollection<CustomerDisplayDto> _customers;
    private int _currentPage = 1;
    private int _pageSize = 10;
    private int _totalItems;
    private List<string> _sortProperties = new();
    private List<ListSortDirection> _sortDirections = new();
    
    [ObservableProperty]
    private bool _isLoading;
    
    [ObservableProperty]
    private DateTime _startDate = DateTime.Now.AddMonths(-3);
    
    [ObservableProperty]
    private DateTime _endDate = DateTime.Now;

    [ObservableProperty]
    private string _searchText = string.Empty;

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

    public ObservableCollection<CustomerDisplayDto> Customers
    {
        get => _customers;
        set => SetProperty(ref _customers, value);
    }

    public int CurrentPage
    {
        get => _currentPage;
        set
        {
            if (SetProperty(ref _currentPage, value))
            {
                LoadPage(value);
            }
        }
    }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (SetProperty(ref _pageSize, value))
            {
                FilterAndPaginateCustomers();
            }
        }
    }

    public int TotalPages => (_totalItems + PageSize - 1) / PageSize;

    public CustomerReportViewModel(ICustomerReportService customerReportService, StormPCDbContext dbContext)
    {
        _customerReportService = customerReportService;
        _dbContext = dbContext;
        _customers = new ObservableCollection<CustomerDisplayDto>();
        _allCustomers = new List<CustomerDisplayDto>();
    }

    [RelayCommand]
    public async Task LoadDataAsync()
    {
        if (IsLoading) return;
        
        try
        {
            IsLoading = true;

            // Load customer list
            await LoadCustomers();

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
                BrandXAxes[0].MinLimit = 0;
                BrandXAxes[0].MaxLimit = maxOrders;
                BrandXAxes[0].Labeler = value => ((int)value).ToString();
                BrandXAxes[0].TextSize = 12;
                BrandXAxes[0].MinStep = 1;
                BrandXAxes[0].ForceStepToMin = true;
                BrandXAxes[0].SeparatorsPaint = new SolidColorPaint(new SKColor(220, 220, 220));

                BrandYAxes[0].ShowSeparatorLines = false;
                BrandYAxes[0].IsVisible = false;

                CustomerPreferenceSeries = new[] { rowSeries };
            }
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task LoadCustomers()
    {
        try
        {
            var query = await _dbContext.Customers
                .AsNoTracking()
                .Include(c => c.City)
                .Where(c => !c.IsDeleted)
                .Select(c => new CustomerDisplayDto
                {
                    CustomerID = c.CustomerID,
                    FullName = c.FullName,
                    Email = c.Email,
                    Phone = c.Phone,
                    Address = c.Address,
                    CityName = c.City.CityName
                })
                .ToListAsync();

            _allCustomers = query;
            FilterAndPaginateCustomers();
        }
        catch (Exception ex)
        {
            // Handle error
            System.Diagnostics.Debug.WriteLine($"Error loading customers: {ex.Message}");
        }
    }

    public void UpdateSorting(List<string> properties, List<ListSortDirection> directions)
    {
        _sortProperties = properties;
        _sortDirections = directions;
        FilterAndPaginateCustomers();
    }

    private void FilterAndPaginateCustomers()
    {
        var filteredCustomers = string.IsNullOrWhiteSpace(SearchText)
            ? _allCustomers
            : _allCustomers.Where(c =>
                c.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                c.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                c.Phone.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                c.CityName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
            ).ToList();

        // Apply sorting
        filteredCustomers = ApplySorting(filteredCustomers);

        _totalItems = filteredCustomers.Count;
        LoadPage(1); // Reset to first page when filtering
    }

    private List<CustomerDisplayDto> ApplySorting(List<CustomerDisplayDto> customers)
    {
        if (_sortProperties.Any())
        {
            customers = Core.Helpers.DataGridSortHelper.ApplySort(
                customers,
                _sortProperties,
                _sortDirections
            ).ToList();
        }
        return customers;
    }

    public void LoadPage(int page)
    {
        if (_allCustomers == null) return;

        var filteredCustomers = string.IsNullOrWhiteSpace(SearchText)
            ? _allCustomers
            : _allCustomers.Where(c =>
                c.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                c.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                c.Phone.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                c.CityName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
            ).ToList();

        // Apply sorting
        filteredCustomers = ApplySorting(filteredCustomers);

        _totalItems = filteredCustomers.Count;

        var pagedCustomers = filteredCustomers
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        Customers = new ObservableCollection<CustomerDisplayDto>(pagedCustomers);
        OnPropertyChanged(nameof(TotalPages));
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

    partial void OnSearchTextChanged(string value)
    {
        FilterAndPaginateCustomers();
    }

    public async Task<CustomerDialogViewModel> CreateCustomerDialogViewModel(int? customerId = null)
    {
        var viewModel = new CustomerDialogViewModel();
        
        // Load cities
        viewModel.Cities = new ObservableCollection<City>(await _dbContext.Cities.ToListAsync());

        if (customerId.HasValue)
        {
            // Load existing customer data
            var customer = await _dbContext.Customers
                .Include(c => c.City)
                .FirstOrDefaultAsync(c => c.CustomerID == customerId);

            if (customer != null)
            {
                viewModel.FullName = customer.FullName;
                viewModel.Email = customer.Email;
                viewModel.Phone = customer.Phone;
                viewModel.Address = customer.Address;
                viewModel.SelectedCity = viewModel.Cities.FirstOrDefault(c => c.Id == customer.CityId);
            }
        }

        return viewModel;
    }

    public async Task<bool> AddCustomerAsync(CustomerDialogViewModel dialogViewModel)
    {
        try
        {
            var customer = new Customer
            {
                FullName = dialogViewModel.FullName,
                Email = dialogViewModel.Email,
                Phone = dialogViewModel.Phone,
                Address = dialogViewModel.Address,
                CityId = dialogViewModel.SelectedCity?.Id ?? 0
            };

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            // Refresh customer list
            await LoadCustomers();
            return true;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error adding customer: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateCustomerAsync(int customerId, CustomerDialogViewModel dialogViewModel)
    {
        try
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);
            if (customer != null)
            {
                customer.FullName = dialogViewModel.FullName;
                customer.Email = dialogViewModel.Email;
                customer.Phone = dialogViewModel.Phone;
                customer.Address = dialogViewModel.Address;
                customer.CityId = dialogViewModel.SelectedCity?.Id ?? 0;

                await _dbContext.SaveChangesAsync();

                // Refresh customer list
                await LoadCustomers();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error updating customer: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteCustomerAsync(int customerId)
    {
        try
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);
            if (customer != null)
            {
                customer.IsDeleted = true;
                await _dbContext.SaveChangesAsync();

                // Refresh customer list
                await LoadCustomers();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error deleting customer: {ex.Message}");
            return false;
        }
    }
} 