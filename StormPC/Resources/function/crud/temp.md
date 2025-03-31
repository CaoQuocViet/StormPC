Tìm tất cả các file model, xử lý, helper liên quan đến @ProductsViewModel.cs , tôi cần bổ sung chức năng: thêm và xóa nhiều laptop (nút trên header), thêm specs, sửa laptop, xóa laptop (trong vùng hover). 
Thêm laptop có thể chọn các caterogy và brand có sẵn từ danh sách.
Sửa cũng tương tự nhưng chỉ được sửa các laptop mà các spec (nếu có) chưa xuất hiện trong 1 đơn order nào.
Xóa thì chỉ cho phép xóa các Laptop mà các specs của nó chưa nằm trong một order nào. Khi xóa product (laptop) thì chỉ cần bật cờ IsDeleted thành true, tất cả các specs liên quan đều bị bật cờ IsDeleted thành true tương ứng. Xóa Hàng loạt thì khó bấm vào sẽ cho phép chọn các tick từ mỗi card muốn xóa.
Thêm specs thì hiện box để thêm spec của laptop đó.


Các chức năng cần thêm dialog: THêm spec, thêm laptop, sửa laptop.
Các file xử lý đã có sẵn, chỉ cần bổ sung hàm.
THam khảo @schema.sql để nắm chắc các mối quan hệ và các trường data.

