using Microsoft.UI.Xaml.Controls;

using StormPC.ViewModels.Settings;

namespace StormPC.Views.Settings;

public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
    }
} 