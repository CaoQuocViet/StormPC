using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StormPC.Core.Models.Customers.Dtos;

public class CustomerDisplayDto : INotifyPropertyChanged
{
    private int _customerID;
    private string _fullName = string.Empty;
    private string _email = string.Empty;
    private string _phone = string.Empty;
    private string _address = string.Empty;
    private string _cityName = string.Empty;

    public int CustomerID
    {
        get => _customerID;
        set
        {
            if (_customerID != value)
            {
                _customerID = value;
                OnPropertyChanged();
            }
        }
    }

    public string FullName
    {
        get => _fullName;
        set
        {
            if (_fullName != value)
            {
                _fullName = value;
                OnPropertyChanged();
            }
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (_email != value)
            {
                _email = value;
                OnPropertyChanged();
            }
        }
    }

    public string Phone
    {
        get => _phone;
        set
        {
            if (_phone != value)
            {
                _phone = value;
                OnPropertyChanged();
            }
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            if (_address != value)
            {
                _address = value;
                OnPropertyChanged();
            }
        }
    }

    public string CityName
    {
        get => _cityName;
        set
        {
            if (_cityName != value)
            {
                _cityName = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
} 