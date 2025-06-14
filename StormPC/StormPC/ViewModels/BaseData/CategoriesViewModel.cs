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
using StormPC.Core.Contracts.Services;
using StormPC.Core.Helpers;

namespace StormPC.ViewModels.BaseData;

public partial class CategoriesViewModel : ObservableObject, IPaginatedViewModel
{
    private readonly StormPCDbContext _dbContext;
    private readonly IActivityLogService _activityLogService;
    private List<CategoryDisplayDto> _allCategories;
    private ObservableCollection<CategoryDisplayDto> _categories;
    private bool _isLoading;
    [ObservableProperty]
    private string searchText = string.Empty;
    private int _currentPage = 1;
    private int _pageSize = 10;
    private int _totalItems;

    // Properties for editing
    private CategoryDisplayDto? _editingCategory;
    public CategoryDisplayDto? EditingCategory
    {
        get => _editingCategory;
        set
        {
            if (_editingCategory != value)
            {
                if (_editingCategory != null)
                {
                    _editingCategory.PropertyChanged -= EditingCategory_PropertyChanged;
                }

                _editingCategory = value;

                if (_editingCategory != null)
                {
                    _editingCategory.PropertyChanged += EditingCategory_PropertyChanged;
                }

                OnPropertyChanged(nameof(EditingCategory));
                ValidateCategoryInput();
            }
        }
    }
    
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

