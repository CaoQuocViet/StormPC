# Kiểm tra OrdersViewModel

## Test Case OVM-001: Tạo đơn hàng mới
- **Mô tả**: Kiểm tra chức năng tạo đơn hàng mới
- **Tiền điều kiện**: 
  - Đã khởi tạo OrdersViewModel
  - Có sản phẩm trong database
- **Các bước kiểm tra**:
  1. Gọi phương thức CreateNewOrder()
  2. Thêm các sản phẩm vào đơn hàng
  3. Lưu đơn hàng
- **Kết quả mong đợi**: 
  - Đơn hàng được lưu vào database
  - OrdersCollection được cập nhật

## Test Case OVM-002: Tính toán tổng tiền
- **Mô tả**: Kiểm tra việc tính toán tổng tiền đơn hàng
- **Các bước kiểm tra**:
  1. Tạo đơn hàng mới
  2. Thêm nhiều sản phẩm với số lượng và giá khác nhau
- **Kết quả mong đợi**: Tổng tiền đơn hàng bằng tổng của (giá sản phẩm * số lượng)

## Test Case OVM-003: Cập nhật trạng thái đơn hàng
- **Mô tả**: Kiểm tra việc thay đổi trạng thái đơn hàng
- **Tiền điều kiện**: Có đơn hàng trong database
- **Các bước kiểm tra**:
  1. Chọn một đơn hàng
  2. Thay đổi trạng thái (ví dụ: từ "Mới tạo" sang "Đã thanh toán")
  3. Lưu thay đổi
- **Kết quả mong đợi**: Trạng thái đơn hàng được cập nhật trong database

## Test Case OVM-004: Phân trang danh sách đơn hàng
- **Mô tả**: Kiểm tra chức năng phân trang khi hiển thị danh sách đơn hàng
- **Tiền điều kiện**: Có nhiều đơn hàng trong database (>10)
- **Các bước kiểm tra**:
  1. Khởi tạo OrdersViewModel với PageSize = 10
  2. Kiểm tra OrdersCollection
  3. Chuyển sang trang tiếp theo bằng cách tăng CurrentPage
- **Kết quả mong đợi**:
  - OrdersCollection chỉ chứa đúng 10 đơn hàng
  - Khi chuyển trang, OrdersCollection hiển thị 10 đơn hàng tiếp theo

## Test Case OVM-005: Tìm kiếm đơn hàng
- **Mô tả**: Kiểm tra chức năng tìm kiếm đơn hàng
- **Tiền điều kiện**: Có nhiều đơn hàng với thông tin khách hàng khác nhau
- **Các bước kiểm tra**:
  1. Đặt SearchText = "tên khách hàng"
  2. Gọi phương thức ApplyFilters()
- **Kết quả mong đợi**: OrdersCollection chỉ hiển thị đơn hàng của khách hàng có tên chứa chuỗi tìm kiếm

## Test Case OVM-006: Lọc đơn hàng theo ngày
- **Mô tả**: Kiểm tra chức năng lọc đơn hàng theo khoảng thời gian
- **Tiền điều kiện**: Có đơn hàng từ nhiều ngày khác nhau
- **Các bước kiểm tra**:
  1. Đặt StartDate và EndDate
  2. Gọi phương thức ApplyFilters()
- **Kết quả mong đợi**: OrdersCollection chỉ hiển thị đơn hàng trong khoảng thời gian đã chọn

## Test Case OVM-007: Lọc đơn hàng theo trạng thái
- **Mô tả**: Kiểm tra chức năng lọc đơn hàng theo trạng thái
- **Tiền điều kiện**: Có đơn hàng với các trạng thái khác nhau
- **Các bước kiểm tra**:
  1. Đặt SelectedStatus = "Đã thanh toán"
  2. Gọi phương thức ApplyFilters()
- **Kết quả mong đợi**: OrdersCollection chỉ hiển thị đơn hàng có trạng thái "Đã thanh toán"

## Test Case OVM-008: Xóa đơn hàng
- **Mô tả**: Kiểm tra chức năng xóa đơn hàng
- **Tiền điều kiện**: Có đơn hàng trong database
- **Các bước kiểm tra**:
  1. Chọn một đơn hàng
  2. Gọi phương thức DeleteOrder()
