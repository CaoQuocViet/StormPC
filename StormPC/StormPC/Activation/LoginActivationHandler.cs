 using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using StormPC.Contracts.Services;

namespace StormPC.Activation;

public class LoginActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly ILoginService _loginService;
    private readonly INavigationService _navigationService;

    public LoginActivationHandler(
        ILoginService loginService,
        INavigationService navigationService)
    {
        _loginService = loginService;
        _navigationService = navigationService;
    }

    protected override async Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        // Kiểm tra xem đã có tài khoản admin chưa
        var isFirstTime = await _loginService.IsFirstTimeSetupAsync();
        if (isFirstTime)
        {
            _navigationService.NavigateTo("FirstTimeSetup");
            return;
        }

        // Kiểm tra xem có phiên đăng nhập hợp lệ không
        var isLoggedIn = await _loginService.IsLoggedInAsync();
        if (isLoggedIn)
        {
            _navigationService.NavigateTo("Main");
            return;
        }

        // Nếu chưa đăng nhập, chuyển đến trang đăng nhập
        _navigationService.NavigateTo("Login");
    }

    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
    {
        return true;
    }
}