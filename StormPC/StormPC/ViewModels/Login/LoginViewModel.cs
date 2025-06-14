using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using StormPC.Core.Services.Login;
using System.Threading.Tasks;
using StormPC.Contracts;
using System;
using System.Reflection;
using Windows.ApplicationModel;
using StormPC.Helpers;
using StormPC.Core.Contracts.Services;

namespace StormPC.ViewModels.Login;

public partial class LoginViewModel : ObservableObject
{
    private readonly AuthenticationService _authService;
    private readonly SecureStorageService _secureStorage;
    private readonly IActivityLogService _activityLogService;
    private const string REMEMBERED_LOGIN_KEY = "remembered_login";

    public event EventHandler? LoginSuccessful;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _rememberMe;

    [ObservableProperty]
    private bool _isPasswordViewable = true;

    [ObservableProperty]
    private string _versionDescription;

    public LoginViewModel(AuthenticationService authService, SecureStorageService secureStorage, IActivityLogService activityLogService)
    {
        _authService = authService;
        _secureStorage = secureStorage;
        _activityLogService = activityLogService;
        _versionDescription = GetVersionDescription();
        LoadRememberedLogin();
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;
            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }

    public async Task<bool> VerifyBackupKeyAsync(string backupKey)
    {
        try 
        {
            var isValid = await _authService.VerifyBackupKeyAsync(backupKey);
            if (isValid)
            {
                await _activityLogService.LogActivityAsync("Đăng nhập", "Khóa sao lưu", 
                    "Khôi phục tài khoản thành công bằng khóa sao lưu", "Success", Username);
            }
            else 
            {
                await _activityLogService.LogActivityAsync("Đăng nhập", "Khóa sao lưu", 
                    "Khôi phục tài khoản thất bại - Khóa sao lưu không hợp lệ", "Error", Username);
            }
            return isValid;
        }
        catch (Exception ex)
        {
            await _activityLogService.LogActivityAsync("Đăng nhập", "Khóa sao lưu", 
                $"Không thể xác thực khóa sao lưu - {ex.Message}", "Error", Username);
            return false;
        }
    }

    public async Task ResetAdminAccountAsync()
    {
        try
        {
            await _authService.ResetAdminAccountAsync();
            await _activityLogService.LogActivityAsync("Đăng nhập", "Đặt lại tài khoản quản trị", 
                "Tài khoản quản trị đã được đặt lại", "Success", "System");
        }
        catch (Exception ex)
        {
            await _activityLogService.LogActivityAsync("Đăng nhập", "Đặt lại tài khoản quản trị", 
                $"Không thể đặt lại tài khoản quản trị - {ex.Message}", "Error", "System");
            throw; // Ném lại ngoại lệ để xử lý ở tầng UI
        }
    }

    private void LoadRememberedLogin()
    {
        var rememberedLogin = _secureStorage.LoadSecureData<RememberedLogin>(REMEMBERED_LOGIN_KEY);
        if (rememberedLogin != null && rememberedLogin.ExpiresAt > DateTime.UtcNow)
        {
            Username = rememberedLogin.Username;
            Password = rememberedLogin.Password;
            RememberMe = true;
            IsPasswordViewable = false;
        }
        else
        {
            // Clear expired remembered login
            _secureStorage.SaveSecureData<RememberedLogin?>(REMEMBERED_LOGIN_KEY, null);
        }
    }

    [RelayCommand]
    private async Task LoginAsync(string password)
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(password))
        {
            ErrorMessage = "Vui lòng nhập cả tên đăng nhập và mật khẩu";
            await _activityLogService.LogActivityAsync("Đăng nhập", "Đăng nhập", 
                "Đăng nhập thất bại - Tên đăng nhập hoặc mật khẩu trống", "Error", Username);
            return;
        }

        try
        {
            var (success, error) = await _authService.LoginAsync(Username, password);
            if (success)
            {
                if (RememberMe)
                {
                    var rememberedLogin = new RememberedLogin
                    {
                        Username = Username,
                        Password = password,
                        LastLoginTime = DateTime.UtcNow,
                        ExpiresAt = DateTime.UtcNow.AddHours(1) // Ghi nhớ trong 1 giờ
                    };
                    _secureStorage.SaveSecureData<RememberedLogin>(REMEMBERED_LOGIN_KEY, rememberedLogin);
                }
                else
                {
                    _secureStorage.SaveSecureData<RememberedLogin?>(REMEMBERED_LOGIN_KEY, null);
                }

                await _activityLogService.LogActivityAsync("Đăng nhập", "Đăng nhập thành công", 
                    "Người dùng đăng nhập thành công", "Success", Username);

                await App.GetService<IActivationService>().ActivateAsync(null!);
                LoginSuccessful?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                ErrorMessage = error ?? "Đăng nhập thất bại. Vui lòng thử lại.";
                await _activityLogService.LogActivityAsync("Đăng nhập", "Đăng nhập thất bại", 
                    $"Đăng nhập thất bại - {error}", "Error", Username);
            }
        }
        catch (System.Exception ex)
        {
            ErrorMessage = $"Đã xảy ra lỗi: {ex.Message}";
            await _activityLogService.LogActivityAsync("Đăng nhập", "Lỗi đăng nhập", 
                $"Lỗi đăng nhập - {ex.Message}", "Error", Username);
        }
    }
}

public class RememberedLogin
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime LastLoginTime { get; set; }
    public DateTime ExpiresAt { get; set; }
}