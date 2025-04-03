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
    private City selectedCity;

    [ObservableProperty]
    private OrderStatus selectedStatus;

    [ObservableProperty]
    private ObservableCollection<LaptopSpec> filteredLaptopSpecs;

    public string TotalAmountText => $"Tổng tiền: {string.Format("{0:N0} VNĐ", CalculateTotalAmount())}";

    private decimal CalculateTotalAmount()
    {
        if (SelectedLaptopSpec == null) return 0;
        return SelectedLaptopSpec.Price * Quantity;
    }

    partial void OnQuantityChanged(int value)
    {
        OnPropertyChanged(nameof(TotalAmountText));
    }

    partial void OnSelectedLaptopSpecChanged(LaptopSpec value)
    {
        OnPropertyChanged(nameof(TotalAmountText));
    }

    partial void OnSelectedLaptopChanged(Laptop value)
    {
        if (value != null)
        {
            // Filter specs for selected laptop
            FilteredLaptopSpecs = new ObservableCollection<LaptopSpec>(
                LaptopSpecs.Where(spec => spec.LaptopID == value.LaptopID)
            );
            // Clear selected spec when laptop changes
            SelectedLaptopSpec = null;
        }
        else
        {
            FilteredLaptopSpecs?.Clear();
            SelectedLaptopSpec = null;
        }
    }

    partial void OnSelectedCustomerChanged(Customer value)
    {
        if (value != null)
        {
            // Auto-fill shipping address with customer's address
            ShippingAddress = value.Address;
            
            // Auto-select customer's city
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