# Project MyShop 2025

# A. Yêu cầu chung

## A1. Tóm tắt yêu cầu

> Tạo ra ứng dụng hỗ trợ chủ cửa hàng bán hàng.

## A2. Người dùng của hệ thống

- Hệ thống chỉ có một người dùng duy nhất là người chủ cửa hàng nhỏ.

## A3. Kiến trúc chương trình

Chương trình có kiến trúc client - server, sử dụng database tùy chọn. 

## A4. Luồng màn hình chính

LoginScreen -> Đăng nhập -> MainApp
MainApp gồm:
- Dashboard
- ProductsScreen
- OrdersScreen
- ReportScreen
- SettingsScreen

## A5. Lược đồ CSDL

### Lược đồ tổng quan

ORDER -> ORDER-ITEM (includes)
CATEGORY -> PRODUCT (belongs to)
PRODUCT -> ORDER-ITEM (ordered in)

### Chi tiết CSDL

ORDER: order_id, created_Time, final_price
ORDER-ITEM: order_item_id, quantity, unit_sale_price, total_price
PRODUCT: product_id, sku, name, import_price, count, description
CATEGORY: category_id, name, description

# B. Các chức năng cơ sở (5 điểm)

## B1. Đăng nhập (0.25 điểm)

- Tự động đăng nhập nếu có thông tin từ lần trước
- Thông tin đăng nhập được mã hóa
- Hiển thị thông tin phiên bản chương trình
- Cho phép cấu hình server từ màn hình Config

## B2. Dashboard tổng quan hệ thống (0.5 điểm)

- Tổng số sản phẩm
- Top 5 sản phẩm sắp hết hàng
- Top 5 sản phẩm bán chạy
- Tổng số đơn hàng & doanh thu trong ngày
- Biểu đồ doanh thu theo ngày

## B3. Quản lí sản phẩm - Products (1.25 điểm)

- Xem danh sách sản phẩm theo loại
- Xóa / Sửa / Thêm mới sản phẩm
- Phân trang, tìm kiếm, lọc theo giá
- Import từ tập tin Excel hoặc Access

## B4. Quản lí đơn hàng - Orders (1.5 điểm)

- Tạo, xóa, cập nhật đơn hàng
- Danh sách đơn hàng có phân trang, tìm kiếm theo ngày
- Trạng thái đơn hàng: Mới tạo, Đã thanh toán, Đã hủy

## B5. Báo cáo thống kê - Report (1 điểm)

- Xem số lượng bán theo ngày, tuần, tháng, năm (biểu đồ đường)
- Báo cáo doanh thu & lợi nhuận theo khoảng thời gian (biểu đồ cột / bánh)

## B6. Cấu hình chương trình (0.25 điểm)

- Hiệu chỉnh số lượng sản phẩm mỗi trang
- Ghi nhớ màn hình cuối cùng khi đăng nhập lại

## B7. Đóng gói thành file cài đặt (0.25 điểm)

- Xuất file .exe để cài đặt chương trình

# C. Các chức năng tự chọn (5 điểm)

- Auto save khi tạo đơn hàng, thêm sản phẩm (0.25)
- Responsive layout (0.5 điểm)
- Hỗ trợ plugin mở rộng (1 điểm)
- Quản lí khuyến mãi (1 điểm)
- Mã nguồn chống dịch ngược (0.25 điểm)
- Chế độ dùng thử 15 ngày (0.5 điểm)
- Backup / Restore database (0.25 điểm)
- Dùng GraphQL API thay REST (1 điểm)
- Kiến trúc MVVM (0.5 điểm)
- Dependency Injection (0.5 điểm)
- Phân quyền admin/sale (0.5 điểm)
- Trả hoa hồng theo KPI (0.25 điểm)
- Quản lí khách hàng (0.5 điểm)
- Test case kiểm thử chức năng (0.5 điểm)
- In đơn hàng (0.5 điểm)
- Hỗ trợ sắp xếp nâng cao (0.5 điểm)
- Tìm kiếm nâng cao (1 điểm)
- Hướng dẫn sử dụng (0.5 điểm)

# D. Hướng dẫn nộp bài

- Source code (xóa file trung gian, thư mục .vs)
- Release: file thực thi hoặc file setup
- readme.txt: chứa thông tin nhóm, chức năng hoàn thành, đánh giá
- Nếu quá lớn, upload lên Google Drive và nộp link
- File nén đặt tên theo MSSV nhóm: MSSV1_MSSV2_..._MSSVn.zip hoặc .rar
