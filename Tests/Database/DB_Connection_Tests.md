# Kiểm tra kết nối Database

## Test Case DB-001: Kiểm tra kết nối cơ sở dữ liệu PostgreSQL
- **Mô tả**: Xác minh ứng dụng có thể kết nối với cơ sở dữ liệu PostgreSQL
- **Tiền điều kiện**: 
  - Docker PostgreSQL container đang chạy
  - File .env được cấu hình đúng
- **Các bước kiểm tra**:
  1. Khởi động ứng dụng
  2. Theo dõi output log kết nối
- **Kết quả mong đợi**: Ứng dụng hiển thị thông báo "Kết nối cơ sở dữ liệu thành công"

## Test Case DB-002: Kiểm tra cấu hình môi trường từ file .env
- **Mô tả**: Xác minh ứng dụng đọc đúng thông tin kết nối từ file .env
- **Các bước kiểm tra**:
  1. Sửa đổi thông tin trong file .env (host, user, password)
  2. Khởi động ứng dụng
- **Kết quả mong đợi**: 
  - Nếu thông tin sai: Hiển thị lỗi kết nối
  - Nếu thông tin đúng: Kết nối thành công

## Test Case DB-003: Kiểm tra tìm file .env tự động
- **Mô tả**: Đảm bảo service có thể tự động tìm file .env trong thư mục dự án
- **Các bước kiểm tra**:
  1. Di chuyển file .env đến thư mục gốc của dự án
  2. Khởi động ứng dụng
- **Kết quả mong đợi**: Ứng dụng vẫn tìm thấy và đọc được file .env

## Test Case DB-004: Kiểm tra xử lý lỗi khi không tìm thấy file .env
- **Mô tả**: Kiểm tra cách hệ thống phản ứng khi không tìm thấy file .env
- **Các bước kiểm tra**:
  1. Đổi tên file .env tạm thời
  2. Khởi động ứng dụng
- **Kết quả mong đợi**: Hiển thị lỗi "Không tìm thấy file .env trong project"

## Test Case DB-005: Kiểm tra kết nối với port không chuẩn
- **Mô tả**: Xác minh khả năng kết nối cơ sở dữ liệu trên cổng tùy chọn
- **Tiền điều kiện**: Docker PostgreSQL container chạy trên port tùy chỉnh
- **Các bước kiểm tra**:
  1. Cập nhật file .env với port khác (ví dụ: 5444 thay vì 5432)
  2. Khởi động ứng dụng
- **Kết quả mong đợi**: Kết nối thành công đến database trên port tùy chỉnh

## Test Case DB-006: Kiểm tra retry connection khi mất kết nối
- **Mô tả**: Kiểm tra khả năng tự động kết nối lại khi mất kết nối database
- **Các bước kiểm tra**:
  1. Khởi động ứng dụng với kết nối database thành công
  2. Tạm dừng Docker container database
  3. Thực hiện các thao tác truy vấn dữ liệu
  4. Khởi động lại Docker container
- **Kết quả mong đợi**: Ứng dụng hiển thị thông báo lỗi khi mất kết nối và tự động kết nối lại khi database online

## Test Case DB-007: Kiểm tra chế độ read-only khi không có quyền ghi
- **Mô tả**: Xác minh ứng dụng hoạt động ở chế độ chỉ đọc khi user database không có quyền ghi
- **Tiền điều kiện**: User database được cấu hình với quyền chỉ đọc
- **Các bước kiểm tra**:
  1. Cấu hình file .env với user chỉ đọc
  2. Khởi động ứng dụng
  3. Thử thực hiện các thao tác thêm/sửa/xóa dữ liệu
- **Kết quả mong đợi**: Ứng dụng chỉ hiển thị dữ liệu, các thao tác ghi báo lỗi quyền truy cập

## Test Case DB-008: Kiểm tra mã hóa thông tin kết nối database
- **Mô tả**: Đảm bảo thông tin kết nối (đặc biệt là mật khẩu) không xuất hiện trong log
- **Các bước kiểm tra**:
  1. Bật chế độ debug logging
  2. Khởi động ứng dụng 
  3. Kiểm tra log ứng dụng
- **Kết quả mong đợi**: Thông tin nhạy cảm (mật khẩu) không xuất hiện dưới dạng plain text trong log