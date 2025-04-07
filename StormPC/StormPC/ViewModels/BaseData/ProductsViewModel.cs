using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StormPC.Core.Models.Products.Dtos;
using StormPC.Core.Models.Products;
using StormPC.Core.Services.Products;
using StormPC.Core.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Windows.Storage;
using System;
using StormPC.Core.Contracts.Services;
using StormPC.Core.Models.ActivityLog;
using StormPC.Contracts.Services;

namespace StormPC.ViewModels.BaseData;

public partial class ProductsViewModel : ObservableObject, IPaginatedViewModel
{
    private readonly IProductService _productService;
    private readonly IDialogService _dialogService;
    private readonly IActivityLogService _activityLogService;
    private List<LaptopDisplayDto> _allLaptops;
    private ObservableCollection<LaptopDisplayDto> _laptops;
    private int _currentPage = 1;
    private int _pageSize = 10; 
    private int _totalItems;
    private int _selectedSortIndex;
    private LaptopDisplayDto? _selectedLaptop;
    private ObservableCollection<LaptopDisplayDto> _selectedLaptops;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string cpu = string.Empty;

    [ObservableProperty]
    private string gpu = string.Empty;

    [ObservableProperty]
    private int ram;

    [ObservableProperty]
    private int storage;

    [ObservableProperty]
    private List<Brand> brands;

    [ObservableProperty]
    private List<Category> categories;

    [ObservableProperty]
    private int selectedBrandId;

    [ObservableProperty] 
    private int selectedCategoryId;

    [ObservableProperty]
    private string modelName;

    [ObservableProperty]
    private decimal? screenSize;

    [ObservableProperty]
    private string operatingSystem;

    [ObservableProperty]
    private int releaseYear;

    [ObservableProperty]
    private decimal discount;

    [ObservableProperty]
    private string picture;

    [ObservableProperty]
    private string description;

    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private bool isMultipleSelectionEnabled;

    [ObservableProperty]
    private string storageType = string.Empty;

    [ObservableProperty]
    private string color = string.Empty;

    [ObservableProperty]
    private decimal importPrice;

    [ObservableProperty]
    private decimal price;

    [ObservableProperty]
    private int stockQuantity;

    [ObservableProperty]
    private decimal _minPrice = 0;

    [ObservableProperty]
    private decimal _maxPrice = 2000000000;

    [ObservableProperty]
    private string _formattedMinPrice = "0 ₫";

    [ObservableProperty]
    private string _formattedMaxPrice = "2.000.000.000 ₫";

    public LaptopDisplayDto? SelectedLaptop
    {
        get => _selectedLaptop;
        set => SetProperty(ref _selectedLaptop, value);
    }

    public ObservableCollection<LaptopDisplayDto> SelectedLaptops
    {
        get => _selectedLaptops;
        set => SetProperty(ref _selectedLaptops, value);
    }

