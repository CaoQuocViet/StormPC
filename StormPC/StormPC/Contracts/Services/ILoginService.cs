using System.Threading.Tasks;
using StormPC.Core.Models.Login;

namespace StormPC.Contracts.Services;

public interface ILoginService
{
    Task<bool> IsFirstTimeSetupAsync();
    Task<bool> SetupAdminAccountAsync(string username, string password, bool useWindowsHello);
    Task<bool> LoginAsync(string username, string password);
    Task<bool> LoginWithWindowsHelloAsync();
    Task<bool> LoginWithBackupKeyAsync(string backupKey);
    Task<bool> IsLoggedInAsync();
    Task LogoutAsync();
    Task<bool> ChangePasswordAsync(string currentPassword, string newPassword);
    Task<string> GenerateBackupKeyAsync();
    Task<bool> IsWindowsHelloAvailableAsync();
    Task<bool> EnableWindowsHelloAsync();
    Task<bool> DisableWindowsHelloAsync();
} 