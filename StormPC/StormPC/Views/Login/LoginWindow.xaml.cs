using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Windowing;
using StormPC.Core.Services.Login;
using System;
using WinRT.Interop;
using StormPC.ViewModels.Login;

namespace StormPC.Views.Login;

public sealed partial class LoginWindow : Window
{
    public LoginViewModel ViewModel { get; }

    public LoginWindow()
    {
        ViewModel = App.GetService<LoginViewModel>();
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

        Title = "Login - StormPC";

        // Subscribe to command execution
        ViewModel.LoginCommand.PropertyChanged += LoginCommand_PropertyChanged;
    }

    private async void OnLoginClick(object sender, RoutedEventArgs e)
    {
        var password = PasswordBox.Password;
        await ViewModel.LoginCommand.ExecuteAsync(password);
    }

    private void LoginCommand_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsRunning" && !ViewModel.LoginCommand.IsRunning)
        {
            if (string.IsNullOrEmpty(ViewModel.ErrorMessage))
            {
                var mainWindow = App.GetService<MainWindow>();
                mainWindow.Activate();
                Close();
            }
        }
    }
} 