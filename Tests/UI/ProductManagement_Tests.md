# Kiểm tra giao diện Quản lý Sản phẩm

## Test Case UI-P-001: Hiển thị danh sách sản phẩm
- **Mô tả**: Kiểm tra GridView hiển thị đúng danh sách sản phẩm
- **Tiền điều kiện**: Có sản phẩm trong database
- **Các bước kiểm tra**:
  1. Mở trang Products
  2. Kiểm tra các sản phẩm được hiển thị
- **Kết quả mong đợi**: 
  - GridView hiển thị đúng các sản phẩm với thông tin và hình ảnh
  - Các thông tin cơ bản như tên model, thương hiệu, giá, thông số kỹ thuật hiển thị chính xác
  - Hiển thị đúng định dạng giá tiền và phần trăm giảm giá

## Test Case UI-P-002: Thêm sản phẩm mới
- **Mô tả**: Kiểm tra dialog thêm sản phẩm mới
- **Tiền điều kiện**: Đã tải danh sách thương hiệu và danh mục
- **Các bước kiểm tra**:
  1. Nhấn nút "Thêm mới"
  2. Điền đầy đủ thông tin hợp lệ vào form dialog: tên model, thương hiệu, danh mục, kích thước màn hình, hệ điều hành, năm phát hành
  3. Nhấn nút "Thêm"
- **Kết quả mong đợi**: 
  - Sản phẩm mới được thêm vào database
  - GridView cập nhật hiển thị sản phẩm mới
  - Hiển thị thông báo thành công
  - Hệ thống ghi log hành động thêm sản phẩm

## Test Case UI-P-003: Lọc sản phẩm theo khoảng giá
- **Mô tả**: Kiểm tra chức năng lọc sản phẩm theo khoảng giá
- **Tiền điều kiện**: Có sản phẩm với các mức giá khác nhau
- **Các bước kiểm tra**:
  1. Điều chỉnh thanh trượt khoảng giá (RangeSelector) để đặt mức giá từ A đến B
  2. Kiểm tra danh sách sản phẩm được lọc
- **Kết quả mong đợi**: 
  - Chỉ hiển thị sản phẩm có giá nằm trong khoảng đã chọn
  - Giá trị khoảng giá hiển thị đúng định dạng tiền tệ
  - Các sản phẩm ngoài khoảng giá không hiển thị trong danh sách

## Test Case UI-P-004: Tìm kiếm sản phẩm theo từ khóa
- **Mô tả**: Kiểm tra chức năng tìm kiếm sản phẩm theo nhiều tiêu chí
- **Tiền điều kiện**: Có nhiều sản phẩm với tên và thương hiệu khác nhau
- **Các bước kiểm tra**:
  1. Nhập từ khóa tìm kiếm vào SearchBox
  2. Kiểm tra kết quả hiển thị
- **Kết quả mong đợi**: 
  - Hiển thị sản phẩm có tên model chứa từ khóa tìm kiếm
  - Hiển thị sản phẩm có tên thương hiệu chứa từ khóa tìm kiếm
  - Tìm kiếm không phân biệt chữ hoa/thường
  - Danh sách được cập nhật ngay khi nhập từ khóa (OnSearchTextChanged)

## Test Case UI-P-005: Xác nhận xóa sản phẩm
- **Mô tả**: Kiểm tra dialog xác nhận khi xóa sản phẩm đơn lẻ
- **Tiền điều kiện**: Có sản phẩm không liên kết với đơn hàng
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm
  2. Nhấn nút "Xóa" trên hover menu
  3. Kiểm tra dialog xác nhận xuất hiện
  4. Nhấn "Hủy" trong dialog
- **Kết quả mong đợi**: 
  - Dialog xác nhận hiển thị với nội dung cảnh báo phù hợp
  - Sản phẩm không bị xóa khi nhấn "Hủy"
  - Dialog đóng lại sau khi nhấn "Hủy"

## Test Case UI-P-006: Xóa sản phẩm thành công
- **Mô tả**: Kiểm tra quá trình xóa sản phẩm đơn lẻ
- **Tiền điều kiện**: Có sản phẩm không liên kết với đơn hàng
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm
  2. Nhấn nút "Xóa" trên hover menu
  3. Xác nhận xóa trong dialog (nhấn "Xóa")
- **Kết quả mong đợi**: 
  - Sản phẩm bị đánh dấu là đã xóa trong database (soft delete)
  - GridView cập nhật, không còn hiển thị sản phẩm đã xóa
  - Hệ thống ghi log hành động xóa sản phẩm

