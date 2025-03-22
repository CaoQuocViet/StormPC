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