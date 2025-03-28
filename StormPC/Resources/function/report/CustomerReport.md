# Báo Cáo Khách Hàng (Customer Report)

## 1. Mục Tiêu
- Phân tích hành vi mua hàng của khách hàng
- Đánh giá hiệu quả chương trình khuyến mãi
- Theo dõi tần suất mua hàng và giá trị đơn hàng trung bình
- Phân loại khách hàng theo mức độ trung thành

## 2. Các Biểu Đồ và Chức Năng

### 2.1. Phân Tích RFM (Heat Map)
- Sử dụng `HeatSeries` để hiển thị ma trận RFM
  + Recency (Thời gian mua gần nhất)
  + Frequency (Tần suất mua hàng)
  + Monetary (Giá trị mua hàng)
- Màu sắc thể hiện mức độ tập trung khách hàng
- Tooltip hiển thị số lượng khách hàng trong mỗi phân khúc

### 2.2. Xu Hướng Mua Hàng (Multi-Line Chart)
- Sử dụng `CartesianChart` với nhiều `LineSeries<double>`
- Hiển thị đồng thời:
  + Số lượng đơn hàng
  + Giá trị đơn hàng trung bình
  + Tỷ lệ khách hàng quay lại
- Có thể chọn hiển thị theo ngày/tuần/tháng
- Tooltip hiển thị chi tiết tại mỗi điểm dữ liệu

### 2.3. Phân Bổ Khách Hàng (Funnel Chart)
- Sử dụng `FunnelChart` với `FunnelSeries<double>`
- Phân tích khách hàng theo:
  + Tần suất mua hàng
  + Giá trị chi tiêu
  + Thời gian gắn bó
- Hiển thị tỷ lệ chuyển đổi giữa các cấp độ
- Animation khi chuyển đổi dữ liệu

### 2.4. Phân Tích Giỏ Hàng (Scatter Chart)
- Sử dụng `CartesianChart` với `ScatterSeries<double>`
- Hiển thị mối tương quan giữa:
  + Số lượng sản phẩm/đơn
  + Giá trị đơn hàng
  + Tần suất mua hàng
- Kích thước điểm thể hiện số lượng khách hàng
- Có thể zoom để xem chi tiết các cụm khách hàng

## 3. Bảng Thống Kê Chi Tiết

### 3.1. Thông Tin Tổng Quan
- Tổng số khách hàng
- Số khách hàng mới trong kỳ
- Tỷ lệ khách hàng quay lại
- Giá trị đơn hàng trung bình
- Tần suất mua hàng trung bình

### 3.2. Phân Tích Chi Tiết
- Danh sách top khách hàng theo:
  + Giá trị mua hàng
  + Tần suất mua hàng
  + Thời gian gắn bó
- Thống kê theo phân khúc khách hàng
- Tỷ lệ sử dụng khuyến mãi

## 4. Tính Năng Tương Tác

### 4.1. Bộ Lọc Thời Gian
- Lọc theo khoảng thời gian
- So sánh giữa các kỳ
- Xem xu hướng theo thời gian

### 4.2. Bộ Lọc Khách Hàng
- Theo phân khúc
- Theo khu vực
- Theo hành vi mua hàng
- Theo chương trình khuyến mãi đã sử dụng

### 4.3. Tính Năng Xuất Báo Cáo
- Xuất báo cáo PDF
- Xuất dữ liệu Excel
- Tự động gửi báo cáo định kỳ
- Tùy chỉnh mẫu báo cáo

## 5. Giao Diện
- Responsive design
- Dark/Light mode
- Tùy chỉnh màu sắc và theme
- Lưu cấu hình người dùng
- Hỗ trợ thao tác touch trên tablet 