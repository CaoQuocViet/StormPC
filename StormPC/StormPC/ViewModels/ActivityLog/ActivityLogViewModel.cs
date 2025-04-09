using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StormPC.Core.Contracts.Services;
using StormPC.Core.Models.ActivityLog;
using System.Diagnostics;
using Microsoft.UI.Dispatching;
using System.Linq;

namespace StormPC.ViewModels.ActivityLog;

public partial class ActivityLogViewModel : ObservableObject
{
    private readonly IActivityLogService _activityLogService;

    [ObservableProperty]
    private ObservableCollection<ActivityLogEntry> _logs;

    public ICommand ClearLogsCommand { get; }
    public ICommand RefreshLogsCommand { get; }

    public ActivityLogViewModel(IActivityLogService activityLogService)
    {
        _activityLogService = activityLogService;
        _logs = new ObservableCollection<ActivityLogEntry>();
        ClearLogsCommand = new AsyncRelayCommand(ClearLogsAsync);
        RefreshLogsCommand = new AsyncRelayCommand(LoadLogsAsync);
        
        // Load logs immediately when the view model is created
        _ = LoadLogsAsync();
    }

    private async Task LoadLogsAsync()
    {
        try
        {
            Debug.WriteLine("Loading logs...");
            var logs = await _activityLogService.GetAllLogsAsync();
            Debug.WriteLine($"Loaded {logs.Count} logs");

            // Sort logs by timestamp in descending order (newest first)
            logs = logs.OrderByDescending(log => log.Timestamp).ToList();

            // Update the ObservableCollection on the UI thread
            var dispatcher = DispatcherQueue.GetForCurrentThread();
            if (dispatcher != null)
            {
                dispatcher.TryEnqueue(() =>
                {
                    Logs.Clear();
                    foreach (var log in logs)
                    {
                        Logs.Add(log);
                    }
                    Debug.WriteLine($"Updated UI with {Logs.Count} logs");
                });
            }
            else
            {
                Debug.WriteLine("Warning: No dispatcher available for UI updates");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading logs: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }

    private async Task ClearLogsAsync()
    {
        await _activityLogService.ClearLogsAsync();
        await LoadLogsAsync();
    }
}