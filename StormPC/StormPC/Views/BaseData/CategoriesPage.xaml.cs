using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Navigation;
using StormPC.ViewModels.BaseData;
using CommunityToolkit.WinUI.UI.Controls;
using StormPC.Helpers.UI;
using StormPC.Core.Models.Products.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System;
using System.Diagnostics;

namespace StormPC.Views.BaseData;

public sealed partial class CategoriesPage : Page
{
    private CategoriesViewModel _viewModel;
    public CategoriesViewModel ViewModel 
    { 
        get => _viewModel; 
        private set => _viewModel = value; 
    }
    private List<CategoryDisplayDto> _originalCategories;
    // Keeps track of the currently active DataGrid
    private DataGrid? _activeDataGrid;

    public CategoriesPage()
    {
        this.InitializeComponent();
        ViewModel = App.GetService<CategoriesViewModel>();
        _originalCategories = new List<CategoryDisplayDto>();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        try 
        {
            base.OnNavigatedTo(e);
            
            // Đảm bảo ViewModel đã được khởi tạo trước
            if (ViewModel == null)
            {
                ViewModel = App.GetService<CategoriesViewModel>();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in OnNavigatedTo: {ex.Message}");
        }
    }

    private void Page_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // Đảm bảo DataGrid được khởi tạo đúng cách trước khi load dữ liệu
            if (CategoriesDataGrid != null)
            {
                // Tránh data binding issues trong quá trình load
                CategoriesDataGrid.ItemsSource = null;
                
                // Load dữ liệu sau khi UI đã được render
                DispatcherQueue.TryEnqueue(async () =>
                {
                    try
                    {
                        // Sử dụng phương thức đúng của ViewModel để tải dữ liệu
                        await ViewModel.InitializeAsync();
                        CategoriesDataGrid.ItemsSource = ViewModel.Categories;
                    }
                    catch (Exception innerEx)
                    {
                        Debug.WriteLine($"Error loading categories: {innerEx.Message}");
                    }
                });
                
                // Đảm bảo trạng thái MultiColumnSortToggle ban đầu
                if (MultiColumnSortToggle != null)
                {
                    MultiColumnSortToggle.IsChecked = false;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error in Page_Loaded: {ex.Message}");
        }
    }

    private async void AddButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Đảm bảo ViewModel đã được khởi tạo
            if (ViewModel != null)
            {
                // Initialize with empty strings and trigger property change
                var newCategory = new CategoryDisplayDto
                {
                    CategoryName = string.Empty,
                    Description = string.Empty
                };
                
                ViewModel.EditingCategory = newCategory;
                CategoryDialog.Title = "Thêm loại sản phẩm mới";
                CategoryDialog.XamlRoot = this.XamlRoot;
                var result = await CategoryDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    var success = await ViewModel.AddCategoryAsync(ViewModel.EditingCategory);
                    if (!success)
                    {
                        var errorDialog = new ContentDialog
                        {
                            Title = "Lỗi",
                            Content = "Không thể thêm loại sản phẩm. Vui lòng thử lại sau.",
                            CloseButtonText = "Đóng",
                            XamlRoot = this.XamlRoot
                        };
                        await errorDialog.ShowAsync();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in AddButton_Click: {ex.Message}");
        }
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var button = sender as Button;
            if (button?.DataContext is CategoryDisplayDto category)
            {
                ViewModel.EditingCategory = new CategoryDisplayDto
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    Description = category.Description
                };

                CategoryDialog.Title = "Sửa loại sản phẩm";
                var result = await CategoryDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    var success = await ViewModel.UpdateCategoryAsync(ViewModel.EditingCategory);
                    if (!success)
                    {
                        var errorDialog = new ContentDialog
                        {
                            Title = "Lỗi",
                            Content = "Không thể cập nhật loại sản phẩm. Vui lòng thử lại sau.",
                            CloseButtonText = "Đóng",
                            XamlRoot = this.XamlRoot
                        };
                        await errorDialog.ShowAsync();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in EditButton_Click: {ex.Message}");
        }
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var button = sender as Button;
            if (button?.DataContext is CategoryDisplayDto category)
            {
                DeleteConfirmationText.Text = $"Bạn có chắc chắn muốn xóa loại sản phẩm \"{category.CategoryName}\" không?";
                var result = await DeleteConfirmationDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    var (success, message) = await ViewModel.DeleteCategoryAsync(category.CategoryID);
                    var dialog = new ContentDialog
                    {
                        Title = success ? "Thành công" : "Lỗi",
                        Content = message,
                        CloseButtonText = "Đóng",
                        XamlRoot = this.XamlRoot
                    };
                    await dialog.ShowAsync();
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in DeleteButton_Click: {ex.Message}");
        }
    }

    private void CategoryDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        // Validation is handled by the ViewModel through IsValidCategoryInput
        if (!ViewModel.IsValidCategoryInput)
        {
            args.Cancel = true;
        }
    }

    private void DataGrid_Sorting(object sender, DataGridColumnEventArgs e)
    {
        try
        {
            if (sender is DataGrid dataGrid && e.Column?.Tag != null)
            {
                var propertyName = e.Column.Tag.ToString();
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
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in DataGrid_Sorting: {ex.Message}");
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
            System.Diagnostics.Debug.WriteLine($"Error in MultiColumnSortToggle_Checked: {ex.Message}");
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
            System.Diagnostics.Debug.WriteLine($"Error in PaginationControl_PageChanged: {ex.Message}");
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
            System.Diagnostics.Debug.WriteLine($"Error in PaginationControl_PageSizeChanged: {ex.Message}");
        }
    }
} 