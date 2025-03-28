using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using StormPC.ViewModels.Orders;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Layout.Borders;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.IO.Font;
using Windows.Storage.Pickers;
using Windows.Storage;
using System;
using System.IO;
using System.Threading.Tasks;
using WinRT.Interop;

namespace StormPC.Views.Orders;

public sealed partial class OrderDetailPage : Page
{
    public OrderDetailViewModel ViewModel { get; }

    public OrderDetailPage()
    {
        ViewModel = App.GetService<OrderDetailViewModel>();
        InitializeComponent();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        // Only load latest order if we didn't navigate with a specific order ID
        if (Frame.BackStack.Count == 0)
        {
            await ViewModel.InitializeAsync();
        }
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        // If we have a parameter and it's an integer (OrderID)
        if (e.Parameter is int orderId)
        {
            await ViewModel.LoadOrderByIdAsync(orderId);
        }
    }

    private async void ExportPdfButton_Click(object sender, RoutedEventArgs e)
    {
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
                // Tạo font hỗ trợ Unicode
                string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                var font = PdfFontFactory.CreateFont(arialFontPath, PdfEncodings.IDENTITY_H);

                using var writer = new PdfWriter(await file.OpenStreamForWriteAsync());
                using var pdf = new PdfDocument(writer);
                using var document = new Document(pdf);

                // Set font mặc định cho document
                document.SetFont(font);

                // Header
                document.Add(new Paragraph($"HÓA ĐƠN #{ViewModel.OrderDetail.OrderID}")
                    .SetFont(font)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(20)
                    .SetBorderBottom(new SolidBorder(1))
                    .SetPaddingBottom(20)
                    .SetMarginBottom(20));

                // Order Info
                document.Add(new Paragraph("THÔNG TIN ĐƠN HÀNG")
                    .SetFont(font)
                    .SetFontSize(14)
                    .SetPaddingBottom(10)
                    .SetUnderline());

                var orderInfo = new Table(UnitValue.CreatePercentArray(new float[] { 30, 70 }))
                    .UseAllAvailableWidth()
                    .SetMarginBottom(20);

                AddTableRow(orderInfo, "Ngày đặt:", ViewModel.OrderDetail.FormattedOrderDate, font);
                AddTableRow(orderInfo, "Trạng thái:", ViewModel.OrderDetail.StatusName, font);
                AddTableRow(orderInfo, "Khách hàng:", ViewModel.OrderDetail.CustomerName, font);
                AddTableRow(orderInfo, "Địa chỉ:", ViewModel.OrderDetail.ShippingAddress, font);
                AddTableRow(orderInfo, "Thành phố:", ViewModel.OrderDetail.ShippingCity, font);
                AddTableRow(orderInfo, "Thanh toán:", ViewModel.OrderDetail.PaymentMethod, font);

                document.Add(orderInfo);

                // Order Items
                document.Add(new Paragraph("CHI TIẾT SẢN PHẨM")
                    .SetFont(font)
                    .SetFontSize(14)
                    .SetPaddingBottom(10)
                    .SetUnderline());

                var items = new Table(UnitValue.CreatePercentArray(new float[] { 15, 35, 15, 15, 20 }))
                    .UseAllAvailableWidth()
                    .SetMarginBottom(20);

                // Table header
                var headerBackground = new DeviceRgb(240, 240, 240);
                items.AddHeaderCell(CreateHeaderCell("ID", headerBackground, font));
                items.AddHeaderCell(CreateHeaderCell("Sản phẩm", headerBackground, font));
                items.AddHeaderCell(CreateHeaderCell("Đơn giá", headerBackground, font));
                items.AddHeaderCell(CreateHeaderCell("Số lượng", headerBackground, font));
                items.AddHeaderCell(CreateHeaderCell("Thành tiền", headerBackground, font));

                // Table content
                foreach (var item in ViewModel.OrderDetail.Items)
                {
                    items.AddCell(new Cell().Add(new Paragraph(item.VariantID).SetFont(font))
                        .SetPadding(5));
                    items.AddCell(new Cell().Add(new Paragraph(item.ModelName).SetFont(font))
                        .SetPadding(5));
                    items.AddCell(new Cell().Add(new Paragraph(item.FormattedUnitPrice).SetFont(font))
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                        .SetPadding(5));
                    items.AddCell(new Cell().Add(new Paragraph(item.Quantity.ToString()).SetFont(font))
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetPadding(5));
                    items.AddCell(new Cell().Add(new Paragraph(item.FormattedSubtotal).SetFont(font))
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                        .SetPadding(5));
                }

                document.Add(items);

                // Total
                document.Add(new Paragraph($"Tổng cộng: {ViewModel.OrderDetail.FormattedTotalAmount}")
                    .SetFont(font)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                    .SetFontSize(14)
                    .SetPaddingTop(10)
                    .SetBorderTop(new SolidBorder(1)));

                // Footer
                document.Add(new Paragraph($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}")
                    .SetFont(font)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFontSize(10)
                    .SetMarginTop(30));

                document.Close();

                var dialog = new ContentDialog
                {
                    Title = "Thành công",
                    Content = "Xuất file PDF thành công!",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dialog.ShowAsync();
            }
            catch (IOException ioEx)
            {
                var dialog = new ContentDialog
                {
                    Title = "Lỗi",
                    Content = $"Không thể tạo hoặc ghi file PDF. Vui lòng đảm bảo file không bị khóa và thử lại.\nChi tiết: {ioEx.Message}",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dialog.ShowAsync();
            }
            catch (Exception ex)
            {
                var errorMessage = "Có lỗi xảy ra khi xuất PDF:\n";
                if (ex.InnerException != null)
                {
                    errorMessage += $"Chi tiết: {ex.InnerException.Message}";
                }
                else
                {
                    errorMessage += $"Chi tiết: {ex.Message}";
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

    private Cell CreateHeaderCell(string text, DeviceRgb backgroundColor, PdfFont font)
    {
        return new Cell()
            .Add(new Paragraph(text).SetFont(font))
            .SetBackgroundColor(backgroundColor)
            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
            .SetPadding(5);
    }

    private void AddTableRow(Table table, string label, string value, PdfFont font)
    {
        table.AddCell(new Cell()
            .Add(new Paragraph(label).SetFont(font))
            .SetPadding(5));
        
        table.AddCell(new Cell()
            .Add(new Paragraph(value).SetFont(font))
            .SetPadding(5));
    }
} 