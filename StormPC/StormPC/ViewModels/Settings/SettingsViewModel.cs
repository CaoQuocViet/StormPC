using System.Reflection;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using StormPC.Contracts;
using StormPC.Helpers;
using Windows.ApplicationModel;
using StormPC.Core.Services;
using Microsoft.UI.Xaml.Controls;

namespace StormPC.ViewModels.Settings;

public partial class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly IDatabaseService _databaseService;

    [ObservableProperty]
    private ElementTheme _elementTheme;

    [ObservableProperty]
    private string _versionDescription;

    [ObservableProperty]
    private bool _isBackupInProgress;

    [ObservableProperty]
    private bool _isRestoreInProgress;

    public ICommand SwitchThemeCommand { get; }
    public IAsyncRelayCommand BackupDatabaseCommand { get; }
    public IAsyncRelayCommand RestoreDatabaseCommand { get; }

    public IEnumerable<ElementTheme> Themes => Enum.GetValues<ElementTheme>();

    public SettingsViewModel(IThemeSelectorService themeSelectorService, IDatabaseService databaseService)
    {
        _themeSelectorService = themeSelectorService;
        _databaseService = databaseService;
        _elementTheme = _themeSelectorService.Theme;
        _versionDescription = GetVersionDescription();

        SwitchThemeCommand = new RelayCommand<ElementTheme>(
            async (param) =>
            {
                if (ElementTheme != param)
                {
                    ElementTheme = param;
                    await _themeSelectorService.SetThemeAsync(param);
                }
            });

        BackupDatabaseCommand = new AsyncRelayCommand(BackupDatabaseAsync);
        RestoreDatabaseCommand = new AsyncRelayCommand(RestoreDatabaseAsync);
    }

    private async Task BackupDatabaseAsync()
    {
        try
        {
            IsBackupInProgress = true;
            var backupPath = await _databaseService.BackupDatabaseAsync();
            
            var dialog = new ContentDialog
            {
                Title = "Backup Successful",
                Content = $"Database has been backed up to:\n{backupPath}",
                CloseButtonText = "OK",
                XamlRoot = App.MainWindow.Content.XamlRoot
            };
            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            var dialog = new ContentDialog
            {
                Title = "Backup Failed",
                Content = $"Failed to backup database: {ex.Message}",
                CloseButtonText = "OK",
                XamlRoot = App.MainWindow.Content.XamlRoot
            };
            await dialog.ShowAsync();
        }
        finally
        {
            IsBackupInProgress = false;
        }
    }

    private async Task RestoreDatabaseAsync()
    {
        try
        {
            IsRestoreInProgress = true;

            var confirmDialog = new ContentDialog
            {
                Title = "Confirm Restore",
                Content = "This will overwrite your current database with the last backup. Are you sure you want to continue?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = App.MainWindow.Content.XamlRoot
            };

            var result = await confirmDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                await _databaseService.RestoreDatabaseAsync();
                var successDialog = new ContentDialog
                {
                    Title = "Restore Successful",
                    Content = "Database has been restored successfully. Please restart the application.",
                    CloseButtonText = "OK",
                    XamlRoot = App.MainWindow.Content.XamlRoot
                };
                await successDialog.ShowAsync();
            }
        }
        catch (Exception ex)
        {
            var dialog = new ContentDialog
            {
                Title = "Restore Failed",
                Content = $"Failed to restore database: {ex.Message}",
                CloseButtonText = "OK",
                XamlRoot = App.MainWindow.Content.XamlRoot
            };
            await dialog.ShowAsync();
        }
        finally
        {
            IsRestoreInProgress = false;
        }
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;
            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
} 