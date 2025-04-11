using Microsoft.UI.Xaml;

using StormPC.Contracts;
using StormPC.Helpers;

namespace StormPC.Services;

/// <summary>
/// Dịch vụ quản lý và chọn chủ đề giao diện
/// </summary>
public class ThemeSelectorService : IThemeSelectorService
{
    private const string SettingsKey = "AppBackgroundRequestedTheme";

    /// <summary>
    /// Chủ đề hiện tại của ứng dụng
    /// </summary>
    public ElementTheme Theme { get; set; } = ElementTheme.Default;

    private readonly ILocalSettingsService _localSettingsService;

    /// <summary>
    /// Khởi tạo dịch vụ chọn chủ đề
    /// </summary>
    public ThemeSelectorService(ILocalSettingsService localSettingsService)
    {
        _localSettingsService = localSettingsService;
    }

    /// <summary>
    /// Khởi tạo và nạp chủ đề từ cài đặt
    /// </summary>
    public async Task InitializeAsync()
    {
        Theme = await LoadThemeFromSettingsAsync();
        await Task.CompletedTask;
    }

    /// <summary>
    /// Thiết lập chủ đề mới và lưu vào cài đặt
    /// </summary>
    public async Task SetThemeAsync(ElementTheme theme)
    {
        Theme = theme;

        await SetRequestedThemeAsync();
        await SaveThemeInSettingsAsync(Theme);
    }

    /// <summary>
    /// Áp dụng chủ đề đã chọn cho giao diện ứng dụng
    /// </summary>
    public async Task SetRequestedThemeAsync()
    {
        if (App.MainWindow.Content is FrameworkElement rootElement)
        {
            rootElement.RequestedTheme = Theme;

            TitleBarHelper.UpdateTitleBar(Theme);
        }

        await Task.CompletedTask;
    }

    /// <summary>
    /// Nạp chủ đề từ cài đặt đã lưu
    /// </summary>
    private async Task<ElementTheme> LoadThemeFromSettingsAsync()
    {
        var themeName = await _localSettingsService.ReadSettingAsync<string>(SettingsKey);

        if (Enum.TryParse(themeName, out ElementTheme cacheTheme))
        {
            return cacheTheme;
        }

        return ElementTheme.Default;
    }

    /// <summary>
    /// Lưu chủ đề vào cài đặt
    /// </summary>
    private async Task SaveThemeInSettingsAsync(ElementTheme theme)
    {
        await _localSettingsService.SaveSettingAsync(SettingsKey, theme.ToString());
    }
}
