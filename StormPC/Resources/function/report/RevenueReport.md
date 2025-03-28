# Báo Cáo Doanh Thu (Revenue Report)

## 1. Mục Tiêu
- Cung cấp cái nhìn tổng quan về tình hình doanh thu của cửa hàng
- Phân tích xu hướng doanh thu theo thời gian
- So sánh doanh thu và lợi nhuận giữa các khoảng thời gian
- Phân tích tỷ trọng doanh thu theo danh mục sản phẩm

## 2. Các Biểu Đồ và Chức Năng

### 2.1. Biểu Đồ Doanh Thu Theo Thời Gian (Line Chart)
- Sử dụng `CartesianChart` với `LineSeries<double>`
- Hiển thị doanh thu theo ngày/tuần/tháng/năm (có thể chuyển đổi)
- Có thể zoom và pan để xem chi tiết từng khoảng thời gian
- Tooltip hiển thị chi tiết doanh thu tại mỗi điểm
- Có đường trend line để thể hiện xu hướng

### 2.2. So Sánh Doanh Thu và Lợi Nhuận (Column Chart)
- Sử dụng `CartesianChart` với `ColumnSeries<double>`
- Hiển thị cột doanh thu và lợi nhuận song song
- Phân chia theo tháng hoặc quý
- Có thể so sánh với cùng kỳ năm trước
- Tooltip hiển thị:
  + Doanh thu
  + Lợi nhuận
  + Tỷ suất lợi nhuận (%)
  + % tăng/giảm so với kỳ trước

### 2.3. Cơ Cấu Doanh Thu (Pie Chart)
- Sử dụng `PieChart` với `PieSeries<double>`
- Hiển thị tỷ trọng doanh thu theo:
  + Danh mục sản phẩm
  + Kênh bán hàng
  + Phương thức thanh toán
- Có animation khi chuyển đổi dữ liệu
- Tooltip hiển thị giá trị và phần trăm

### 2.4. Bảng Thống Kê Chi Tiết
- Hiển thị số liệu chi tiết theo từng khoảng thời gian
- Các chỉ số bao gồm:
  + Doanh thu thuần
  + Giá vốn hàng bán
  + Lợi nhuận gộp
  + Tỷ suất lợi nhuận
  + Số đơn hàng
  + Giá trị trung bình/đơn

## 3. Tính Năng Tương Tác

### 3.1. Bộ Lọc Thời Gian
- Lọc theo ngày/tuần/tháng/quý/năm
- Tùy chọn khoảng thời gian tùy chỉnh
- So sánh với cùng kỳ năm trước

### 3.2. Bộ Lọc Dữ Liệu
- Lọc theo danh mục sản phẩm
- Lọc theo kênh bán hàng
- Lọc theo phương thức thanh toán
- Lọc theo trạng thái đơn hàng

### 3.3. Tính Năng Xuất Báo Cáo
- Xuất PDF với đầy đủ biểu đồ và số liệu
- Xuất Excel để phân tích thêm
- Tự động gửi email báo cáo định kỳ

## 4. Giao Diện
- Responsive layout thích ứng với mọi kích thước màn hình
- Dark/Light mode
- Có thể tùy chỉnh màu sắc biểu đồ
- Hỗ trợ thu phóng và di chuyển trên biểu đồ
- Lưu tùy chọn hiển thị của người dùng 