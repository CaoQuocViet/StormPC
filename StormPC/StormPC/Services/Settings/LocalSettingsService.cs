﻿using Microsoft.Extensions.Options;

using StormPC.Contracts;
using StormPC.Core.Contracts.Services;
using StormPC.Core.Helpers;
using StormPC.Helpers;
using StormPC.Models;

using Windows.ApplicationModel;
using Windows.Storage;

namespace StormPC.Services;

public class LocalSettingsService : ILocalSettingsService
{
    private const string _defaultApplicationDataFolder = "StormPC/ApplicationData";
    private const string _defaultLocalSettingsFile = "LocalSettings.json";

    private readonly IFileService _fileService;
    private readonly LocalSettingsOptions _options;

    private readonly string _localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private readonly string _applicationDataFolder;
    private readonly string _localsettingsFile;

    private IDictionary<string, object> _settings;

    private bool _isInitialized;

    public LocalSettingsService(IFileService fileService, IOptions<LocalSettingsOptions> options)
    {
        _fileService = fileService;
        _options = options.Value;

        _applicationDataFolder = Path.Combine(_localApplicationData, _options.ApplicationDataFolder ?? _defaultApplicationDataFolder);
        _localsettingsFile = _options.LocalSettingsFile ?? _defaultLocalSettingsFile;

        _settings = new Dictionary<string, object>();
    }

    private async Task InitializeAsync()
    {
        if (!_isInitialized)
        {
            _settings = await Task.Run(() => _fileService.Read<IDictionary<string, object>>(_applicationDataFolder, _localsettingsFile)) ?? new Dictionary<string, object>();

            _isInitialized = true;
        }
    }

    public async Task<T?> ReadSettingAsync<T>(string key)
    {
        if (RuntimeHelper.IsMSIX)
        {
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue(key, out var obj))
            {
                return await Json.ToObjectAsync<T>((string)obj);
            }
        }
        else
        {
            await InitializeAsync();

            if (_settings != null && _settings.TryGetValue(key, out var obj))
            {
                return await Json.ToObjectAsync<T>((string)obj);
            }
        }

        return default;
    }

    public async Task SaveSettingAsync<T>(string key, T? value)
    {
        if (RuntimeHelper.IsMSIX)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value != null ? await Json.StringifyAsync(value) : null;
        }
        else
        {
            await InitializeAsync();

            if (value != null)
            {
                _settings[key] = await Json.StringifyAsync(value);
            }
            else
            {
                _settings[key] = null!;
            }

            await Task.Run(() => _fileService.Save(_applicationDataFolder, _localsettingsFile, _settings));
        }
    }
}
