using StormPC.Contracts;
using StormPC.Core.Helpers;
using StormPC.Helpers;

namespace StormPC.Services;

public class LastPageService : ILastPageService
{
    private const string SettingsKey = "LastPageType";
    private readonly ILocalSettingsService _localSettingsService;

    public LastPageService(ILocalSettingsService localSettingsService)
    {
        _localSettingsService = localSettingsService;
    }

    public async Task<string?> GetLastPageAsync()
    {
        return await _localSettingsService.ReadSettingAsync<string>(SettingsKey);
    }

    public async Task SaveLastPageAsync(string pageType)
    {
        await _localSettingsService.SaveSettingAsync<string>(SettingsKey, pageType);
    }

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