using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace StormPC.Core.Services.Login;

public class SecureStorageService
{
    private readonly string _storageFilePath;
    private const string STORAGE_FILENAME = "secure_storage.dat";

    public SecureStorageService()
    {
        var appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "StormPC");
        
        if (!Directory.Exists(appDataPath))
        {
            Directory.CreateDirectory(appDataPath);
        }

        _storageFilePath = Path.Combine(appDataPath, STORAGE_FILENAME);
    }

    public void SaveSecureData<T>(string key, T data)
    {
        var jsonData = JsonSerializer.Serialize(data);
        var dataBytes = Encoding.UTF8.GetBytes(jsonData);
        var encryptedData = ProtectedData.Protect(dataBytes, null, DataProtectionScope.CurrentUser);
        
        var storage = LoadStorageFile();
        storage[key] = Convert.ToBase64String(encryptedData);
        File.WriteAllText(_storageFilePath, JsonSerializer.Serialize(storage));
    }

    public T? LoadSecureData<T>(string key)
    {
        var storage = LoadStorageFile();
        if (!storage.TryGetValue(key, out var encryptedBase64))
        {
            return default;
        }

        try
        {
            var encryptedData = Convert.FromBase64String(encryptedBase64);
            var decryptedData = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.CurrentUser);
            var jsonData = Encoding.UTF8.GetString(decryptedData);
            return JsonSerializer.Deserialize<T>(jsonData);
        }
        catch
        {
            return default;
        }
    }

    private Dictionary<string, string> LoadStorageFile()
    {
        if (!File.Exists(_storageFilePath))
        {
            return new Dictionary<string, string>();
        }

        try
        {
            var json = File.ReadAllText(_storageFilePath);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json) 
                   ?? new Dictionary<string, string>();
        }
        catch
        {
            return new Dictionary<string, string>();
        }
    }
} 