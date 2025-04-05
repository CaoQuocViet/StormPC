# Kiểm tra Dashboard

## Test Case DASH-001: Hiển thị tổng quan doanh số
- **Mô tả**: Kiểm tra hiển thị tổng quan doanh số trên dashboard
- **Tiền điều kiện**: Có dữ liệu đơn hàng trong database
- **Các bước kiểm tra**:
  1. Mở trang Dashboard
  2. Kiểm tra các chỉ số KPI hiển thị
- **Kết quả mong đợi**: Hiển thị chính xác tổng doanh số, số đơn hàng, và các chỉ số khác

## Test Case DASH-002: Biểu đồ doanh thu theo thời gian
- **Mô tả**: Kiểm tra biểu đồ doanh thu theo thời gian
- **Tiền điều kiện**: Có dữ liệu đơn hàng từ nhiều ngày khác nhau
- **Các bước kiểm tra**:
  1. Mở trang Dashboard
  2. Kiểm tra biểu đồ doanh thu
- **Kết quả mong đợi**: Biểu đồ hiển thị chính xác doanh thu theo ngày/tuần/tháng

## Test Case DASH-003: Danh sách sản phẩm sắp hết hàng
- **Mô tả**: Kiểm tra danh sách sản phẩm sắp hết hàng
- **Tiền điều kiện**: Có sản phẩm với số lượng tồn thấp
- **Các bước kiểm tra**:
  1. Mở trang Dashboard
  2. Kiểm tra phần "Top 5 sản phẩm sắp hết hàng"
- **Kết quả mong đợi**: Hiển thị chính xác 5 sản phẩm có số lượng tồn thấp nhất

## Test Case DASH-004: Biểu đồ phân bổ đơn hàng theo trạng thái
- **Mô tả**: Kiểm tra biểu đồ phân bổ đơn hàng theo trạng thái
- **Tiền điều kiện**: Có đơn hàng với các trạng thái khác nhau
- **Các bước kiểm tra**:
  1. Mở trang Dashboard
  2. Kiểm tra biểu đồ tròn trạng thái đơn hàng
- **Kết quả mong đợi**: Biểu đồ hiển thị chính xác tỷ lệ các đơn hàng theo trạng thái

## Test Case DASH-005: Lọc dữ liệu dashboard theo khoảng thời gian
- **Mô tả**: Kiểm tra chức năng lọc dữ liệu theo khoảng thời gian
- **Tiền điều kiện**: Có dữ liệu đơn hàng từ nhiều thời điểm khác nhau
- **Các bước kiểm tra**:
  1. Mở trang Dashboard
  2. Chọn khoảng thời gian (ví dụ: tuần này, tháng này)
  3. Kiểm tra dữ liệu hiển thị
- **Kết quả mong đợi**: Dữ liệu dashboard được cập nhật theo khoảng thời gian đã chọn

## Test Case DASH-006: Xuất báo cáo doanh thu
- **Mô tả**: Kiểm tra chức năng xuất báo cáo doanh thu
- **Tiền điều kiện**: Có dữ liệu đơn hàng trong database
- **Các bước kiểm tra**:
  1. Mở trang Dashboard
  2. Nhấn nút "Xuất báo cáo"
  3. Chọn định dạng xuất (PDF/Excel)
- **Kết quả mong đợi**: Báo cáo được xuất ra với đúng định dạng và dữ liệu

## Test Case DASH-007: So sánh doanh thu với kỳ trước
- **Mô tả**: Kiểm tra chức năng so sánh doanh thu với kỳ trước
- **Tiền điều kiện**: Có dữ liệu đơn hàng từ nhiều tháng
- **Các bước kiểm tra**:
  1. Mở trang Dashboard
  2. Kiểm tra phần "So sánh với kỳ trước"
- **Kết quả mong đợi**: Hiển thị chính xác phần trăm tăng/giảm so với kỳ trước

## Test Case DASH-008: Biểu đồ phân tích khách hàng (Funnel Chart)
- **Mô tả**: Kiểm tra biểu đồ phân tích khách hàng dạng phễu
- **Tiền điều kiện**: Có dữ liệu khách hàng và đơn hàng
- **Các bước kiểm tra**:
  1. Mở trang "Báo cáo khách hàng"
  2. Kiểm tra biểu đồ phễu phân tích khách hàng
- **Kết quả mong đợi**: Biểu đồ hiển thị chính xác tỷ lệ khách hàng theo các phân khúc

## Test Case DASH-009: Tính toán lợi nhuận
- **Mô tả**: Kiểm tra tính toán lợi nhuận từ dữ liệu bán hàng
- **Tiền điều kiện**: Có đơn hàng với thông tin giá nhập và giá bán
- **Các bước kiểm tra**:
  1. Mở trang "Báo cáo lợi nhuận"
  2. Kiểm tra chỉ số lợi nhuận
- **Kết quả mong đợi**: Hiển thị chính xác lợi nhuận (doanh thu - giá vốn)

## Test Case DASH-010: Hiệu ứng tương tác với biểu đồ
- **Mô tả**: Kiểm tra các hiệu ứng tương tác trên biểu đồ
- **Tiền điều kiện**: Dashboard đang hiển thị các biểu đồ
- **Các bước kiểm tra**:
  1. Di chuột qua các phần tử của biểu đồ
  2. Nhấp vào các phần tử biểu đồ
- **Kết quả mong đợi**: Hiển thị thông tin chi tiết khi hover và xử lý đúng khi click