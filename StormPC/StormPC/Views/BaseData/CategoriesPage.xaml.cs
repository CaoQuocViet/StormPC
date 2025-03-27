using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using StormPC.ViewModels.BaseData;
using CommunityToolkit.WinUI.UI.Controls;
using StormPC.Helpers.UI;
using StormPC.Core.Models.Products.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace StormPC.Views.BaseData;

public sealed partial class CategoriesPage : Page
{
    public CategoriesViewModel ViewModel { get; }
    private List<CategoryDisplayDto> _originalCategories;
    // Keeps track of the currently active DataGrid
    private DataGrid _activeDataGrid;

    public CategoriesPage()
    {
        this.InitializeComponent();
        ViewModel = App.GetService<CategoriesViewModel>();
        _originalCategories = new List<CategoryDisplayDto>();
    }

    private async void Page_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        await ViewModel.InitializeAsync();
        
        // Store original data for sorting operations
        _originalCategories = ViewModel.Categories.ToList();
        
        // Make sure multi-column sorting is disabled when page loads
        DataGridSortingHelper.SetMultiColumnSortMode(false);
        
        // Get reference to MultiColumnSortToggle for this view
        var toggleButton = FindName("MultiColumnSortToggle") as ToggleButton;
        if (toggleButton != null)
        {
            toggleButton.IsChecked = false;
        }
    }
    
    private void DataGrid_Sorting(object sender, DataGridColumnEventArgs e)
    {
        var dataGrid = sender as DataGrid;
        if (dataGrid == null) return;
        
        // Store reference to active DataGrid
        _activeDataGrid = dataGrid;
        
        // Get up-to-date original list if needed
        if (!_originalCategories.Any() || _originalCategories.Count != ViewModel.Categories.Count)
        {
            _originalCategories = ViewModel.Categories.ToList();
        }
        
        // Process sorting using the helper
        var sortedItems = DataGridSortingHelper.ProcessSorting(dataGrid, e, _originalCategories);
        
        // Update the view model with sorted items
        ViewModel.Categories.Clear();
        foreach (var item in sortedItems)
        {
            ViewModel.Categories.Add(item);
        }
    }
    
    private void MultiColumnSortToggle_Checked(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton toggleButton)
        {
            bool isMultiColumnMode = toggleButton.IsChecked ?? false;
            
            // Enable or disable multi-column sorting based on toggle state
            DataGridSortingHelper.SetMultiColumnSortMode(isMultiColumnMode);
            
            // Find the active DataGrid
            var dataGrid = _activeDataGrid ?? FindName("CategoriesDataGrid") as DataGrid;
            
            // If turning off multi-column sort and we have a DataGrid reference
            if (!isMultiColumnMode && dataGrid != null)
            {
                // Force single column mode using the helper method
                DataGridSortingHelper.ForceSingleColumnMode(dataGrid);
                
                // Re-sort the collection (if needed)
                var primarySortColumn = dataGrid.Columns.FirstOrDefault(col => col.SortDirection != null);
                if (primarySortColumn != null)
                {
                    DataGrid_Sorting(dataGrid, new DataGridColumnEventArgs(primarySortColumn));
                }
            }
        }
    }
} 