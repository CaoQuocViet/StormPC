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
        
        // Tải log ngay khi view model được tạo
        _ = LoadLogsAsync();
    }

    private async Task LoadLogsAsync()
    {
        try
        {
            Debug.WriteLine("Đang tải log...");
            var logs = await _activityLogService.GetAllLogsAsync();
            Debug.WriteLine($"Đã tải {logs.Count} bản ghi log");

            // Sắp xếp log theo thời gian giảm dần (mới nhất trước)
            logs = logs.OrderByDescending(log => log.Timestamp).ToList();

            // Cập nhật ObservableCollection trên luồng UI
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
                    Debug.WriteLine($"Đã cập nhật UI với {Logs.Count} bản ghi log");
                });
            }
            else
            {
                Debug.WriteLine("Cảnh báo: Không có dispatcher khả dụng cho cập nhật UI");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Lỗi khi tải log: {ex.Message}");
            Debug.WriteLine($"Chi tiết lỗi: {ex.StackTrace}");
        }
    }

    private async Task ClearLogsAsync()
    {
        await _activityLogService.ClearLogsAsync();
        await LoadLogsAsync();
    }
}