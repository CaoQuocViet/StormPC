using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using StormPC.Core.Services.Login;
using System.Threading.Tasks;
using StormPC.Contracts.Services;
using System;

namespace StormPC.ViewModels.Login;

public partial class LoginViewModel : ObservableObject
{
    private readonly AuthenticationService _authService;

    public event EventHandler? LoginSuccessful;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    public LoginViewModel(AuthenticationService authService)
    {
        _authService = authService;
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