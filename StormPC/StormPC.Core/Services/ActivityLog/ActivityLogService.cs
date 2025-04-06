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
            Debug.WriteLine($"Attempting to load logs from: {_logFilePath}");
            if (File.Exists(_logFilePath))
            {
                Debug.WriteLine("Log file exists, reading content...");
                var jsonContent = File.ReadAllText(_logFilePath);
                Debug.WriteLine($"File content length: {jsonContent?.Length ?? 0}");
                _logs = JsonSerializer.Deserialize<List<ActivityLogEntry>>(jsonContent) ?? new List<ActivityLogEntry>();
                Debug.WriteLine($"Successfully loaded {_logs.Count} logs");
            }
            else
            {
                Debug.WriteLine("Log file does not exist, creating new log list");
                _logs = new List<ActivityLogEntry>();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to load logs: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
            _logs = new List<ActivityLogEntry>();
        }
    }

    public async Task LogActivityAsync(string module, string action, string details, string status, string username)
    {
        try
        {
            Debug.WriteLine($"Logging activity: {module} - {action} - {details}");
            
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
                Debug.WriteLine($"Added log entry. Total logs: {_logs.Count}");
            }

            await SaveLogsAsync();
            Debug.WriteLine($"Saved logs to file: {_logFilePath}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to log activity: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
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
            Debug.WriteLine($"Attempting to save {_logs.Count} logs");
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonContent = JsonSerializer.Serialize(_logs, options);
            Debug.WriteLine($"Serialized content length: {jsonContent.Length}");
            
            // Write to a temporary file first
            var tempPath = _logFilePath + ".tmp";
            Debug.WriteLine($"Writing to temporary file: {tempPath}");
            await File.WriteAllTextAsync(tempPath, jsonContent);
            
            // Then replace the original file
            if (File.Exists(_logFilePath))
            {
                Debug.WriteLine($"Deleting existing log file: {_logFilePath}");
                File.Delete(_logFilePath);
            }
            Debug.WriteLine($"Moving temporary file to final location");
            File.Move(tempPath, _logFilePath);
            
            // Verify the file was written
            if (File.Exists(_logFilePath))
            {
                var fileInfo = new FileInfo(_logFilePath);
                Debug.WriteLine($"File saved successfully. Size: {fileInfo.Length} bytes");
            }
            else
            {
                Debug.WriteLine("Warning: File does not exist after save operation!");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to save logs: {ex.Message}");
            Debug.WriteLine($"Stack trace: {ex.StackTrace}");
        }
    }
} 