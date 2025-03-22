 using Microsoft.UI.Xaml.Controls;
using StormPC.ViewModels.Login;

namespace StormPC.Views.Login;

public sealed partial class FirstTimeSetupPage : Page
{
    public FirstTimeSetupViewModel ViewModel
    {
        get;
    }

    public FirstTimeSetupPage()
    {
        ViewModel = App.GetService<FirstTimeSetupViewModel>();
        InitializeComponent();
    }
}