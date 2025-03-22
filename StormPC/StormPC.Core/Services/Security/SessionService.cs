using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using StormPC.Core.Models.Login;

namespace StormPC.Core.Services.Security;

public class SessionService : ISessionService
{
    private readonly ISecureStorageService _secureStorage;
    private const int TOKEN_SIZE = 32;
    private const int SESSION_HOURS = 8;

    public SessionService(ISecureStorageService secureStorage)
    {
        _secureStorage = secureStorage;
    }

    public async Task<SessionToken> CreateSessionAsync()
    {
        var token = GenerateToken();
        var sessionToken = new SessionToken
        {
            Token = token,
            CreatedAt = DateTime.Now,
            ExpiresAt = DateTime.Now.AddHours(SESSION_HOURS)
        };

        await _secureStorage.SaveSessionTokenAsync(sessionToken);
        return sessionToken;
    }

    public async Task<bool> ValidateSessionAsync(string token)
    {
        var storedToken = await _secureStorage.LoadSessionTokenAsync();
        if (storedToken == null) return false;
        if (!storedToken.IsValid) return false;
        return storedToken.Token == token;
    }

    public async Task InvalidateSessionAsync()
    {
        await _secureStorage.SaveSessionTokenAsync(new SessionToken
        {
            ExpiresAt = DateTime.Now.AddMinutes(-1)
        });
    }

    public async Task<bool> HasValidSessionAsync()
    {
        var token = await _secureStorage.LoadSessionTokenAsync();
        return token?.IsValid ?? false;
    }

    private string GenerateToken()
    {
        var bytes = new byte[TOKEN_SIZE];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }
        return Convert.ToBase64String(bytes);
    }
} 