    public CategoriesViewModel(StormPCDbContext dbContext, IActivityLogService activityLogService)
    {
        _dbContext = dbContext;
        _activityLogService = activityLogService;
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
            await _activityLogService.LogActivityAsync(
                "Categories",
                "Load Categories",
                "Đang tải danh sách danh mục",
                "Info",
                GetUserName.GetCurrentUsername()
            );

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

            await _activityLogService.LogActivityAsync(
                "Categories",
                "Load Categories",
                $"Tải thành công {categories.Count} danh mục",
                "Success",
                GetUserName.GetCurrentUsername()
            );
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading categories: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            await _activityLogService.LogActivityAsync(
                "Categories",
                "Load Categories",
                $"Lỗi khi tải danh mục: {ex.Message}",
                "Error",
                GetUserName.GetCurrentUsername()
            );
        }
        finally
        {
            IsLoading = false;
        }
    }

    /// <summary>
    /// Xử lý sự kiện khi thuộc tính của category đang chỉnh sửa thay đổi
    /// </summary>
    private void EditingCategory_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CategoryDisplayDto.CategoryName))
        {
            ValidateCategoryInput();
        }
    }

    /// <summary>
    /// Xác thực dữ liệu nhập vào cho category
    /// </summary>
    private void ValidateCategoryInput()
    {
        IsValidCategoryInput = EditingCategory != null && 
                              !string.IsNullOrWhiteSpace(EditingCategory.CategoryName?.Trim());
        OnPropertyChanged(nameof(IsValidCategoryInput));
    }

    /// <summary>
    /// Thêm danh mục mới vào cơ sở dữ liệu
    /// </summary>
    public async Task<bool> AddCategoryAsync(CategoryDisplayDto newCategory)
    {
        try
        {
            await _activityLogService.LogActivityAsync(
                "Categories",
                "Add Category",
                $"Đang thêm danh mục mới: {newCategory.CategoryName}",
                "Info",
                GetUserName.GetCurrentUsername()
            );

            if (string.IsNullOrWhiteSpace(newCategory.CategoryName?.Trim()))
            {
                await _activityLogService.LogActivityAsync(
                    "Categories",
                    "Add Category",
                    "Thêm danh mục thất bại - Tên danh mục trống",
                    "Error",
                    GetUserName.GetCurrentUsername()
                );
                return false;
            }

            // Check if category name already exists
            var exists = await _dbContext.Categories
                .Where(c => !c.IsDeleted)
                .AnyAsync(c => c.CategoryName.ToLower() == newCategory.CategoryName.Trim().ToLower());

            if (exists)
            {
                await _activityLogService.LogActivityAsync(
                    "Categories",
                    "Add Category",
                    $"Thêm danh mục thất bại - Danh mục {newCategory.CategoryName} đã tồn tại",
                    "Error",
                    GetUserName.GetCurrentUsername()
                );
                return false;
            }

            // Get max CategoryID from database
            var maxId = await _dbContext.Categories
                .MaxAsync(c => (int?)c.CategoryID) ?? 0;

            // Make sure the new ID doesn't exist (in case of concurrent operations)
            var newId = maxId + 1;
            while (await _dbContext.Categories.AnyAsync(c => c.CategoryID == newId))
            {
                newId++;
            }

            // Create new category with next available ID
            var category = new Category
            {
                CategoryID = newId,
                CategoryName = newCategory.CategoryName.Trim(),
                Description = newCategory.Description?.Trim(),
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            await LoadCategories(); // Reload to get updated list

            await _activityLogService.LogActivityAsync(
                "Categories",
                "Add Category",
                $"Thêm danh mục thành công: {category.CategoryName}",
                "Success",
                GetUserName.GetCurrentUsername()
            );

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding category: {ex.Message}");
            await _activityLogService.LogActivityAsync(
                "Categories",
                "Add Category",
                $"Lỗi khi thêm danh mục: {ex.Message}",
                "Error",
                GetUserName.GetCurrentUsername()
            );
            return false;
        }
    }

    /// <summary>
    /// Cập nhật thông tin danh mục
    /// </summary>
    public async Task<bool> UpdateCategoryAsync(CategoryDisplayDto updatedCategory)
    {
        try
        {
            await _activityLogService.LogActivityAsync(
                "Categories",
                "Update Category",
                $"Đang cập nhật danh mục ID: {updatedCategory.CategoryID}",
                "Info",
                GetUserName.GetCurrentUsername()
            );

            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.CategoryID == updatedCategory.CategoryID);

            if (category == null)
            {
                await _activityLogService.LogActivityAsync(
                    "Categories",
                    "Update Category",
                    $"Cập nhật thất bại - Không tìm thấy danh mục ID: {updatedCategory.CategoryID}",
                    "Error",
                    GetUserName.GetCurrentUsername()
                );
                return false;
            }

            category.CategoryName = updatedCategory.CategoryName;
            category.Description = updatedCategory.Description;
            category.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            await LoadCategories(); // Reload to get updated list

            await _activityLogService.LogActivityAsync(
                "Categories",
                "Update Category",
                $"Cập nhật thành công danh mục: {category.CategoryName}",
                "Success",
                GetUserName.GetCurrentUsername()
            );

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating category: {ex.Message}");
            await _activityLogService.LogActivityAsync(
                "Categories",
                "Update Category",
                $"Lỗi khi cập nhật danh mục: {ex.Message}",
                "Error",
                GetUserName.GetCurrentUsername()
            );
            return false;
        }
    }

    /// <summary>
    /// Xóa danh mục theo ID
    /// </summary>
    public async Task<(bool success, string message)> DeleteCategoryAsync(int categoryId)
    {
        try
        {
            await _activityLogService.LogActivityAsync(
                "Categories",
                "Delete Category",
                $"Đang xóa danh mục ID: {categoryId}",
                "Info",
                GetUserName.GetCurrentUsername()
            );

            var category = await _dbContext.Categories
                .Include(c => c.Laptops.Where(l => !l.IsDeleted))
                .FirstOrDefaultAsync(c => c.CategoryID == categoryId);

            if (category == null)
            {
                await _activityLogService.LogActivityAsync(
                    "Categories",
                    "Delete Category",
                    $"Xóa thất bại - Không tìm thấy danh mục ID: {categoryId}",
                    "Error",
                    GetUserName.GetCurrentUsername()
                );
                return (false, "Không tìm thấy loại sản phẩm này.");
            }

            if (category.Laptops.Any())
            {
                await _activityLogService.LogActivityAsync(
                    "Categories",
                    "Delete Category",
                    $"Xóa thất bại - Danh mục {category.CategoryName} đang có sản phẩm",
                    "Error",
                    GetUserName.GetCurrentUsername()
                );
                return (false, "Không thể xóa loại sản phẩm này vì đang có sản phẩm thuộc loại này.");
            }

            category.IsDeleted = true;
            category.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            await LoadCategories(); // Reload to get updated list

            await _activityLogService.LogActivityAsync(
                "Categories",
                "Delete Category",
                $"Xóa thành công danh mục: {category.CategoryName}",
                "Success",
                GetUserName.GetCurrentUsername()
            );

            return (true, "Xóa loại sản phẩm thành công.");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting category: {ex.Message}");
            await _activityLogService.LogActivityAsync(
                "Categories",
                "Delete Category",
                $"Lỗi khi xóa danh mục: {ex.Message}",
                "Error",
                GetUserName.GetCurrentUsername()
            );
            return (false, "Có lỗi xảy ra khi xóa loại sản phẩm.");
        }
    }

    /// <summary>
    /// Xử lý khi thay đổi SearchText
    /// </summary>
    partial void OnSearchTextChanged(string value)
    {
        FilterAndPaginateCategories();
    }

    /// <summary>
    /// Cập nhật thứ tự sắp xếp của danh sách danh mục
    /// </summary>
    public void UpdateSorting(List<string> properties, List<ListSortDirection> directions)
    {
        _sortProperties = properties;
        _sortDirections = directions;
        FilterAndPaginateCategories();
    }

    /// <summary>
    /// Lọc và phân trang danh sách danh mục
    /// </summary>
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

    /// <summary>
    /// Tải dữ liệu cho trang chỉ định
    /// </summary>
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

    /// <summary>
    /// Áp dụng các quy tắc sắp xếp cho danh sách danh mục
    /// </summary>
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