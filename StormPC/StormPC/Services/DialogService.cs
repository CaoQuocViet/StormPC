using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using StormPC.Contracts.Services;
using StormPC.ViewModels.Shell;
using StormPC.Views.Shell;

namespace StormPC.Services;

/// <summary>
/// Dịch vụ quản lý và hiển thị các hộp thoại
/// </summary>
public class DialogService : IDialogService
{
    /// <summary>
    /// Hiển thị hộp thoại tìm kiếm với từ khóa đã nhập
    /// </summary>
    public async Task ShowSearchDialogAsync(string searchQuery)
    {
        var searchDialog = new SearchDialog();
        searchDialog.ViewModel.SearchQuery = searchQuery;
        
        var dialog = new ContentDialog
        {
            Title = "Tìm kiếm",
            CloseButtonText = "Đóng",
            DefaultButton = ContentDialogButton.Close,
            Content = searchDialog,
            XamlRoot = App.MainWindow.Content.XamlRoot,

        };

        // Kích hoạt tìm kiếm với từ khóa đã nhập
        await searchDialog.ViewModel.SearchCommand.ExecuteAsync(null);

        await dialog.ShowAsync();
    }
}