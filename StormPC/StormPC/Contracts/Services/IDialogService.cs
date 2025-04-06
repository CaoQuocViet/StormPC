using Microsoft.UI.Xaml.Controls;

namespace StormPC.Contracts.Services;

public interface IDialogService
{
    Task ShowSearchDialogAsync(string searchQuery);
} 