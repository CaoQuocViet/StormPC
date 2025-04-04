# BÁO CÁO GIỮA KỲ - STORMPC SHOP MANAGEMENT

**https://github.com/CaoQuocViet/StormPC**

- Đây là báo cáo đáp ứng yêu cầu quản lý sản phẩm, đơn hàng
- Các chức năng CRUD trên sản phẩm, loại hàng và đơn hàng đều đã hoàn thiện
- Xử lý các ngoại lệ đầy đủ, các bài test chỉ ở mức " Exploratory Testing"

## A. TÍNH NĂNG ĐÃ TRIỂN KHAI

### 1. Chức năng cơ sở (Basic Features)

#### 1.1. Đăng nhập và Bảo mật (0.5/0.25 điểm)
- [x] Tự động đăng nhập với thông tin từ lần trước
- [x] Thông tin đăng nhập được mã hóa
- [x] Hiển thị thông tin phiên bản
- [x] Cho phép cấu hình server từ màn hình Config
- [x] Tính năng đăng ký tài khoản mới
- [x] Phân quyền người dùng (Admin/Staff)

#### 1.2. Dashboard tổng quan (0.5/0.5 điểm)
- [x] Hiển thị tổng số sản phẩm
- [x] Top 5 sản phẩm sắp hết hàng
- [x] Tổng số đơn hàng & doanh thu trong ngày
- [x] Biểu đồ doanh thu theo ngày
- [x] Trực quan hóa dữ liệu bằng nhiều loại biểu đồ
- [x] Dashboard tương tác (Interactive charts)

#### 1.3. Quản lý sản phẩm - Products (1.0/1.25 điểm)
- [x] Xem danh sách laptop và thông số kỹ thuật
- [x] CRUD đầy đủ cho laptop và variants
- [x] Phân trang, tìm kiếm cơ bản
- [x] Sắp xếp theo nhiều tiêu chí (Multi-criteria sorting)
- [ ] Chưa có import từ Excel/Access

#### 1.4. Quản lý đơn hàng - Orders (1.5/1.5 điểm)
- [x] Tạo, xóa, cập nhật đơn hàng
- [x] Danh sách đơn có phân trang, tìm kiếm theo ngày
- [x] Quản lý trạng thái đơn hàng (Mới/Đã thanh toán/Đã hủy)
- [x] Tính năng nổi bật:
  - Kiểm tra tồn kho tự động
  - Cập nhật số lượng tồn sau mỗi đơn hàng
  - Tự động tính tổng tiền
  - Quản lý địa chỉ giao hàng và mã bưu điện

#### 1.5. Cấu hình và Bảo trì (0.5/0.25 điểm)
- [x] Hiệu chỉnh số lượng sản phẩm mỗi trang
- [x] Ghi nhớ màn hình cuối cùng khi đăng nhập lại
- [x] Backup/Restore database
- [x] Quản lý phiên bản và cập nhật

### 2. Tính năng tự chọn đã triển khai

#### 2.1. Giao diện và UX (1.5 điểm)
- [x] Responsive layout (0.5 điểm)
- [x] Hỗ trợ sắp xếp nâng cao đa tiêu chí (0.5 điểm)
- [x] Trực quan hóa dữ liệu bằng đồ thị (0.5 điểm)

#### 2.2. Kiến trúc và kỹ thuật (1.5 điểm)
- [x] Kiến trúc MVVM (0.5 điểm)
- [x] Dependency Injection (0.5 điểm)
- [x] Backup/Restore database (0.25 điểm)
- [x] Phân quyền admin/staff (0.25 điểm)

#### 2.3. Quản lý dữ liệu (0.5 điểm)
- [x] Quản lý khách hàng (0.5 điểm)

#### 2.4. Tính năng bổ sung ngoài yêu cầu
- [x] Quản lý thông số kỹ thuật chi tiết cho laptop
- [x] Hệ thống quản lý thành phố và mã bưu điện
- [x] Validation dữ liệu chặt chẽ
- [x] Xử lý lỗi và thông báo người dùng
- [x] Soft delete cho các đối tượng

## B. TÍNH NĂNG ĐANG TRIỂN KHAI

### 1. Chức năng cơ sở
- [ ] Đóng gói thành file cài đặt (0/0.25 điểm)

### 2. Tính năng tự chọn
- [ ] Activity Log - Nhật ký hoạt động (0/0.5 điểm)
- [ ] Auto save khi tạo đơn hàng (0/0.25 điểm)
- [ ] Hỗ trợ plugin mở rộng (0/1.0 điểm)
- [ ] Quản lý khuyến mãi (0/1.0 điểm)
- [ ] Tìm kiếm nâng cao (0/1.0 điểm)
- [ ] Test case kiểm thử (0/0.5 điểm)
- [ ] In đơn hàng (0/0.5 điểm)
- [ ] Hướng dẫn sử dụng (0/0.5 điểm)

## C. ĐIỂM NỔI BẬT

1. **Thiết kế giao diện**
   - Modern UI với WinUI 3
   - Responsive và dễ sử dụng
   - Thông báo lỗi trực quan
   - Biểu đồ tương tác đa dạng

2. **Kiến trúc phần mềm**
   - Áp dụng MVVM pattern
   - Dependency Injection
   - Code organization rõ ràng
   - Phân quyền người dùng

3. **Xử lý dữ liệu**
   - Validation chặt chẽ
   - Soft delete
   - Quản lý transaction
   - Tối ưu query database
   - Backup/Restore tích hợp

4. **Tính năng mở rộng**
   - Quản lý variant sản phẩm
   - Hệ thống địa chỉ và mã bưu điện
   - Sắp xếp đa tiêu chí
   - Trực quan hóa dữ liệu

## D. HƯỚNG PHÁT TRIỂN TIẾP THEO

1. Triển khai Activity Log để theo dõi hoạt động người dùng
2. Phát triển tính năng tìm kiếm nâng cao
3. Thêm tính năng khuyến mãi và quản lý giá
4. Phát triển hệ thống plugin
5. Tối ưu hiệu năng và UX
6. Viết documentation và hướng dẫn sử dụng
7. Triển khai các test case
8. Xây dựng installer
