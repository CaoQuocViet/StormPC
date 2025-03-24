using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using StormPC.ViewModels.BaseData;

namespace StormPC.Views.BaseData;

public sealed partial class CategoriesPage : Page
{
    public CategoriesViewModel ViewModel { get; }

    public CategoriesPage()
    {
        ViewModel = App.GetService<CategoriesViewModel>();
        InitializeComponent();
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        ViewModel.LoadCategoriesCommand.Execute(null);
    }
} 