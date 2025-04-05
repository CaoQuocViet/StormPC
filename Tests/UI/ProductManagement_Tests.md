# Kiểm tra giao diện Quản lý Sản phẩm

## Test Case UI-P-001: Hiển thị danh sách sản phẩm
- **Mô tả**: Kiểm tra GridView hiển thị đúng danh sách sản phẩm
- **Tiền điều kiện**: Có sản phẩm trong database
- **Các bước kiểm tra**:
  1. Mở trang Products
  2. Kiểm tra các sản phẩm được hiển thị
- **Kết quả mong đợi**: GridView hiển thị đúng các sản phẩm với thông tin và hình ảnh

## Test Case UI-P-002: Thêm sản phẩm mới
- **Mô tả**: Kiểm tra dialog thêm sản phẩm mới
- **Các bước kiểm tra**:
  1. Nhấn nút "Thêm mới"
  2. Điền thông tin vào form dialog
  3. Nhấn nút "Thêm"
- **Kết quả mong đợi**: 
  - Sản phẩm mới được thêm vào database
  - GridView cập nhật hiển thị sản phẩm mới

## Test Case UI-P-003: Lọc sản phẩm theo khoảng giá
- **Mô tả**: Kiểm tra chức năng lọc sản phẩm theo khoảng giá
- **Các bước kiểm tra**:
  1. Điều chỉnh thanh trượt khoảng giá
  2. Kiểm tra sản phẩm được hiển thị
- **Kết quả mong đợi**: Chỉ hiển thị sản phẩm có giá nằm trong khoảng đã chọn

## Test Case UI-P-004: Tìm kiếm sản phẩm theo tên
- **Mô tả**: Kiểm tra chức năng tìm kiếm sản phẩm theo tên
- **Tiền điều kiện**: Có nhiều sản phẩm với tên khác nhau
- **Các bước kiểm tra**:
  1. Nhập từ khóa tìm kiếm vào SearchBox
  2. Kiểm tra kết quả hiển thị
- **Kết quả mong đợi**: Chỉ hiển thị sản phẩm có tên chứa từ khóa tìm kiếm

## Test Case UI-P-005: Xác nhận xóa sản phẩm
- **Mô tả**: Kiểm tra dialog xác nhận khi xóa sản phẩm
- **Tiền điều kiện**: Có sản phẩm trong database
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm
  2. Nhấn nút "Xóa"
  3. Kiểm tra dialog xác nhận xuất hiện
  4. Nhấn "Hủy" trong dialog
- **Kết quả mong đợi**: 
  - Dialog xác nhận hiển thị
  - Sản phẩm không bị xóa khi nhấn "Hủy"

## Test Case UI-P-006: Xóa sản phẩm thành công
- **Mô tả**: Kiểm tra quá trình xóa sản phẩm
- **Tiền điều kiện**: Có sản phẩm không liên kết với đơn hàng
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm
  2. Nhấn nút "Xóa"
  3. Xác nhận xóa trong dialog
- **Kết quả mong đợi**: 
  - Sản phẩm bị xóa khỏi database (soft delete)
  - GridView không còn hiển thị sản phẩm đã xóa

## Test Case UI-P-007: Xóa nhiều sản phẩm cùng lúc
- **Mô tả**: Kiểm tra chức năng xóa nhiều sản phẩm
- **Tiền điều kiện**: Có nhiều sản phẩm không liên kết với đơn hàng
- **Các bước kiểm tra**:
  1. Nhấn nút "Chọn nhiều"
  2. Chọn nhiều sản phẩm
  3. Nhấn nút "Xóa đã chọn"
  4. Xác nhận xóa trong dialog
- **Kết quả mong đợi**: Tất cả sản phẩm đã chọn bị xóa

## Test Case UI-P-008: Cập nhật thông tin sản phẩm
- **Mô tả**: Kiểm tra chức năng cập nhật thông tin sản phẩm
- **Tiền điều kiện**: Có sản phẩm trong database
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm
  2. Nhấn nút chỉnh sửa
  3. Sửa đổi thông tin trong dialog
  4. Nhấn nút "Lưu"
- **Kết quả mong đợi**: Thông tin sản phẩm được cập nhật trong database và UI

