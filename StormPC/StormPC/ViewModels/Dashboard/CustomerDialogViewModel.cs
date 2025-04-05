using CommunityToolkit.Mvvm.ComponentModel;
using StormPC.Core.Models.Customers;
using System.Collections.ObjectModel;

namespace StormPC.ViewModels.Dashboard;

public partial class CustomerDialogViewModel : ObservableObject
{
    [ObservableProperty]
    private string _fullName = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _phone = string.Empty;

    [ObservableProperty]
    private string _address = string.Empty;

    [ObservableProperty]
    private ObservableCollection<City> _cities = new();

    [ObservableProperty]
    private City? _selectedCity;

    public bool IsValid => 
        !string.IsNullOrWhiteSpace(FullName) &&
        !string.IsNullOrWhiteSpace(Email) &&
        !string.IsNullOrWhiteSpace(Phone) &&
        !string.IsNullOrWhiteSpace(Address) &&
        SelectedCity != null;
} 