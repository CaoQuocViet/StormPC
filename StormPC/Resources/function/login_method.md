# Luồng hoạt động sẽ như sau:
- Khi app khởi động, nó sẽ kiểm tra xem đã setup lần đầu chưa
- Nếu chưa setup, mở FirstTimeWindow để tạo tài khoản admin
- Sau khi setup xong, FirstTimeWindow sẽ đóng và mở LoginWindow
- Nếu đã setup rồi, mở thẳng LoginWindow
- Khi đăng nhập thành công, LoginWindow sẽ đóng và mở MainWindow