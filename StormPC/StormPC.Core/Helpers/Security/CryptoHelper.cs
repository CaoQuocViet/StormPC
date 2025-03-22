using System;
using System.Security.Cryptography;
using System.Text;

namespace StormPC.Core.Helpers.Security;

public static class CryptoHelper
{
    private const int KEY_SIZE = 32;
    private const int IV_SIZE = 16;

    public static string GenerateRandomKey()
    {
        var bytes = new byte[KEY_SIZE];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }
        return Convert.ToBase64String(bytes);
    }

    public static string EncryptString(string plainText, string key)
    {
        using var aes = Aes.Create();
        aes.Key = Convert.FromBase64String(key);
        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor();
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        var result = new byte[IV_SIZE + cipherBytes.Length];
        Buffer.BlockCopy(aes.IV, 0, result, 0, IV_SIZE);
        Buffer.BlockCopy(cipherBytes, 0, result, IV_SIZE, cipherBytes.Length);

        return Convert.ToBase64String(result);
    }

    public static string DecryptString(string cipherText, string key)
    {
        var fullBytes = Convert.FromBase64String(cipherText);
        if (fullBytes.Length < IV_SIZE) throw new ArgumentException("Invalid cipher text");

        var iv = new byte[IV_SIZE];
        var cipherBytes = new byte[fullBytes.Length - IV_SIZE];
        Buffer.BlockCopy(fullBytes, 0, iv, 0, IV_SIZE);
        Buffer.BlockCopy(fullBytes, IV_SIZE, cipherBytes, 0, cipherBytes.Length);

        using var aes = Aes.Create();
        aes.Key = Convert.FromBase64String(key);
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();
        var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
        return Encoding.UTF8.GetString(plainBytes);
    }
} 