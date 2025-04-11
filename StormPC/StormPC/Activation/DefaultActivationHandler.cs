using Microsoft.UI.Xaml;

using StormPC.Contracts;
using StormPC.ViewModels;
using StormPC.ViewModels.Shell;
using StormPC.Contracts.Services;

namespace StormPC.Activation;

/// <summary>
/// Lớp xử lý kích hoạt mặc định cho ứng dụng
/// </summary>
public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    /// <summary>
    /// Dịch vụ điều hướng
    /// </summary>
    private readonly INavigationService _navigationService;

    /// <summary>
    /// Khởi tạo một đối tượng DefaultActivationHandler mới
    /// </summary>
    /// <param name="navigationService">Dịch vụ điều hướng</param>
    public DefaultActivationHandler(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    /// <summary>
    /// Kiểm tra xem handler có thể xử lý đối số kích hoạt không
    /// </summary>
    /// <param name="args">Đối số kích hoạt</param>
    /// <returns>True nếu không có handler nào khác đã xử lý kích hoạt</returns>
    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
    {
        // None of the ActivationHandlers has handled the activation.
        return _navigationService.Frame?.Content == null;
    }

    /// <summary>
    /// Xử lý sự kiện kích hoạt
    /// </summary>
    protected override async Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        _navigationService.NavigateTo(typeof(ShellViewModel).FullName!, args.Arguments);
        await Task.CompletedTask;
    }
}
