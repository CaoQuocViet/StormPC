using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace StormPC.Contracts.Services;

public interface INavigationService
{
    /// <summary>
    /// Sự kiện khi điều hướng hoàn tất
    /// </summary>
    event NavigatedEventHandler Navigated;

    /// <summary>
    /// Kiểm tra xem có thể quay lại không
    /// </summary>
    bool CanGoBack
    {
        get;
    }

    /// <summary>
    /// Khung điều hướng hiện tại
    /// </summary>
    Frame? Frame
    {
        get; set;
    }

    /// <summary>
    /// Điều hướng đến trang có khóa chỉ định
    /// </summary>
    bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false);

    /// <summary>
    /// Điều hướng đến trang có khóa chỉ định (bất đồng bộ)
    /// </summary>
    Task<bool> NavigateToAsync(string pageKey, object? parameter = null, bool clearNavigation = false);

    /// <summary>
    /// Quay lại trang trước đó
    /// </summary>
    bool GoBack();
}
