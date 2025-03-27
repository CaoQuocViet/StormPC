using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using StormPC.Core.Models.Products;
using StormPC.Core.Models.Products.Dtos;
using StormPC.Core.Infrastructure.Database.Contexts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.ComponentModel;

namespace StormPC.ViewModels.BaseData;

public partial class CategoriesViewModel : ObservableObject, IPaginatedViewModel
{
    private readonly StormPCDbContext _dbContext;
    private List<CategoryDisplayDto> _allCategories;
    private ObservableCollection<CategoryDisplayDto> _categories;
    private bool _isLoading;
    [ObservableProperty]
    private string _searchText = string.Empty;
    private int _currentPage = 1;
    private int _pageSize = 10;
    private int _totalItems;

    // Sorting properties
    private List<string> _sortProperties = new();
    private List<ListSortDirection> _sortDirections = new();

    public ObservableCollection<CategoryDisplayDto> Categories
    {
        get => _categories;
        set => SetProperty(ref _categories, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
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
                FilterAndPaginateCategories();
            }
        }
    }

    public int TotalPages => (_totalItems + PageSize - 1) / PageSize;

    public CategoriesViewModel(StormPCDbContext dbContext)
    {
        _dbContext = dbContext;
        _categories = new ObservableCollection<CategoryDisplayDto>();
        _allCategories = new List<CategoryDisplayDto>();
    }

    public async Task InitializeAsync()
    {
        await LoadCategories();
    }

    [RelayCommand]
    private async Task LoadCategories()
    {
        if (IsLoading) return; // Prevent multiple simultaneous loads

        try
        {
            IsLoading = true;
            Debug.WriteLine("Loading categories...");

            // Get categories with product counts in a single query
            var categories = await _dbContext.Categories
                .Where(c => !c.IsDeleted)
                .Select(c => new
                {
                    c.CategoryID,
                    c.CategoryName,
                    c.Description,
                    ProductCount = _dbContext.Laptops.Count(l => l.CategoryID == c.CategoryID && !l.IsDeleted),
                    c.CreatedAt,
                    UpdatedAt = c.UpdatedAt ?? c.CreatedAt
                })
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            Debug.WriteLine($"Total categories found: {categories.Count}");

            var categoryDtos = categories.Select(c => new CategoryDisplayDto
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                Description = c.Description,
                ProductCount = c.ProductCount,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            _allCategories = categoryDtos;
            FilterAndPaginateCategories();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading categories: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            // Consider showing error message to user
        }
        finally
        {
            IsLoading = false;
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        FilterAndPaginateCategories();
    }

    public async Task LoadCategoriesAsync()
    {
        try
        {
            IsLoading = true;
            var categories = await _dbContext.Categories
                .Where(c => !c.IsDeleted)
                .Select(c => new CategoryDisplayDto
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    Description = c.Description,
                    ProductCount = _dbContext.Laptops.Count(l => l.CategoryID == c.CategoryID && !l.IsDeleted),
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt ?? c.CreatedAt
                })
                .OrderBy(c => c.CategoryName)
                .ToListAsync();
            
            _allCategories = categories;
            FilterAndPaginateCategories();
        }
        finally
        {
            IsLoading = false;
        }
    }

    public void UpdateSorting(List<string> properties, List<ListSortDirection> directions)
    {
        _sortProperties = properties;
        _sortDirections = directions;
        FilterAndPaginateCategories();
    }

    private void FilterAndPaginateCategories()
    {
        var filteredCategories = string.IsNullOrWhiteSpace(SearchText)
            ? _allCategories
            : _allCategories.Where(c =>
                c.CategoryName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                (c.Description?.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ?? false)
            ).ToList();

        // Apply sorting if any sort properties are defined
        if (_sortProperties.Any())
        {
            filteredCategories = Core.Helpers.DataGridSortHelper.ApplySort(
                filteredCategories,
                _sortProperties,
                _sortDirections
            ).ToList();
        }

        _totalItems = filteredCategories.Count;
        CurrentPage = 1;
        LoadPage(1);
    }

    public void LoadPage(int page)
    {
        if (_allCategories == null) return;

        var filteredCategories = string.IsNullOrWhiteSpace(SearchText)
            ? _allCategories
            : _allCategories.Where(c =>
                c.CategoryName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                (c.Description?.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ?? false)
            ).ToList();

        // Apply sorting if any sort properties are defined
        if (_sortProperties.Any())
        {
            filteredCategories = Core.Helpers.DataGridSortHelper.ApplySort(
                filteredCategories,
                _sortProperties,
                _sortDirections
            ).ToList();
        }

        _totalItems = filteredCategories.Count;

        var pagedCategories = filteredCategories
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        Categories = new ObservableCollection<CategoryDisplayDto>(pagedCategories);
        OnPropertyChanged(nameof(TotalPages));
    }
} 