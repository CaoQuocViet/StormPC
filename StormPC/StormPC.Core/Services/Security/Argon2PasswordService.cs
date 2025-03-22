using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;

namespace StormPC.Core.Services.Security;

public class Argon2PasswordService : IPasswordHashService
{
    private const int SALT_SIZE = 16;
    private const int HASH_SIZE = 32;
    private const int ITERATIONS = 3;
    private const int MEMORY_SIZE = 65536;
    private const int DEGREE_OF_PARALLELISM = 4;

    public async Task<string> HashPasswordAsync(string password)
    {
        var salt = GenerateSalt();
        var hash = await GenerateHashAsync(password, salt);
        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }

    public async Task<bool> VerifyPasswordAsync(string password, string hashString)
    {
        var parts = hashString.Split(':');
        if (parts.Length != 2) return false;

        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);
        var newHash = await GenerateHashAsync(password, salt);

        return hash.SequenceEqual(newHash);
    }

    public bool IsPasswordStrong(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;
        if (password.Length < 8) return false;

        bool hasUpperCase = password.Any(char.IsUpper);
        bool hasLowerCase = password.Any(char.IsLower);
        bool hasDigit = password.Any(char.IsDigit);
        bool hasSpecialChar = password.Any(c => !char.IsLetterOrDigit(c));

        return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
    }

    private byte[] GenerateSalt()
    {
        var salt = new byte[SALT_SIZE];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private async Task<byte[]> GenerateHashAsync(string password, byte[] salt)
    {
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = DEGREE_OF_PARALLELISM,
            Iterations = ITERATIONS,
            MemorySize = MEMORY_SIZE
        };

        return await Task.Run(() => argon2.GetBytes(HASH_SIZE));
    }
} 