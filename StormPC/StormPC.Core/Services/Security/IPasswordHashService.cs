using System.Threading.Tasks;

namespace StormPC.Core.Services.Security;

public interface IPasswordHashService
{
    Task<string> HashPasswordAsync(string password);
    Task<bool> VerifyPasswordAsync(string password, string hash);
    bool IsPasswordStrong(string password);
} 