using Microsoft.UI.Xaml.Controls;

using StormPC.ViewModels.BaseData;

namespace StormPC.Views.BaseData;

public sealed partial class ProductsPage : Page
{
    public ProductsViewModel ViewModel
    {
        get;
    }

    public ProductsPage(ProductsViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }
} 