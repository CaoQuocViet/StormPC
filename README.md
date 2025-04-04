# MID Report chưa yêu cầu đóng gói sản phẩm, đây là hướng dẫn chạy project phục vụ chấm bài

**https://github.com/CaoQuocViet/StormPC**

# CÀI ĐẶT VÀ CHẠY DỰ ÁN STORMPC

## Cài đặt yêu cầu
- Windows 10/11
- .NET 8.0
- Docker Desktop
- Visual Studio 2022

## Khởi động PostgreSQL
1. Mở Command Prompt hoặc PowerShell
2. Chuyển đến thư mục dự án:  
   ```bash
   cd đường-dẫn\DoAn_UDQL2
   ```
3. Chạy lệnh:  
   ```bash
   docker-compose -f docker-compose.yml up -d
   ```

## Cấu hình kết nối
- Copy file `.env.example` thành `.env`
- Điều chỉnh thông tin kết nối database nếu cần (mặc định: `user=vietcq`, `password=123456789000`, `port=5444`)

## Khởi tạo dữ liệu
1. Mở terminal tại thư mục `DataBase`
2. Chạy lệnh:
   ```bash
   npx sequelize-cli db:migrate
   ```
3. Tiếp tục:
   ```bash
   npx sequelize-cli db:seed:all
   ```

## Chạy ứng dụng
1. Mở file solution `StormPC.sln` bằng Visual Studio 2022
2. Đặt `StormPC` làm Startup Project
3. Nhấn `F5` hoặc `Start` để chạy

> ⚠️ **Lưu ý:** Nếu có lỗi kết nối database, kiểm tra Docker đang chạy và các thông tin kết nối trong `.env` trùng khớp với `docker-compose.yml`.
