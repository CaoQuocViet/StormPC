using System.Threading.Tasks;
using StormPC.Core.Helpers.Security;

namespace StormPC.Services.Login;

public class WindowsHelloService : IWindowsHelloService
{
    public async Task<bool> IsAvailableAsync()
    {
        return await WindowsHelloHelper.IsWindowsHelloAvailableAsync();
    }

    public async Task<bool> RegisterAsync()
    {
        return await WindowsHelloHelper.RegisterWindowsHelloAsync();
    }

    public async Task<bool> VerifyAsync(string message = "")
    {
        return await WindowsHelloHelper.VerifyWindowsHelloAsync();
    }

    public async Task<bool> RemoveAsync()
    {
        return await WindowsHelloHelper.RemoveWindowsHelloAsync();
    }
} 