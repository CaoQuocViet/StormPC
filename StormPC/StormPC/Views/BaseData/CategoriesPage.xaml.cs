using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using StormPC.ViewModels.BaseData;
using CommunityToolkit.WinUI.UI.Controls;
using StormPC.Helpers.UI;
using StormPC.Core.Models.Products.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

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
        
        // Get property name from Tag property
        string propertyName = e.Column.Tag?.ToString();
        if (string.IsNullOrEmpty(propertyName))
            return;

        // Determine the new sort direction
        ListSortDirection direction;
        if (e.Column.SortDirection == null || e.Column.SortDirection == DataGridSortDirection.Descending)
            direction = ListSortDirection.Ascending;
        else
            direction = ListSortDirection.Descending;

        // Update the column's sort direction
        e.Column.SortDirection = direction == ListSortDirection.Ascending 
            ? DataGridSortDirection.Ascending 
            : DataGridSortDirection.Descending;

        var sortProperties = new List<string>();
        var sortDirections = new List<ListSortDirection>();

        if (DataGridSortingHelper.IsMultiColumnSortMode())
        {
            // In multi-column mode:
            // 1. Remove the current column if it exists in the sort list
            // 2. Add all other sorted columns first
            // 3. Add the current column last (highest priority)
            foreach (var column in dataGrid.Columns)
            {
                if (column.SortDirection != null && column.Tag != null && column != e.Column)
                {
                    sortProperties.Add(column.Tag.ToString());
                    sortDirections.Add(column.SortDirection == DataGridSortDirection.Ascending 
                        ? ListSortDirection.Ascending 
                        : ListSortDirection.Descending);
                }
            }
            
            // Add the current column last (highest priority)
            sortProperties.Add(propertyName);
            sortDirections.Add(direction);
        }
        else
        {
            // In single-column mode, clear other columns' sort directions
            foreach (var column in dataGrid.Columns)
            {
                if (column != e.Column)
                    column.SortDirection = null;
            }
            
            // Only sort by the clicked column
            sortProperties.Add(propertyName);
            sortDirections.Add(direction);
        }

        // Update sorting in ViewModel
        ViewModel.UpdateSorting(sortProperties, sortDirections);
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
                // Clear all sort directions except the primary one
                var primarySortColumn = dataGrid.Columns.FirstOrDefault(col => col.SortDirection != null);
                foreach (var column in dataGrid.Columns)
                {
                    if (column != primarySortColumn)
                    {
                        column.SortDirection = null;
                    }
                }
                
                // Update sorting in ViewModel
                if (primarySortColumn != null && primarySortColumn.Tag != null)
                {
                    var direction = primarySortColumn.SortDirection == DataGridSortDirection.Ascending
                        ? ListSortDirection.Ascending
                        : ListSortDirection.Descending;
                    ViewModel.UpdateSorting(
                        new List<string> { primarySortColumn.Tag.ToString() },
                        new List<ListSortDirection> { direction }
                    );
                }
                else
                {
                    // No sorting
                    ViewModel.UpdateSorting(new List<string>(), new List<ListSortDirection>());
                }
            }
        }
    }

    private void PaginationControl_PageChanged(object sender, int page)
    {
        ViewModel.LoadPage(page);
    }

    private void PaginationControl_PageSizeChanged(object sender, int pageSize)
    {
        ViewModel.PageSize = pageSize;
        ViewModel.LoadPage(1); // Reset to first page when changing page size
    }
} 