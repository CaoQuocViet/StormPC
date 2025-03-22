 using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using StormPC.Contracts.Services;
using StormPC.Core.Helpers.Security;
using StormPC.Core.Models.Login;
using StormPC.Core.Services.Security;

namespace StormPC.Services.Login;

public class LoginService : ILoginService
{
    private readonly ISecureStorageService _secureStorage;
    private readonly IPasswordHashService _passwordHash;
    private readonly ISessionService _sessionService;
    private readonly INavigationService _navigationService;
    private int _failedAttempts;
    private DateTime _lastFailedAttempt;

    public LoginService(
        ISecureStorageService secureStorage,
        IPasswordHashService passwordHash,
        ISessionService sessionService,
        INavigationService navigationService)
    {
        _secureStorage = secureStorage;
        _passwordHash = passwordHash;
        _sessionService = sessionService;
        _navigationService = navigationService;
    }

    public async Task<bool> IsFirstTimeSetupAsync()
    {
        var loginInfo = await _secureStorage.LoadLoginInfoAsync();
        return loginInfo == null;
    }

    public async Task<bool> SetupAdminAccountAsync(string username, string password, bool useWindowsHello)
    {
        if (!_passwordHash.IsPasswordStrong(password))
        {
            return false;
        }

        var passwordHash = await _passwordHash.HashPasswordAsync(password);
        var loginInfo = new LoginInfo
        {
            Username = username,
            PasswordHash = passwordHash,
            UseWindowsHello = useWindowsHello,
            CreatedAt = DateTime.Now,
            LastLoginAt = DateTime.Now
        };

        await _secureStorage.SaveLoginInfoAsync(loginInfo);

        if (useWindowsHello)
        {
            await EnableWindowsHelloAsync();
        }

        // Tạo và lưu backup key
        await GenerateBackupKeyAsync();

        return true;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        if (IsRateLimited()) return false;

        var loginInfo = await _secureStorage.LoadLoginInfoAsync();
        if (loginInfo == null) return false;

        if (loginInfo.Username != username)
        {
            await HandleFailedLoginAsync();
            return false;
        }

        var isValid = await _passwordHash.VerifyPasswordAsync(password, loginInfo.PasswordHash);
        if (!isValid)
        {
            await HandleFailedLoginAsync();
            return false;
        }

        await HandleSuccessfulLoginAsync(loginInfo);
        return true;
    }

    public async Task<bool> LoginWithWindowsHelloAsync()
    {
        var loginInfo = await _secureStorage.LoadLoginInfoAsync();
        if (loginInfo == null || !loginInfo.UseWindowsHello) return false;

        var isVerified = await WindowsHelloHelper.VerifyWindowsHelloAsync();
        if (!isVerified) return false;

        await HandleSuccessfulLoginAsync(loginInfo);
        return true;
    }

    public async Task<bool> LoginWithBackupKeyAsync(string backupKey)
    {
        var storedBackup = await _secureStorage.LoadBackupKeyAsync();
        if (storedBackup == null) return false;

        try
        {
            var decrypted = CryptoHelper.DecryptString(storedBackup.EncryptedData, backupKey);
            var loginInfo = await _secureStorage.LoadLoginInfoAsync();
            if (loginInfo == null) return false;

            await HandleSuccessfulLoginAsync(loginInfo);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> IsLoggedInAsync()
    {
        return await _sessionService.HasValidSessionAsync();
    }

    public async Task LogoutAsync()
    {
        await _sessionService.InvalidateSessionAsync();
        _navigationService.NavigateTo("Login");
    }

    public async Task<bool> ChangePasswordAsync(string currentPassword, string newPassword)
    {
        if (!_passwordHash.IsPasswordStrong(newPassword))
        {
            return false;
        }

        var loginInfo = await _secureStorage.LoadLoginInfoAsync();
        if (loginInfo == null) return false;

        var isValid = await _passwordHash.VerifyPasswordAsync(currentPassword, loginInfo.PasswordHash);
        if (!isValid) return false;

        loginInfo.PasswordHash = await _passwordHash.HashPasswordAsync(newPassword);
        await _secureStorage.SaveLoginInfoAsync(loginInfo);

        // Tạo backup key mới
        await GenerateBackupKeyAsync();

        return true;
    }

    public async Task<string> GenerateBackupKeyAsync()
    {
        var key = CryptoHelper.GenerateRandomKey();
        var loginInfo = await _secureStorage.LoadLoginInfoAsync();
        if (loginInfo == null) throw new InvalidOperationException("No login info found");

        var backupData = $"{loginInfo.Username}:{loginInfo.PasswordHash}";
        var encrypted = CryptoHelper.EncryptString(backupData, key);

        var backup = new BackupKey
        {
            Key = key,
            EncryptedData = encrypted,
            CreatedAt = DateTime.Now
        };

        await _secureStorage.SaveBackupKeyAsync(backup);
        return key;
    }

    public async Task<bool> IsWindowsHelloAvailableAsync()
    {
        return await WindowsHelloHelper.IsWindowsHelloAvailableAsync();
    }

    public async Task<bool> EnableWindowsHelloAsync()
    {
        var loginInfo = await _secureStorage.LoadLoginInfoAsync();
        if (loginInfo == null) return false;

        var isRegistered = await WindowsHelloHelper.RegisterWindowsHelloAsync();
        if (!isRegistered) return false;

        loginInfo.UseWindowsHello = true;
        await _secureStorage.SaveLoginInfoAsync(loginInfo);
        return true;
    }

    public async Task<bool> DisableWindowsHelloAsync()
    {
        var loginInfo = await _secureStorage.LoadLoginInfoAsync();
        if (loginInfo == null) return false;

        var isRemoved = await WindowsHelloHelper.RemoveWindowsHelloAsync();
        if (!isRemoved) return false;

        loginInfo.UseWindowsHello = false;
        await _secureStorage.SaveLoginInfoAsync(loginInfo);
        return true;
    }

    private bool IsRateLimited()
    {
        if (_failedAttempts >= 5 && DateTime.Now - _lastFailedAttempt < TimeSpan.FromMinutes(5))
        {
            return true;
        }
        return false;
    }

    private async Task HandleFailedLoginAsync()
    {
        _failedAttempts++;
        _lastFailedAttempt = DateTime.Now;
    }

    private async Task HandleSuccessfulLoginAsync(LoginInfo loginInfo)
    {
        _failedAttempts = 0;
        loginInfo.LastLoginAt = DateTime.Now;
        await _secureStorage.SaveLoginInfoAsync(loginInfo);
        await _sessionService.CreateSessionAsync();
    }
}