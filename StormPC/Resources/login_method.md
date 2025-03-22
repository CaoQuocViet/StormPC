# Lưu tài khoản Admin vào config file (không cần bảng DB riêng)

## 💡 Cách thực hiện

- Lưu thông tin đăng nhập admin (**username/password đã hash**) vào file config (`appsettings.json`, registry, hoặc encrypted file).
- Khi đăng nhập, ứng dụng kiểm tra thông tin từ file này.

## 📊 Ưu điểm

✅ Đơn giản, không cần tạo bảng trong database.
✅ Dễ triển khai, không ảnh hưởi đến DB.
✅ Bảo mật tốt nếu mã hóa password đúng cách.

## ⛔️ Nhược điểm

❌ Không linh hoạt nếu muốn thay đổi thông tin đăng nhập (phải chỉnh sửa file config).
❌ Cần mã hóa mạnh để tránh lộ thông tin (dùng **PBKDF2, Argon2, hoặc BCrypt**).

## 🔍 Dùng khi

- Ứng dụng nhỏ, không có nhiều người truy cập.
- Không cần thay đổi admin thường xuyên.

## NẾU:

### Lần đầu mở phần mềm
- Nhập thôn tin đăng nhập
- Bấm đăng nhập thì thông tin đăng nhập đó sẽ được lưu thành thông tin đăng nhập cho lần sau
- Có tùy chọn đăng nhập bằng Windows Hello

### Quên mật khẩu
- Chọn nút "Quên mật khẩu"
- Đăng nhập Windows Hello
- Nhập lại thông tin đăng nhập như lần đầu tiên

// Lưu ý thông tin đăng nhập phải được tuyệt đối mã hóa và không nhúng trong mã nguồn
======================================================================

# Hướng dẫn triển khai đăng nhập bằng Windows Hello

HƯỚNG DẪN TRIỂN KHAI ĐĂNG NHẬP BẰNG WINDOWS HELLO CHO ỨNG DỤNG WINUI 3

## 1. GIỚI THIỆU
Windows Hello là hệ thống xác thực sinh trắc học của Microsoft tích hợp vào Windows 10 và 11.
Windows Hello cho phép xác thực bằng khuôn mặt, vân tay hoặc mã PIN.
Windows Hello hoạt động dựa trên cặp khóa công khai và riêng tư, với khóa riêng tư được lưu trữ an toàn trong TPM.

## 2. YÊU CẦU HỆ THỐNG
- Windows 10 hoặc 11
- Thiết bị hỗ trợ Windows Hello (webcam hồng ngoại, đầu đọc vân tay, hoặc PIN)
- SDK Windows 10 phiên bản 10.0.10586.0 trở lên
- Visual Studio 2019 hoặc mới hơn với workload phát triển Windows

## 3. THIẾT LẬP DỰ ÁN
- Tạo dự án WinUI 3 mới trong Visual Studio
- Thêm tham chiếu namespace:
  using Windows.Security.Credentials;
  using Windows.Storage.Streams;
  using Windows.Security.Cryptography;
  using Windows.Security.Cryptography.Core;

## 4. KIỂM TRA TÍNH KHẢ DỤNG
```C#
public async Task<bool> IsWindowsHelloAvailableAsync()
{
    return await KeyCredentialManager.IsSupportedAsync();
}
```

## 5. THIẾT LẬP WINDOWS HELLO CHO NGƯỜI DÙNG
```C#
public async Task<bool> SetupWindowsHelloAsync(string userName)
{
    if (!await KeyCredentialManager.IsSupportedAsync())
    {
        return false;
    }

    var keyCreationResult = await KeyCredentialManager.RequestCreateAsync(
        userName, 
        KeyCredentialCreationOption.ReplaceExisting);

    if (keyCreationResult.Status != KeyCredentialStatus.Success)
    {
        return false;
    }

    var publicKey = keyCreationResult.Credential.RetrievePublicKey();
    var hashProvider = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
    var publicKeyHash = hashProvider.HashData(publicKey);
    string base64PublicKeyHash = CryptographicBuffer.EncodeToBase64String(publicKeyHash);
    
    Windows.Storage.ApplicationData.Current.LocalSettings.Values[$"WindowsHello_{userName}_PublicKeyHint"] = base64PublicKeyHash;
    
    return true;
}
```

