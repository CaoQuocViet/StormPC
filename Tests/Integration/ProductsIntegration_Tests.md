# Kiểm tra tích hợp quản lý sản phẩm

## Test Case PI-001: Tích hợp giữa ViewModel và Service khi tải dữ liệu
- **Mô tả**: Kiểm tra tích hợp giữa ProductsViewModel và ProductService khi tải dữ liệu
- **Tiền điều kiện**: 
  - Đã cấu hình DI cho ProductService và ProductsViewModel
  - Database có dữ liệu sản phẩm
- **Các bước kiểm tra**:
  1. Khởi tạo ProductsViewModel
  2. Gọi phương thức LoadProductsAsync() của ViewModel
- **Kết quả mong đợi**: 
  - ProductService.GetLaptopsAsync() được gọi đúng cách
  - ViewModel.Laptops được cập nhật với dữ liệu từ Service
  - ViewModel.IsLoading thay đổi trạng thái đúng (true -> false)

## Test Case PI-002: Tích hợp giữa ViewModel và UI khi thêm sản phẩm mới
- **Mô tả**: Kiểm tra tích hợp giữa ProductsViewModel và UI khi thêm sản phẩm mới
- **Tiền điều kiện**: Đã tải ProductsPage với ViewModel
- **Các bước kiểm tra**:
  1. Nhấn nút "Thêm mới" trên UI
  2. Điền thông tin sản phẩm mới trong dialog
  3. Nhấn nút "Thêm" để lưu
- **Kết quả mong đợi**: 
  - ViewModel.AddLaptop() được gọi với thông tin chính xác
  - ProductService.AddLaptopAsync() được gọi với đúng dữ liệu
  - Sau khi thêm, ViewModel.LoadProductsAsync() được gọi để làm mới danh sách
  - UI được cập nhật với sản phẩm mới

## Test Case PI-003: Tích hợp giữa ViewModel và Service khi xóa sản phẩm
- **Mô tả**: Kiểm tra tích hợp giữa ProductsViewModel và ProductService khi xóa sản phẩm
- **Tiền điều kiện**: ViewModel đã tải danh sách sản phẩm
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm trong ViewModel.Laptops
  2. Gọi phương thức Delete() trên ViewModel
- **Kết quả mong đợi**: 
  - ViewModel gọi ProductService.CanDeleteLaptopAsync() để kiểm tra điều kiện
  - Nếu có thể xóa, ViewModel gọi ProductService.DeleteLaptopAsync() với đúng ID
  - ViewModel gọi LoadProductsAsync() để cập nhật danh sách sau khi xóa
  - ActivityLogService.LogActivityAsync() được gọi để ghi log

## Test Case PI-004: Tích hợp giữa ViewModel và UI khi lọc sản phẩm
- **Mô tả**: Kiểm tra tích hợp giữa ProductsViewModel và UI khi lọc sản phẩm
- **Tiền điều kiện**: ProductsPage đã tải với dữ liệu sản phẩm
- **Các bước kiểm tra**:
  1. Thay đổi giá trị thanh trượt khoảng giá
  2. Nhập từ khóa tìm kiếm
- **Kết quả mong đợi**: 
  - UI gọi đúng các handler cho PriceRangeSelector_ValueChanged
  - ViewModel.MinPrice và ViewModel.MaxPrice được cập nhật
  - ViewModel.SearchText được cập nhật
  - ViewModel.ApplyFilters() được gọi để lọc dữ liệu
  - UI được cập nhật với kết quả lọc

## Test Case PI-005: Tích hợp giữa ViewModel và Service khi thêm cấu hình sản phẩm
- **Mô tả**: Kiểm tra tích hợp giữa ProductsViewModel và ProductService khi thêm cấu hình mới
- **Tiền điều kiện**: ViewModel đã tải danh sách sản phẩm
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm trong ViewModel.Laptops
  2. Gọi phương thức AddSpec() trên ViewModel với dữ liệu cấu hình mới
- **Kết quả mong đợi**: 
  - ViewModel thu thập dữ liệu cấu hình mới
  - ViewModel gọi ProductService.AddLaptopSpecAsync() với đúng dữ liệu
  - ProductService.GenerateSkuAsync() được gọi để tạo SKU tự động
  - ViewModel gọi LoadProductsAsync() để cập nhật danh sách
  - ActivityLogService.LogActivityAsync() được gọi để ghi log

## Test Case PI-006: Tích hợp giữa ViewModel và Service khi cập nhật thông tin sản phẩm
- **Mô tả**: Kiểm tra tích hợp giữa ProductsViewModel và ProductService khi cập nhật sản phẩm
- **Tiền điều kiện**: ViewModel đã tải danh sách sản phẩm
- **Các bước kiểm tra**:
  1. Chọn một sản phẩm trong ViewModel.Laptops
  2. Gọi phương thức Edit() trên ViewModel
  3. Cập nhật thông tin sản phẩm
  4. Gọi phương thức EditLaptop() trên ViewModel
- **Kết quả mong đợi**: 
  - ViewModel gọi ProductService.CanEditLaptopAsync() để kiểm tra điều kiện
  - ViewModel gọi ProductService.EditLaptopAsync() với đúng dữ liệu cập nhật
  - ViewModel gọi LoadProductsAsync() để làm mới danh sách
  - ActivityLogService.LogActivityAsync() được gọi để ghi log

