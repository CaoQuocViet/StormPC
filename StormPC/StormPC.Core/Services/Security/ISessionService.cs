using System.Threading.Tasks;
using StormPC.Core.Models.Login;

namespace StormPC.Core.Services.Security;

public interface ISessionService
{
    Task<SessionToken> CreateSessionAsync();
    Task<bool> ValidateSessionAsync(string token);
    Task InvalidateSessionAsync();
    Task<bool> HasValidSessionAsync();
} 