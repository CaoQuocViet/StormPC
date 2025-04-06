using Microsoft.UI.Xaml.Controls;
using StormPC.ViewModels.Shell;

namespace StormPC.Views.Shell;

public sealed partial class SearchDialog : UserControl
{
    public SearchDialogViewModel ViewModel { get; }

    public SearchDialog()
    {
        ViewModel = App.GetService<SearchDialogViewModel>();
        InitializeComponent();
    }

    private async void OnFilterSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem item && item.Tag is string tag)
        {
            await ViewModel.FilterByTypeAsync(tag);
        }
    }
} 