## Test Case UI-P-007: Xóa nhiều sản phẩm cùng lúc
- **Mô tả**: Kiểm tra chức năng xóa nhiều sản phẩm đồng thời
- **Tiền điều kiện**: Có nhiều sản phẩm không liên kết với đơn hàng
- **Các bước kiểm tra**:
  1. Nhấn nút "Chọn nhiều"
  2. Chọn nhiều sản phẩm trong GridView (chế độ MultipleSelection)
  3. Nhấn nút "Xóa đã chọn"
  4. Xác nhận xóa trong dialog
- **Kết quả mong đợi**: 
  - Tất cả sản phẩm đã chọn được xóa (soft delete)
  - GridView không còn hiển thị các sản phẩm đã xóa
  - Hệ thống ghi log hành động xóa nhiều sản phẩm
  - Trở về chế độ chọn đơn sau khi hoàn tất

## Test Case UI-P-008: Cập nhật thông tin sản phẩm
- **Mô tả**: Kiểm tra chức năng cập nhật thông tin sản phẩm
- **Tiền điều kiện**: Có sản phẩm không liên kết với đơn hàng
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm
  2. Nhấn nút chỉnh sửa
  3. Sửa đổi thông tin trong dialog (thay đổi tên model, thương hiệu, v.v.)
  4. Nhấn nút "Lưu"
- **Kết quả mong đợi**: 
  - Thông tin sản phẩm được cập nhật trong database
  - GridView hiển thị thông tin đã cập nhật
  - Hệ thống ghi log hành động cập nhật sản phẩm

## Test Case UI-P-009: Thêm cấu hình mới cho sản phẩm
- **Mô tả**: Kiểm tra chức năng thêm cấu hình mới cho sản phẩm
- **Tiền điều kiện**: Có sản phẩm trong database
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm
  2. Nhấn nút "Thêm cấu hình"
  3. Điền đầy đủ thông số cấu hình trong dialog: CPU, GPU, RAM, Storage, màu sắc, giá nhập, giá bán, số lượng tồn kho
  4. Nhấn nút "Thêm"
- **Kết quả mong đợi**: 
  - Cấu hình mới được thêm vào database
  - Số lượng cấu hình (OptionsCount) của sản phẩm tăng lên
  - SKU được tạo tự động theo format đúng
  - Hệ thống ghi log hành động thêm cấu hình

## Test Case UI-P-010: Hiển thị thông số kỹ thuật của sản phẩm
- **Mô tả**: Kiểm tra hiển thị thông số kỹ thuật chi tiết của sản phẩm
- **Tiền điều kiện**: Có sản phẩm với nhiều cấu hình khác nhau
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm
  2. Nhấn vào nút xem chi tiết
- **Kết quả mong đợi**: 
  - Dialog hiển thị đầy đủ thông số cơ bản của sản phẩm: tên model, thương hiệu, danh mục, kích thước màn hình, v.v.
  - Hiển thị danh sách tất cả các cấu hình của sản phẩm với thông tin chi tiết
  - Thông tin định dạng đúng (giá tiền, đơn vị)

## Test Case UI-P-011: Phân trang danh sách sản phẩm
- **Mô tả**: Kiểm tra chức năng phân trang
- **Tiền điều kiện**: Có nhiều sản phẩm (>10)
- **Các bước kiểm tra**:
  1. Mở trang Products
  2. Kiểm tra điều khiển phân trang
  3. Nhấn nút chuyển sang trang tiếp theo
  4. Nhấn nút chuyển đến trang cuối cùng
  5. Nhấn nút quay lại trang đầu tiên
- **Kết quả mong đợi**: 
  - Hiển thị số trang chính xác dựa trên tổng số sản phẩm và kích thước trang
  - Khi chuyển trang, hiển thị đúng sản phẩm của trang đã chọn
  - Các nút điều hướng trang hoạt động chính xác

## Test Case UI-P-012: Sắp xếp sản phẩm
- **Mô tả**: Kiểm tra các tiêu chí sắp xếp sản phẩm
- **Tiền điều kiện**: Có nhiều sản phẩm với giá, tên, năm phát hành khác nhau
- **Các bước kiểm tra**:
  1. Mở dropdown sắp xếp
  2. Lần lượt chọn các tiêu chí sắp xếp:
     - Mới nhất
     - Giá từ thấp đến cao
     - Giá cao đến thấp
     - Giảm giá nhiều nhất
     - Tên A-Z
     - Tên Z-A
