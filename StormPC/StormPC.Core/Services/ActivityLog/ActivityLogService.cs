using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using StormPC.Core.Contracts.Services;
using StormPC.Core.Models.ActivityLog;

namespace StormPC.Core.Services.ActivityLog;

public class ActivityLogService : IActivityLogService
{
    private readonly string _logFilePath;
    private List<ActivityLogEntry> _logs;
    private readonly object _lockObject = new();

    public ActivityLogService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var stormPcPath = Path.Combine(appDataPath, "StormPC");
        Directory.CreateDirectory(stormPcPath);
        _logFilePath = Path.Combine(stormPcPath, "activity_logs.dat");
        _logs = new List<ActivityLogEntry>();
        LoadLogs();
    }

    private void LoadLogs()
    {
        try
        {
            Debug.WriteLine($"Đang nạp nhật ký từ: {_logFilePath}");
            if (File.Exists(_logFilePath))
            {
                Debug.WriteLine("Tệp nhật ký tồn tại, đang đọc nội dung...");
                var jsonContent = File.ReadAllText(_logFilePath);
                Debug.WriteLine($"Độ dài nội dung tệp: {jsonContent?.Length ?? 0}");
                _logs = JsonSerializer.Deserialize<List<ActivityLogEntry>>(jsonContent) ?? new List<ActivityLogEntry>();
                Debug.WriteLine($"Đã nạp thành công {_logs.Count} bản ghi nhật ký");
            }
            else
            {
                Debug.WriteLine("Tệp nhật ký không tồn tại, tạo danh sách mới");
                _logs = new List<ActivityLogEntry>();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Không thể nạp nhật ký: {ex.Message}");
            Debug.WriteLine($"Chi tiết lỗi: {ex.StackTrace}");
            _logs = new List<ActivityLogEntry>();
        }
    }

    public async Task LogActivityAsync(string module, string action, string details, string status, string username)
    {
        try
        {
            Debug.WriteLine($"Ghi nhật ký hoạt động: {module} - {action} - {details}");
            
            var logEntry = new ActivityLogEntry
            {
                Timestamp = DateTime.Now,
                Module = module,
                Action = action,
                Details = details,
                Status = status,
                Username = username
            };

            lock (_lockObject)
            {
                _logs.Add(logEntry);
                Debug.WriteLine($"Đã thêm bản ghi nhật ký. Tổng số nhật ký: {_logs.Count}");
            }

            await SaveLogsAsync();
            Debug.WriteLine($"Đã lưu nhật ký vào tệp: {_logFilePath}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Không thể ghi nhật ký hoạt động: {ex.Message}");
            Debug.WriteLine($"Chi tiết lỗi: {ex.StackTrace}");
        }
    }

    public Task<List<ActivityLogEntry>> GetAllLogsAsync()
    {
        lock (_lockObject)
        {
            return Task.FromResult(_logs.ToList());
        }
    }

    public async Task ClearLogsAsync()
    {
        lock (_lockObject)
        {
            _logs.Clear();
        }
        await SaveLogsAsync();
    }

    public async Task SaveLogsAsync()
    {
        try
        {
            Debug.WriteLine($"Đang lưu {_logs.Count} bản ghi nhật ký");
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonContent = JsonSerializer.Serialize(_logs, options);
            Debug.WriteLine($"Độ dài nội dung đã chuyển đổi: {jsonContent.Length}");
            
            // Ghi vào tệp tạm trước
            var tempPath = _logFilePath + ".tmp";
            Debug.WriteLine($"Đang ghi vào tệp tạm: {tempPath}");
            await File.WriteAllTextAsync(tempPath, jsonContent);
            
            // Sau đó thay thế tệp gốc
            if (File.Exists(_logFilePath))
            {
                Debug.WriteLine($"Đang xóa tệp nhật ký hiện tại: {_logFilePath}");
                File.Delete(_logFilePath);
            }
            Debug.WriteLine($"Đang di chuyển tệp tạm đến vị trí cuối cùng");
            File.Move(tempPath, _logFilePath);
            
            // Xác minh tệp đã được ghi
            if (File.Exists(_logFilePath))
            {
                var fileInfo = new FileInfo(_logFilePath);
                Debug.WriteLine($"Tệp đã được lưu thành công. Kích thước: {fileInfo.Length} byte");
            }
            else
            {
                Debug.WriteLine("Cảnh báo: Tệp không tồn tại sau khi thực hiện lưu!");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Không thể lưu nhật ký: {ex.Message}");
            Debug.WriteLine($"Chi tiết lỗi: {ex.StackTrace}");
        }
    }
}