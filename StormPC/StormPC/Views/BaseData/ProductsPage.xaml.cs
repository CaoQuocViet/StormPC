using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Navigation;
using StormPC.Core.Models.Products.Dtos;
using StormPC.ViewModels.BaseData;
using System;
using System.Threading.Tasks;

namespace StormPC.Views.BaseData;

public sealed partial class ProductsPage : Page
{
    public ProductsViewModel ViewModel { get; }

    public ProductsPage()
    {
        this.InitializeComponent();
        ViewModel = App.GetService<ProductsViewModel>();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        await ViewModel.LoadBrandsAndCategories();
        await ViewModel.LoadProductsAsync();
    }

    private void PaginationControl_PageChanged(object sender, int e)
    {
        ViewModel.CurrentPage = e;
    }

    private void PaginationControl_PageSizeChanged(object sender, int e)
    {
        ViewModel.PageSize = e;
    }

    private void SortingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ComboBox comboBox)
        {
            ViewModel.SelectedSortIndex = comboBox.SelectedIndex;
        }
    }

    private void ProductsGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is GridView gridView && ViewModel.IsMultipleSelectionEnabled)
        {
            ViewModel.SelectedLaptops.Clear();
            foreach (var item in gridView.SelectedItems)
            {
                if (item is LaptopDisplayDto laptop)
                {
                    ViewModel.SelectedLaptops.Add(laptop);
                }
            }
        }
    }

    private async void AddButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new ContentDialog
        {
            Title = "Thêm laptop mới",
            XamlRoot = this.XamlRoot,
            PrimaryButtonText = "Thêm",
            CloseButtonText = "Hủy",
            DefaultButton = ContentDialogButton.Primary,
            MinWidth = 600 // Đặt chiều rộng tối thiểu cho dialog
        };

        var scrollViewer = new ScrollViewer
        {
            MaxHeight = 600, // Giới hạn chiều cao tối đa
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto
        };

        var stackPanel = new StackPanel { Spacing = 12, Margin = new Thickness(0, 12, 0, 0) };

        // Tên model
        stackPanel.Children.Add(new TextBlock { Text = "Tên mẫu laptop *" });
        var modelNameBox = new TextBox
        {
            PlaceholderText = "Ví dụ: MacBook Pro 14 inch",
            Text = ViewModel.ModelName
        };
        modelNameBox.TextChanged += (s, args) => { ViewModel.ModelName = modelNameBox.Text; };
        stackPanel.Children.Add(modelNameBox);

        // Thương hiệu
        stackPanel.Children.Add(new TextBlock { Text = "Thương hiệu *" });
        var brandCombo = new ComboBox
        {
            Width = double.NaN,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            ItemsSource = ViewModel.Brands,
            DisplayMemberPath = "BrandName",
            SelectedValuePath = "BrandID"
        };
        brandCombo.SelectionChanged += (s, args) =>
        {
            if (brandCombo.SelectedItem is StormPC.Core.Models.Products.Brand brand)
            {
                ViewModel.SelectedBrandId = brand.BrandID;
            }
        };
        stackPanel.Children.Add(brandCombo);

        // Danh mục
        stackPanel.Children.Add(new TextBlock { Text = "Danh mục *" });
        var categoryCombo = new ComboBox
        {
            Width = double.NaN,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            ItemsSource = ViewModel.Categories,
            DisplayMemberPath = "CategoryName",
            SelectedValuePath = "CategoryID"
        };
        categoryCombo.SelectionChanged += (s, args) =>
        {
            if (categoryCombo.SelectedItem is StormPC.Core.Models.Products.Category category)
            {
                ViewModel.SelectedCategoryId = category.CategoryID;
            }
        };
        stackPanel.Children.Add(categoryCombo);

        // Đường dẫn ảnh
        stackPanel.Children.Add(new TextBlock { Text = "Đường dẫn ảnh" });
        var pictureBox = new TextBox
        {
            PlaceholderText = "Nhập đường dẫn ảnh (URL hoặc đường dẫn local)",
            Text = ViewModel.Picture
        };
        pictureBox.TextChanged += (s, args) => { ViewModel.Picture = pictureBox.Text; };
        stackPanel.Children.Add(pictureBox);

        // Mô tả
        stackPanel.Children.Add(new TextBlock { Text = "Mô tả" });
        var descriptionBox = new TextBox
        {
            PlaceholderText = "Nhập mô tả chi tiết về laptop",
            Text = ViewModel.Description,
            TextWrapping = TextWrapping.Wrap,
            AcceptsReturn = true,
            Height = 100
        };
        descriptionBox.TextChanged += (s, args) => { ViewModel.Description = descriptionBox.Text; };
        stackPanel.Children.Add(descriptionBox);

        // Kích thước màn hình
        stackPanel.Children.Add(new TextBlock { Text = "Kích thước màn hình (inch) *" });
        var screenSizeBox = new NumberBox
        {
            Value = (double)(ViewModel.ScreenSize ?? 0),
            SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
            Minimum = 0,
            Maximum = 30,
            SmallChange = 0.1
        };
        screenSizeBox.ValueChanged += (s, args) => { ViewModel.ScreenSize = (decimal)screenSizeBox.Value; };
        stackPanel.Children.Add(screenSizeBox);

        // Hệ điều hành
        stackPanel.Children.Add(new TextBlock { Text = "Hệ điều hành *" });
        var osBox = new TextBox
        {
            PlaceholderText = "Ví dụ: Windows 11 Home",
            Text = ViewModel.OperatingSystem
        };
        osBox.TextChanged += (s, args) => { ViewModel.OperatingSystem = osBox.Text; };
        stackPanel.Children.Add(osBox);

        // Năm phát hành
        stackPanel.Children.Add(new TextBlock { Text = "Năm phát hành *" });
        var yearBox = new NumberBox
        {
            Value = ViewModel.ReleaseYear,
            SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
            Minimum = 2000,
            Maximum = DateTime.Now.Year + 1,
            SmallChange = 1
        };
        yearBox.ValueChanged += (s, args) => { ViewModel.ReleaseYear = (int)yearBox.Value; };
        stackPanel.Children.Add(yearBox);

        // Giảm giá
        stackPanel.Children.Add(new TextBlock { Text = "Giảm giá (VNĐ)" });
        var discountBox = new NumberBox
        {
            Value = (double)ViewModel.Discount,
            SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
            Minimum = 0,
            Maximum = 100000000,
            SmallChange = 1000
        };
        discountBox.ValueChanged += (s, args) => { ViewModel.Discount = (decimal)discountBox.Value; };
        stackPanel.Children.Add(discountBox);

        scrollViewer.Content = stackPanel;
        dialog.Content = scrollViewer;

        var result = await dialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            await ViewModel.AddLaptop();
        }
    }
}

// Chuyển đổi boolean sang SelectionMode
public class BoolToSelectionModeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return (bool)value ? ListViewSelectionMode.Multiple : ListViewSelectionMode.Single;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return (ListViewSelectionMode)value == ListViewSelectionMode.Multiple;
    }
} 