    [RelayCommand]
    public async Task LoadBrandsAndCategories()
    {
        try
        {
            IsLoading = true;
            Brands = (await _productService.GetAllBrandsAsync()).ToList();
            Categories = (await _productService.GetAllCategoriesAsync()).ToList();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Không thể tải thương hiệu và danh mục: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task AddSpec()
    {
        if (SelectedLaptop == null) return;
        
        try
        {
            var dialog = new ContentDialog
            {
                Title = $"Thêm cấu hình cho {SelectedLaptop.ModelName}",
                PrimaryButtonText = "Thêm",
                CloseButtonText = "Hủy",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = App.MainWindow.Content.XamlRoot,
                MinWidth = 500
            };

            var scrollViewer = new ScrollViewer
            {
                MaxHeight = 600,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };

            var stackPanel = new StackPanel
            {
                Spacing = 10,
                Margin = new Thickness(0, 12, 0, 0),
                Children =
                {
                    new TextBox
                    {
                        Header = "CPU",
                        Text = Cpu,
                        PlaceholderText = "Nhập thông tin CPU"
                    },
                    new TextBox
                    {
                        Header = "GPU",
                        Text = Gpu,
                        PlaceholderText = "Nhập thông tin GPU"
                    },
                    new NumberBox
                    {
                        Header = "RAM (GB)",
                        Value = Ram,
                        SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
                        Minimum = 1
                    },
                    new NumberBox
                    {
                        Header = "Dung lượng ổ cứng (GB)",
                        Value = Storage,
                        SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
                        Minimum = 1
                    },
                    new ComboBox
                    {
                        Header = "Loại ổ cứng",
                        PlaceholderText = "Chọn loại ổ cứng",
                        ItemsSource = new List<string> { "SSD", "HDD", "NVMe" },
                        SelectedItem = StorageType
                    },
                    new TextBox
                    {
                        Header = "Màu sắc",
                        Text = Color,
                        PlaceholderText = "Nhập màu sắc"
                    },
                    new NumberBox
                    {
                        Header = "Giá nhập (VNĐ)",
                        Value = (double)ImportPrice,
                        SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
                        Minimum = 1
                    },
                    new NumberBox
                    {
                        Header = "Giá bán (VNĐ)",
                        Value = (double)Price,
                        SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
                        Minimum = 1
                    },
                    new NumberBox
                    {
                        Header = "Số lượng tồn kho",
                        Value = StockQuantity,
                        SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
                        Minimum = 0
                    }
                }
            };

            scrollViewer.Content = stackPanel;
            dialog.Content = scrollViewer;
            
            var result = await dialog.ShowAsync();
            
            if (result == ContentDialogResult.Primary)
            {
                var stackPanelContent = (StackPanel)((ScrollViewer)dialog.Content).Content;
                Cpu = ((TextBox)stackPanelContent.Children[0]).Text;
                Gpu = ((TextBox)stackPanelContent.Children[1]).Text;
                Ram = (int)((NumberBox)stackPanelContent.Children[2]).Value;
                Storage = (int)((NumberBox)stackPanelContent.Children[3]).Value;
                StorageType = ((ComboBox)stackPanelContent.Children[4]).SelectedItem?.ToString() ?? string.Empty;
                Color = ((TextBox)stackPanelContent.Children[5]).Text;
                ImportPrice = (decimal)((NumberBox)stackPanelContent.Children[6]).Value;
                Price = (decimal)((NumberBox)stackPanelContent.Children[7]).Value;
                StockQuantity = (int)((NumberBox)stackPanelContent.Children[8]).Value;
                
                if (string.IsNullOrWhiteSpace(Cpu) ||
                    string.IsNullOrWhiteSpace(Gpu) ||
                    string.IsNullOrWhiteSpace(StorageType) ||
                    string.IsNullOrWhiteSpace(Color) ||
                    Ram <= 0 ||
                    Storage <= 0 ||
                    ImportPrice <= 0 ||
                    Price <= 0 ||
                    StockQuantity < 0)
                {
                    var errorDialog = new ContentDialog
                    {
                        Title = "Lỗi",
                        Content = "Vui lòng nhập đầy đủ thông tin và đảm bảo giá trị hợp lệ",
                        CloseButtonText = "Đóng",
                        XamlRoot = App.MainWindow.Content.XamlRoot
                    };
                    await errorDialog.ShowAsync();
                    return;
                }
                
                var spec = new LaptopSpec
                {
                    LaptopID = SelectedLaptop.LaptopID,
                    CPU = Cpu,
                    GPU = Gpu,
                    RAM = Ram,
                    Storage = Storage,
                    StorageType = StorageType,
                    Color = Color,
                    ImportPrice = ImportPrice,
                    Price = Price,
                    StockQuantity = StockQuantity
                };
                
                IsLoading = true;
                var success = await _productService.AddLaptopSpecAsync(spec);
                
                if (success)
                {
                    await LoadProductsAsync();
                    await _activityLogService.LogActivityAsync(
                        "Sản phẩm",
                        "Thêm cấu hình",
                        $"Thêm cấu hình mới cho laptop {SelectedLaptop.ModelName}",
                        "Success",
                        "Admin"
                    );
                    
                    var successDialog = new ContentDialog
                    {
                        Title = "Thành công",
                        Content = "Đã thêm cấu hình mới",
                        CloseButtonText = "Đóng",
                        XamlRoot = App.MainWindow.Content.XamlRoot
                    };
                    await successDialog.ShowAsync();
                }
                else
                {
                    await _activityLogService.LogActivityAsync(
                        "Sản phẩm",
                        "Lỗi thêm cấu hình",
                        $"Không thể thêm cấu hình cho laptop {SelectedLaptop.ModelName}",
                        "Error",
                        "Admin"
                    );
                    
                    var failureDialog = new ContentDialog
                    {
                        Title = "Lỗi",
                        Content = "Không thể thêm cấu hình. Vui lòng thử lại",
                        CloseButtonText = "Đóng",
                        XamlRoot = App.MainWindow.Content.XamlRoot
                    };
                    await failureDialog.ShowAsync();
                }
                
                IsLoading = false;
            }
        }
        catch (Exception ex)
        {
            await _activityLogService.LogActivityAsync(
                "Sản phẩm",
                "Lỗi thêm cấu hình",
                $"Lỗi khi thêm cấu hình: {ex.Message}",
                "Error",
                "Admin"
            );
            
            var exceptionDialog = new ContentDialog
            {
                Title = "Lỗi",
                Content = "Đã xảy ra lỗi. Vui lòng thử lại",
                CloseButtonText = "Đóng",
                XamlRoot = App.MainWindow.Content.XamlRoot
            };
            await exceptionDialog.ShowAsync();
        }
    }

    [RelayCommand]
    private async Task Edit()
    {
        if (SelectedLaptop == null) return;

        var canEdit = await _productService.CanEditLaptopAsync(SelectedLaptop.LaptopID);
        if (!canEdit)
        {
            await _activityLogService.LogActivityAsync(
                "Sản phẩm",
                "Lỗi chỉnh sửa",
                $"Không thể sửa laptop {SelectedLaptop.ModelName} vì đã có đơn hàng liên quan",
                "Error",
                "Admin"
            );
            
            ContentDialog errorDialog = new ContentDialog
            {
                Title = "Không thể sửa",
                Content = "Không thể sửa sản phẩm này vì đã có đơn hàng liên quan.",
                CloseButtonText = "Đóng",
                XamlRoot = App.MainWindow.Content.XamlRoot
            };
            await errorDialog.ShowAsync();
            return;
        }

        // Lấy thông tin laptop hiện tại
        var currentLaptop = await _productService.GetLaptopByIdAsync(SelectedLaptop.LaptopID);
        if (currentLaptop == null)
        {
            ContentDialog errorDialog = new ContentDialog
            {
                Title = "Lỗi",
                Content = "Không tìm thấy thông tin sản phẩm.",
                CloseButtonText = "Đóng",
                XamlRoot = App.MainWindow.Content.XamlRoot
            };
            await errorDialog.ShowAsync();
            return;
        }

        // Gán giá trị hiện tại cho các trường
        ModelName = currentLaptop.ModelName;
        SelectedBrandId = currentLaptop.BrandID;
        SelectedCategoryId = currentLaptop.CategoryID;
        ScreenSize = currentLaptop.ScreenSize;
        OperatingSystem = currentLaptop.OperatingSystem;
        ReleaseYear = currentLaptop.ReleaseYear ?? DateTime.Now.Year;
        Discount = currentLaptop.Discount;
        Picture = currentLaptop.Picture;
        Description = currentLaptop.Description;

        // Hiển thị dialog chỉnh sửa
        var dialog = new ContentDialog
        {
            Title = "Sửa thông tin laptop",
            XamlRoot = App.MainWindow.Content.XamlRoot,
            PrimaryButtonText = "Lưu",
            CloseButtonText = "Hủy",
            DefaultButton = ContentDialogButton.Primary,
            MinWidth = 900
        };

        var scrollViewer = new ScrollViewer
        {
            MaxHeight = 600,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto
        };

        var stackPanel = new StackPanel { Spacing = 12, Margin = new Thickness(0, 12, 0, 0) };

        // Tên model
        stackPanel.Children.Add(new TextBlock { Text = "Tên mẫu laptop *" });
        var modelNameBox = new TextBox
        {
            PlaceholderText = "Ví dụ: MacBook Pro 14 inch",
            Text = ModelName
        };
        modelNameBox.TextChanged += (s, args) => { ModelName = modelNameBox.Text; };
        stackPanel.Children.Add(modelNameBox);

        // Thương hiệu
        stackPanel.Children.Add(new TextBlock { Text = "Thương hiệu *" });
        var brandCombo = new ComboBox
        {
            Width = double.NaN,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            ItemsSource = Brands,
            DisplayMemberPath = "BrandName",
            SelectedValuePath = "BrandID",
            SelectedValue = SelectedBrandId
        };
        brandCombo.SelectionChanged += (s, args) =>
        {
            if (brandCombo.SelectedItem is Brand brand)
            {
                SelectedBrandId = brand.BrandID;
            }
        };
        stackPanel.Children.Add(brandCombo);

        // Danh mục
        stackPanel.Children.Add(new TextBlock { Text = "Danh mục *" });
        var categoryCombo = new ComboBox
        {
            Width = double.NaN,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            ItemsSource = Categories,
            DisplayMemberPath = "CategoryName",
            SelectedValuePath = "CategoryID",
            SelectedValue = SelectedCategoryId
        };
        categoryCombo.SelectionChanged += (s, args) =>
        {
            if (categoryCombo.SelectedItem is Category category)
            {
                SelectedCategoryId = category.CategoryID;
            }
        };
        stackPanel.Children.Add(categoryCombo);

        // Đường dẫn ảnh
        stackPanel.Children.Add(new TextBlock { Text = "Đường dẫn ảnh" });
        var pictureBox = new TextBox
        {
            PlaceholderText = "Nhập đường dẫn ảnh (URL hoặc đường dẫn local)",
            Text = Picture
        };
        pictureBox.TextChanged += (s, args) => { Picture = pictureBox.Text; };
        stackPanel.Children.Add(pictureBox);

        // Mô tả
        stackPanel.Children.Add(new TextBlock { Text = "Mô tả" });
        var descriptionBox = new TextBox
        {
            PlaceholderText = "Nhập mô tả chi tiết về laptop",
            Text = Description,
            TextWrapping = TextWrapping.Wrap,
            AcceptsReturn = true,
            Height = 100
        };
        descriptionBox.TextChanged += (s, args) => { Description = descriptionBox.Text; };
        stackPanel.Children.Add(descriptionBox);

        // Kích thước màn hình
        stackPanel.Children.Add(new TextBlock { Text = "Kích thước màn hình (inch) *" });
        var screenSizeBox = new NumberBox
        {
            Value = (double)(ScreenSize ?? 0),
            SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
            Minimum = 0,
            Maximum = 30,
            SmallChange = 0.1
        };
        screenSizeBox.ValueChanged += (s, args) => { ScreenSize = (decimal)screenSizeBox.Value; };
        stackPanel.Children.Add(screenSizeBox);

        // Hệ điều hành
        stackPanel.Children.Add(new TextBlock { Text = "Hệ điều hành *" });
        var osBox = new TextBox
        {
            PlaceholderText = "Ví dụ: Windows 11 Home",
            Text = OperatingSystem
        };
        osBox.TextChanged += (s, args) => { OperatingSystem = osBox.Text; };
        stackPanel.Children.Add(osBox);

        // Năm phát hành
        stackPanel.Children.Add(new TextBlock { Text = "Năm phát hành *" });
        var yearBox = new NumberBox
        {
            Value = ReleaseYear,
            SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
            Minimum = 2000,
            Maximum = DateTime.Now.Year + 1,
            SmallChange = 1
        };
        yearBox.ValueChanged += (s, args) => { ReleaseYear = (int)yearBox.Value; };
        stackPanel.Children.Add(yearBox);

        // Giảm giá
        stackPanel.Children.Add(new TextBlock { Text = "Giảm giá (VNĐ)" });
        var discountBox = new NumberBox
        {
            Value = (double)Discount,
            SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact,
            Minimum = 0,
            Maximum = 100000000,
            SmallChange = 1000
        };
        discountBox.ValueChanged += (s, args) => { Discount = (decimal)discountBox.Value; };
        stackPanel.Children.Add(discountBox);

        scrollViewer.Content = stackPanel;
        dialog.Content = scrollViewer;

        var result = await dialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            await EditLaptop(currentLaptop.LaptopID);
        }
    }

    private async Task EditLaptop(int laptopId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(ModelName))
            {
                ErrorMessage = "Vui lòng nhập tên mẫu laptop.";
                return;
            }

            if (SelectedBrandId <= 0)
            {
                ErrorMessage = "Vui lòng chọn thương hiệu.";
                return;
            }

            if (SelectedCategoryId <= 0)
            {
                ErrorMessage = "Vui lòng chọn danh mục.";
                return;
            }

            if (ScreenSize <= 0)
            {
                ErrorMessage = "Kích thước màn hình phải lớn hơn 0.";
                return;
            }

            if (string.IsNullOrWhiteSpace(OperatingSystem))
            {
                ErrorMessage = "Vui lòng nhập hệ điều hành.";
                return;
            }

            if (ReleaseYear <= 0)
            {
                ErrorMessage = "Năm phát hành không hợp lệ.";
                return;
            }

            if (Discount < 0)
            {
                ErrorMessage = "Giảm giá không thể âm.";
                return;
            }

            // Làm tròn giảm giá xuống đơn vị 1000 VND
            decimal roundedDiscount = Math.Floor(Discount / 1000) * 1000;

            var now = DateTime.UtcNow;
            var updatedLaptop = new Laptop
            {
                LaptopID = laptopId,
                ModelName = ModelName,
                BrandID = SelectedBrandId,
                CategoryID = SelectedCategoryId,
                ScreenSize = ScreenSize,
                OperatingSystem = OperatingSystem,
                ReleaseYear = ReleaseYear,
                Discount = roundedDiscount,
                Description = Description ?? string.Empty,
                Picture = Picture,
                UpdatedAt = now
            };

            IsLoading = true;
            var success = await _productService.EditLaptopAsync(updatedLaptop);
            IsLoading = false;

            if (success)
            {
                await _activityLogService.LogActivityAsync(
                    "Sản phẩm",
                    "Chỉnh sửa",
                    $"Chỉnh sửa thông tin laptop {updatedLaptop.ModelName}",
                    "Success",
                    "Admin"
                );
                
                // Reset form and reload
                ModelName = string.Empty;
                SelectedBrandId = 0;
                SelectedCategoryId = 0;
                ScreenSize = 0;
                OperatingSystem = string.Empty;
                ReleaseYear = DateTime.Now.Year;
                Discount = 0;
                Picture = string.Empty;
                Description = string.Empty;

                await LoadProductsAsync();
                ErrorMessage = string.Empty;
            }
            else
            {
                await _activityLogService.LogActivityAsync(
                    "Sản phẩm",
                    "Lỗi chỉnh sửa",
                    $"Không thể cập nhật laptop {updatedLaptop.ModelName}",
                    "Error",
                    "Admin"
                );
                ErrorMessage = "Không thể cập nhật laptop. Vui lòng thử lại.";
            }
        }
        catch (Exception ex)
        {
            await _activityLogService.LogActivityAsync(
                "Sản phẩm",
                "Lỗi chỉnh sửa",
                $"Lỗi khi cập nhật laptop: {ex.Message}",
                "Error",
                "Admin"
            );
            ErrorMessage = $"Lỗi: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedLaptop == null) return;

        var canDelete = await _productService.CanDeleteLaptopAsync(SelectedLaptop.LaptopID);
        if (!canDelete)
        {
            await _activityLogService.LogActivityAsync(
                "Sản phẩm",
                "Lỗi xóa",
                $"Không thể xóa laptop {SelectedLaptop.ModelName} vì đã có đơn hàng liên quan",
                "Error",
                "Admin"
            );
            
            ContentDialog errorDialog = new ContentDialog
            {
                Title = "Không thể xóa",
                Content = "Không thể xóa sản phẩm này vì đã có đơn hàng liên quan.",
                CloseButtonText = "Đóng",
                XamlRoot = App.MainWindow.Content.XamlRoot
            };
            await errorDialog.ShowAsync();
            return;
        }

        ContentDialog confirmDialog = new ContentDialog
        {
            Title = "Xác nhận xóa",
            Content = $"Bạn có chắc chắn muốn xóa laptop {SelectedLaptop.ModelName}?",
            PrimaryButtonText = "Xóa",
            CloseButtonText = "Hủy",
            DefaultButton = ContentDialogButton.Close,
            XamlRoot = App.MainWindow.Content.XamlRoot
        };

        var result = await confirmDialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            IsLoading = true;
            var success = await _productService.DeleteLaptopAsync(SelectedLaptop.LaptopID);
            IsLoading = false;

            if (success)
            {
                await _activityLogService.LogActivityAsync(
                    "Sản phẩm",
                    "Xóa",
                    $"Xóa laptop {SelectedLaptop.ModelName}",
                    "Success",
                    "Admin"
                );
                await LoadProductsAsync();
            }
            else
            {
                await _activityLogService.LogActivityAsync(
                    "Sản phẩm",
                    "Lỗi xóa",
                    $"Không thể xóa laptop {SelectedLaptop.ModelName}",
                    "Error",
                    "Admin"
                );
                
                ContentDialog errorDialog = new ContentDialog
                {
                    Title = "Lỗi",
                    Content = "Không thể xóa sản phẩm. Vui lòng thử lại sau.",
                    CloseButtonText = "Đóng",
                    XamlRoot = App.MainWindow.Content.XamlRoot
                };
                await errorDialog.ShowAsync();
            }
        }
    }

    [RelayCommand]
    private async Task DeleteMultiple()
    {
        if (SelectedLaptops == null || SelectedLaptops.Count == 0)
        {
            ContentDialog errorDialog = new ContentDialog
            {
                Title = "Lỗi",
                Content = "Vui lòng chọn ít nhất một sản phẩm để xóa.",
                CloseButtonText = "Đóng",
                XamlRoot = App.MainWindow.Content.XamlRoot
            };
            await errorDialog.ShowAsync();
            return;
        }

        // Kiểm tra từng laptop có thể xóa được không
        var laptopIds = SelectedLaptops.Select(l => l.LaptopID).ToList();
        var deletableLaptops = new List<int>();
        var nonDeletableLaptops = new List<string>();

        foreach (var laptop in SelectedLaptops)
        {
            var canDelete = await _productService.CanDeleteLaptopAsync(laptop.LaptopID);
            if (canDelete)
            {
                deletableLaptops.Add(laptop.LaptopID);
            }
            else
            {
                nonDeletableLaptops.Add(laptop.ModelName);
            }
        }

        if (deletableLaptops.Count == 0)
        {
            ContentDialog errorDialog = new ContentDialog
            {
                Title = "Không thể xóa",
                Content = "Không thể xóa các sản phẩm đã chọn vì tất cả đều đã có đơn hàng.",
                CloseButtonText = "Đóng",
                XamlRoot = App.MainWindow.Content.XamlRoot
            };
            await errorDialog.ShowAsync();
            return;
        }

        var confirmMessage = deletableLaptops.Count == SelectedLaptops.Count
            ? $"Bạn có chắc chắn muốn xóa {deletableLaptops.Count} sản phẩm đã chọn?"
            : $"Có {nonDeletableLaptops.Count} sản phẩm không thể xóa do đã có đơn hàng:\n" +
              $"{string.Join("\n", nonDeletableLaptops)}\n\n" +
              $"Bạn có muốn tiếp tục xóa {deletableLaptops.Count} sản phẩm còn lại?";

        ContentDialog confirmDialog = new ContentDialog
        {
            Title = "Xác nhận xóa",
            Content = confirmMessage,
            PrimaryButtonText = "Xóa",
            CloseButtonText = "Hủy",
            DefaultButton = ContentDialogButton.Close,
            XamlRoot = App.MainWindow.Content.XamlRoot
        };

        var result = await confirmDialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            IsLoading = true;
            var success = await _productService.DeleteMultipleLaptopsAsync(deletableLaptops);
            IsLoading = false;

            if (success)
            {
                await _activityLogService.LogActivityAsync(
                    "Sản phẩm",
                    "Xóa nhiều",
                    $"Xóa {deletableLaptops.Count} laptop",
                    "Success",
                    "Admin"
                );
                
                await LoadProductsAsync();
                IsMultipleSelectionEnabled = false;

                if (nonDeletableLaptops.Count > 0)
                {
                    ContentDialog resultDialog = new ContentDialog
                    {
                        Title = "Kết quả xóa",
                        Content = $"Đã xóa thành công {deletableLaptops.Count} sản phẩm.\n" +
                                 $"{nonDeletableLaptops.Count} sản phẩm không thể xóa do đã có đơn hàng.",
                        CloseButtonText = "Đóng",
                        XamlRoot = App.MainWindow.Content.XamlRoot
                    };
                    await resultDialog.ShowAsync();
                }
            }
            else
            {
                await _activityLogService.LogActivityAsync(
                    "Sản phẩm",
                    "Lỗi xóa nhiều",
                    $"Lỗi khi xóa {deletableLaptops.Count} laptop",
                    "Error",
                    "Admin"
                );
                
                ContentDialog errorDialog = new ContentDialog
                {
                    Title = "Lỗi",
                    Content = "Đã xảy ra lỗi khi xóa sản phẩm. Vui lòng thử lại sau.",
                    CloseButtonText = "Đóng",
                    XamlRoot = App.MainWindow.Content.XamlRoot
                };
                await errorDialog.ShowAsync();
            }
        }
    }

    [RelayCommand]
    private void ToggleSelectionMode()
    {
        IsMultipleSelectionEnabled = !IsMultipleSelectionEnabled;
        if (!IsMultipleSelectionEnabled)
        {
            SelectedLaptops?.Clear();
        }
    }

    [RelayCommand]
    public async Task AddLaptop()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(ModelName))
            {
                ErrorMessage = "Vui lòng nhập tên mẫu laptop.";
                return;
            }

            if (SelectedBrandId <= 0)
            {
                ErrorMessage = "Vui lòng chọn thương hiệu.";
                return;
            }

            if (SelectedCategoryId <= 0)
            {
                ErrorMessage = "Vui lòng chọn danh mục.";
                return;
            }

            if (ScreenSize <= 0)
            {
                ErrorMessage = "Kích thước màn hình phải lớn hơn 0.";
                return;
            }

            if (string.IsNullOrWhiteSpace(OperatingSystem))
            {
                ErrorMessage = "Vui lòng nhập hệ điều hành.";
                return;
            }

            if (ReleaseYear <= 0)
            {
                ErrorMessage = "Năm phát hành không hợp lệ.";
                return;
            }

            if (Discount < 0)
            {
                ErrorMessage = "Giảm giá không thể âm.";
                return;
            }

            // Làm tròn giảm giá xuống đơn vị 1000 VND
            decimal roundedDiscount = Math.Floor(Discount / 1000) * 1000;

            var now = DateTime.UtcNow;
            var newLaptop = new Laptop
            {
                ModelName = ModelName,
                BrandID = SelectedBrandId,
                CategoryID = SelectedCategoryId,
                ScreenSize = ScreenSize,
                OperatingSystem = OperatingSystem,
                ReleaseYear = ReleaseYear,
                Discount = roundedDiscount,
                Description = Description ?? string.Empty,
                Picture = Picture,
                IsDeleted = false,
                CreatedAt = now,
                UpdatedAt = now
            };

            // Nếu có giảm giá, thêm thời gian giảm giá (mặc định 1 tháng)
            if (roundedDiscount > 0)
            {
                newLaptop.DiscountStartDate = now;
                newLaptop.DiscountEndDate = now.AddMonths(1);
            }

            IsLoading = true;
            var success = await _productService.AddLaptopAsync(newLaptop);
            IsLoading = false;

            if (success)
            {
                await _activityLogService.LogActivityAsync(
                    "Sản phẩm",
                    "Thêm mới",
                    $"Thêm laptop mới: {newLaptop.ModelName}",
                    "Success",
                    "Admin"
                );
                
                // Reset form
                ModelName = string.Empty;
                SelectedBrandId = 0;
                SelectedCategoryId = 0;
                ScreenSize = 0;
                OperatingSystem = string.Empty;
                ReleaseYear = DateTime.Now.Year;
                Discount = 0;
                Picture = string.Empty;
                Description = string.Empty;

                await LoadProductsAsync();
                ErrorMessage = string.Empty;
            }
            else
            {
                await _activityLogService.LogActivityAsync(
                    "Sản phẩm",
                    "Lỗi thêm mới",
                    $"Không thể thêm laptop {newLaptop.ModelName}",
                    "Error",
                    "Admin"
                );
                ErrorMessage = "Không thể thêm laptop mới. Vui lòng thử lại.";
            }
        }
        catch (Exception ex)
        {
            await _activityLogService.LogActivityAsync(
                "Sản phẩm",
                "Lỗi thêm mới",
                $"Lỗi khi thêm laptop: {ex.Message}",
                "Error",
                "Admin"
            );
            ErrorMessage = $"Lỗi: {ex.Message}";
        }
    }

    public int SelectedSortIndex
    {
        get => _selectedSortIndex;
        set
        {
            if (SetProperty(ref _selectedSortIndex, value))
            {
                FilterAndPaginateProducts();
            }
        }
    }

    public ObservableCollection<LaptopDisplayDto> Laptops
    {
        get => _laptops;
        set => SetProperty(ref _laptops, value);
    }

    [ObservableProperty]
    private string searchText = string.Empty;

    public int CurrentPage
    {
        get => _currentPage;
        set
        {
            if (SetProperty(ref _currentPage, value))
            {
                LoadPage(value);
            }
        }
    }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (SetProperty(ref _pageSize, value))
            {
                FilterAndPaginateProducts();
            }
        }
    }

    public int TotalPages => (_totalItems + PageSize - 1) / PageSize;

    public ProductsViewModel(IProductService productService, IDialogService dialogService, IActivityLogService activityLogService)
    {
        _productService = productService;
        _dialogService = dialogService;
        _activityLogService = activityLogService;
        _laptops = new ObservableCollection<LaptopDisplayDto>();
        _allLaptops = new List<LaptopDisplayDto>();
        _selectedLaptops = new ObservableCollection<LaptopDisplayDto>();
        ReleaseYear = DateTime.Now.Year;
    }

    partial void OnSearchTextChanged(string value)
    {
        FilterAndPaginateProducts();
    }

    partial void OnMinPriceChanged(decimal value)
    {
        FormattedMinPrice = value.ToString("N0") + " ₫";
        ApplyFilters();
    }

    partial void OnMaxPriceChanged(decimal value)
    {
        FormattedMaxPrice = value.ToString("N0") + " ₫";
        ApplyFilters();
    }

    public void ApplyFilters()
    {
        if (_allLaptops == null) return;

        var filteredLaptops = _allLaptops
            .Where(l => l.LowestPrice >= MinPrice && l.LowestPrice <= MaxPrice);

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filteredLaptops = filteredLaptops.Where(l => 
                l.ModelName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                l.BrandName.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        _totalItems = filteredLaptops.Count();
        var paginatedLaptops = filteredLaptops
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        Laptops = new ObservableCollection<LaptopDisplayDto>(paginatedLaptops);
        OnPropertyChanged(nameof(TotalPages));
    }

    public async Task LoadProductsAsync()
    {
        try
        {
            IsLoading = true;
            _allLaptops = await _productService.GetLaptopsAsync();
            FilterAndPaginateProducts();
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void FilterAndPaginateProducts()
    {
        if (_allLaptops == null) return;

        var filteredProducts = _allLaptops;

        // Lọc theo khoảng giá
        filteredProducts = filteredProducts.Where(l =>
            l.LowestPrice >= MinPrice && l.LowestPrice <= MaxPrice
        ).ToList();

        // Lọc theo text search nếu có
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filteredProducts = filteredProducts.Where(l =>
                l.ModelName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                l.BrandName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        // Áp dụng sắp xếp
        filteredProducts = SortProducts(filteredProducts);

        // Tính tổng số items và phân trang
        _totalItems = filteredProducts.Count;
        var pagedProducts = filteredProducts
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        Laptops = new ObservableCollection<LaptopDisplayDto>(pagedProducts);
        OnPropertyChanged(nameof(TotalPages));
    }

    private List<LaptopDisplayDto> SortProducts(List<LaptopDisplayDto> products)
    {
        return SelectedSortIndex switch
        {
            0 => products.OrderByDescending(p => p.ReleaseYear).ToList(), // Mới nhất
            1 => products.OrderBy(p => p.LowestPrice).ToList(), // Giá thấp đến cao
            2 => products.OrderByDescending(p => p.LowestPrice).ToList(), // Giá cao đến thấp
            3 => products.OrderByDescending(p => p.Discount).ToList(), // Giảm giá nhiều nhất
            4 => products.OrderBy(p => p.ModelName).ToList(), // Tên A-Z
            5 => products.OrderByDescending(p => p.ModelName).ToList(), // Tên Z-A
            _ => products
        };
    }

    public void LoadPage(int page)
    {
        if (_allLaptops == null) return;

        var filteredProducts = string.IsNullOrWhiteSpace(SearchText)
            ? _allLaptops
            : _allLaptops.Where(l =>
                l.ModelName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                l.BrandName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase)
            ).ToList();

        // Apply sorting
        filteredProducts = SortProducts(filteredProducts);

        _totalItems = filteredProducts.Count;

        var pagedProducts = filteredProducts
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList();

        Laptops = new ObservableCollection<LaptopDisplayDto>(pagedProducts);
        OnPropertyChanged(nameof(TotalPages));
    }

    public void UpdatePriceRange(double minValue, double maxValue)
    {
        MinPrice = (decimal)minValue;
        MaxPrice = (decimal)maxValue;
    }

    private void UpdatePagination()
    {
        OnPropertyChanged(nameof(TotalPages));
    }
} 