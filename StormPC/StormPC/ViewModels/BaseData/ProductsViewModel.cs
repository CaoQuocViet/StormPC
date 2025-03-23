using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using StormPC.Core.Models.Products.Dtos;
using StormPC.Core.Services.Products;

namespace StormPC.ViewModels.BaseData;

public partial class ProductsViewModel : ObservableObject
{
    private readonly IProductService _productService;

    [ObservableProperty]
    private ObservableCollection<LaptopDisplayDto> _laptops = new();

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _searchText = string.Empty;

    public ProductsViewModel(IProductService productService)
    {
        _productService = productService;
    }

    public async Task LoadDataAsync()
    {
        try
        {
            IsLoading = true;
            var laptops = await _productService.GetAllLaptopsForDisplayAsync();
            Laptops = new ObservableCollection<LaptopDisplayDto>(laptops);
        }
        finally
        {
            IsLoading = false;
        }
    }
} 