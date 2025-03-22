using System;

namespace StormPC.Core.Models.Login;

public class LoginInfo
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool UseWindowsHello { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastLoginAt { get; set; }
} 