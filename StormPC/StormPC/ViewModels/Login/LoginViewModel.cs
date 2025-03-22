using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using StormPC.Contracts.Services;
using StormPC.Core.Services.Security;

namespace StormPC.ViewModels.Login;

public partial class LoginViewModel : ObservableObject
{
    private readonly ILoginService _loginService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private bool _isWindowsHelloAvailable;

    public LoginViewModel(ILoginService loginService, INavigationService navigationService)
    {
        _loginService = loginService;
        _navigationService = navigationService;
        CheckWindowsHelloAvailabilityAsync();
    }

    private async void CheckWindowsHelloAvailabilityAsync()
    {
        IsWindowsHelloAvailable = await _loginService.IsWindowsHelloAvailableAsync();
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Vui lòng nhập đầy đủ thông tin";
                return;
            }

            var success = await _loginService.LoginAsync(Username, Password);
            if (success)
            {
                _navigationService.NavigateTo("Main");
            }
            else
            {
                ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng";
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

    [RelayCommand]
    private async Task LoginWithWindowsHelloAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            var success = await _loginService.LoginWithWindowsHelloAsync();
            if (success)
            {
                _navigationService.NavigateTo("Main");
            }
            else
            {
                ErrorMessage = "Xác thực Windows Hello thất bại";
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

    [RelayCommand]
    private async Task RecoverWithBackupKeyAsync()
    {
        try
        {
            var dialog = new ContentDialog
            {
                Title = "Khôi phục bằng backup key",
                PrimaryButtonText = "Khôi phục",
                CloseButtonText = "Hủy",
                DefaultButton = ContentDialogButton.Primary,
                Content = new TextBox
                {
                    PlaceholderText = "Nhập backup key của bạn"
                }
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var backupKey = ((TextBox)dialog.Content).Text;
                if (string.IsNullOrWhiteSpace(backupKey))
                {
                    ErrorMessage = "Vui lòng nhập backup key";
                    return;
                }

                IsLoading = true;
                ErrorMessage = string.Empty;

                var success = await _loginService.LoginWithBackupKeyAsync(backupKey);
                if (success)
                {
                    _navigationService.NavigateTo("Main");
                }
                else
                {
                    ErrorMessage = "Backup key không hợp lệ";
                }
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
} 