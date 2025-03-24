using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using StormPC.Core.Models.Products.Dtos;
using StormPC.Core.Services.Products;
using StormPC.Core.Helpers;

namespace StormPC.ViewModels.BaseData;

public partial class ProductsViewModel(IProductService productService) : ObservableObject
{
    private readonly IProductService _productService = productService;

    [ObservableProperty]
    private ObservableCollection<LaptopDisplayDto> _laptops = [];

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _searchText = string.Empty;

    public async Task LoadDataAsync()
    {
        try
        {
            IsLoading = true;
            var laptops = await _productService.GetAllLaptopsForDisplayAsync();
            foreach (var laptop in laptops)
            {
                laptop.FormattedPrice = CurrencyHelper.FormatCurrency(laptop.LowestPrice);

                // Tính phần trăm giảm giá dựa trên số tiền giảm
                if (laptop.Discount > 0)
                {
                    // Discount là số tiền giảm (VNĐ)
                    // Giá gốc = Giá hiện tại + Số tiền giảm
                    var originalPrice = laptop.LowestPrice + laptop.Discount;
                    var discountPercent = Math.Floor((laptop.Discount / originalPrice) * 100);
                    laptop.FormattedDiscount = discountPercent.ToString();
                }
                else
                {
                    laptop.FormattedDiscount = "0";
                }

                // Tính số lượng tùy chọn (số phiên bản - 1)
                var variantsCount = await _productService.GetVariantsCountAsync(laptop.LaptopID);
                laptop.OptionsCount = Math.Max(0, variantsCount - 1); // Trừ đi phiên bản hiện tại
            }
            Laptops = [.. laptops];
        }
        finally
        {
            IsLoading = false;
        }
    }
} 