using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Navigation;
using CommunityToolkit.WinUI.UI.Controls;
using StormPC.ViewModels.Orders;
using StormPC.Core.Models.Orders.Dtos;
using StormPC.Contracts;
using StormPC.Helpers.UI;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using StormPC.Contracts.Services;
using System;
using System.Diagnostics;

namespace StormPC.Views.Orders;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class OrderListPage : Page
{
    private OrderListViewModel _viewModel;
    public OrderListViewModel ViewModel 
    { 
        get => _viewModel; 
        private set => _viewModel = value; 
    }
    private readonly INavigationService _navigationService;
    private List<OrderDisplayDto> _originalOrders;
    // Keeps track of the currently active DataGrid
    private DataGrid? _activeDataGrid;
    private bool _isActionButtonClick;

    public OrderListPage()
    {
        InitializeComponent();
        ViewModel = App.GetService<OrderListViewModel>();
        _navigationService = App.GetService<INavigationService>();
        _originalOrders = new List<OrderDisplayDto>();
        _isActionButtonClick = false;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        try 
        {
            base.OnNavigatedTo(e);
            
            // Đảm bảo DataContext đã được khởi tạo
            if (ViewModel == null)
            {
                // Khởi tạo ViewModel từ DI hoặc tạo mới nếu cần
                ViewModel = App.GetService<OrderListViewModel>();
            }
        }
        catch (Exception ex)
        {
            // Log lỗi hoặc hiển thị thông báo
            Debug.WriteLine($"Error in OnNavigatedTo: {ex.Message}");
        }
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // Đảm bảo DataGrid được khởi tạo đúng cách trước khi load dữ liệu
            if (OrdersDataGrid != null)
            {
                // Tránh sử dụng binding trực tiếp cho Source trong quá trình load
                OrdersDataGrid.ItemsSource = null;
                
                // Load dữ liệu sau khi UI đã được render
                DispatcherQueue.TryEnqueue(async () =>
                {
                    try
                    {
                        await ViewModel.LoadOrdersAsync();
                        OrdersDataGrid.ItemsSource = ViewModel.Orders;
                    }
                    catch (Exception innerEx)
                    {
                        Debug.WriteLine($"Error loading data: {innerEx.Message}");
                    }
                });
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in Page_Loaded: {ex.Message}");
        }
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            // Nếu đang click nút trong cột action thì không navigate
            if (_isActionButtonClick)
            {
                _isActionButtonClick = false;
                return;
            }

            var dataGrid = sender as DataGrid;
            if (dataGrid?.SelectedItem is OrderDisplayDto selectedOrder)
            {
                _navigationService.NavigateTo("StormPC.ViewModels.Orders.OrderDetailViewModel", selectedOrder.OrderID);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in selection changed: {ex.Message}");
        }
    }
    
    private void DataGrid_Sorting(object sender, DataGridColumnEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in DataGrid_Sorting: {ex.Message}");
        }
    }

    private void MultiColumnSortToggle_Checked(object sender, RoutedEventArgs e)
    {
        try
        {
            if (sender is ToggleButton toggleButton)
            {
                var isMultiColumnMode = toggleButton.IsChecked ?? false;
                
                // Enable or disable multi-column sorting based on toggle state
                DataGridSortingHelper.SetMultiColumnSortMode(isMultiColumnMode);
                
                // Find the active DataGrid
                var dataGrid = _activeDataGrid ?? FindName("OrdersDataGrid") as DataGrid;
                
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
                        // No sorting
                        ViewModel.UpdateSorting(new List<string>(), new List<ListSortDirection>());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in sort toggle: {ex.Message}");
        }
    }

    private void PaginationControl_PageChanged(object sender, int page)
    {
        try
        {
            if (ViewModel != null)
            {
                ViewModel.CurrentPage = page;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in page changed: {ex.Message}");
        }
    }

    private void PaginationControl_PageSizeChanged(object sender, int pageSize)
    {
        try
        {
            if (ViewModel != null)
            {
                ViewModel.PageSize = pageSize;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in page size changed: {ex.Message}");
        }
    }

    private async void AddButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new ContentDialog
        {
            Title = "Thêm đơn hàng mới",
            PrimaryButtonText = "Thêm",
            CloseButtonText = "Hủy",
            DefaultButton = ContentDialogButton.Primary,
            Content = new ContentControl
            {
                ContentTemplate = Resources["OrderDialogTemplate"] as DataTemplate,
                DataContext = await ViewModel.CreateNewOrderDialogViewModel()
            },
            XamlRoot = Content.XamlRoot
        };

        var result = await dialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            var dialogViewModel = (dialog.Content as ContentControl)?.DataContext as OrderDialogViewModel;
            if (dialogViewModel != null)
            {
                await ViewModel.AddOrderAsync(dialogViewModel);
            }
        }
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        _isActionButtonClick = true;
        if (sender is Button button && button.DataContext is OrderDisplayDto order)
        {
            var dialog = new ContentDialog
            {
                Title = $"Sửa đơn hàng #{order.OrderID}",
                PrimaryButtonText = "Lưu",
                CloseButtonText = "Hủy",
                DefaultButton = ContentDialogButton.Primary,
                Content = new ContentControl
                {
                    ContentTemplate = Resources["OrderDialogTemplate"] as DataTemplate,
                    DataContext = await ViewModel.CreateEditOrderDialogViewModel(order.OrderID)
                },
                XamlRoot = Content.XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var dialogViewModel = (dialog.Content as ContentControl)?.DataContext as OrderDialogViewModel;
                if (dialogViewModel != null)
                {
                    await ViewModel.UpdateOrderAsync(order.OrderID, dialogViewModel);
                }
            }
        }
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        _isActionButtonClick = true;
        if (sender is Button button && button.DataContext is OrderDisplayDto order)
        {
            var dialog = new ContentDialog
            {
                Title = "Xác nhận xóa",
                Content = $"Bạn có chắc chắn muốn xóa đơn hàng #{order.OrderID}?",
                PrimaryButtonText = "Xóa",
                CloseButtonText = "Hủy",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = Content.XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    var success = await ViewModel.DeleteOrderAsync(order.OrderID);
                    if (!success)
                    {
                        var errorDialog = new ContentDialog
                        {
                            Title = "Không thể xóa",
                            Content = "Chỉ có thể xóa đơn hàng có trạng thái 'Cancelled'.",
                            CloseButtonText = "Đóng",
                            XamlRoot = Content.XamlRoot
                        };
                        await errorDialog.ShowAsync();
                    }
                }
                catch
                {
                    var errorDialog = new ContentDialog
                    {
                        Title = "Lỗi",
                        Content = "Không thể xóa đơn hàng. Vui lòng thử lại sau.",
                        CloseButtonText = "Đóng",
                        XamlRoot = Content.XamlRoot
                    };
                    await errorDialog.ShowAsync();
                }
            }
        }
    }
} 