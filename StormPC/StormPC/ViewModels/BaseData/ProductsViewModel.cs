using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using StormPC.Core.Models.Products.Dtos;
using StormPC.Core.Services.Products;
using StormPC.Core.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace StormPC.ViewModels.BaseData;

public partial class ProductsViewModel : ObservableObject, IPaginatedViewModel
{
    private readonly IProductService _productService;
    private List<LaptopDisplayDto> _allLaptops;
    private ObservableCollection<LaptopDisplayDto> _laptops;
    private bool _isLoading;
    private string _searchText;
    private int _currentPage = 1;
    private int _pageSize = 10; 
    private int _totalItems;
    private int _selectedSortIndex;

    public int SelectedSortIndex
    {
        get => _selectedSortIndex;
        set
        {
            if (SetProperty(ref _selectedSortIndex, value))
            {
                FilterAndPaginateProducts();
            }
        }
    }

    public ObservableCollection<LaptopDisplayDto> Laptops
    {
        get => _laptops;
        set => SetProperty(ref _laptops, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    [ObservableProperty]
    private string searchText = string.Empty;

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
                FilterAndPaginateProducts();
            }
        }
    }

    public int TotalPages => (_totalItems + PageSize - 1) / PageSize;

    public ProductsViewModel(IProductService productService)
    {
        _productService = productService;
        _laptops = new ObservableCollection<LaptopDisplayDto>();
        _allLaptops = new List<LaptopDisplayDto>();
    }

    partial void OnSearchTextChanged(string value)
    {
        FilterAndPaginateProducts();
    }

    public async Task LoadProductsAsync()
    {
        try
        {
            IsLoading = true;
            _allLaptops = await _productService.GetLaptopsAsync();
            FilterAndPaginateProducts();
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void FilterAndPaginateProducts()
    {
        var filteredProducts = string.IsNullOrWhiteSpace(SearchText)
            ? _allLaptops
            : _allLaptops.Where(l =>
                l.ModelName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                l.BrandName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                l.CPU.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase)
            ).ToList();

        // Apply sorting based on selected index
        filteredProducts = SortProducts(filteredProducts);

        _totalItems = filteredProducts.Count;
        CurrentPage = 1;
        LoadPage(1);
    }

    private List<LaptopDisplayDto> SortProducts(List<LaptopDisplayDto> products)
    {
        return SelectedSortIndex switch
        {
            0 => products.OrderByDescending(p => p.ReleaseYear).ToList(), // Mới nhất
            1 => products.OrderBy(p => p.LowestPrice).ToList(), // Giá thấp đến cao
            2 => products.OrderByDescending(p => p.LowestPrice).ToList(), // Giá cao đến thấp
            3 => products.OrderByDescending(p => p.Discount).ToList(), // Giảm giá nhiều nhất
            4 => products.OrderBy(p => p.ModelName).ToList(), // Tên A-Z
            5 => products.OrderByDescending(p => p.ModelName).ToList(), // Tên Z-A
            _ => products
        };
    }

    public void LoadPage(int page)
    {
        if (_allLaptops == null) return;

        var filteredProducts = string.IsNullOrWhiteSpace(SearchText)
            ? _allLaptops
            : _allLaptops.Where(l =>
                l.ModelName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                l.BrandName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                l.CPU.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase)
            ).ToList();

        // Apply sorting
        filteredProducts = SortProducts(filteredProducts);

        _totalItems = filteredProducts.Count;

        var pagedProducts = filteredProducts
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        Laptops = new ObservableCollection<LaptopDisplayDto>(pagedProducts);
        OnPropertyChanged(nameof(TotalPages));
    }
} 