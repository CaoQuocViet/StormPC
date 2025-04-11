using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

using StormPC.Contracts;
using StormPC.Contracts.Services;
using StormPC.Helpers;

namespace StormPC.Services;

/// <summary>
/// Dịch vụ điều hướng giữa các trang trong ứng dụng
/// </summary>
public class NavigationService : INavigationService
{
    private readonly IPageService _pageService;
    private object? _lastParameterUsed;
    private Frame? _frame;

    public event NavigatedEventHandler? Navigated;

    /// <summary>
    /// Frame điều hướng chính của ứng dụng
    /// </summary>
    public Frame? Frame
    {
        get
        {
            if (_frame == null)
            {
                _frame = App.MainWindow.Content as Frame;
                RegisterFrameEvents();
            }

            return _frame;
        }

        set
        {
            UnregisterFrameEvents();
            _frame = value;
            RegisterFrameEvents();
        }
    }

    /// <summary>
    /// Kiểm tra xem có thể quay lại trang trước đó không
    /// </summary>
    [MemberNotNullWhen(true, nameof(Frame), nameof(_frame))]
    public bool CanGoBack => Frame != null && Frame.CanGoBack;

    /// <summary>
    /// Khởi tạo dịch vụ điều hướng
    /// </summary>
    public NavigationService(IPageService pageService)
    {
        _pageService = pageService;
    }

    /// <summary>
    /// Đăng ký sự kiện cho frame điều hướng
    /// </summary>
    private void RegisterFrameEvents()
    {
        if (_frame != null)
        {
            _frame.Navigated += OnNavigated;
        }
    }

    /// <summary>
    /// Hủy đăng ký sự kiện cho frame điều hướng
    /// </summary>
    private void UnregisterFrameEvents()
    {
        if (_frame != null)
        {
            _frame.Navigated -= OnNavigated;
        }
    }

    /// <summary>
    /// Quay lại trang trước đó
    /// </summary>
    public bool GoBack()
    {
        if (CanGoBack)
        {
            var vmBeforeNavigation = _frame?.GetPageViewModel();
            _frame?.GoBack();
            if (vmBeforeNavigation is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedFrom();
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Điều hướng đến trang được chỉ định bằng khóa
    /// </summary>
    public bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false)
    {
        var pageType = _pageService.GetPageType(pageKey);

        if (_frame != null && (_frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed))))
        {
            _frame.Tag = clearNavigation;
            var vmBeforeNavigation = _frame.GetPageViewModel();
            var navigated = _frame.Navigate(pageType, parameter);
            if (navigated)
            {
                _lastParameterUsed = parameter;
                if (vmBeforeNavigation is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedFrom();
                }
            }

            return navigated;
        }

        return false;
    }

    /// <summary>
    /// Điều hướng đến trang được chỉ định bằng khóa một cách bất đồng bộ
    /// </summary>
    public async Task<bool> NavigateToAsync(string pageKey, object? parameter = null, bool clearNavigation = false)
    {
        return await Task.Run(() => NavigateTo(pageKey, parameter, clearNavigation));
    }

    /// <summary>
    /// Xử lý sự kiện khi điều hướng xảy ra
    /// </summary>
    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        if (sender is Frame frame)
        {
            var clearNavigation = (bool)frame.Tag;
            if (clearNavigation)
            {
                frame.BackStack.Clear();
            }

            if (frame.GetPageViewModel() is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(e.Parameter);
            }

            Navigated?.Invoke(sender, e);
        }
    }
}
