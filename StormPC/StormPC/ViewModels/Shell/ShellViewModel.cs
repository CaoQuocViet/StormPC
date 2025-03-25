using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using StormPC.Contracts;
using StormPC.Helpers;
using StormPC.ViewModels.Dashboard;
using StormPC.ViewModels.BaseData;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace StormPC.ViewModels.Shell;

public partial class ShellViewModel : ObservableRecipient
{
    private readonly INavigationService _navigationService;
    private readonly INavigationViewService _navigationViewService;
    private readonly ILastPageService _lastPageService;

    [ObservableProperty]
    private bool _isBackEnabled;

    [ObservableProperty]
    private object? _selected;

    public IList<object>? MenuItems => _navigationViewService.MenuItems;

    public object? SettingsItem => _navigationViewService.SettingsItem;

    public ShellViewModel(INavigationService navigationService, INavigationViewService navigationViewService, ILastPageService lastPageService)
    {
        _navigationService = navigationService;
        _navigationViewService = navigationViewService;
        _lastPageService = lastPageService;

        _navigationService.Navigated += OnNavigated;
    }

    private async void OnNavigated(object sender, NavigationEventArgs e)
    {
        IsBackEnabled = _navigationService.CanGoBack;

        if (e.SourcePageType != null)
        {
            Selected = _navigationViewService.GetSelectedItem(e.SourcePageType);
            
            // Get the corresponding ViewModel type name
            var viewModelTypeName = e.SourcePageType.FullName!.Replace("Views", "ViewModels").Replace("Page", "ViewModel");
            Debug.WriteLine($"Saving last page: {viewModelTypeName}");
            await _lastPageService.SaveLastPageAsync(viewModelTypeName);
        }
    }

    [RelayCommand]
    private void GoBack()
    {
        if (_navigationService.CanGoBack)
        {
            _navigationService.GoBack();
        }
    }

    public async Task InitializeAsync()
    {
        var lastPage = await _lastPageService.GetLastPageAsync();
        Debug.WriteLine($"Retrieved last page: {lastPage}");

        if (!string.IsNullOrEmpty(lastPage))
        {
            Debug.WriteLine($"Attempting to navigate to: {lastPage}");
            _navigationService.NavigateTo(lastPage);
        }
        else
        {
            Debug.WriteLine("No last page found, navigating to default page");
            _navigationService.NavigateTo(typeof(InventoryReportViewModel).FullName!);
        }
    }

    public void UnregisterEvents()
    {
        _navigationService.Navigated -= OnNavigated;
    }
} 