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

namespace StormPC.ViewModels.Login;

public partial class LoginViewModel : ObservableObject
{
    private readonly AuthenticationService _authService;
    private readonly SecureStorageService _secureStorage;
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

    public LoginViewModel(AuthenticationService authService, SecureStorageService secureStorage)
    {
        _authService = authService;
        _secureStorage = secureStorage;
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
        return await _authService.VerifyBackupKeyAsync(backupKey);
    }

    public void ResetAdminAccount()
    {
        _authService.ResetAdminAccount();
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
            _secureStorage.SaveSecureData<RememberedLogin>(REMEMBERED_LOGIN_KEY, null);
        }
    }

    [RelayCommand]
    private async Task LoginAsync(string password)
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(password))
        {
            ErrorMessage = "Please enter both username and password";
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
                        ExpiresAt = DateTime.UtcNow.AddHours(1) // Remember for 1 hour
                    };
                    _secureStorage.SaveSecureData<RememberedLogin>(REMEMBERED_LOGIN_KEY, rememberedLogin);
                }
                else
                {
                    _secureStorage.SaveSecureData<RememberedLogin>(REMEMBERED_LOGIN_KEY, null);
                }

                await App.GetService<IActivationService>().ActivateAsync(null!);
                LoginSuccessful?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                ErrorMessage = error ?? "Login failed. Please try again.";
            }
        }
        catch (System.Exception ex)
        {
            ErrorMessage = $"An error occurred: {ex.Message}";
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