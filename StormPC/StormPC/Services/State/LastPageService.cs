using StormPC.Contracts;
using StormPC.Core.Helpers;
using StormPC.Helpers;

namespace StormPC.Services;

/// <summary>
/// Dịch vụ quản lý và lưu trữ trang cuối cùng đã truy cập
/// </summary>
public class LastPageService : ILastPageService
{
    private const string SettingsKey = "LastPageType";
    private readonly ILocalSettingsService _localSettingsService;

    /// <summary>
    /// Khởi tạo dịch vụ quản lý trang cuối cùng
    /// </summary>
    public LastPageService(ILocalSettingsService localSettingsService)
    {
        _localSettingsService = localSettingsService;
    }

    /// <summary>
    /// Lấy thông tin về trang cuối cùng đã truy cập
    /// </summary>
    public async Task<string?> GetLastPageAsync()
    {
        return await _localSettingsService.ReadSettingAsync<string>(SettingsKey);
    }

    /// <summary>
    /// Lưu thông tin về trang hiện tại làm trang cuối cùng
    /// </summary>
    public async Task SaveLastPageAsync(string pageType)
    {
        await _localSettingsService.SaveSettingAsync<string>(SettingsKey, pageType);
    }

    /// <summary>
    /// Xóa thông tin về trang cuối cùng đã lưu
    /// </summary>
    public async Task ClearLastPageAsync()
    {
        if (RuntimeHelper.IsMSIX)
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values.Remove(SettingsKey);
        }
        else
        {
            await _localSettingsService.SaveSettingAsync<string>(SettingsKey, string.Empty);
        }
    }
}