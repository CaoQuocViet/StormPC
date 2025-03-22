using System.Threading.Tasks;
using StormPC.Core.Models.Login;

namespace StormPC.Core.Services.Security;

public interface ISecureStorageService
{
    Task SaveLoginInfoAsync(LoginInfo loginInfo);
    Task<LoginInfo?> LoadLoginInfoAsync();
    Task SaveSessionTokenAsync(SessionToken token);
    Task<SessionToken?> LoadSessionTokenAsync();
    Task SaveBackupKeyAsync(BackupKey backupKey);
    Task<BackupKey?> LoadBackupKeyAsync();
    Task ClearAllAsync();
} 