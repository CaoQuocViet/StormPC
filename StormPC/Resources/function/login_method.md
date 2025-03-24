# Luồng hoạt động sẽ như sau:
- Khi app khởi động, nó sẽ kiểm tra xem đã setup lần đầu chưa
- Nếu chưa setup, mở FirstTimeWindow để tạo tài khoản admin
- Sau khi setup xong, FirstTimeWindow sẽ đóng và mở LoginWindow
- Nếu đã setup rồi, mở thẳng LoginWindow
- Khi đăng nhập thành công, LoginWindow sẽ đóng và mở MainWindow

# Hệ Thống Xác Thực & Đăng Nhập

## 🔐 Mã Hóa Mật Khẩu
- Sử dụng **Argon2id** thông qua package `Konscious.Security.Cryptography.Argon2`.
- Tham số mã hóa:
  ```csharp
  using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
  {
      DegreeOfParallelism = 8,
      MemorySize = 65536,
      Iterations = 4
  };
  ```

## 🔏 Lưu Trữ Thông Tin Đăng Nhập
- **Sử dụng `SecureStorageService`** để bảo vệ dữ liệu nhạy cảm.
- **Dữ liệu được mã hóa trước khi lưu** bằng `ProtectedData.Protect` (Windows).
- Lưu trong file **`secure_storage.dat`** tại **LocalApplicationData**.
- **Mã hóa theo user Windows** (`DataProtectionScope.CurrentUser`).

## 🛡️ Tính Năng Bảo Mật
- **Giới hạn đăng nhập sai:** `MAX_LOGIN_ATTEMPTS = 5`.
- **Khóa tài khoản sau nhiều lần đăng nhập sai:** `LOCKOUT_MINUTES = 15`.
- **Thời hạn session:** `SESSION_EXPIRY_HOURS = 12`.

## 🔑 Backup Key
- **Trường `BackupKeyHash`** có trong model `UserAccount` nhưng chưa triển khai chức năng khôi phục tài khoản.

## 💾 Ghi Nhớ Đăng Nhập
- Thông tin đăng nhập sẽ được ghi nhớ trong 1 tiếng
- Sau 1 tiếng, người dùng sẽ phải đăng nhập lại
- Thông tin ghi nhớ sẽ tự động bị xóa khi hết hạn
- Password vẫn được mã hóa khi lưu trữ trong SecureStorageService

