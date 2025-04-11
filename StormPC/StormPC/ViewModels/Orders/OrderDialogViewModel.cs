using CommunityToolkit.Mvvm.ComponentModel;
using StormPC.Core.Models.Orders;
using StormPC.Core.Models.Customers;
using StormPC.Core.Models.Products;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace StormPC.ViewModels.Orders;

public partial class OrderDialogViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Customer> customers;

    [ObservableProperty]
    private ObservableCollection<Laptop> laptops;

    [ObservableProperty]
    private ObservableCollection<LaptopSpec> laptopSpecs;

    [ObservableProperty]
    private ObservableCollection<PaymentMethod> paymentMethods;

    [ObservableProperty]
    private ObservableCollection<City> cities;

    [ObservableProperty]
    private ObservableCollection<OrderStatus> orderStatuses;

    [ObservableProperty]
    private Customer selectedCustomer;

    [ObservableProperty]
    private Laptop selectedLaptop;

    [ObservableProperty]
    private LaptopSpec selectedLaptopSpec;

    [ObservableProperty]
    private int quantity = 1;

    [ObservableProperty]
    private PaymentMethod selectedPaymentMethod;

    [ObservableProperty]
    private string shippingAddress = string.Empty;

    [ObservableProperty]
    private string shippingPostalCode = string.Empty;

    [ObservableProperty]
    private bool isNewOrder = true;

    public bool IsNotNewOrder => !IsNewOrder;

    [ObservableProperty]
    private City selectedCity;

    [ObservableProperty]
    private OrderStatus selectedStatus;

    [ObservableProperty]
    private ObservableCollection<LaptopSpec> filteredLaptopSpecs;

    // Cập nhật giá trị TotalAmountText
    public string TotalAmountText => $"Tổng tiền: {string.Format("{0:N0} VNĐ", CalculateTotalAmount())}";

    // Phương thức tính tổng tiền đơn hàng
    private decimal CalculateTotalAmount()
    {
        if (SelectedLaptopSpec == null) return 0;
        return SelectedLaptopSpec.Price * Quantity;
    }

    // Xử lý sự kiện khi thay đổi số lượng sản phẩm
    partial void OnQuantityChanged(int value)
    {
        OnPropertyChanged(nameof(TotalAmountText));
    }

    // Xử lý sự kiện khi thay đổi cấu hình laptop
    partial void OnSelectedLaptopSpecChanged(LaptopSpec value)
    {
        OnPropertyChanged(nameof(TotalAmountText));
    }

    // Xử lý sự kiện khi thay đổi laptop - lọc danh sách cấu hình tương ứng
    partial void OnSelectedLaptopChanged(Laptop value)
    {
        if (value != null)
        {
            // Lọc cấu hình cho laptop đã chọn
            FilteredLaptopSpecs = new ObservableCollection<LaptopSpec>(
                LaptopSpecs.Where(spec => spec.LaptopID == value.LaptopID)
            );
            // Xóa cấu hình đã chọn khi thay đổi laptop
            SelectedLaptopSpec = null;
        }
        else
        {
            FilteredLaptopSpecs?.Clear();
            SelectedLaptopSpec = null;
        }
    }

    // Xử lý sự kiện khi thay đổi khách hàng - tự động điền thông tin địa chỉ
    partial void OnSelectedCustomerChanged(Customer value)
    {
        if (value != null)
        {
            // Tự động điền địa chỉ giao hàng từ địa chỉ khách hàng
            ShippingAddress = value.Address;
            
            // Tự động chọn thành phố của khách hàng
            SelectedCity = Cities.FirstOrDefault(c => c.Id == value.CityId);
        }
    }

    public OrderDialogViewModel()
    {
        customers = new ObservableCollection<Customer>();
        laptops = new ObservableCollection<Laptop>();
        laptopSpecs = new ObservableCollection<LaptopSpec>();
        filteredLaptopSpecs = new ObservableCollection<LaptopSpec>();
        paymentMethods = new ObservableCollection<PaymentMethod>();
        cities = new ObservableCollection<City>();
        orderStatuses = new ObservableCollection<OrderStatus>();
    }
}