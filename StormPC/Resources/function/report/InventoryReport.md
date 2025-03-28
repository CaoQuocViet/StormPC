# Báo Cáo Kho Hàng (Inventory Report)

## 1. Mục Tiêu
- Theo dõi tình trạng tồn kho theo thời gian thực
- Phân tích hiệu quả quản lý hàng tồn kho
- Dự báo nhu cầu và đề xuất nhập hàng
- Cảnh báo hàng tồn kho thấp/cao

## 2. Các Biểu Đồ và Chức Năng

### 2.1. Biểu Đồ Tồn Kho Theo Thời Gian (Area Chart)
- Sử dụng `CartesianChart` với `StackedAreaSeries<double>`
- Hiển thị số lượng tồn kho theo:
  + Danh mục sản phẩm
  + Nhà cung cấp
  + Kho lưu trữ
- Có thể zoom và pan để xem chi tiết
- Hiển thị ngưỡng tồn kho tối ưu
- Cảnh báo khi vượt ngưỡng min/max

### 2.2. Phân Tích ABC (Pareto Chart)
- Sử dụng `CartesianChart` kết hợp `ColumnSeries<double>` và `LineSeries<double>`
- Phân loại sản phẩm theo:
  + Giá trị tồn kho
  + Tần suất xuất/nhập
  + Thời gian tồn kho
- Hiển thị đường cong tích lũy
- Tooltip hiển thị chi tiết từng nhóm

### 2.3. Vòng Quay Hàng Tồn Kho (Gauge Chart)
- Sử dụng `PieChart` với `GaugeSeries<double>`
- Hiển thị các chỉ số:
  + Vòng quay hàng tồn kho
  + Thời gian tồn kho trung bình
  + Tỷ lệ đáp ứng đơn hàng
- Animation khi cập nhật dữ liệu
- Màu sắc thể hiện hiệu quả quản lý

### 2.4. Dự Báo Nhu Cầu (Line Chart)
- Sử dụng `CartesianChart` với `LineSeries<double>`
- Hiển thị:
  + Xu hướng tiêu thụ
  + Dự báo nhu cầu
  + Khoảng tin cậy dự báo
- Có thể điều chỉnh khoảng thời gian dự báo
- Tự động cập nhật theo dữ liệu mới

## 3. Bảng Thống Kê Chi Tiết

### 3.1. Thông Tin Tổng Quan
- Tổng giá trị hàng tồn kho
- Số lượng SKU đang quản lý
- Tỷ lệ sản phẩm dưới mức tồn tối thiểu
- Tỷ lệ sản phẩm trên mức tồn tối đa
- Chi phí lưu kho trung bình

### 3.2. Phân Tích Chi Tiết
- Danh sách sản phẩm cần nhập hàng
- Sản phẩm tồn kho lâu
- Lịch sử xuất nhập kho
- Dự báo thời điểm cần nhập hàng

## 4. Tính Năng Tương Tác

### 4.1. Bộ Lọc Kho
- Theo danh mục sản phẩm
- Theo nhà cung cấp
- Theo kho lưu trữ
- Theo trạng thái tồn kho

### 4.2. Bộ Lọc Thời Gian
- Xem theo ngày/tuần/tháng
- So sánh giữa các kỳ
- Tùy chỉnh khoảng thời gian

### 4.3. Tính Năng Xuất Báo Cáo
- Xuất báo cáo tồn kho định kỳ
- Xuất báo cáo dự báo nhu cầu
- Xuất danh sách cần nhập hàng
- Tùy chỉnh mẫu báo cáo

## 5. Tính Năng Nâng Cao

### 5.1. Quản Lý Ngưỡng Tồn Kho
- Thiết lập ngưỡng min/max cho từng SKU
- Tự động điều chỉnh ngưỡng theo mùa
- Cảnh báo khi vượt ngưỡng

### 5.2. Dự Báo Thông Minh
- Sử dụng ML để dự báo nhu cầu
- Tự động điều chỉnh mô hình
- Cảnh báo sớm nguy cơ hết hàng

### 5.3. Tối Ưu Hóa Tồn Kho
- Đề xuất số lượng nhập tối ưu
- Phân tích chi phí lưu kho
- Đề xuất điều chuyển hàng giữa các kho

## 6. Giao Diện
- Responsive layout
- Dark/Light mode
- Tùy chỉnh bố cục hiển thị
- Lưu cấu hình người dùng
- Hỗ trợ cảnh báo realtime 