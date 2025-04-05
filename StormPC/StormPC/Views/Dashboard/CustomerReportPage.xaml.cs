using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using CommunityToolkit.WinUI.UI.Controls;
using StormPC.ViewModels.Dashboard;
using StormPC.Core.Models.Customers.Dtos;
using StormPC.Helpers.UI;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using LiveChartsCore.SkiaSharpView.WinUI;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace StormPC.Views.Dashboard;

public sealed partial class CustomerReportPage : Page
{
    public CustomerReportViewModel ViewModel { get; }
    private List<CustomerDisplayDto> _originalCustomers;
    private DataGrid? _activeDataGrid;
    private bool _isActionButtonClick;

    public CustomerReportPage()
    {
        ViewModel = App.GetService<CustomerReportViewModel>();
        InitializeComponent();
        _originalCustomers = new List<CustomerDisplayDto>();
        _isActionButtonClick = false;
    }

    private async void CustomerReportPage_Loaded(object sender, RoutedEventArgs e)
    {
        if (ViewModel != null)
        {
            await ViewModel.LoadDataAsync();
        }

        // Configure tooltips for CartesianChart
        if (this.FindName("PurchaseTrendsChart") is CartesianChart chart)
        {
            chart.TooltipTextSize = 14;
            chart.TooltipPosition = LiveChartsCore.Measure.TooltipPosition.Top;
            chart.TooltipBackgroundPaint = new SolidColorPaint(new SKColor(30, 30, 30));
            chart.TooltipTextPaint = new SolidColorPaint(SKColors.White);
        }

        // Store original data for sorting operations
        _originalCustomers = ViewModel.Customers.ToList();
        
        // Make sure multi-column sorting is disabled when page loads
        DataGridSortingHelper.SetMultiColumnSortMode(false);
        
        // Get reference to MultiColumnSortToggle for this view
        var toggleButton = FindName("MultiColumnSortToggle") as ToggleButton;
        if (toggleButton != null)
        {
            toggleButton.IsChecked = false;
        }
    }

    private async void AddCustomerButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new ContentDialog
        {
            Title = "Thêm khách hàng mới",
            PrimaryButtonText = "Lưu",
            CloseButtonText = "Hủy",
            DefaultButton = ContentDialogButton.Primary,
            Content = new ContentControl
            {
                ContentTemplate = Resources["CustomerDialogTemplate"] as DataTemplate,
                DataContext = await ViewModel.CreateCustomerDialogViewModel()
            },
            XamlRoot = Content.XamlRoot
        };

        var result = await dialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            var dialogViewModel = (dialog.Content as ContentControl)?.DataContext as CustomerDialogViewModel;
            if (dialogViewModel != null)
            {
                await ViewModel.AddCustomerAsync(dialogViewModel);
            }
        }
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        _isActionButtonClick = true;
        if (sender is Button button && button.DataContext is CustomerDisplayDto customer)
        {
            var dialog = new ContentDialog
            {
                Title = $"Sửa thông tin khách hàng",
                PrimaryButtonText = "Lưu",
                CloseButtonText = "Hủy",
                DefaultButton = ContentDialogButton.Primary,
                Content = new ContentControl
                {
                    ContentTemplate = Resources["CustomerDialogTemplate"] as DataTemplate,
                    DataContext = await ViewModel.CreateCustomerDialogViewModel(customer.CustomerID)
                },
                XamlRoot = Content.XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var dialogViewModel = (dialog.Content as ContentControl)?.DataContext as CustomerDialogViewModel;
                if (dialogViewModel != null)
                {
                    await ViewModel.UpdateCustomerAsync(customer.CustomerID, dialogViewModel);
                }
            }
        }
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        _isActionButtonClick = true;
        if (sender is Button button && button.DataContext is CustomerDisplayDto customer)
        {
            var dialog = new ContentDialog
            {
                Title = "Xác nhận xóa",
                Content = $"Bạn có chắc chắn muốn xóa khách hàng {customer.FullName}?",
                PrimaryButtonText = "Xóa",
                CloseButtonText = "Hủy",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = Content.XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                await ViewModel.DeleteCustomerAsync(customer.CustomerID);
            }
        }
    }

    private void DataGrid_Sorting(object sender, DataGridColumnEventArgs e)
    {
        var dataGrid = sender as DataGrid;
        if (dataGrid == null) return;
        
        // Store reference to active DataGrid
        _activeDataGrid = dataGrid;
        
        // Get property name from Tag property
        var propertyName = e.Column.Tag?.ToString();
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
                    var tagString = column.Tag.ToString();
                    if (!string.IsNullOrEmpty(tagString))
                    {
                        sortProperties.Add(tagString);
                        sortDirections.Add(column.SortDirection == DataGridSortDirection.Ascending 
                            ? ListSortDirection.Ascending 
                            : ListSortDirection.Descending);
                    }
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
            var isMultiColumnMode = toggleButton.IsChecked ?? false;
            
            // Enable or disable multi-column sorting based on toggle state
            DataGridSortingHelper.SetMultiColumnSortMode(isMultiColumnMode);
            
            // Find the active DataGrid
            var dataGrid = _activeDataGrid ?? FindName("CustomersDataGrid") as DataGrid;
            
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
                    var tagString = primarySortColumn.Tag.ToString();
                    if (!string.IsNullOrEmpty(tagString))
                    {
                        var direction = primarySortColumn.SortDirection == DataGridSortDirection.Ascending
                            ? ListSortDirection.Ascending
                            : ListSortDirection.Descending;
                        ViewModel.UpdateSorting(
                            new List<string> { tagString },
                            new List<ListSortDirection> { direction }
                        );
                    }
                }
                else
                {
                    // If no primary sort column, clear all sorting
                    ViewModel.UpdateSorting(new List<string>(), new List<ListSortDirection>());
                }
            }
        }
    }

    private void PaginationControl_PageChanged(object sender, int e)
    {
        ViewModel.CurrentPage = e;
    }

    private void PaginationControl_PageSizeChanged(object sender, int e)
    {
        ViewModel.PageSize = e;
    }
} 