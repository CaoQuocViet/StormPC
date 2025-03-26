using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using StormPC.ViewModels.BaseData;

namespace StormPC.Views.BaseData;

public sealed partial class CategoriesPage : Page
{
    public CategoriesViewModel ViewModel { get; }

    public CategoriesPage()
    {
        this.InitializeComponent();
        ViewModel = App.GetService<CategoriesViewModel>();
    }

    private async void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        await ViewModel.InitializeAsync();
    }
} 