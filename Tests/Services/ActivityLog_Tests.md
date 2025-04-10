# Kiểm tra Activity Log Service

## Test Case ALS-001: Ghi log hoạt động thành công
- **Mô tả**: Kiểm tra ghi log cho các hoạt động thành công
- **Các bước kiểm tra**:
  1. Thực hiện các hoạt động CRUD thành công
  2. Kiểm tra log được ghi
- **Kết quả mong đợi**: 
  - Log chứa đầy đủ thông tin: thời gian, người dùng, hành động, kết quả
  - Level log là "Success" hoặc "Info"

## Test Case ALS-002: Ghi log lỗi
- **Mô tả**: Kiểm tra ghi log cho các hoạt động thất bại
- **Các bước kiểm tra**:
  1. Thực hiện các hoạt động gây lỗi
  2. Kiểm tra log được ghi
- **Kết quả mong đợi**:
  - Log chứa chi tiết lỗi
  - Level log là "Error"
  - Có stack trace nếu có exception

## Test Case ALS-003: Phân loại log theo module
- **Mô tả**: Kiểm tra phân loại log theo từng module
- **Các bước kiểm tra**:
  1. Thực hiện hoạt động ở các module khác nhau
  2. Kiểm tra phân loại log
- **Kết quả mong đợi**:
  - Log được phân loại đúng theo module
  - Dễ dàng lọc và tìm kiếm theo module

## Test Case ALS-004: Xử lý concurrent logging
- **Mô tả**: Kiểm tra khả năng ghi log đồng thời
- **Các bước kiểm tra**:
  1. Thực hiện nhiều hoạt động đồng thời
  2. Kiểm tra việc ghi log
- **Kết quả mong đợi**:
  - Không bị mất log
  - Log được ghi theo đúng thứ tự thời gian
  - Không xảy ra race condition

## Test Case ALS-005: Giới hạn kích thước log
- **Mô tả**: Kiểm tra xử lý khi log file đạt giới hạn
- **Các bước kiểm tra**:
  1. Tạo nhiều log đến khi đạt giới hạn
  2. Kiểm tra cơ chế rotation
- **Kết quả mong đợi**:
  - Tự động tạo file log mới
  - Không mất dữ liệu log
  - Dọn dẹp log cũ theo cấu hình

## Test Case ALS-006: Format log
- **Mô tả**: Kiểm tra format của log entries
- **Các bước kiểm tra**:
  1. Tạo các loại log khác nhau
  2. Kiểm tra format
- **Kết quả mong đợi**:
  - Format nhất quán
  - Đầy đủ thông tin cần thiết
  - Dễ đọc và phân tích

## Test Case ALS-007: Bảo mật log
- **Mô tả**: Kiểm tra bảo mật thông tin trong log
- **Các bước kiểm tra**:
  1. Ghi log có thông tin nhạy cảm
  2. Kiểm tra cách xử lý
- **Kết quả mong đợi**:
  - Thông tin nhạy cảm được che giấu
  - Chỉ admin có quyền xem full log
  - Tuân thủ chính sách bảo mật