## Test Case PI-007: Tích hợp giữa UI và ViewModel khi phân trang
- **Mô tả**: Kiểm tra tích hợp giữa UI và ViewModel khi thay đổi trang
- **Tiền điều kiện**: ProductsPage đã tải với nhiều sản phẩm (>PageSize)
- **Các bước kiểm tra**:
  1. Nhấn nút chuyển trang tiếp theo
  2. Nhấn nút quay lại trang trước
  3. Thay đổi kích thước trang
- **Kết quả mong đợi**: 
  - UI gọi ViewModel.CurrentPage = newPage khi chuyển trang
  - ViewModel gọi LoadPage() với số trang mới
  - UI gọi ViewModel.PageSize = newSize khi thay đổi kích thước trang
  - ViewModel gọi FilterAndPaginateProducts() để cập nhật danh sách
  - UI hiển thị đúng dữ liệu của trang và kích thước trang hiện tại

## Test Case PI-008: Tích hợp giữa ViewModel và Service khi sắp xếp sản phẩm
- **Mô tả**: Kiểm tra tích hợp giữa ProductsViewModel và Service khi sắp xếp sản phẩm
- **Tiền điều kiện**: ViewModel đã tải danh sách sản phẩm
- **Các bước kiểm tra**:
  1. Thay đổi ViewModel.SelectedSortIndex thành các giá trị khác nhau
- **Kết quả mong đợi**: 
  - ViewModel gọi FilterAndPaginateProducts() khi SelectedSortIndex thay đổi
  - ViewModel.SortProducts() được gọi với tiêu chí sắp xếp đúng
  - ViewModel.Laptops được cập nhật với thứ tự sắp xếp mới
  - UI hiển thị danh sách đã sắp xếp

## Test Case PI-009: Tích hợp giữa Filter, Sort và Pagination
- **Mô tả**: Kiểm tra tích hợp giữa các chức năng lọc, sắp xếp và phân trang
- **Tiền điều kiện**: ViewModel đã tải danh sách sản phẩm
- **Các bước kiểm tra**:
  1. Thiết lập bộ lọc (khoảng giá, từ khóa)
  2. Thiết lập tiêu chí sắp xếp
  3. Chuyển đến các trang khác nhau
- **Kết quả mong đợi**: 
  - Kết quả lọc được duy trì khi thay đổi trang và sắp xếp
  - Thứ tự sắp xếp được duy trì khi thay đổi trang và bộ lọc
  - Số lượng trang được tính toán lại chính xác khi thay đổi bộ lọc
  - UI hiển thị đúng dữ liệu dựa trên kết hợp của lọc, sắp xếp và phân trang

## Test Case PI-010: Tích hợp giữa ViewModel và UI khi xử lý lỗi
- **Mô tả**: Kiểm tra tích hợp trong xử lý lỗi trên toàn bộ luồng
- **Tiền điều kiện**: Môi trường đã được cấu hình để có thể gây ra lỗi
- **Các bước kiểm tra**:
  1. Gây ra lỗi khi tải dữ liệu (ngắt kết nối database)
  2. Gây ra lỗi khi thêm sản phẩm (dữ liệu không hợp lệ)
  3. Gây ra lỗi khi xóa sản phẩm (đã có ràng buộc)
- **Kết quả mong đợi**: 
  - Lỗi được bắt và xử lý đúng cách tại mọi cấp độ
  - ViewModel.ErrorMessage được thiết lập với thông báo hợp lý
  - UI hiển thị thông báo lỗi cho người dùng
  - Hệ thống ghi log lỗi thông qua ActivityLogService
  - Không có crash hoặc exception không được xử lý

## Test Case PI-011: Tích hợp giữa nhiều chức năng trong quá trình sử dụng
- **Mô tả**: Kiểm tra luồng tích hợp đầy đủ trong quá trình sử dụng thông thường
- **Tiền điều kiện**: Hệ thống đã được cấu hình đầy đủ
- **Các bước kiểm tra**:
  1. Tải danh sách sản phẩm
  2. Thêm sản phẩm mới
  3. Thêm cấu hình cho sản phẩm
  4. Lọc danh sách theo giá và tên
  5. Chỉnh sửa thông tin sản phẩm
  6. Xóa sản phẩm
- **Kết quả mong đợi**: 
  - Mỗi bước trong quy trình hoạt động chính xác
  - Các tương tác giữa ViewModel, Service và UI diễn ra trơn tru
  - Dữ liệu được cập nhật chính xác tại mỗi bước
  - Không có lỗi hoặc hành vi không mong muốn xảy ra

## Test Case PI-012: Hiệu suất khi tải nhiều dữ liệu
- **Mô tả**: Kiểm tra hiệu suất khi tải và xử lý nhiều dữ liệu
- **Tiền điều kiện**: Database có số lượng lớn sản phẩm (>1000)
- **Các bước kiểm tra**:
  1. Tải danh sách sản phẩm
  2. Lọc và sắp xếp danh sách
  3. Thay đổi kích thước trang thành giá trị lớn
- **Kết quả mong đợi**: 
  - Thời gian tải và xử lý dữ liệu nằm trong ngưỡng chấp nhận được
  - UI vẫn đáp ứng tốt khi xử lý dữ liệu lớn
  - Không có memory leak hoặc sử dụng quá nhiều tài nguyên
  - Phân trang hoạt động hiệu quả với bộ dữ liệu lớn 