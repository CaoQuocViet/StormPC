using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using StormPC.Activation;
using StormPC.Contracts;
using StormPC.Views;
using StormPC.Views.Shell;

namespace StormPC.Services;

/// <summary>
/// Dịch vụ kích hoạt ứng dụng, xử lý quá trình khởi động ứng dụng
/// </summary>
public class ActivationService : IActivationService
{
    private readonly ActivationHandler<LaunchActivatedEventArgs> _defaultHandler;
    private readonly IEnumerable<IActivationHandler> _activationHandlers;
    private readonly IThemeSelectorService _themeSelectorService;
    private UIElement? _shell = null;

    /// <summary>
    /// Khởi tạo dịch vụ kích hoạt với các handler cần thiết
    /// </summary>
    public ActivationService(ActivationHandler<LaunchActivatedEventArgs> defaultHandler, IEnumerable<IActivationHandler> activationHandlers, IThemeSelectorService themeSelectorService)
    {
        _defaultHandler = defaultHandler;
        _activationHandlers = activationHandlers;
        _themeSelectorService = themeSelectorService;
    }

    /// <summary>
    /// Kích hoạt ứng dụng với các tham số được cung cấp
    /// </summary>
    public async Task ActivateAsync(object activationArgs)
    {
        // Thực hiện các tác vụ trước khi kích hoạt
        await InitializeAsync();

        // Thiết lập nội dung cho MainWindow
        if (App.MainWindow.Content == null)
        {
            _shell = App.GetService<ShellPage>();
            App.MainWindow.Content = _shell ?? new Frame();
        }

        // Xử lý kích hoạt thông qua các ActivationHandler
        await HandleActivationAsync(activationArgs);

        // Kích hoạt MainWindow
        App.MainWindow.Activate();

        // Thực hiện các tác vụ sau khi kích hoạt
        await StartupAsync();
    }

    /// <summary>
    /// Xử lý quá trình kích hoạt thông qua handler phù hợp
    /// </summary>
    private async Task HandleActivationAsync(object activationArgs)
    {
        var activationHandler = _activationHandlers.FirstOrDefault(h => h.CanHandle(activationArgs));

        if (activationHandler != null)
        {
            await activationHandler.HandleAsync(activationArgs);
        }

        if (_defaultHandler.CanHandle(activationArgs))
        {
            await _defaultHandler.HandleAsync(activationArgs);
        }
    }

    /// <summary>
    /// Khởi tạo các dịch vụ cần thiết trước khi kích hoạt
    /// </summary>
    private async Task InitializeAsync()
    {
        await _themeSelectorService.InitializeAsync().ConfigureAwait(false);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Thực hiện các tác vụ sau khi kích hoạt ứng dụng
    /// </summary>
    private async Task StartupAsync()
    {
        await _themeSelectorService.SetRequestedThemeAsync();
        await Task.CompletedTask;
    }
}
