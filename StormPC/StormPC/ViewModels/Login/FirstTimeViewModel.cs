using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StormPC.Core.Services.Login;
using StormPC.Core.Contracts.Services;
using System.Threading.Tasks;
using System.Reflection;
using Windows.ApplicationModel;
using StormPC.Helpers;

namespace StormPC.ViewModels.Login;

public partial class FirstTimeViewModel : ObservableObject
{
    private readonly AuthenticationService _authService;
    private readonly IActivityLogService _activityLogService;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private string _backupKey = string.Empty;

    [ObservableProperty]
    private string _versionDescription;

    public event EventHandler? AccountCreated;

    public FirstTimeViewModel(AuthenticationService authService, IActivityLogService activityLogService)
    {
        _authService = authService;
        _activityLogService = activityLogService;
        _versionDescription = GetVersionDescription();
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

    [RelayCommand]
    private async Task CreateAdminAccountAsync((string password, string confirmPassword) passwords)
    {
        ErrorMessage = string.Empty;

        // Validation
        if (string.IsNullOrEmpty(Username))
        {
            ErrorMessage = "Username is required";
            await _activityLogService.LogActivityAsync("Registration", "Validation Error", 
                "Admin account creation failed - Empty username", "Error", Username);
            return;
        }

        if (string.IsNullOrEmpty(passwords.password))
        {
            ErrorMessage = "Password is required";
            await _activityLogService.LogActivityAsync("Registration", "Validation Error", 
                "Admin account creation failed - Empty password", "Error", Username);
            return;
        }

        if (passwords.password.Length < 8)
        {
            ErrorMessage = "Password must be at least 8 characters long";
            await _activityLogService.LogActivityAsync("Registration", "Validation Error", 
                "Admin account creation failed - Password too short", "Error", Username);
            return;
        }

        if (passwords.password != passwords.confirmPassword)
        {
            ErrorMessage = "Passwords do not match";
            await _activityLogService.LogActivityAsync("Registration", "Validation Error", 
                "Admin account creation failed - Passwords don't match", "Error", Username);
            return;
        }

        try
        {
            var success = await _authService.CreateAdminAccount(Username, passwords.password);
            if (success)
            {
                // Generate backup key
                BackupKey = await _authService.GenerateBackupKeyAsync();
                await _activityLogService.LogActivityAsync("Registration", "Admin Account Created", 
                    $"Admin account created successfully for user: {Username}", "Success", Username);
                AccountCreated?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                ErrorMessage = "Failed to create admin account. Please try again.";
                await _activityLogService.LogActivityAsync("Registration", "Creation Error", 
                    "Admin account creation failed - Unknown error", "Error", Username);
            }
        }
        catch (System.Exception ex)
        {
            ErrorMessage = $"An error occurred: {ex.Message}";
            await _activityLogService.LogActivityAsync("Registration", "System Error", 
                $"Admin account creation failed - {ex.Message}", "Error", Username);
        }
    }
}