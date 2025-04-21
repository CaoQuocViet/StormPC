# STORMPC 2024

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
