using System;

namespace StormPC.Core.Models.Login;

public class UserAccount
{
    public string Username { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string? BackupKeyHash { get; set; }
    public int FailedLoginAttempts { get; set; }
    public DateTime? LastFailedLoginAttempt { get; set; }
} 