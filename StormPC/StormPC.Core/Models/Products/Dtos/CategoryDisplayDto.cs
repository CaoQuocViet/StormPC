using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StormPC.Core.Models.Products.Dtos;

public class CategoryDisplayDto : INotifyPropertyChanged
{
    private int _categoryID;
    private string _categoryName = string.Empty;
    private string? _description;
    private int _productCount;
    private DateTime _createdAt;
    private DateTime? _updatedAt;

    public int CategoryID
    {
        get => _categoryID;
        set
        {
            if (_categoryID != value)
            {
                _categoryID = value;
                OnPropertyChanged();
            }
        }
    }

    public string CategoryName
    {
        get => _categoryName;
        set
        {
            if (_categoryName != value)
            {
                _categoryName = value;
                OnPropertyChanged();
            }
        }
    }

    public string? Description
    {
        get => _description;
        set
        {
            if (_description != value)
            {
                _description = value;
                OnPropertyChanged();
            }
        }
    }

    public int ProductCount
    {
        get => _productCount;
        set
        {
            if (_productCount != value)
            {
                _productCount = value;
                OnPropertyChanged();
            }
        }
    }

    public DateTime CreatedAt
    {
        get => _createdAt;
        set
        {
            if (_createdAt != value)
            {
                _createdAt = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FormattedCreatedAt));
            }
        }
    }

    public DateTime? UpdatedAt
    {
        get => _updatedAt;
        set
        {
            if (_updatedAt != value)
            {
                _updatedAt = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FormattedUpdatedAt));
            }
        }
    }

    public string FormattedCreatedAt => CreatedAt.ToString("dd/MM/yyyy HH:mm");
    public string FormattedUpdatedAt => UpdatedAt?.ToString("dd/MM/yyyy HH:mm") ?? "--";

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
} 