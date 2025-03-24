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

            // Map to CategoryDisplayDto
            Categories = new ObservableCollection<CategoryDisplayDto>(
                categories.Select(c => new CategoryDisplayDto
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    Description = c.Description,
                    ProductCount = c.ProductCount,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt
                })
            );

            Debug.WriteLine($"Categories loaded: {Categories.Count}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading categories: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            // Handle error appropriately
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