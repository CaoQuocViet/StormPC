 using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using StormPC.Contracts.Services;
using StormPC.Core.Services.Security;

namespace StormPC.ViewModels.Login;

public partial class FirstTimeSetupViewModel : ObservableObject
{
    private readonly ILoginService _loginService;
    private readonly INavigationService _navigationService;
    private readonly IPasswordHashService _passwordService;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _confirmPassword = string.Empty;

    [ObservableProperty]
    private bool _useWindowsHello;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private bool _isWindowsHelloAvailable;

    public FirstTimeSetupViewModel(
        ILoginService loginService,
        INavigationService navigationService,
        IPasswordHashService passwordService)
    {
        _loginService = loginService;
        _navigationService = navigationService;
        _passwordService = passwordService;
        CheckWindowsHelloAvailabilityAsync();
    }

    private async void CheckWindowsHelloAvailabilityAsync()
    {
        IsWindowsHelloAvailable = await _loginService.IsWindowsHelloAvailableAsync();
        if (IsWindowsHelloAvailable)
        {
            UseWindowsHello = true;
        }
    }

    [RelayCommand]
    private async Task SetupAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Username) || 
                string.IsNullOrWhiteSpace(Password) || 
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage = "Vui lòng nhập đầy đủ thông tin";
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Mật khẩu xác nhận không khớp";
                return;
            }

            if (!_passwordService.IsPasswordStrong(Password))
            {
                ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt";
                return;
            }

            var success = await _loginService.SetupAdminAccountAsync(Username, Password, UseWindowsHello);
            if (success)
            {
                var backupKey = await _loginService.GenerateBackupKeyAsync();
                await ShowBackupKeyDialog(backupKey);
                _navigationService.NavigateTo("Main");
            }
            else
            {
                ErrorMessage = "Không thể thiết lập tài khoản, vui lòng thử lại";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = "Đã có lỗi xảy ra, vui lòng thử lại";
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task ShowBackupKeyDialog(string backupKey)
    {
        var dialog = new ContentDialog
        {
            Title = "Backup Key của bạn",
            Content = $"Đây là backup key của bạn. Hãy lưu nó ở nơi an toàn để khôi phục tài khoản khi cần:\n\n{backupKey}",
            PrimaryButtonText = "Đã lưu",
            DefaultButton = ContentDialogButton.Primary
        };

        await dialog.ShowAsync();
    }
}