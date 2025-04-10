# Kiểm tra xử lý lỗi Database

## Test Case DBE-001: Xử lý lỗi kết nối
- **Mô tả**: Kiểm tra cách hệ thống xử lý khi không thể kết nối đến database
- **Các bước kiểm tra**:
  1. Ngắt kết nối database
  2. Thực hiện các thao tác CRUD
- **Kết quả mong đợi**: 
  - Hiển thị thông báo lỗi phù hợp
  - Ghi log lỗi
  - Không crash ứng dụng

## Test Case DBE-002: Xử lý timeout
- **Mô tả**: Kiểm tra xử lý khi truy vấn database bị timeout
- **Các bước kiểm tra**:
  1. Tạo truy vấn phức tạp gây timeout
  2. Thực hiện truy vấn
- **Kết quả mong đợi**:
  - Hiển thị thông báo timeout
  - Ghi log lỗi
  - Cho phép thử lại

## Test Case DBE-003: Xử lý deadlock
- **Mô tả**: Kiểm tra xử lý khi xảy ra deadlock trong database
- **Các bước kiểm tra**:
  1. Tạo tình huống deadlock
  2. Thực hiện các thao tác đồng thời
- **Kết quả mong đợi**:
  - Phát hiện và giải quyết deadlock
  - Rollback giao dịch nếu cần
  - Ghi log sự cố

## Test Case DBE-004: Xử lý lỗi toàn vẹn dữ liệu
- **Mô tả**: Kiểm tra xử lý vi phạm ràng buộc toàn vẹn
- **Các bước kiểm tra**:
  1. Thử thêm/sửa dữ liệu vi phạm ràng buộc
  2. Kiểm tra phản hồi hệ thống
- **Kết quả mong đợi**:
  - Hiển thị thông báo lỗi rõ ràng
  - Không lưu dữ liệu không hợp lệ
  - Ghi log vi phạm

## Test Case DBE-005: Xử lý lỗi truy vấn không hợp lệ
- **Mô tả**: Kiểm tra xử lý khi có truy vấn SQL không hợp lệ
- **Các bước kiểm tra**:
  1. Thực hiện truy vấn không hợp lệ
  2. Kiểm tra xử lý lỗi
- **Kết quả mong đợi**:
  - Bắt được lỗi SQL
  - Hiển thị thông báo lỗi an toàn
  - Ghi log chi tiết lỗi
