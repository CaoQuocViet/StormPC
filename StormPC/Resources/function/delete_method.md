# 1️⃣ Những trang có thể cho phép xóa

## 🛒 Đơn hàng
- **Danh sách đơn hàng** (có thể xóa đơn hàng nếu chưa hoàn thành hoặc theo chính sách)

## 📂 Dữ liệu cơ sở
- **Loại sản phẩm** (có thể xóa nếu không có sản phẩm liên quan)
- **Sản phẩm** (xóa sản phẩm nếu không có đơn hàng liên quan)

## 👤 Khách hàng
- **Khách hàng** (có thể xóa nếu không có đơn hàng gắn với khách hàng)

---

## 2️⃣ Cách quản lý xóa ở cấp độ Database

### ✅ Xóa mềm (Soft Delete) - Khuyến nghị  
Thay vì xóa hoàn toàn dữ liệu, thêm cột `IsDeleted` (hoặc `DeletedAt`) để đánh dấu bản ghi đã bị xóa. Điều này giúp khôi phục dữ liệu nếu cần.
```sql
ALTER TABLE Products ADD IsDeleted BIT DEFAULT 0;
UPDATE Products SET IsDeleted = 1 WHERE ProductID = 123;