## 6. ĐĂNG NHẬP BẰNG WINDOWS HELLO
```C#
public async Task<bool> SignInWithWindowsHelloAsync(string userName)
{
    if (!await KeyCredentialManager.IsSupportedAsync())
    {
        return false;
    }

    // Kiểm tra xem người dùng đã thiết lập Windows Hello chưa
    if (!Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey($"WindowsHello_{userName}_PublicKeyHint"))
    {
        return false;
    }

    var retrieveResult = await KeyCredentialManager.OpenAsync(userName);
    
    if (retrieveResult.Status != KeyCredentialStatus.Success)
    {
        return false;
    }

    // Tạo challenge ngẫu nhiên
    var challengeBuffer = CryptographicBuffer.GenerateRandom(32);

    // Yêu cầu người dùng xác thực
    var credential = retrieveResult.Credential;
    var signResult = await credential.RequestSignAsync(challengeBuffer);

    if (signResult.Status != KeyCredentialStatus.Success)
    {
        return false;
    }

    return true;
}
```

## 7. XÓA THÔNG TIN XÁC THỰC
```C#
public async Task<bool> RemoveWindowsHelloCredentialAsync(string userName)
{
    try
    {
        await KeyCredentialManager.DeleteAsync(userName);
        Windows.Storage.ApplicationData.Current.LocalSettings.Values.Remove($"WindowsHello_{userName}_PublicKeyHint");
        return true;
    }
    catch
    {
        return false;
    }
}
```

## 8. TÍCH HỢP VÀO LUỒNG ĐĂNG NHẬP

```C#
public async Task<bool> LoginAsync(string userName, string password = null)
{
    // Kiểm tra Windows Hello đã được thiết lập cho người dùng
    bool windowsHelloEnabled = Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey($"WindowsHello_{userName}_PublicKeyHint");
    
    if (windowsHelloEnabled && await KeyCredentialManager.IsSupportedAsync())
    {
        // Thử đăng nhập bằng Windows Hello
        bool success = await SignInWithWindowsHelloAsync(userName);
        if (success)
        {
            // Đăng nhập thành công
            return true;
        }
    }
    
    // Đăng nhập bằng mật khẩu
    bool loginSuccess = VerifyPassword(userName, password);
    
    // Nếu đăng nhập thành công và Windows Hello khả dụng, hỏi người dùng có muốn thiết lập không
    if (loginSuccess && !windowsHelloEnabled && await KeyCredentialManager.IsSupportedAsync())
    {
        // Trong ứng dụng thực tế, hiển thị hộp thoại hỏi người dùng
        await SetupWindowsHelloAsync(userName);
    }
    
    return loginSuccess;
}
```

## 9. HIỂN THỊ HỘP THOẠI THIẾT LẬP WINDOWS HELLO
```C#
private async Task<bool> ShowWindowsHelloSetupDialog()
{
    ContentDialog dialog = new ContentDialog
    {
        Title = "Thiết lập Windows Hello",
        Content = "Bạn có muốn sử dụng Windows Hello để đăng nhập dễ dàng hơn trong tương lai không?",
        PrimaryButtonText = "Có",
        SecondaryButtonText = "Không",
        DefaultButton = ContentDialogButton.Primary
    };

    var result = await dialog.ShowAsync();
    return result == ContentDialogResult.Primary;
}
```

## 10. LƯU Ý BẢO MẬT
- Luôn xác thực ở phía server, không chỉ dựa vào xác thực phía client
- Sử dụng HTTPS để bảo vệ quá trình truyền tải dữ liệu
- Không lưu trữ thông tin nhạy cảm khác trong ứng dụng client
- Trong ứng dụng thực tế, sử dụng challenge từ server để tránh tấn công replay
- Kết hợp với các phương thức xác thực khác để đảm bảo tính khả dụng

# Phương pháp xác thực cho StormPC

## 💡 Cách thực hiện

### 1. Lưu trữ thông tin đăng nhập
- Sử dụng Windows Data Protection API (DPAPI) để lưu trữ thông tin đăng nhập
- Mã hóa mật khẩu bằng Argon2id trước khi lưu trữ
- Lưu token phiên làm việc cho đăng nhập tự động

### 2. Quy trình đăng nhập lần đầu
1. Hiển thị form thiết lập thông tin admin:
   - Username
   - Password (có kiểm tra độ mạnh)
   - Tùy chọn bật Windows Hello
2. Tạo và lưu trữ:
   - Hash mật khẩu với Argon2id
   - Mã hóa thông tin với DPAPI
   - Thiết lập Windows Hello (nếu được chọn)