- **Kết quả mong đợi**: 
  - Danh sách sản phẩm được sắp xếp chính xác theo từng tiêu chí
  - Thứ tự sắp xếp được áp dụng ngay lập tức khi chọn
  - Kết hợp đúng với các bộ lọc hiện tại

## Test Case UI-P-013: Lọc sản phẩm theo thương hiệu
- **Mô tả**: Kiểm tra chức năng lọc sản phẩm theo thương hiệu
- **Tiền điều kiện**: Có sản phẩm từ nhiều thương hiệu khác nhau
- **Các bước kiểm tra**:
  1. Chọn một thương hiệu từ dropdown Brand
  2. Kiểm tra danh sách sản phẩm được lọc
- **Kết quả mong đợi**: 
  - Chỉ hiển thị sản phẩm của thương hiệu đã chọn
  - Áp dụng đúng với các bộ lọc khác (nếu có)
  - Số lượng sản phẩm hiển thị và phân trang cập nhật chính xác

## Test Case UI-P-014: Xử lý lỗi khi thêm sản phẩm không hợp lệ
- **Mô tả**: Kiểm tra xử lý lỗi validation khi người dùng nhập thông tin không hợp lệ
- **Các bước kiểm tra**:
  1. Nhấn nút "Thêm mới"
  2. Nhập thông tin không hợp lệ (test các trường hợp):
     - Để trống tên model
     - Không chọn thương hiệu hoặc danh mục
     - Nhập giá trị âm cho kích thước màn hình
     - Nhập giá trị âm cho giảm giá
  3. Nhấn nút "Thêm"
- **Kết quả mong đợi**: 
  - Hiển thị thông báo lỗi tương ứng cho từng trường hợp invalid input
  - Không thêm sản phẩm vào database khi có lỗi validation
  - Giữ nguyên form với dữ liệu đã nhập để người dùng chỉnh sửa

## Test Case UI-P-015: Hiệu ứng hover trên sản phẩm
- **Mô tả**: Kiểm tra hiệu ứng hover và các nút thao tác
- **Các bước kiểm tra**:
  1. Di chuột qua một sản phẩm trong GridView
  2. Kiểm tra hiệu ứng hover và các nút chức năng hiển thị
  3. Di chuột ra khỏi sản phẩm
- **Kết quả mong đợi**: 
  - Khi hover, hiển thị layer màu tối mờ đè lên sản phẩm
  - Hiển thị các nút thao tác: Thêm cấu hình, Chỉnh sửa, Xóa
  - Khi di chuột ra khỏi sản phẩm, trở lại hiển thị bình thường

## Test Case UI-P-016: Thay đổi số lượng sản phẩm mỗi trang
- **Mô tả**: Kiểm tra chức năng thay đổi số lượng sản phẩm hiển thị trên mỗi trang
- **Các bước kiểm tra**:
  1. Tìm điều khiển PageSize
  2. Thay đổi giá trị lần lượt thành 10, 20, 50, 100
- **Kết quả mong đợi**: 
  - Số lượng sản phẩm hiển thị thay đổi theo giá trị PageSize mới
  - Điều khiển phân trang cập nhật tổng số trang chính xác
  - Giữ nguyên các bộ lọc và tiêu chí sắp xếp hiện tại

## Test Case UI-P-017: Kiểm tra responsive của giao diện
- **Mô tả**: Kiểm tra khả năng responsive của giao diện quản lý sản phẩm
- **Các bước kiểm tra**:
  1. Thay đổi kích thước cửa sổ ứng dụng (thu nhỏ, mở rộng)
  2. Kiểm tra cách layout thích ứng
- **Kết quả mong đợi**: 
  - UI điều chỉnh phù hợp với kích thước cửa sổ
  - GridView thay đổi số cột hiển thị phù hợp
  - Các điều khiển (nút, dropdown) vẫn hiển thị và sử dụng được

## Test Case UI-P-018: Chỉnh sửa sản phẩm đã có đơn hàng
- **Mô tả**: Kiểm tra xử lý khi cố gắng chỉnh sửa sản phẩm đã liên kết với đơn hàng
- **Tiền điều kiện**: Có sản phẩm đã liên kết với đơn hàng
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm đã có đơn hàng
  2. Nhấn nút chỉnh sửa
- **Kết quả mong đợi**: 
  - Hiển thị thông báo không thể chỉnh sửa sản phẩm đã có đơn hàng
  - Không mở dialog chỉnh sửa
  - Hệ thống ghi log lỗi chỉnh sửa

