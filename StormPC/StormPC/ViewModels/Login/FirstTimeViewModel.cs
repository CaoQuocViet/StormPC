using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StormPC.Core.Services.Login;
using System.Threading.Tasks;

namespace StormPC.ViewModels.Login;

public partial class FirstTimeViewModel : ObservableObject
{
    private readonly AuthenticationService _authService;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private string _backupKey = string.Empty;

    public event EventHandler? AccountCreated;

    public FirstTimeViewModel(AuthenticationService authService)
    {
        _authService = authService;
    }

    [RelayCommand]
    private async Task CreateAdminAccountAsync((string password, string confirmPassword) passwords)
    {
        ErrorMessage = string.Empty;

        // Validation
        if (string.IsNullOrEmpty(Username))
        {
            ErrorMessage = "Username is required";
            return;
        }

        if (string.IsNullOrEmpty(passwords.password))
        {
            ErrorMessage = "Password is required";
            return;
        }

        if (passwords.password.Length < 8)
        {
            ErrorMessage = "Password must be at least 8 characters long";
            return;
        }

        if (passwords.password != passwords.confirmPassword)
        {
            ErrorMessage = "Passwords do not match";
            return;
        }

        try
        {
            var success = await _authService.CreateAdminAccount(Username, passwords.password);
            if (success)
            {
                // Generate backup key
                BackupKey = await _authService.GenerateBackupKeyAsync();
                AccountCreated?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                ErrorMessage = "Failed to create admin account. Please try again.";
            }
        }
        catch (System.Exception ex)
        {
            ErrorMessage = $"An error occurred: {ex.Message}";
        }
    }
}