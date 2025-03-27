using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using CommunityToolkit.WinUI.UI.Controls;
using StormPC.ViewModels.Orders;
using StormPC.Core.Models.Orders.Dtos;
using StormPC.Contracts;
using StormPC.Helpers.UI;
using System.Collections.Generic;
using System.Linq;

namespace StormPC.Views.Orders;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class OrderListPage : Page
{
    public OrderListViewModel ViewModel { get; }
    private readonly INavigationService _navigationService;
    private List<OrderDisplayDto> _originalOrders;
    // Keeps track of the currently active DataGrid
    private DataGrid _activeDataGrid;

    public OrderListPage()
    {
        InitializeComponent();
        ViewModel = App.GetService<OrderListViewModel>();
        _navigationService = App.GetService<INavigationService>();
        _originalOrders = new List<OrderDisplayDto>();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.InitializeAsync();
        
        // Store original data for sorting operations
        _originalOrders = ViewModel.Orders.ToList();
        
        // Make sure multi-column sorting is disabled when page loads
        DataGridSortingHelper.SetMultiColumnSortMode(false);
        
        // Get reference to MultiColumnSortToggle for this view
        var toggleButton = FindName("MultiColumnSortToggle") as ToggleButton;
        if (toggleButton != null)
        {
            toggleButton.IsChecked = false;
        }
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var dataGrid = sender as DataGrid;
        if (dataGrid?.SelectedItem is OrderDisplayDto selectedOrder)
        {
            _navigationService.NavigateTo("StormPC.ViewModels.Orders.OrderDetailViewModel", selectedOrder.OrderID);
        }
    }
    
    private void DataGrid_Sorting(object sender, DataGridColumnEventArgs e)
    {
        var dataGrid = sender as DataGrid;
        if (dataGrid == null) return;
        
        // Store reference to active DataGrid
        _activeDataGrid = dataGrid;
        
        // Get up-to-date original list if needed
        if (!_originalOrders.Any() || _originalOrders.Count != ViewModel.Orders.Count)
        {
            _originalOrders = ViewModel.Orders.ToList();
        }
        
        // Process sorting using the helper
        var sortedItems = DataGridSortingHelper.ProcessSorting(dataGrid, e, _originalOrders);
        
        // Update the view model with sorted items
        ViewModel.Orders.Clear();
        foreach (var item in sortedItems)
        {
            ViewModel.Orders.Add(item);
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
            var dataGrid = _activeDataGrid ?? FindName("OrdersDataGrid") as DataGrid;
            
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