## Test Case UI-P-019: Xóa sản phẩm đã có đơn hàng
- **Mô tả**: Kiểm tra xử lý khi cố gắng xóa sản phẩm đã liên kết với đơn hàng
- **Tiền điều kiện**: Có sản phẩm đã liên kết với đơn hàng
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm đã có đơn hàng
  2. Nhấn nút xóa
- **Kết quả mong đợi**: 
  - Hiển thị thông báo không thể xóa sản phẩm đã có đơn hàng
  - Sản phẩm không bị xóa
  - Hệ thống ghi log lỗi xóa

## Test Case UI-P-020: Xóa nhiều sản phẩm có cả sản phẩm liên kết với đơn hàng
- **Mô tả**: Kiểm tra xử lý khi xóa nhiều sản phẩm trong đó có cả sản phẩm liên kết với đơn hàng
- **Tiền điều kiện**: Có sản phẩm liên kết với đơn hàng và có sản phẩm không liên kết
- **Các bước kiểm tra**:
  1. Bật chế độ chọn nhiều
  2. Chọn một số sản phẩm (bao gồm cả sản phẩm có liên kết đơn hàng)
  3. Nhấn nút "Xóa đã chọn"
- **Kết quả mong đợi**: 
  - Hiển thị thông báo danh sách sản phẩm có thể xóa và không thể xóa
  - Chỉ xóa được những sản phẩm không liên kết với đơn hàng
  - Cập nhật GridView hiển thị đúng kết quả

## Test Case UI-P-021: Nhập dữ liệu hợp lệ khi thêm cấu hình sản phẩm
- **Mô tả**: Kiểm tra validation khi nhập dữ liệu thêm cấu hình sản phẩm
- **Tiền điều kiện**: Có sản phẩm trong database
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm
  2. Nhấn nút "Thêm cấu hình"
  3. Thử các trường hợp dữ liệu không hợp lệ:
     - Để trống CPU
     - Nhập RAM = 0
     - Nhập Storage = 0
     - Nhập giá âm
     - Nhập số lượng tồn kho âm
  4. Nhấn nút "Thêm"
- **Kết quả mong đợi**: 
  - Hiển thị thông báo lỗi cho từng trường hợp
  - Không thêm cấu hình khi có lỗi validation
  - Form vẫn giữ nguyên để người dùng sửa lại

## Test Case UI-P-022: Kết hợp nhiều bộ lọc trong tìm kiếm sản phẩm
- **Mô tả**: Kiểm tra khả năng kết hợp nhiều bộ lọc khác nhau
- **Tiền điều kiện**: Có nhiều sản phẩm với các đặc điểm khác nhau
- **Các bước kiểm tra**:
  1. Nhập từ khóa tìm kiếm
  2. Thiết lập khoảng giá
  3. Chọn thương hiệu
  4. Áp dụng tiêu chí sắp xếp
- **Kết quả mong đợi**: 
  - Danh sách sản phẩm được lọc theo tất cả các tiêu chí đã chọn
  - Thứ tự hiển thị đúng với tiêu chí sắp xếp
  - Số lượng phân trang chính xác với kết quả lọc

## Test Case UI-P-023: Kiểm tra hiển thị giảm giá và ưu đãi
- **Mô tả**: Kiểm tra hiển thị thông tin giảm giá và ưu đãi trên sản phẩm
- **Tiền điều kiện**: Có sản phẩm với giảm giá
- **Các bước kiểm tra**:
  1. Xem danh sách sản phẩm có giảm giá
  2. Kiểm tra hiển thị % giảm giá
  3. Kiểm tra hiển thị giá gốc và giá đã giảm
- **Kết quả mong đợi**: 
  - Hiển thị chính xác % giảm giá
  - Hiển thị giá sau khi giảm là giá chính
  - Định dạng đúng số tiền với đơn vị VNĐ

## Test Case UI-P-024: Kiểm tra loading state khi tải dữ liệu
- **Mô tả**: Kiểm tra trạng thái loading khi tải danh sách sản phẩm
- **Các bước kiểm tra**:
  1. Mở trang Products
  2. Quan sát trong quá trình tải dữ liệu
- **Kết quả mong đợi**: 
  - Hiển thị loading indicator trong quá trình tải dữ liệu
  - Sau khi tải xong, hiển thị danh sách sản phẩm
  - Không hiển thị loading indicator khi đã tải xong