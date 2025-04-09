using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Windowing;
using StormPC.ViewModels.Login;
using WinRT.Interop;
using System.IO;
using System.Threading.Tasks;

namespace StormPC.Views.Login;

public sealed partial class LoginWindow : Window
{
    public LoginViewModel ViewModel { get; }

    public LoginWindow()
    {
        ViewModel = App.GetService<LoginViewModel>();
        ViewModel.LoginSuccessful += ViewModel_LoginSuccessful;
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

        Title = "Login - StormPC";

        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/img/icon/WindowIcon-512.ico"));

        // Setup password visibility toggle
        if (ShowPasswordButton != null)
        {
            ShowPasswordButton.Click += ShowPasswordButton_Click;
        }
    }

    private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
    {
        if (ShowPasswordButton?.IsChecked == true)
        {
            PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible;
        }
        else
        {
            PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
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

    private async void OnRecoverAccountTapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
    {
        await RecoverAccountDialog.ShowAsync();
    }

    private async void OnRecoverAccountConfirm(object sender, ContentDialogButtonClickEventArgs e)
    {
        try
        {
            var backupKey = BackupKeyTextBox.Text;
            if (await ViewModel.VerifyBackupKeyAsync(backupKey))
            {
                await ViewModel.ResetAdminAccountAsync();
                
                // Đóng dialog trước
                RecoverAccountDialog.Hide();
                
                // Tạo và hiển thị FirstTimeWindow
                var firstTimeWindow = App.GetService<FirstTimeWindow>();
                firstTimeWindow.Activate();
                
                // Đợi một chút để window mới hiển thị
                await Task.Delay(500);
                
                // Đóng window hiện tại
                Close();
            }
            else
            {
                ViewModel.ErrorMessage = "Invalid backup key. Please try again.";
            }
        }
        catch (Exception ex)
        {
            ViewModel.ErrorMessage = $"Error resetting account: {ex.Message}";
        }
    }

    private void OnRecoverAccountCancel(object sender, ContentDialogButtonClickEventArgs e)
    {
        BackupKeyTextBox.Text = string.Empty;
        ViewModel.ErrorMessage = string.Empty;
        RecoverAccountDialog.Hide();
    }
}