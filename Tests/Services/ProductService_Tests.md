# Kiểm tra ProductService

## Test Case PS-001: Lấy danh sách laptop
- **Mô tả**: Kiểm tra việc lấy danh sách laptop từ database
- **Tiền điều kiện**: Database có dữ liệu laptop
- **Các bước kiểm tra**:
  1. Gọi phương thức GetAllLaptopsAsync()
- **Kết quả mong đợi**: Trả về danh sách các laptop

## Test Case PS-002: Thêm laptop mới
- **Mô tả**: Kiểm tra khả năng thêm laptop mới vào database
- **Các bước kiểm tra**:
  1. Tạo đối tượng Laptop mới với thông tin hợp lệ
  2. Gọi phương thức AddLaptopAsync()
  3. Kiểm tra laptop mới trong database
- **Kết quả mong đợi**: Laptop được thêm thành công với ID mới

## Test Case PS-003: Kiểm tra ràng buộc xóa laptop đã có đơn hàng
- **Mô tả**: Kiểm tra việc không cho phép xóa laptop đã có trong đơn hàng
- **Tiền điều kiện**: 
  - Có laptop trong database
  - Laptop có liên kết với ít nhất một đơn hàng
- **Các bước kiểm tra**:
  1. Gọi phương thức DeleteLaptopAsync() với ID laptop đã có đơn hàng
- **Kết quả mong đợi**: Phương thức ném ngoại lệ hoặc trả về false

## Test Case PS-004: Cập nhật thông tin laptop
- **Mô tả**: Kiểm tra khả năng cập nhật thông tin laptop
- **Tiền điều kiện**: Có laptop trong database
- **Các bước kiểm tra**:
  1. Lấy laptop hiện có từ database
  2. Sửa đổi thông tin (tên, giá, mô tả)
  3. Gọi phương thức UpdateLaptopAsync()
  4. Truy vấn lại laptop từ database
- **Kết quả mong đợi**: Thông tin laptop được cập nhật trong database

## Test Case PS-005: Tìm kiếm laptop theo tên mẫu
- **Mô tả**: Kiểm tra chức năng tìm kiếm laptop theo tên mẫu
- **Tiền điều kiện**: Database có nhiều laptop với tên khác nhau
- **Các bước kiểm tra**:
  1. Gọi phương thức SearchLaptopsByModelAsync() với một chuỗi tìm kiếm
- **Kết quả mong đợi**: Trả về danh sách laptop có tên chứa chuỗi tìm kiếm

## Test Case PS-006: Lọc laptop theo khoảng giá
- **Mô tả**: Kiểm tra chức năng lọc laptop theo khoảng giá
- **Các bước kiểm tra**:
  1. Gọi phương thức FilterLaptopsByPriceRangeAsync() với min và max price
- **Kết quả mong đợi**: Trả về danh sách laptop có giá nằm trong khoảng đã chọn

## Test Case PS-007: Lọc laptop theo thương hiệu
- **Mô tả**: Kiểm tra chức năng lọc laptop theo thương hiệu
- **Các bước kiểm tra**:
  1. Gọi phương thức FilterLaptopsByBrandAsync() với ID thương hiệu
- **Kết quả mong đợi**: Trả về danh sách laptop thuộc thương hiệu đã chọn

## Test Case PS-008: Thêm cấu hình (LaptopSpec) cho laptop
- **Mô tả**: Kiểm tra chức năng thêm cấu hình chi tiết cho laptop
- **Tiền điều kiện**: Có laptop trong database
- **Các bước kiểm tra**:
  1. Tạo đối tượng LaptopSpec mới với thông tin hợp lệ
  2. Gán LaptopID của laptop hiện có
  3. Gọi phương thức AddLaptopSpecAsync()
- **Kết quả mong đợi**: Cấu hình được thêm vào database và liên kết với laptop

## Test Case PS-009: Kiểm tra lỗi khi thêm laptop với dữ liệu không hợp lệ
- **Mô tả**: Kiểm tra xử lý lỗi khi thêm laptop với dữ liệu không hợp lệ
- **Các bước kiểm tra**:
  1. Tạo đối tượng Laptop với dữ liệu không hợp lệ (ví dụ: không có tên, giá âm)
  2. Gọi phương thức AddLaptopAsync()
- **Kết quả mong đợi**: Phương thức ném ngoại lệ với thông báo lỗi phù hợp

## Test Case PS-010: Lấy danh sách thương hiệu (Brand)
- **Mô tả**: Kiểm tra việc lấy danh sách thương hiệu từ database
- **Các bước kiểm tra**:
  1. Gọi phương thức GetAllBrandsAsync()
- **Kết quả mong đợi**: Trả về danh sách các thương hiệu

## Test Case PS-011: Lấy danh sách danh mục (Category)
- **Mô tả**: Kiểm tra việc lấy danh sách danh mục từ database
- **Các bước kiểm tra**:
  1. Gọi phương thức GetAllCategoriesAsync()
- **Kết quả mong đợi**: Trả về danh sách các danh mục

## Test Case PS-012: Kiểm tra soft delete cho laptop
- **Mô tả**: Kiểm tra chức năng xóa mềm (soft delete) laptop
- **Tiền điều kiện**: Có laptop trong database
- **Các bước kiểm tra**:
  1. Gọi phương thức DeleteLaptopAsync() với laptop không có ràng buộc
  2. Truy vấn trực tiếp database để kiểm tra cờ IsDeleted
- **Kết quả mong đợi**: Laptop vẫn tồn tại trong database với IsDeleted = true

## Test Case PS-013: Kiểm tra cập nhật hàng loạt laptop
- **Mô tả**: Kiểm tra chức năng cập nhật hàng loạt laptop
- **Tiền điều kiện**: Có nhiều laptop trong database
- **Các bước kiểm tra**:
  1. Tạo danh sách ID laptop cần cập nhật
  2. Gọi phương thức BatchUpdateLaptopsAsync() với danh sách ID và thông tin cập nhật
- **Kết quả mong đợi**: Tất cả laptop trong danh sách được cập nhật

## Test Case PS-014: Quản lý tồn kho
- **Mô tả**: Kiểm tra chức năng cập nhật tồn kho
- **Tiền điều kiện**: Có laptop spec với số lượng tồn kho
- **Các bước kiểm tra**:
  1. Gọi phương thức UpdateStockQuantityAsync() với ID spec và số lượng mới
  2. Kiểm tra số lượng tồn kho trong database
- **Kết quả mong đợi**: Số lượng tồn kho được cập nhật chính xác

## Test Case PS-015: Lấy laptop theo ID
- **Mô tả**: Kiểm tra việc lấy thông tin chi tiết laptop theo ID
- **Tiền điều kiện**: Có laptop trong database
- **Các bước kiểm tra**:
  1. Gọi phương thức GetLaptopByIdAsync() với ID hợp lệ
- **Kết quả mong đợi**: Trả về thông tin chi tiết của laptop

## Test Case PS-016: Xử lý khi không tìm thấy laptop
- **Mô tả**: Kiểm tra xử lý khi truy vấn laptop không tồn tại
- **Các bước kiểm tra**:
  1. Gọi phương thức GetLaptopByIdAsync() với ID không tồn tại
- **Kết quả mong đợi**: Phương thức trả về null hoặc ném ngoại lệ phù hợp