using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Windowing;
using StormPC.ViewModels.Login;
using WinRT.Interop;
using System.IO;

namespace StormPC.Views.Login;

public sealed partial class FirstTimeWindow : Window
{
    public FirstTimeViewModel ViewModel { get; }

    public FirstTimeWindow()
    {
        ViewModel = App.GetService<FirstTimeViewModel>();
        ViewModel.AccountCreated += ViewModel_AccountCreated;
        this.InitializeComponent();

        // Set window size and properties
        var windowHandle = WindowNative.GetWindowHandle(this);
        var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
        var appWindow = AppWindow.GetFromWindowId(windowId);
        
        // Set size
        var size = new Windows.Graphics.SizeInt32(666, 640);
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

        // Set window icon
        var iconPath = Path.Combine(AppContext.BaseDirectory, "Assets/img/icon/WindowIcon-512.ico");
        if (File.Exists(iconPath))
        {
            AppWindow.SetIcon(iconPath);
        }
    }

    private async void ViewModel_AccountCreated(object? sender, EventArgs e)
    {
        // Show backup key dialog
        _ = await BackupKeyDialog.ShowAsync();
        
        // After dialog is closed, show login window
        var loginWindow = App.GetService<LoginWindow>();
        loginWindow.Activate();
        Close();
    }

    private async void OnCreateAdminClick(object sender, RoutedEventArgs e)
    {
        var password = PasswordBox.Password;
        var confirmPassword = ConfirmPasswordBox.Password;
        await ViewModel.CreateAdminAccountCommand.ExecuteAsync((password, confirmPassword));
    }

    private void OnCopyBackupKey(object sender, ContentDialogButtonClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(ViewModel.BackupKey))
        {
            var dataPackage = new Windows.ApplicationModel.DataTransfer.DataPackage();
            dataPackage.SetText(ViewModel.BackupKey);
            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
        }
    }

    private void OnCloseBackupKeyDialog(object sender, ContentDialogButtonClickEventArgs e)
    {
        BackupKeyDialog.Hide();
    }
}