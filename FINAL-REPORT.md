# TỰ ĐÁNH GIÁ ĐỒ ÁN STORMPC 2024

> **Ghi chú**:  
> - Bài nộp báo cáo quá trình và cuối kì là giống nhau vì đồ án được hoàn thành trước deadline báo cáo quá trình. 
> - Link GitHub được để private, chỉ mở sau khi học kì kết thúc, sau DL cuối kì là 30 ngày.
> - Link GitHub: [https://github.com/CaoQuocViet/StormPC](https://github.com/CaoQuocViet/StormPC)
> - Video Demo: [https://youtu.be/dRkxu4bkW9A](https://youtu.be/dRkxu4bkW9A)

## Đánh Giá Tổng Quan
- Tự đánh giá đã hoàn thành gần như tất cả các chức năng trong Requirements ở phần chức năng cơ sở, chỉ còn xót:
  + Cho phép import dữ liệu từ tập tin Excel hoặc Access
  + Tìm kiếm các đơn hàng từ ngày đến ngày
- Thay vào đó là các chức năng bổ sung phù hợp với yêu cầu của chủ đề Laptop và nhu cầu thực tế của người dùng
- Các chức năng tự chọn đã hoàn thành được tổng 5,5 điểm
- Các chức năng tự tìm hiểu, mong được xem xét cộng thêm 1 điểm (do tính ứng dụng thực tế, công sức tìm hiểu và độ khó cao):
  + Backup key để khôi phục đăng nhập
  + Activity log ghi lại toàn bộ hoạt động người dùng
  + Dark mode (cái này dễ, mặc định template đã có)

* ĐIỂM TỰ CHẤM CHO TOÀN BỘ ĐỒ ÁN: 10đ

## Hình Ảnh Demo Chức Năng
1. Màn hình cài đặt (Settings):  
   ![Settings](/Resources/demo_img/1-setting-dark.png)

2. Nhật ký hoạt động (Activity Log):  
   ![Activity Log](/Resources/demo_img/2-activitylog-dark.png)

3. Quản lý sản phẩm (Products):  
   ![Products](/Resources/demo_img/3-product-dark.png)

4. Quản lý danh mục (Categories):  
   ![Categories](/Resources/demo_img/4-category-dark.png)

5. Danh sách đơn hàng (Order List):  
   ![Order List](/Resources/demo_img/5-orderlist-dark.png)

6. Chi tiết đơn hàng (Order Details):  
   ![Order Details](/Resources/demo_img/6-orderdetails-dark.png)

7. Báo cáo khách hàng (Customer Report):  
   ![Customer Report](/Resources/demo_img/7-customerreport-light.png)

8. Bảng báo cáo khách hàng (Customer Report Table):  
   ![Customer Report Table](/Resources/demo_img/8-customerreporttable-light.png)

9. Báo cáo doanh thu (Revenue Report):  
   ![Revenue Report](/Resources/demo_img/9-revenuereport-light.png)

10. Báo cáo tồn kho (Inventory Report):  
    ![Inventory Report](/Resources/demo_img/10-inventoryreport-light.png)

11. Bảng báo cáo tồn kho (Inventory Report Table):  
    ![Inventory Report Table](/Resources/demo_img/11-inventoryreporttable-light.png)

12. Tìm kiếm nâng cao (Advanced Search):  
    ![Advanced Search](/Resources/demo_img/12-advancedsearch-light.png)

---

# BÁO CÁO CUỐI KỲ - STORMPC 2024

## A. Chức Năng Cơ Sở

### B1. Đăng Nhập

#### Mô tả chi tiết
- Hệ thống đăng nhập với tài khoản admin duy nhất
- Tự động đăng nhập nếu có thông tin lưu từ lần trước (thời hạn 1 giờ)
- Mã hóa thông tin đăng nhập bằng Argon2id
- Hiển thị thông tin phiên bản chương trình
- Cho phép cấu hình thông tin server từ màn hình Config
- Giới hạn 5 lần đăng nhập sai và khóa 15 phút
- Phiên làm việc có hiệu lực trong 12 giờ

### B2. Dashboard Tổng Quan Hệ Thống

#### Mô tả chi tiết
- Hiển thị tổng quan về tình trạng cửa hàng:
  - Tổng số sản phẩm và tồn kho
  - Top 5 sản phẩm sắp hết hàng (số lượng < 5)
  - Top 5 sản phẩm bán chạy
  - Tổng số đơn hàng trong ngày
  - Tổng doanh thu trong ngày
  - Chi tiết 3 đơn hàng gần nhất
  - Biểu đồ doanh thu theo ngày trong tháng

### B3. Quản Lý Sản Phẩm

#### Mô tả chi tiết
- Quản lý danh sách sản phẩm theo loại
- Hỗ trợ phân trang (5/10/15/20 sản phẩm/trang)
- Sắp xếp theo nhiều tiêu chí:
  - Mới nhất
  - Giá thấp đến cao
  - Giá cao đến thấp
  - Giảm giá nhiều nhất
  - Tên A-Z/Z-A
- Lọc theo khoảng giá
- Tìm kiếm theo tên sản phẩm
- Thêm mới, chỉnh sửa và xóa sản phẩm
- Quản lý danh mục sản phẩm

### B4. Quản Lý Đơn Hàng

#### Mô tả chi tiết
- Quản lý toàn bộ đơn hàng trong hệ thống
- Tạo mới, chỉnh sửa và xóa đơn hàng
- Theo dõi trạng thái đơn hàng:
  - Mới tạo
  - Đã thanh toán
  - Đã hủy
- Tìm kiếm đơn hàng theo ngày
- Phân trang và sắp xếp linh hoạt

### B5. Báo Cáo Thống Kê

#### Mô tả chi tiết
1. Báo cáo doanh thu:
   - Thống kê theo ngày, tuần, tháng, năm
   - Biểu đồ xu hướng doanh thu
   - Phân tích theo danh mục sản phẩm
   - Thống kê phương thức thanh toán

2. Báo cáo lợi nhuận:
   - Phân tích lợi nhuận theo thời gian
   - So sánh với cùng kỳ
   - Biểu đồ cột/bánh thể hiện tỷ lệ

### B6. Cấu Hình Chương Trình

#### Mô tả chi tiết
- Hiệu chỉnh số lượng sản phẩm mỗi trang (5/10/15/20)
- Lưu lại chức năng chính lần cuối mở
- Cấu hình thông tin kết nối database
- Quản lý thông tin doanh nghiệp

### B7. Đóng Gói File Cài Đặt

#### Mô tả chi tiết
- Đóng gói thành file exe để cài đặt
- Tự động cập nhật phiên bản mới



### 8. Backup / Restore Database (0.25 điểm)
- Tích hợp chức năng backup và restore database cho PostgreSQL:
  - Sử dụng Docker commands để thực hiện pg_dump và psql
  - Lưu trữ backup files trong thư mục cục bộ với timestamp
  - Tự động dọn dẹp các file backup cũ
  - Giao diện người dùng thân thiện trong phần Settings
  - Log lại các hoạt động backup/restore
  - Tự động kill các connection hiện tại khi restore

## B. Chức Năng Tự Chọn Đã Triển Khai (5,5đ)

### 1. Quản Lý Khách Hàng (0.5 điểm)
- Triển khai đầy đủ trong CustomerReportViewModel:
  - Phân loại khách hàng thành Platinum/Gold/Silver/Bronze dựa trên tổng chi tiêu
  - Biểu đồ tròn trực quan hiển thị tỷ lệ các phân khúc khách hàng
  - Phân tích xu hướng mua hàng qua biểu đồ doanh số theo thời gian
  - Hiển thị thông tin thương hiệu ưa thích của khách hàng
  - Quản lý thành phố và địa chỉ chi tiết với mô hình City
  - CRUD (Thêm/Sửa/Xóa/Xem) thông tin khách hàng

### 2. Tạo Ra Các Test Case Kiểm Thử Chức Năng và Giao Diện (0.5 điểm)
- Thiết lập các test case cho chức năng và giao diện người dùng
- Đảm bảo các chức năng hoạt động đúng và giao diện thân thiện
- Thực hiện kiểm tra tự động hoặc thủ công

### 3. In Đơn Hàng (0.5 điểm)
- Sử dụng thư viện iText để xuất hóa đơn ra file PDF
- Tùy chỉnh thông tin in:
  - Thông tin khách hàng và địa chỉ giao hàng
  - Danh sách sản phẩm đã đặt với thông tin chi tiết
  - Tổng tiền và thông tin thanh toán
  - Sử dụng font Unicode để hỗ trợ tiếng Việt
  - Có định dạng bảng, màu sắc, header, và footer chuyên nghiệp

### 4. Hỗ Trợ Sắp Xếp Khi Xem Danh Sách Theo Nhiều Tiêu Chí, Tùy Biến Chiều Tăng/Giảm (0.5 điểm)
- Hỗ trợ đa tiêu chí sắp xếp:
  - MultiColumnSort trong danh sách sản phẩm, đơn hàng, khách hàng, v.v.
  - Tùy biến hướng sắp xếp (tăng/giảm)
  - Lưu trữ và áp dụng thứ tự sắp xếp theo yêu cầu của người dùng

### 5. Hỗ Trợ Tìm Kiếm Nâng Cao (1 điểm)
- Triển khai SearchService với khả năng tìm kiếm đa tiêu chí:
  - Tìm kiếm theo loại đối tượng (Laptop, Customer, Brand, Category, City, Payment)
  - Hỗ trợ tìm kiếm đồng thời nhiều trường (tên, mô tả, email, địa chỉ, số điện thoại)
  - Hiển thị kết quả tìm kiếm được phân nhóm theo loại đối tượng
  - Phân trang kết quả tìm kiếm cho hiệu suất tốt
  - Hỗ trợ AutoSuggestBox khi nhập từ khóa tìm kiếm
  - Tìm kiếm không phân biệt chữ hoa/thường (case-insensitive)

### 6. Sử Dụng Kiến Trúc MVVM (0.5 điểm)
- Áp dụng triệt để mô hình MVVM trong toàn bộ ứng dụng:
  - Tách biệt rõ ràng View (XAML), ViewModel và Model
  - Sử dụng CommunityToolkit.Mvvm để hỗ trợ binding
  - ObservableProperty tự động thông báo thay đổi
  - Command pattern với RelayCommand
  - Dễ dàng unit testing và mở rộng

### 7. Báo Cáo và Thống Kê (0.75 điểm)
- Triển khai đầy đủ các dashboard báo cáo:
  - Dashboard tổng quan với KPI chính: doanh thu, lợi nhuận, số đơn hàng, giá trị đơn trung bình
  - Báo cáo doanh thu theo thời gian sử dụng biểu đồ
  - Báo cáo phân tích sản phẩm tồn kho
  - Báo cáo khách hàng với phân khúc Platinum/Gold/Silver/Bronze
  - Tính toán và hiển thị các chỉ số quan trọng
  - Sử dụng thư viện LiveCharts để tạo biểu đồ trực quan

### 8. Sử Dụng Dependency Injection (0.5 điểm)
- Sử dụng DI xuyên suốt trong ứng dụng:
  - Constructor injection cho các service
  - Interface-based programming giúp dễ thay thế và mock
  - Singleton, Transient và Scoped lifecycle management
  - Cấu hình DI trong App.xaml.cs
  - Service Locator pattern với App.GetService<T>()

### 9. Bổ Sung Khuyến Mãi Giảm Giá (1 điểm)
- Triển khai trong hệ thống sản phẩm:
  - Discount: lưu trữ số tiền giảm giá (không phải phần trăm)
  - DiscountStartDate và DiscountEndDate: thời gian bắt đầu và kết thúc khuyến mãi
  - FormattedDiscount: hiển thị phần trăm giảm giá được tính dựa trên giá gốc
  - Tự động tính toán phần trăm giảm giá từ số tiền giảm giá thực tế
  - Badge hiển thị phần trăm giảm giá ở góc sản phẩm
  - Làm tròn giá trị tiền tệ theo đơn vị 1000 VNĐ

### 10. Tự Động Thay Đổi Sắp Xếp Hợp Lí Các Thành Phần Theo Độ Rộng Màn Hình (Responsive Layout) (0.5 điểm)
- Triển khai thiết kế responsive:
  - Sử dụng Grid với các định nghĩa ColumnDefinitions và RowDefinitions linh hoạt
  - Sử dụng ItemsWrapGrid cho hiển thị sản phẩm động theo kích thước màn hình
  - Áp dụng các Thickness và Margin được định nghĩa trong ResourceDictionary
  - Các Style ứng dụng trong toàn bộ giao diện để duy trì tính nhất quán
  - Sử dụng CardStyle với thuộc tính tự thích ứng cho các phần tử Dashboard
- Giao diện tự điều chỉnh theo kích thước màn hình:
  - Grid layout với Auto và Star sizing
  - StackPanel với Spacing thay đổi theo kích thước
  - Adaptive controls trong ứng dụng
  - Thành phần UI thay đổi vị trí, kích thước tự động
  - Đảm bảo trải nghiệm người dùng trên các kích cỡ màn hình khác nhau

### 11. Hỗ Trợ Đa Ngôn Ngữ (0.25 điểm)
- Sử dụng ResourceLoader và tệp Resources.resw cho quốc tế hóa:
  - Hỗ trợ cơ bản cho localization thông qua ResourceExtensions
  - Tập trung vào giao diện tiếng Việt với các tệp dữ liệu ngôn ngữ
  - Chuẩn bị cấu trúc để mở rộng hỗ trợ thêm ngôn ngữ khác
  - Lưu và đọc cài đặt người dùng thông qua LocalSettingsService

## C. Đề Xuất Cộng Điểm

### 1. Bảo mật nâng cao
- Sử dụng Argon2id cho mã hóa password
- Tạo backup key để khôi phục tài khoản
- Giới hạn số lần đăng nhập sai và tự động khóa
- Lưu trữ an toàn với Windows ProtectedData

### 2. Hỗ trợ Dark Mode
- Giao diện dark mode tùy chỉnh
- Tự động chuyển đổi theo cài đặt hệ thống

### 3. Activity Logging
- Ghi lại toàn bộ hoạt động người dùng
- Phân loại log theo mức độ nghiêm trọng
- Tìm kiếm và lọc log theo nhiều tiêu chí
- Xuất báo cáo log cho việc kiểm tra an ninh