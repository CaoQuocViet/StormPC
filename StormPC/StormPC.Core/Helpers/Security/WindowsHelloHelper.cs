using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.Security.Cryptography;
using System.Linq;

namespace StormPC.Core.Helpers.Security;

public static class WindowsHelloHelper
{
    private const string RESOURCE_NAME = "StormPC";
    private static readonly byte[] _salt = new byte[32];

    static WindowsHelloHelper()
    {
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(_salt);
    }

    public static async Task<bool> IsWindowsHelloAvailableAsync()
    {
        try
        {
            var publicClientApp = PublicClientApplicationBuilder
                .Create("StormPC")
                .WithAuthority(AadAuthorityAudience.AzureAdMyOrg)
                .WithDefaultRedirectUri()
                .Build();

            var accounts = await publicClientApp.GetAccountsAsync();
            return accounts.Any();
        }
        catch
        {
            return false;
        }
    }

    public static async Task<bool> RegisterWindowsHelloAsync()
    {
        try
        {
            var publicClientApp = PublicClientApplicationBuilder
                .Create("StormPC")
                .WithAuthority(AadAuthorityAudience.AzureAdMyOrg)
                .WithDefaultRedirectUri()
                .Build();

            var result = await publicClientApp.AcquireTokenInteractive(new[] { "openid", "profile" })
                .WithPrompt(Prompt.SelectAccount)
                .ExecuteAsync();

            return !string.IsNullOrEmpty(result.AccessToken);
        }
        catch
        {
            return false;
        }
    }

    public static async Task<bool> VerifyWindowsHelloAsync()
    {
        try
        {
            var publicClientApp = PublicClientApplicationBuilder
                .Create("StormPC")
                .WithAuthority(AadAuthorityAudience.AzureAdMyOrg)
                .WithDefaultRedirectUri()
                .Build();

            var accounts = await publicClientApp.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();
            if (firstAccount == null)
            {
                return false;
            }

            var result = await publicClientApp.AcquireTokenSilent(new[] { "openid", "profile" }, firstAccount)
                .ExecuteAsync();

            return !string.IsNullOrEmpty(result.AccessToken);
        }
        catch
        {
            return false;
        }
    }

    public static async Task<bool> RemoveWindowsHelloAsync()
    {
        try
        {
            var publicClientApp = PublicClientApplicationBuilder
                .Create("StormPC")
                .WithAuthority(AadAuthorityAudience.AzureAdMyOrg)
                .WithDefaultRedirectUri()
                .Build();

            var accounts = await publicClientApp.GetAccountsAsync();
            foreach (var account in accounts)
            {
                await publicClientApp.RemoveAsync(account);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
} 