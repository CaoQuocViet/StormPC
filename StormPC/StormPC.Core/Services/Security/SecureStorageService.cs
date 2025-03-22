using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using StormPC.Core.Models.Login;

namespace StormPC.Core.Services.Security;

public class SecureStorageService : ISecureStorageService
{
    private const string LOGIN_INFO_FILE = "login_info.dat";
    private const string SESSION_TOKEN_FILE = "session.dat";
    private const string BACKUP_KEY_FILE = "backup.dat";
    private readonly string _basePath;

    public SecureStorageService()
    {
        _basePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "StormPC",
            "Security"
        );
        Directory.CreateDirectory(_basePath);
    }

    public async Task SaveLoginInfoAsync(LoginInfo loginInfo)
    {
        await SaveSecureDataAsync(LOGIN_INFO_FILE, loginInfo);
    }

    public async Task<LoginInfo?> LoadLoginInfoAsync()
    {
        return await LoadSecureDataAsync<LoginInfo>(LOGIN_INFO_FILE);
    }

    public async Task SaveSessionTokenAsync(SessionToken token)
    {
        await SaveSecureDataAsync(SESSION_TOKEN_FILE, token);
    }

    public async Task<SessionToken?> LoadSessionTokenAsync()
    {
        return await LoadSecureDataAsync<SessionToken>(SESSION_TOKEN_FILE);
    }

    public async Task SaveBackupKeyAsync(BackupKey backupKey)
    {
        await SaveSecureDataAsync(BACKUP_KEY_FILE, backupKey);
    }

    public async Task<BackupKey?> LoadBackupKeyAsync()
    {
        return await LoadSecureDataAsync<BackupKey>(BACKUP_KEY_FILE);
    }

    public async Task ClearAllAsync()
    {
        var files = new[] { LOGIN_INFO_FILE, SESSION_TOKEN_FILE, BACKUP_KEY_FILE };
        foreach (var file in files)
        {
            var path = Path.Combine(_basePath, file);
            if (File.Exists(path))
            {
                await Task.Run(() => File.Delete(path));
            }
        }
    }

    private async Task SaveSecureDataAsync<T>(string fileName, T data)
    {
        var json = JsonSerializer.Serialize(data);
        var bytes = System.Text.Encoding.UTF8.GetBytes(json);
        var encrypted = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
        var path = Path.Combine(_basePath, fileName);
        await File.WriteAllBytesAsync(path, encrypted);
    }

    private async Task<T?> LoadSecureDataAsync<T>(string fileName)
    {
        var path = Path.Combine(_basePath, fileName);
        if (!File.Exists(path)) return default;

        var encrypted = await File.ReadAllBytesAsync(path);
        var decrypted = ProtectedData.Unprotect(encrypted, null, DataProtectionScope.CurrentUser);
        var json = System.Text.Encoding.UTF8.GetString(decrypted);
        return JsonSerializer.Deserialize<T>(json);
    }
} 