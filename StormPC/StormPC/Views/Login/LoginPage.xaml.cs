 using Microsoft.UI.Xaml.Controls;
using StormPC.ViewModels.Login;

namespace StormPC.Views.Login;

public sealed partial class LoginPage : Page
{
    public LoginViewModel ViewModel
    {
        get;
    }

    public LoginPage()
    {
        ViewModel = App.GetService<LoginViewModel>();
        InitializeComponent();
    }
}