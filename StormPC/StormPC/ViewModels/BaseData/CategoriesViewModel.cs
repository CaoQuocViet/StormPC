using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using StormPC.Core.Infrastructure.Database;
using StormPC.Core.Infrastructure.Database.Contexts;
using StormPC.Core.Models.Products;
using StormPC.Core.Models.Products.Dtos;

namespace StormPC.ViewModels.BaseData;

public partial class CategoriesViewModel : ObservableObject
{
    private readonly StormPCDbContext _dbContext;

    [ObservableProperty]
    private ObservableCollection<CategoryDisplayDto> _categories = new();

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _searchText = string.Empty;

    public CategoriesViewModel(StormPCDbContext dbContext)
    {
        _dbContext = dbContext;
        Task.Run(async () => await LoadCategories());
    }

    [RelayCommand]
    private async Task LoadCategories()
    {
        try
        {
            IsLoading = true;
            System.Diagnostics.Debug.WriteLine("Loading categories...");

            var rawCategories = await _dbContext.Categories.ToListAsync();
            System.Diagnostics.Debug.WriteLine($"Total categories found: {rawCategories.Count}");

            var categories = await _dbContext.Categories
                .Select(c => new CategoryDisplayDto
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    Description = c.Description,
                    ProductCount = _dbContext.Laptops.Count(l => l.CategoryID == c.CategoryID && !l.IsDeleted),
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            System.Diagnostics.Debug.WriteLine($"Processed categories: {categories.Count}");

            Categories = new ObservableCollection<CategoryDisplayDto>(categories);
            System.Diagnostics.Debug.WriteLine($"Categories loaded: {Categories.Count}");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading categories: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            LoadCategoriesCommand.Execute(null);
            return;
        }

        var searchText = value.ToLower();
        var filteredCategories = Categories
            .Where(c => c.CategoryName.ToLower().Contains(searchText) ||
                       (c.Description?.ToLower().Contains(searchText) ?? false))
            .ToList();

        Categories = new ObservableCollection<CategoryDisplayDto>(filteredCategories);
    }
} 