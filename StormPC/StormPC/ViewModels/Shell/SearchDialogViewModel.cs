using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StormPC.Core.Models.System.Search;
using StormPC.Core.Services.System;

namespace StormPC.ViewModels.Shell;

public partial class SearchDialogViewModel : ObservableObject
{
    private readonly ISearchService _searchService;

    [ObservableProperty]
    private string _searchQuery = string.Empty;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string _selectedType = "All";

    [ObservableProperty]
    private int _currentPage = 1;

    [ObservableProperty]
    private int _totalPages;

    [ObservableProperty]
    private ObservableCollection<SearchResult> _searchResults = new();

    [ObservableProperty]
    private Dictionary<string, int> _resultCounts = new();

    public SearchDialogViewModel(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery))
            return;

        IsLoading = true;
        try
        {
            SearchResults results;
            if (SelectedType == "All")
            {
                results = await _searchService.SearchAsync(SearchQuery, CurrentPage);
            }
            else
            {
                results = await _searchService.SearchByTypeAsync(SearchQuery, SelectedType, CurrentPage);
            }

            SearchResults.Clear();
            foreach (var result in results.Items)
            {
                SearchResults.Add(result);
            }

            ResultCounts = results.TypeCounts;
            TotalPages = (int)Math.Ceiling(results.TotalCount / 20.0);
        }
        catch (Exception ex)
        {
            // Log the error
            System.Diagnostics.Debug.WriteLine($"Search error: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task NavigateToPageAsync(int page)
    {
        if (page < 1 || page > TotalPages)
            return;

        CurrentPage = page;
        await SearchAsync();
    }

    [RelayCommand]
    public async Task FilterByTypeAsync(string type)
    {
        SelectedType = type;
        await SearchAsync();
    }

    partial void OnSearchQueryChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            SearchResults.Clear();
            ResultCounts.Clear();
            TotalPages = 0;
            CurrentPage = 1;
        }
    }
} 