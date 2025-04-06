using System.Collections.Generic;
using System.Threading.Tasks;
using StormPC.Core.Models.ActivityLog;

namespace StormPC.Core.Contracts.Services;

public interface IActivityLogService
{
    Task LogActivityAsync(string module, string action, string details, string status, string username);
    Task<List<ActivityLogEntry>> GetAllLogsAsync();
    Task ClearLogsAsync();
    Task SaveLogsAsync();
} 