- **Kết quả mong đợi**: 
  - Đơn hàng được đánh dấu xóa trong database (IsDeleted = true)
  - OrdersCollection không còn hiển thị đơn hàng đã xóa

## Test Case OVM-009: Xử lý lỗi khi tạo đơn hàng
- **Mô tả**: Kiểm tra xử lý lỗi khi tạo đơn hàng không hợp lệ
- **Các bước kiểm tra**:
  1. Tạo đơn hàng mới không có sản phẩm
  2. Gọi phương thức SaveOrder()
- **Kết quả mong đợi**: 
  - Phương thức hiển thị thông báo lỗi
  - Đơn hàng không được lưu vào database

## Test Case OVM-010: Thêm sản phẩm vào đơn hàng
- **Mô tả**: Kiểm tra chức năng thêm sản phẩm vào đơn hàng
- **Tiền điều kiện**: 
  - Đã khởi tạo đơn hàng mới
  - Có sản phẩm trong database
- **Các bước kiểm tra**:
  1. Gọi phương thức AddProductToOrder() với thông tin sản phẩm và số lượng
- **Kết quả mong đợi**: 
  - Sản phẩm được thêm vào đơn hàng với số lượng đúng
  - Tổng tiền đơn hàng được tính lại

## Test Case OVM-011: Xóa sản phẩm khỏi đơn hàng
- **Mô tả**: Kiểm tra chức năng xóa sản phẩm khỏi đơn hàng
- **Tiền điều kiện**: Đơn hàng có ít nhất một sản phẩm
- **Các bước kiểm tra**:
  1. Gọi phương thức RemoveProductFromOrder() với sản phẩm đang có trong đơn hàng
- **Kết quả mong đợi**: 
  - Sản phẩm được xóa khỏi đơn hàng
  - Tổng tiền đơn hàng được tính lại

## Test Case OVM-012: Cập nhật số lượng sản phẩm trong đơn hàng
- **Mô tả**: Kiểm tra chức năng cập nhật số lượng sản phẩm trong đơn hàng
- **Tiền điều kiện**: Đơn hàng có ít nhất một sản phẩm
- **Các bước kiểm tra**:
  1. Gọi phương thức UpdateProductQuantity() với sản phẩm và số lượng mới
- **Kết quả mong đợi**: 
  - Số lượng sản phẩm được cập nhật
  - Tổng tiền đơn hàng được tính lại

## Test Case OVM-013: Xác thực số lượng tồn kho
- **Mô tả**: Kiểm tra việc xác thực số lượng tồn kho khi thêm sản phẩm vào đơn hàng
- **Tiền điều kiện**: 
  - Có sản phẩm với số lượng tồn kho giới hạn
  - Đã khởi tạo đơn hàng mới
- **Các bước kiểm tra**:
  1. Gọi phương thức AddProductToOrder() với số lượng lớn hơn tồn kho
- **Kết quả mong đợi**: Hiển thị thông báo lỗi "Không đủ số lượng tồn kho"

## Test Case OVM-014: Khôi phục đơn hàng từ trạng thái "Đã hủy"
- **Mô tả**: Kiểm tra chức năng khôi phục đơn hàng từ trạng thái "Đã hủy"
- **Tiền điều kiện**: Có đơn hàng với trạng thái "Đã hủy"
- **Các bước kiểm tra**:
  1. Chọn đơn hàng đã hủy
  2. Gọi phương thức RestoreOrder()
- **Kết quả mong đợi**: Trạng thái đơn hàng chuyển về "Mới tạo"

## Test Case OVM-015: Kiểm tra khởi tạo ViewModel
- **Mô tả**: Kiểm tra quá trình khởi tạo OrdersViewModel
- **Các bước kiểm tra**:
  1. Tạo instance mới của OrdersViewModel
  2. Gọi phương thức InitializeAsync()
- **Kết quả mong đợi**: 
  - OrdersCollection được khởi tạo đúng
  - StatusList chứa các trạng thái hợp lệ
  - PageSize và CurrentPage có giá trị mặc định