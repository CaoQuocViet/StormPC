using Microsoft.UI.Xaml.Controls;

using StormPC.ViewModels;

namespace StormPC.Views;

public sealed partial class Blank1Page : Page
{
    public Blank1ViewModel ViewModel
    {
        get;
    }

    public Blank1Page()
    {
        ViewModel = App.GetService<Blank1ViewModel>();
        InitializeComponent();
    }
}