3. Tạo file backup key được mã hóa để khôi phục

### 3. Quy trình đăng nhập thông thường
1. Kiểm tra Windows Hello đã được thiết lập:
   - Nếu có, hiển thị tùy chọn đăng nhập Windows Hello
   - Nếu không, hiển thị form đăng nhập thông thường
2. Sau khi đăng nhập thành công:
   - Tạo và lưu token phiên làm việc
   - Hỏi thiết lập Windows Hello (nếu chưa có)
3. Rate limiting cho đăng nhập thất bại:
   - Giới hạn 5 lần thử/phút
   - Khóa tạm thời 5 phút sau 5 lần thất bại

### 4. Quy trình khôi phục mật khẩu
1. Yêu cầu xác thực Windows Hello (nếu đã thiết lập)
2. Hoặc sử dụng backup key để xác thực
3. Cho phép thiết lập lại thông tin đăng nhập mới

## 📊 Code triển khai chính

```csharp
public class SecureStorage
{
    private static readonly string CREDENTIAL_PATH = "StormPC_Admin";
    private static readonly string BACKUP_KEY_PATH = "StormPC_Backup";

    // Lưu thông tin đăng nhập sử dụng DPAPI
    public static void SaveCredentials(string username, string password)
    {
        // Hash password với Argon2id
        string passwordHash = HashPassword(password);
        
        // Tạo và mã hóa dữ liệu
        var data = $"{username}:{passwordHash}";
        byte[] encrypted = ProtectedData.Protect(
            Encoding.UTF8.GetBytes(data),
            null,
            DataProtectionScope.CurrentUser
        );
        
        // Lưu vào file
        File.WriteAllBytes(CREDENTIAL_PATH, encrypted);
        
        // Tạo backup key
        CreateBackupKey(username, passwordHash);
    }

    // Kiểm tra thông tin đăng nhập
    public static bool VerifyCredentials(string username, string password)
    {
        var (storedUsername, storedHash) = LoadCredentials();
        if (storedUsername != username) return false;
        
        return VerifyPassword(password, storedHash);
    }

    // Tạo token phiên làm việc
    public static string CreateSessionToken()
    {
        var token = GenerateSecureToken();
        // Lưu token với thời hạn
        SaveSessionToken(token, DateTime.Now.AddHours(8));
        return token;
    }

    // Kiểm tra token hợp lệ
    public static bool ValidateSessionToken(string token)
    {
        return LoadAndVerifySessionToken(token);
    }
}

public class LoginManager
{
    private readonly SecureStorage _storage;
    private readonly WindowsHelloAuth _windowsHello;
    private int _failedAttempts = 0;
    private DateTime _lastFailedAttempt;

    public async Task<bool> Login(string username, string password)
    {
        // Kiểm tra rate limiting
        if (IsRateLimited()) return false;

        // Thử đăng nhập Windows Hello
        if (_windowsHello.IsEnabled && await _windowsHello.Authenticate())
        {
            return await CompleteLogin();
        }

        // Đăng nhập thông thường
        if (_storage.VerifyCredentials(username, password))
        {
            ResetFailedAttempts();
            return await CompleteLogin();
        }

        // Xử lý đăng nhập thất bại
        HandleFailedLogin();
        return false;
    }

    private async Task<bool> CompleteLogin()
    {
        var token = _storage.CreateSessionToken();
        // Lưu thông tin phiên làm việc
        return true;
    }

    private bool IsRateLimited()
    {
        if (_failedAttempts >= 5 && 
            DateTime.Now - _lastFailedAttempt < TimeSpan.FromMinutes(5))
        {
            return true;
        }
        return false;
    }

    private void HandleFailedLogin()
    {
        _failedAttempts++;
        _lastFailedAttempt = DateTime.Now;
    }
}
```

## ⚡ Ưu điểm của giải pháp

✅ Bảo mật cao với DPAPI và Argon2id
✅ Tích hợp tốt với Windows Hello
✅ Có cơ chế khôi phục đáng tin cậy
✅ UX thân thiện với người dùng
✅ Dễ dàng bảo trì và nâng cấp

## 🔒 Các biện pháp bảo mật

1. Sử dụng DPAPI cho việc lưu trữ an toàn
2. Argon2id cho việc hash mật khẩu
3. Rate limiting chống brute force
4. Token phiên làm việc có thời hạn
5. Backup key được mã hóa
6. Log các hoạt động đăng nhập bất thường