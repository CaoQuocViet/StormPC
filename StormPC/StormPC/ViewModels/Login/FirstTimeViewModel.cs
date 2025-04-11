using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StormPC.Core.Services.Login;
using StormPC.Core.Contracts.Services;
using System.Threading.Tasks;
using System.Reflection;
using Windows.ApplicationModel;
using StormPC.Helpers;

namespace StormPC.ViewModels.Login;

public partial class FirstTimeViewModel : ObservableObject
{
    private readonly AuthenticationService _authService;
    private readonly IActivityLogService _activityLogService;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private string _backupKey = string.Empty;

    [ObservableProperty]
    private string _versionDescription;

    public event EventHandler? AccountCreated;

    public FirstTimeViewModel(AuthenticationService authService, IActivityLogService activityLogService)
    {
        _authService = authService;
        _activityLogService = activityLogService;
        _versionDescription = GetVersionDescription();
    }

    /// <summary>
    /// Lấy mô tả phiên bản ứng dụng
    /// </summary>
    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;
            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }

    /// <summary>
    /// Tạo tài khoản quản trị viên
    /// </summary>
    [RelayCommand]
    private async Task CreateAdminAccountAsync((string password, string confirmPassword) passwords)
    {
        ErrorMessage = string.Empty;

        // Kiểm tra dữ liệu
        if (string.IsNullOrEmpty(Username))
        {
            ErrorMessage = "Tên đăng nhập không được để trống";
            await _activityLogService.LogActivityAsync("Đăng ký", "Lỗi xác thực", 
                "Tạo tài khoản quản trị thất bại - Tên đăng nhập trống", "Error", Username);
            return;
        }

        if (string.IsNullOrEmpty(passwords.password))
        {
            ErrorMessage = "Mật khẩu không được để trống";
            await _activityLogService.LogActivityAsync("Đăng ký", "Lỗi xác thực", 
                "Tạo tài khoản quản trị thất bại - Mật khẩu trống", "Error", Username);
            return;
        }

        if (passwords.password.Length < 8)
        {
            ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự";
            await _activityLogService.LogActivityAsync("Đăng ký", "Lỗi xác thực", 
                "Tạo tài khoản quản trị thất bại - Mật khẩu quá ngắn", "Error", Username);
            return;
        }

        if (passwords.password != passwords.confirmPassword)
        {
            ErrorMessage = "Mật khẩu không khớp";
            await _activityLogService.LogActivityAsync("Đăng ký", "Lỗi xác thực", 
                "Tạo tài khoản quản trị thất bại - Mật khẩu không khớp", "Error", Username);
            return;
        }

        try
        {
            var success = await _authService.CreateAdminAccount(Username, passwords.password);
            if (success)
            {
                // Tạo khóa dự phòng
                BackupKey = await _authService.GenerateBackupKeyAsync();
                await _activityLogService.LogActivityAsync("Đăng ký", "Tạo tài khoản quản trị", 
                    $"Tạo tài khoản quản trị thành công cho người dùng: {Username}", "Success", Username);
                AccountCreated?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                ErrorMessage = "Không thể tạo tài khoản quản trị. Vui lòng thử lại.";
                await _activityLogService.LogActivityAsync("Đăng ký", "Lỗi tạo tài khoản", 
                    "Tạo tài khoản quản trị thất bại - Lỗi không xác định", "Error", Username);
            }
        }
        catch (System.Exception ex)
        {
            ErrorMessage = $"Đã xảy ra lỗi: {ex.Message}";
            await _activityLogService.LogActivityAsync("Đăng ký", "Lỗi hệ thống", 
                $"Tạo tài khoản quản trị thất bại - {ex.Message}", "Error", Username);
        }
    }
}