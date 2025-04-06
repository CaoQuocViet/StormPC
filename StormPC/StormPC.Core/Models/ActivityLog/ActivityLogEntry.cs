using System;

namespace StormPC.Core.Models.ActivityLog;

public class ActivityLogEntry
{
    public DateTime Timestamp { get; set; }
    public string FormattedTimestamp => Timestamp.ToString("dd/MM/yyyy HH:mm:ss");
    public string Module { get; set; }
    public string Action { get; set; }
    public string Details { get; set; }
    public string Status { get; set; } // Success/Error
    public string Username { get; set; }
} 