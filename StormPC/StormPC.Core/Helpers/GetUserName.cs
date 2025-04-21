using StormPC.Core.Services.Login;

namespace StormPC.Core.Helpers;

public static class GetUserName
{
    private static AuthenticationService? _authService;

    public static void Initialize(AuthenticationService authService)
    {
        _authService = authService;
    }

    public static string GetCurrentUsername()
    {
        return _authService?.GetCurrentUsername() ?? "System";
    }
} 