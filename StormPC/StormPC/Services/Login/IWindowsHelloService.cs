using System.Threading.Tasks;

namespace StormPC.Services.Login;

public interface IWindowsHelloService
{
    Task<bool> IsAvailableAsync();
    Task<bool> RegisterAsync();
    Task<bool> VerifyAsync(string message = "");
    Task<bool> RemoveAsync();
} 