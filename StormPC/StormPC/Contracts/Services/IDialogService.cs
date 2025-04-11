using Microsoft.UI.Xaml.Controls;

namespace StormPC.Contracts.Services;

public interface IDialogService
{
    /// <summary>
    /// Hiển thị hộp thoại tìm kiếm
    /// </summary>
    Task ShowSearchDialogAsync(string searchQuery);
} 