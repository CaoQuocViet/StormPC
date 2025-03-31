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
using StormPC.Core.Services.Products;

namespace StormPC.ViewModels.BaseData;

public partial class CategoriesViewModel : ObservableObject, IPaginatedViewModel
{
    private readonly StormPCDbContext _dbContext;
    private List<CategoryDisplayDto> _allCategories;
    private ObservableCollection<CategoryDisplayDto> _categories;
    private bool _isLoading;
    [ObservableProperty]
    private string searchText = string.Empty;
    private int _currentPage = 1;
    private int _pageSize = 10;
    private int _totalItems;

    // New properties for editing
    [ObservableProperty]
    private CategoryDisplayDto? editingCategory;
    
    [ObservableProperty]
    private bool isValidCategoryInput;

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
        if (IsLoading) return;

        try
        {
            IsLoading = true;
            Debug.WriteLine("Loading categories...");

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
        }
        finally
        {
            IsLoading = false;
        }
    }

    // Add new category
    public async Task<bool> AddCategoryAsync(CategoryDisplayDto newCategory)
    {
        try
        {
            var category = new Category
            {
                CategoryName = newCategory.CategoryName,
                Description = newCategory.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            await LoadCategories(); // Reload to get updated list
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding category: {ex.Message}");
            return false;
        }
    }

    // Update existing category
    public async Task<bool> UpdateCategoryAsync(CategoryDisplayDto updatedCategory)
    {
        try
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.CategoryID == updatedCategory.CategoryID);

            if (category == null) return false;

            category.CategoryName = updatedCategory.CategoryName;
            category.Description = updatedCategory.Description;
            category.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            await LoadCategories(); // Reload to get updated list
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating category: {ex.Message}");
            return false;
        }
    }

    // Delete category
    public async Task<(bool success, string message)> DeleteCategoryAsync(int categoryId)
    {
        try
        {
            var category = await _dbContext.Categories
                .Include(c => c.Laptops.Where(l => !l.IsDeleted))
                .FirstOrDefaultAsync(c => c.CategoryID == categoryId);

            if (category == null)
                return (false, "Không tìm thấy loại sản phẩm này.");

            if (category.Laptops.Any())
                return (false, "Không thể xóa loại sản phẩm này vì đang có sản phẩm thuộc loại này.");

            category.IsDeleted = true;
            category.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            await LoadCategories(); // Reload to get updated list
            return (true, "Xóa loại sản phẩm thành công.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting category: {ex.Message}");
            return (false, "Có lỗi xảy ra khi xóa loại sản phẩm.");
        }
    }

    // Validate category input
    partial void OnEditingCategoryChanged(CategoryDisplayDto? value)
    {
        ValidateCategoryInput();
    }

    private void ValidateCategoryInput()
    {
        IsValidCategoryInput = EditingCategory != null && 
                              !string.IsNullOrWhiteSpace(EditingCategory.CategoryName);
    }

    // Existing methods...
    partial void OnSearchTextChanged(string value)
    {
        FilterAndPaginateCategories();
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
                c.CategoryName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                (c.Description != null && c.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
            ).ToList();

        // Apply sorting based on current sort properties
        filteredCategories = ApplySorting(filteredCategories);

        _totalItems = filteredCategories.Count;
        LoadPage(1); // Reset to first page when filtering
    }

    public void LoadPage(int page)
    {
        if (_allCategories == null) return;

        var filteredCategories = string.IsNullOrWhiteSpace(SearchText)
            ? _allCategories
            : _allCategories.Where(c =>
                c.CategoryName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                (c.Description != null && c.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
            ).ToList();

        // Apply sorting
        filteredCategories = ApplySorting(filteredCategories);

        _totalItems = filteredCategories.Count;

        var pagedCategories = filteredCategories
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        Categories = new ObservableCollection<CategoryDisplayDto>(pagedCategories);
        OnPropertyChanged(nameof(TotalPages));
    }

    private List<CategoryDisplayDto> ApplySorting(List<CategoryDisplayDto> categories)
    {
        if (_sortProperties.Any())
        {
            categories = Core.Helpers.DataGridSortHelper.ApplySort(
                categories,
                _sortProperties,
                _sortDirections
            ).ToList();
        }
        return categories;
    }
} 