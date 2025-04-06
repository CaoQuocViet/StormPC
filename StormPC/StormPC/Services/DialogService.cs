using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using StormPC.Contracts.Services;
using StormPC.ViewModels.Shell;
using StormPC.Views.Shell;

namespace StormPC.Services;

public class DialogService : IDialogService
{
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

        // Trigger search with the query
        await searchDialog.ViewModel.SearchCommand.ExecuteAsync(null);

        await dialog.ShowAsync();
    }
} 