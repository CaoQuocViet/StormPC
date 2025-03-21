using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using StormPC.Core.Models;

namespace StormPC.ViewModels.BaseData;

public partial class ProductsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Product> _products;

    public ProductsViewModel()
    {
        _products = new ObservableCollection<Product>();
        LoadProducts();
    }

    public void LoadProducts()
    {
        // TODO: Load products from your data source
        // This is just a sample implementation
        Products.Clear();
        // Add sample products for testing
        Products.Add(new Product { Name = "Sample Product 1", Description = "Description 1" });
        Products.Add(new Product { Name = "Sample Product 2", Description = "Description 2" });
    }

    public void SearchProducts(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            // Reset to full list or load default data
            LoadProducts();
            return;
        }

        // Thực hiện tìm kiếm dựa trên searchText
        var searchResults = Products.Where(p => 
            p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
            p.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .ToList();

        // Cập nhật danh sách hiển thị
        Products.Clear();
        foreach (var product in searchResults)
        {
            Products.Add(product);
        }
    }
} 