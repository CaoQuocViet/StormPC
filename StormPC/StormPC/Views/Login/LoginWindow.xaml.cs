using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Windowing;
using StormPC.ViewModels.Login;
using WinRT.Interop;
using System.IO;

namespace StormPC.Views.Login;

public sealed partial class LoginWindow : Window
{
    public LoginViewModel ViewModel { get; }

    public LoginWindow()
    {
        ViewModel = App.GetService<LoginViewModel>();
        ViewModel.LoginSuccessful += ViewModel_LoginSuccessful;
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

        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/img/icon/WindowIcon-512.ico"));

        // Setup password visibility toggle
        ShowPasswordButton.Click += ShowPasswordButton_Click;
    }

    private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
    {
        if (ShowPasswordButton.IsChecked == true)
        {
            var passwordBox = (PasswordBox)PasswordBox;
            var passwordRevealMode = passwordBox.PasswordRevealMode;
            passwordBox.PasswordRevealMode = passwordRevealMode == PasswordRevealMode.Hidden 
                ? PasswordRevealMode.Visible 
                : PasswordRevealMode.Hidden;
        }
    }

    private void ViewModel_LoginSuccessful(object? sender, EventArgs e)
    {
        Close();
    }

    private async void OnLoginClick(object sender, RoutedEventArgs e)
    {
        var password = PasswordBox.Password;
        await ViewModel.LoginCommand.ExecuteAsync(password);
    }
}