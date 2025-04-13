using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using StormPC.ViewModels.Orders;
using Windows.Storage.Pickers;
using Windows.Storage;
using System;
using System.IO;
using System.Threading.Tasks;
using WinRT.Interop;
using System.Diagnostics;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace StormPC.Views.Orders;

public sealed partial class OrderDetailPage : Page
{
    public OrderDetailViewModel ViewModel { get; }

    public OrderDetailPage()
    {
        ViewModel = App.GetService<OrderDetailViewModel>();
        InitializeComponent();
        QuestPDF.Settings.License = LicenseType.Community;
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        // Only load latest order if we didn't navigate with a specific order ID
        if (Frame.BackStack.Count == 0)
        {
            await ViewModel.InitializeAsync();
        }
    }

    protected async override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if (e.Parameter is int orderId)
        {
            await ViewModel.LoadOrderByIdAsync(orderId);
        }
    }

    private async void ExportPdfButton_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.OrderDetail == null)
        {
            var errorDialog = new ContentDialog
            {
                Title = "Lỗi",
                Content = "Không có thông tin đơn hàng để xuất",
                CloseButtonText = "Đóng",
                XamlRoot = this.XamlRoot
            };
            await errorDialog.ShowAsync();
            return;
        }

        var savePicker = new FileSavePicker
        {
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            SuggestedFileName = $"Order_{ViewModel.OrderDetail.OrderID}",
            FileTypeChoices = { { "PDF files", new[] { ".pdf" } } }
        };

        var window = App.MainWindow;
        var hwnd = WindowNative.GetWindowHandle(window);
        InitializeWithWindow.Initialize(savePicker, hwnd);

        StorageFile file = await savePicker.PickSaveFileAsync();
        if (file != null)
        {
            try
            {
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(10));

                        page.Header().Element(header =>
                        {
                            header.Row(row =>
                            {
                                row.RelativeItem().AlignCenter()
                                    .Text($"HÓA ĐƠN #{ViewModel.OrderDetail.OrderID}")
                                    .SemiBold().FontSize(20).FontColor("#2196F3");
                            });
                        });

                        page.Content().PaddingVertical(1, Unit.Centimetre).Element(content =>
                        {
                            content.Column(column =>
                            {
                                // Thông tin đơn hàng
                                column.Item().Text("THÔNG TIN ĐƠN HÀNG")
                                    .SemiBold().FontSize(14).FontColor("#1976D2");

                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                        columns.RelativeColumn(3);
                                    });

                                    // Info rows
                                    table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(text => text.Span("Ngày đặt:").SemiBold());
                                    table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(ViewModel.OrderDetail.FormattedOrderDate);

                                    table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(text => text.Span("Trạng thái:").SemiBold());
                                    table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(ViewModel.OrderDetail.StatusName);

                                    table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(text => text.Span("Khách hàng:").SemiBold());
                                    table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(ViewModel.OrderDetail.CustomerName);

                                    table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(text => text.Span("Địa chỉ:").SemiBold());
                                    table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(ViewModel.OrderDetail.ShippingAddress);

                                    table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(text => text.Span("Thành phố:").SemiBold());
                                    table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(ViewModel.OrderDetail.ShippingCity);

                                    table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(text => text.Span("Thanh toán:").SemiBold());
                                    table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(ViewModel.OrderDetail.PaymentMethod);
                                });

                                // Chi tiết sản phẩm
                                column.Item().PaddingTop(1, Unit.Centimetre);
                                column.Item().Text("CHI TIẾT SẢN PHẨM")
                                    .SemiBold().FontSize(14).FontColor("#1976D2");

                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                        columns.RelativeColumn();
                                    });

                                    // Header
                                    table.Header(header =>
                                    {
                                        header.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text("ID").SemiBold();
                                        header.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text("Sản phẩm").SemiBold();
                                        header.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text("Đơn giá").SemiBold();
                                        header.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text("Số lượng").SemiBold();
                                        header.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text("Thành tiền").SemiBold();
                                    });

                                    foreach (var item in ViewModel.OrderDetail.Items)
                                    {
                                        table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(item.VariantID.ToString());
                                        table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(item.ModelName);
                                        table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(item.FormattedUnitPrice);
                                        table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(item.Quantity.ToString());
                                        table.Cell().BorderBottom(1).BorderColor("#DDDDDD").Text(item.FormattedSubtotal);
                                    }
                                });

                                // Tổng cộng
                                column.Item().PaddingTop(1, Unit.Centimetre)
                                    .Row(row =>
                                    {
                                        row.RelativeItem().AlignRight()
                                            .Text($"Tổng cộng: {ViewModel.OrderDetail.FormattedTotalAmount}")
                                            .SemiBold();
                                    });
                            });
                        });

                        page.Footer().AlignRight().Text(text =>
                        {
                            text.Span($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}")
                                .FontSize(9);
                        });
                    });
                });

                await using (var stream = await file.OpenStreamForWriteAsync())
                {
                    document.GeneratePdf(stream);
                }

                var dialog = new ContentDialog
                {
                    Title = "Thành công",
                    Content = "Xuất file PDF thành công!",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dialog.ShowAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi: {ex.Message}");
                var errorMessage = $"Có lỗi xảy ra khi xuất PDF:\nChi tiết: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\nNguyên nhân: {ex.InnerException.Message}";
                }

                var dialog = new ContentDialog
                {
                    Title = "Lỗi",
                    Content = errorMessage,
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dialog.ShowAsync();
            }
        }
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel.OrderDetail == null) return;

        try
        {
            var result = await ViewModel.DeleteOrderAsync(ViewModel.OrderDetail.OrderID);
            if (result)
            {
                var successDialog = new ContentDialog
                {
                    Title = "Thành công",
                    Content = "Đã xóa đơn hàng thành công!",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await successDialog.ShowAsync();
                
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            }
            else
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Lỗi",
                    Content = "Không thể xóa đơn hàng. Chỉ có thể xóa đơn hàng có trạng thái 'Cancelled'.",
                    CloseButtonText = "Đóng",
                    XamlRoot = this.XamlRoot
                };
                await errorDialog.ShowAsync();
            }
        }
        catch (Exception ex)
        {
            var errorDialog = new ContentDialog
            {
                Title = "Lỗi",
                Content = $"Có lỗi xảy ra khi xóa đơn hàng: {ex.Message}",
                CloseButtonText = "Đóng",
                XamlRoot = this.XamlRoot
            };
            await errorDialog.ShowAsync();
        }
    }
} 