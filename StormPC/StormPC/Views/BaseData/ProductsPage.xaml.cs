using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using StormPC.ViewModels.BaseData;

namespace StormPC.Views.BaseData;

public sealed partial class ProductsPage : Page
{
    public ProductsViewModel ViewModel { get; }

    public ProductsPage()
    {
        ViewModel = App.GetService<ProductsViewModel>();
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        await ViewModel.LoadDataAsync();
    }
} 