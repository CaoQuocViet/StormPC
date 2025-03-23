using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Windowing;
using StormPC.Core.Services.Login;
using System;
using WinRT.Interop;
using StormPC.ViewModels.Login;

namespace StormPC.Views.Login;

public sealed partial class FirstTimeWindow : Window
{
    public FirstTimeViewModel ViewModel { get; }

    public FirstTimeWindow()
    {
        ViewModel = App.GetService<FirstTimeViewModel>();
        InitializeComponent();

        // Set window size and properties
        var windowHandle = WindowNative.GetWindowHandle(this);
        var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
        var appWindow = AppWindow.GetFromWindowId(windowId);
        
        // Set size
        var size = new Windows.Graphics.SizeInt32(400, 600);
        appWindow.Resize(size);

        // Center on screen
        var displayArea = DisplayArea.GetFromWindowId(windowId, DisplayAreaFallback.Primary);
        if (displayArea != null)
        {
            var centerX = (displayArea.WorkArea.Width - size.Width) / 2;
            var centerY = (displayArea.WorkArea.Height - size.Height) / 2;
            appWindow.Move(new Windows.Graphics.PointInt32(centerX, centerY));
        }

        Title = "First Time Setup - StormPC";

        // Subscribe to command execution
        ViewModel.CreateAdminAccountCommand.PropertyChanged += CreateAdminAccountCommand_PropertyChanged;
    }

    private async void OnCreateAdminClick(object sender, RoutedEventArgs e)
    {
        var password = PasswordBox.Password;
        var confirmPassword = ConfirmPasswordBox.Password;

        if (ViewModel.IsPasswordValid(password, confirmPassword))
        {
            await ViewModel.CreateAdminAccountCommand.ExecuteAsync(password);
        }
    }

    private void CreateAdminAccountCommand_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsRunning" && !ViewModel.CreateAdminAccountCommand.IsRunning)
        {
            if (string.IsNullOrEmpty(ViewModel.ErrorMessage))
            {
                var loginWindow = App.GetService<LoginWindow>();
                loginWindow.Activate();
                Close();
            }
        }
    }
} 