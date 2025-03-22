using System;

namespace StormPC.Core.Models.Login;

public class BackupKey
{
    public string Key { get; set; } = string.Empty;
    public string EncryptedData { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
} 