## Test Case UI-P-009: Thêm cấu hình mới cho sản phẩm
- **Mô tả**: Kiểm tra chức năng thêm cấu hình mới cho sản phẩm
- **Tiền điều kiện**: Có sản phẩm trong database
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm
  2. Nhấn nút "Thêm cấu hình"
  3. Điền thông số cấu hình trong dialog
  4. Nhấn nút "Thêm"
- **Kết quả mong đợi**: Cấu hình mới được thêm vào sản phẩm

## Test Case UI-P-010: Hiển thị thông số kỹ thuật của sản phẩm
- **Mô tả**: Kiểm tra hiển thị thông số kỹ thuật chi tiết của sản phẩm
- **Tiền điều kiện**: Có sản phẩm với nhiều cấu hình khác nhau
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm
  2. Nhấn vào nút xem chi tiết
- **Kết quả mong đợi**: Chi tiết thông số kỹ thuật được hiển thị chính xác

## Test Case UI-P-011: Phân trang danh sách sản phẩm
- **Mô tả**: Kiểm tra chức năng phân trang
- **Tiền điều kiện**: Có nhiều sản phẩm (>10)
- **Các bước kiểm tra**:
  1. Mở trang Products
  2. Kiểm tra điều khiển phân trang
  3. Nhấn nút chuyển trang
- **Kết quả mong đợi**: 
  - Hiển thị số trang chính xác
  - Khi chuyển trang, hiển thị sản phẩm của trang đã chọn

## Test Case UI-P-012: Sắp xếp sản phẩm
- **Mô tả**: Kiểm tra chức năng sắp xếp sản phẩm
- **Tiền điều kiện**: Có nhiều sản phẩm với giá và tên khác nhau
- **Các bước kiểm tra**:
  1. Mở dropdown sắp xếp
  2. Chọn tiêu chí sắp xếp (ví dụ: "Giá từ thấp đến cao")
- **Kết quả mong đợi**: Danh sách sản phẩm được sắp xếp theo tiêu chí đã chọn

## Test Case UI-P-013: Lọc sản phẩm theo thương hiệu
- **Mô tả**: Kiểm tra chức năng lọc sản phẩm theo thương hiệu
- **Tiền điều kiện**: Có sản phẩm từ nhiều thương hiệu khác nhau
- **Các bước kiểm tra**:
  1. Chọn một thương hiệu từ dropdown
  2. Kiểm tra danh sách sản phẩm
- **Kết quả mong đợi**: Chỉ hiển thị sản phẩm của thương hiệu đã chọn

## Test Case UI-P-014: Xử lý lỗi khi thêm sản phẩm không hợp lệ
- **Mô tả**: Kiểm tra xử lý lỗi khi người dùng nhập thông tin không hợp lệ
- **Các bước kiểm tra**:
  1. Nhấn nút "Thêm mới"
  2. Nhập thông tin không hợp lệ (ví dụ: giá âm, để trống tên)
  3. Nhấn nút "Thêm"
- **Kết quả mong đợi**: Hiển thị thông báo lỗi tương ứng, không thêm sản phẩm

## Test Case UI-P-015: Hiệu ứng hover trên sản phẩm
- **Mô tả**: Kiểm tra hiệu ứng hover khi di chuột qua sản phẩm
- **Các bước kiểm tra**:
  1. Di chuột qua một sản phẩm trong GridView
- **Kết quả mong đợi**: Hiển thị hiệu ứng hover (thay đổi màu nền, hiện các nút thao tác)

## Test Case UI-P-016: Thay đổi số lượng sản phẩm mỗi trang
- **Mô tả**: Kiểm tra chức năng thay đổi số lượng sản phẩm hiển thị trên mỗi trang
- **Các bước kiểm tra**:
  1. Tìm điều khiển PageSize
  2. Thay đổi giá trị (ví dụ: từ 10 thành 20)
- **Kết quả mong đợi**: 
  - Số lượng sản phẩm hiển thị thay đổi theo giá trị mới
  - Điều khiển phân trang cập nhật tổng số trang

## Test Case UI-P-017: Kiểm tra responsive của giao diện
- **Mô tả**: Kiểm tra khả năng responsive của giao diện quản lý sản phẩm
- **Các bước kiểm tra**:
  1. Thay đổi kích thước cửa sổ ứng dụng
  2. Kiểm tra cách layout thích ứng
- **Kết quả mong đợi**: UI điều chỉnh phù hợp với kích thước cửa sổ