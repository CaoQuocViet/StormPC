# Cấu trúc StormPC hiện tại:
+ StormPC: UI Layer (WinUI 3)
+ StormPC.Core: Business Logic & Data Layer
+ Resources: Shared Resources

# StormPC (Dự án chính)
Dự án chính này chứa giao diện người dùng và phần logic ứng dụng.

## Các thư mục con:

### 🔹 Properties
Chứa thông tin cấu hình cơ bản của ứng dụng.

### 🔹 Activation
Quản lý quá trình khởi động ứng dụng, xử lý khi ứng dụng được mở từ trạng thái đóng hoặc từ một nguồn cụ thể (ví dụ: từ thông báo, deep linking, v.v.).

### 🔹 Assets
Chứa tài nguyên hình ảnh, icon, splash screen, và các file tĩnh khác được sử dụng trong giao diện người dùng.

### 🔹 Behaviors
Chứa các Behavior (hành vi mở rộng) giúp bổ sung các tính năng động cho giao diện mà không cần sửa đổi trực tiếp View.

### 🔹 Contracts
Thường chứa các interface dùng để định nghĩa các hợp đồng (contract) giữa các thành phần, hỗ trợ Dependency Injection.

### 🔹 Helpers
Chứa các lớp tiện ích (Utility classes), có thể hỗ trợ xử lý chuỗi, thao tác file, hoặc các tác vụ chung.

### 🔹 Models (M - Model)
Chứa các lớp biểu diễn dữ liệu trong ứng dụng.
Thường ánh xạ với các dữ liệu được truy xuất từ API hoặc database.

### 🔹 Services
Chứa các lớp dịch vụ cung cấp các chức năng như xử lý dữ liệu, gọi API, truy vấn database, hoặc quản lý trạng thái ứng dụng.

### 🔹 Styles
Chứa các tài nguyên định dạng giao diện như ResourceDictionary, giúp quản lý thống nhất kiểu dáng của ứng dụng.

### 🔹 Strings
Chứa các file tài nguyên về ngôn ngữ (Localization), giúp ứng dụng hỗ trợ đa ngôn ngữ.

### 🔹 ViewModels (VM - ViewModel)
Chứa logic trung gian giữa Model và View.
Các lớp trong thư mục này xử lý dữ liệu, thực hiện các lệnh (Command), và thông báo thay đổi cho UI.

### 🔹 Views (V - View)
Chứa các XAML file dùng để hiển thị giao diện người dùng.
Mỗi View thường có một ViewModel tương ứng.

# StormPC.Core (Dự án lõi)
Dự án StormPC.Core chứa phần logic nghiệp vụ có thể dùng lại trong nhiều dự án khác nhau.

## Các thư mục con:

### 🔹 Dependencies
Quản lý các gói thư viện được sử dụng trong dự án.

### 🔹 Contracts
Định nghĩa các interface để giúp dễ dàng mở rộng và thay thế các thành phần.

### 🔹 Helpers
Chứa các lớp tiện ích hỗ trợ như xử lý JSON, format dữ liệu.

### 🔹 Models
Định nghĩa các lớp dữ liệu chung, có thể dùng trong nhiều dự án khác nhau.

### 🔹 Services
Chứa các dịch vụ dùng chung, ví dụ như FileService.cs giúp xử lý tệp tin.

# Các File Chính
- **App.xaml & App.xaml.cs**: Quản lý vòng đời ứng dụng.
- **MainWindow.xaml**: Cửa sổ chính của ứng dụng.
- **appsettings.json**: File cấu hình chính của ứng dụng.
- **README.md**: Hướng dẫn về dự án.

# 🔥 Tóm lại:
- **Models**: Chứa dữ liệu.
- **ViewModels**: Xử lý logic và trung gian giữa Models và Views.
- **Views**: Giao diện người dùng.
- **Services**: Xử lý nghiệp vụ chung.
- **StormPC.Core**: Chứa phần lõi có thể tái sử dụng.

Với cấu trúc này, dự án của bạn theo sát mô hình MVVM giúp dễ bảo trì, mở rộng, và kiểm thử. 🚀