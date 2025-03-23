using System;

namespace StormPC.Core.Models.Login;

public class UserSession
{
    public string Username { get; set; } = string.Empty;
    public string SessionToken { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsAdmin { get; set; }
} 