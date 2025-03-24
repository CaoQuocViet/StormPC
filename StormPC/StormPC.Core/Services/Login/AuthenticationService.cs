using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;
using StormPC.Core.Models.Login;
using System.Text;
using System.IO;

namespace StormPC.Core.Services.Login;

public class AuthenticationService
{
    private const string USERS_STORAGE_KEY = "users";
    private const string SESSIONS_STORAGE_KEY = "sessions";
    private const int MAX_LOGIN_ATTEMPTS = 5;
    private const int LOCKOUT_MINUTES = 15;
    private const int SESSION_EXPIRY_HOURS = 12;

    private readonly SecureStorageService _secureStorage;
    private UserSession? _currentSession;

    public AuthenticationService(SecureStorageService secureStorage)
    {
        _secureStorage = secureStorage;
    }

    public bool IsFirstTimeSetup()
    {
        var users = _secureStorage.LoadSecureData<UserAccount[]>(USERS_STORAGE_KEY);
        return users == null || users.Length == 0;
    }

    public async Task<bool> CreateAdminAccount(string username, string password)
    {
        if (!IsFirstTimeSetup())
        {
            return false;
        }

        var hashedPassword = await HashPasswordAsync(password);
        var adminAccount = new UserAccount
        {
            Username = username,
            HashedPassword = hashedPassword,
            IsAdmin = true,
            CreatedAt = DateTime.UtcNow,
            FailedLoginAttempts = 0
        };

        _secureStorage.SaveSecureData(USERS_STORAGE_KEY, new[] { adminAccount });
        return true;
    }

    public async Task<(bool success, string? error)> LoginAsync(string username, string password)
    {
        var users = _secureStorage.LoadSecureData<UserAccount[]>(USERS_STORAGE_KEY);
        var user = users?.FirstOrDefault(u => u.Username == username);

        if (user == null)
        {
            return (false, "Invalid username or password");
        }

        // Check rate limiting
        if (user.FailedLoginAttempts >= MAX_LOGIN_ATTEMPTS && 
            user.LastFailedLoginAttempt?.AddMinutes(LOCKOUT_MINUTES) > DateTime.UtcNow)
        {
            return (false, $"Account is locked. Try again in {LOCKOUT_MINUTES} minutes");
        }

        var hashedPassword = await HashPasswordAsync(password);
        if (user.HashedPassword != hashedPassword)
        {
            user.FailedLoginAttempts++;
            user.LastFailedLoginAttempt = DateTime.UtcNow;
            _secureStorage.SaveSecureData(USERS_STORAGE_KEY, users);
            return (false, "Invalid username or password");
        }

        // Reset failed attempts on successful login
        user.FailedLoginAttempts = 0;
        user.LastLoginAt = DateTime.UtcNow;
        _secureStorage.SaveSecureData(USERS_STORAGE_KEY, users);

        // Create new session
        var session = new UserSession
        {
            Username = username,
            SessionToken = GenerateSessionToken(),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddHours(SESSION_EXPIRY_HOURS),
            IsAdmin = user.IsAdmin
        };

        var sessions = _secureStorage.LoadSecureData<UserSession[]>(SESSIONS_STORAGE_KEY) ?? Array.Empty<UserSession>();
        var validSessions = sessions.Where(s => s.ExpiresAt > DateTime.UtcNow).ToList();
        validSessions.Add(session);
        _secureStorage.SaveSecureData(SESSIONS_STORAGE_KEY, validSessions.ToArray());

        _currentSession = session;
        return (true, null);
    }

    public void Logout()
    {
        if (_currentSession == null) return;

        var sessions = _secureStorage.LoadSecureData<UserSession[]>(SESSIONS_STORAGE_KEY);
        if (sessions == null) return;

        var validSessions = sessions
            .Where(s => s.SessionToken != _currentSession.SessionToken && s.ExpiresAt > DateTime.UtcNow)
            .ToArray();

        _secureStorage.SaveSecureData(SESSIONS_STORAGE_KEY, validSessions);
        _currentSession = null;
    }

    private async Task<string> HashPasswordAsync(string password)
    {
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            DegreeOfParallelism = 8,
            MemorySize = 65536,
            Iterations = 4
        };

        var hash = await argon2.GetBytesAsync(32);
        return Convert.ToBase64String(hash);
    }

    private string GenerateSessionToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public async Task<string> GenerateBackupKeyAsync()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        var backupKey = Convert.ToBase64String(randomBytes);
        
        // Hash the backup key
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(backupKey))
        {
            DegreeOfParallelism = 8,
            MemorySize = 65536,
            Iterations = 4
        };
        var hash = await argon2.GetBytesAsync(32);
        var backupKeyHash = Convert.ToBase64String(hash);

        // Save the hash to the admin account
        var users = _secureStorage.LoadSecureData<UserAccount[]>(USERS_STORAGE_KEY);
        if (users != null)
        {
            var adminAccount = users.FirstOrDefault(u => u.IsAdmin);
            if (adminAccount != null)
            {
                adminAccount.BackupKeyHash = backupKeyHash;
                _secureStorage.SaveSecureData(USERS_STORAGE_KEY, users);
            }
        }

        return backupKey;
    }

    public async Task<bool> VerifyBackupKeyAsync(string backupKey)
    {
        var users = _secureStorage.LoadSecureData<UserAccount[]>(USERS_STORAGE_KEY);
        if (users == null) return false;

        var adminAccount = users.FirstOrDefault(u => u.IsAdmin);
        if (adminAccount == null || string.IsNullOrEmpty(adminAccount.BackupKeyHash)) return false;

        // Hash the provided backup key
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(backupKey))
        {
            DegreeOfParallelism = 8,
            MemorySize = 65536,
            Iterations = 4
        };
        var hash = await argon2.GetBytesAsync(32);
        var providedHash = Convert.ToBase64String(hash);

        return adminAccount.BackupKeyHash == providedHash;
    }

    public void ResetAdminAccount()
    {
        // Delete the secure storage file
        var appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "StormPC");
        var storageFilePath = Path.Combine(appDataPath, "secure_storage.dat");
        if (File.Exists(storageFilePath))
        {
            File.Delete(storageFilePath);
        }
    }

    public UserSession? GetCurrentSession() => _currentSession;

    public bool IsSessionValid()
    {
        if (_currentSession == null) return false;
        return _currentSession.ExpiresAt > DateTime.UtcNow;
    }
} 