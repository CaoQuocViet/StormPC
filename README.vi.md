<p align="right">
  <a href="README.vi.md"><img src="https://img.shields.io/badge/🇻🇳-Tiếng_Việt-blue?style=flat-square" alt="Vietnamese" /></a>
  &nbsp;|&nbsp;
  <a href="README.md"><img src="https://img.shields.io/badge/🇺🇸-English-lightgrey?style=flat-square" alt="English" /></a>
</p>

# STORMPC 2024

![demo](Resources/demo_img/default.png)

## Video Demo và Hướng Dẫn
- Demo chi tiết tính năng: [YouTube - Demo StormPC](https://youtu.be/dRkxu4bkW9A)
- Hướng dẫn cài đặt ứng dụng vào máy: Xem video trong thư mục `/output_stormpc/Final_QL2_release.mp4`

## A. Cài Đặt Từ Installer
1. Vào thư mục `/output_stormpc/StormPC_1.0.6.2_Debug_Test`
2. xem hướng dẫn chi tiết từ video đính kèm

## B. Chạy Từ Source Code

### 1. Yêu Cầu Hệ Thống
- Windows 10/11
- .NET 8.0 SDK
- Docker Desktop
- Visual Studio 2022

### 2. Khởi Động PostgreSQL Database
1. Mở Command Prompt hoặc PowerShell
2. Chuyển đến thư mục dự án:  
   ```
   cd đường-dẫn\DoAn_UDQL2\DataBase
   ```
3. Chạy lệnh Docker Compose:  
   ```
   docker-compose -f docker-compose.yml up -d
   ```
4. Kiểm tra container đã chạy thành công:
   ```
   docker ps
   ```
   (Thấy container có tên `stormpc_container` đang chạy)

### 3. Cấu Hình Kết Nối Database
1. Trong thư mục `StormPC/StormPC.Core`, kiểm tra file `.env` với cấu hình sau:
   ```
   DB_PROVIDER=postgresql
   DB_HOST=localhost
   DB_PORT=5444
   DB_NAME=stormpc_db
   DB_USER=vietcq
   DB_PASSWORD=123456789000
   ```
2. Nếu chưa có file `.env`, hãy tạo mới từ file `.env.example`

### 4. Khởi Tạo Dữ Liệu Mẫu
1. Mở terminal tại thư mục `DataBase`
2. Chạy lệnh để tạo cấu trúc database:
   ```
   npx sequelize-cli db:migrate
   ```
3. Chạy lệnh để thêm dữ liệu mẫu:
   ```
   npx sequelize-cli db:seed:all
   ```
> ⚠️ **Lưu ý:** Đảm bảo môi trường chạy lệnh có nodejs để chạy Sequelize CLI

### 5. Khởi Chạy Ứng Dụng (Debug Mode)
1. Mở file solution `StormPC.sln` bằng Visual Studio 2022
2. Đặt `StormPC` làm Startup Project (chuột phải > Set as Startup Project)
3. Chọn cấu hình Debug và nền tảng x64
4. Nhấn `F5` hoặc nút `Start` để chạy ứng dụng
5. Trong lần chạy đầu tiên sẽ được yêu cầu thiết lập tài khoản admin

## C. Xử Lý Sự Cố

### Lỗi Kết Nối Database
1. Kiểm tra Docker đang chạy
2. Xác nhận container database đang hoạt động:
   ```
   docker ps | findstr stormpc
   ```
3. Kiểm tra thông tin kết nối trong `.env` trùng khớp với `docker-compose.yml`
4. Nếu cần, khởi động lại container:
   ```
   docker-compose -f docker-compose.yml down
   docker-compose -f docker-compose.yml up -d
   ```

### Lỗi Khởi Động Ứng Dụng
1. Đảm bảo đã cài đặt .NET 8.0 SDK
2. Làm sạch và rebuild solution:
   ```
   dotnet clean StormPC.sln
   dotnet build StormPC.sln
   ```
3. Kiểm tra log lỗi trong Output window của Visual Studio

## D. Tính Năng Chính
- Đăng nhập bảo mật với Argon2id
- Dashboard tổng quan hệ thống
- Quản lý sản phẩm (laptop) với thông số kỹ thuật chi tiết
- Quản lý đơn hàng và thanh toán
- Báo cáo thống kê doanh thu và tồn kho
- Quản lý khách hàng và phân loại theo nhóm
- Tìm kiếm nâng cao đa tiêu chí
- Sao lưu và khôi phục dữ liệu
- Giao diện responsive với hỗ trợ dark mode

> ⚠️ **Lưu ý:** Chạy ứng dụng ở chế độ Debug sẽ đảm bảo hoạt động đầy đủ các tính năng. Bản release có thể gặp một số hạn chế do môi trường Windows. Chưa fix được các lỗi runtime cho môi trường production.


## 🖥️ Demo Giao Diện và Chức Năng

### 1. Cài Đặt Giao Diện (Dark Mode)
![Cài đặt Dark Mode](Resources/demo_img/1-setting-dark.png)  
*Trang cài đặt với chế độ dark mode, cho phép người dùng tùy chỉnh giao diện theo sở thích*

### 2. Nhật Ký Hoạt Động
![Nhật ký hoạt động](Resources/demo_img/2-activitylog-dark.png)  
*Theo dõi và ghi lại tất cả các hoạt động của người dùng trong hệ thống*

### 3. Quản Lý Sản Phẩm
![Quản lý sản phẩm](Resources/demo_img/3-product-dark.png)  
*Giao diện quản lý danh sách laptop với thông tin chi tiết và thao tác CRUD*

### 4. Quản Lý Danh Mục
![Quản lý danh mục](Resources/demo_img/4-category-dark.png)  
*Quản lý các danh mục sản phẩm, phân loại laptop theo từng nhóm*

### 5. Danh Sách Đơn Hàng
![Danh sách đơn hàng](Resources/demo_img/5-orderlist-dark.png)  
*Hiển thị tất cả đơn hàng với trạng thái và thông tin tổng quan*

### 6. Chi Tiết Đơn Hàng
![Chi tiết đơn hàng](Resources/demo_img/6-orderdetails-dark.png)  
*Xem chi tiết đơn hàng bao gồm sản phẩm, số lượng, giá tiền và thông tin khách hàng*

### 7. Báo Cáo Khách Hàng
![Báo cáo khách hàng](Resources/demo_img/7-customerreport-light.png)  
*Báo cáo thống kê về khách hàng với biểu đồ trực quan*

### 8. Bảng Báo Cáo Khách Hàng
![Bảng báo cáo khách hàng](Resources/demo_img/8-customerreporttable-light.png)  
*Bảng dữ liệu chi tiết về thông tin và hoạt động mua hàng của khách hàng*

### 9. Báo Cáo Doanh Thu
![Báo cáo doanh thu](Resources/demo_img/9-revenuereport-light.png)  
*Biểu đồ thống kê doanh thu theo thời gian với nhiều dạng hiển thị*

### 10. Báo Cáo Tồn Kho
![Báo cáo tồn kho](Resources/demo_img/10-inventoryreport-light.png)  
*Báo cáo tình trạng tồn kho với biểu đồ phân tích*

### 11. Bảng Báo Cáo Tồn Kho
![Bảng báo cáo tồn kho](Resources/demo_img/11-inventoryreporttable-light.png)  
*Bảng dữ liệu chi tiết về số lượng tồn kho từng sản phẩm*

### 12. Tìm Kiếm Nâng Cao
![Tìm kiếm nâng cao](Resources/demo_img/12-advancedsearch-light.png)  
*Chức năng tìm kiếm đa tiêu chí với bộ lọc chi tiết*

---