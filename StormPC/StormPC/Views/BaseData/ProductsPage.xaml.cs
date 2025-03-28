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
        await ViewModel.LoadProductsAsync();
    }

    private void PaginationControl_PageChanged(object sender, int page)
    {
        ViewModel.LoadPage(page);
    }

    private void PaginationControl_PageSizeChanged(object sender, int pageSize)
    {
        ViewModel.PageSize = pageSize;
        ViewModel.LoadPage(1); // Reset to first page when changing page size
    }

    private async void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        await ViewModel.LoadProductsAsync();
    }

    private void SortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox comboBox)
        {
            ViewModel.SelectedSortIndex = comboBox.SelectedIndex;
        }
    }
} 