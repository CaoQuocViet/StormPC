using System;

namespace StormPC.Core.Models.Login;

public class SessionToken
{
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsValid => DateTime.Now < ExpiresAt;
} 