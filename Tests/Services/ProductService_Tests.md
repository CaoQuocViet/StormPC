# Kiểm tra ProductService

## Test Case PS-001: Lấy danh sách laptop
- **Mô tả**: Kiểm tra việc lấy danh sách laptop từ database
- **Tiền điều kiện**: Database có dữ liệu laptop
- **Các bước kiểm tra**:
  1. Gọi phương thức GetAllLaptopsAsync()
- **Kết quả mong đợi**: 
  - Trả về danh sách các laptop không bị đánh dấu xóa (IsDeleted = false)
  - Thông tin laptop chứa đầy đủ dữ liệu liên quan (Brand, Category, Specs)

## Test Case PS-002: Thêm laptop mới
- **Mô tả**: Kiểm tra khả năng thêm laptop mới vào database
- **Các bước kiểm tra**:
  1. Tạo đối tượng Laptop mới với thông tin hợp lệ
  2. Gọi phương thức AddLaptopAsync()
  3. Kiểm tra laptop mới trong database
- **Kết quả mong đợi**: 
  - Laptop được thêm thành công với ID mới
  - Giá trị IsDeleted được đặt là false
  - Các thông tin thời gian (CreatedAt, UpdatedAt) được gán chính xác
  - Giá trị Discount được làm tròn về đơn vị 1000 VNĐ

## Test Case PS-003: Kiểm tra ràng buộc xóa laptop đã có đơn hàng
- **Mô tả**: Kiểm tra việc không cho phép xóa laptop đã có trong đơn hàng
- **Tiền điều kiện**: 
  - Có laptop trong database
  - Laptop có liên kết với ít nhất một đơn hàng
- **Các bước kiểm tra**:
  1. Gọi phương thức CanDeleteLaptopAsync() với ID laptop đã có đơn hàng
  2. Gọi phương thức DeleteLaptopAsync() với ID laptop đã có đơn hàng
- **Kết quả mong đợi**: 
  - CanDeleteLaptopAsync() trả về false
  - DeleteLaptopAsync() không xóa laptop và trả về false
  - Laptop vẫn còn trong database và IsDeleted = false

## Test Case PS-004: Cập nhật thông tin laptop
- **Mô tả**: Kiểm tra khả năng cập nhật thông tin laptop
- **Tiền điều kiện**: Có laptop trong database
- **Các bước kiểm tra**:
  1. Lấy laptop hiện có từ database
  2. Sửa đổi thông tin (tên, thương hiệu, danh mục, giá)
  3. Gọi phương thức EditLaptopAsync()
  4. Truy vấn lại laptop từ database
- **Kết quả mong đợi**: 
  - Thông tin laptop được cập nhật trong database
  - Trường UpdatedAt được cập nhật với thời gian hiện tại
  - Giảm giá được làm tròn về đơn vị 1000 VNĐ nếu có

## Test Case PS-005: Tìm kiếm laptop theo tên mẫu
- **Mô tả**: Kiểm tra chức năng tìm kiếm laptop theo tên mẫu
- **Tiền điều kiện**: Database có nhiều laptop với tên khác nhau
- **Các bước kiểm tra**:
  1. Gọi phương thức SearchLaptopsByModelAsync() với một chuỗi tìm kiếm
- **Kết quả mong đợi**: 
  - Trả về danh sách laptop có tên chứa chuỗi tìm kiếm
  - Tìm kiếm không phân biệt chữ hoa/thường
  - Chỉ trả về laptop chưa bị xóa mềm (IsDeleted = false)
  - Bao gồm cả kết quả tìm kiếm dựa trên tên thương hiệu và danh mục

## Test Case PS-006: Lọc laptop theo khoảng giá
- **Mô tả**: Kiểm tra chức năng lọc laptop theo khoảng giá
- **Tiền điều kiện**: Database có laptop với các mức giá khác nhau
- **Các bước kiểm tra**:
  1. Gọi phương thức FilterLaptopsByPriceRangeAsync() với min và max price
- **Kết quả mong đợi**: 
  - Trả về danh sách laptop có giá nằm trong khoảng đã chọn
  - Chỉ trả về laptop chưa bị xóa mềm (IsDeleted = false)
  - Lọc dựa trên giá của cấu hình rẻ nhất trong laptop

## Test Case PS-007: Lọc laptop theo thương hiệu
- **Mô tả**: Kiểm tra chức năng lọc laptop theo thương hiệu
- **Tiền điều kiện**: Database có laptop từ nhiều thương hiệu khác nhau
- **Các bước kiểm tra**:
  1. Gọi phương thức FilterLaptopsByBrandAsync() với ID thương hiệu
- **Kết quả mong đợi**: 
  - Trả về danh sách laptop thuộc thương hiệu đã chọn
  - Chỉ trả về laptop chưa bị xóa mềm (IsDeleted = false)

## Test Case PS-008: Thêm cấu hình (LaptopSpec) cho laptop
- **Mô tả**: Kiểm tra chức năng thêm cấu hình chi tiết cho laptop
- **Tiền điều kiện**: Có laptop trong database
- **Các bước kiểm tra**:
  1. Tạo đối tượng LaptopSpec mới với thông tin hợp lệ
  2. Gán LaptopID của laptop hiện có
  3. Gọi phương thức AddLaptopSpecAsync()
- **Kết quả mong đợi**: 
  - Cấu hình được thêm vào database và liên kết với laptop
  - SKU được tạo tự động theo format đúng
  - IsDeleted được đặt là false
  - Giá được làm tròn về đơn vị 1000 VNĐ

## Test Case PS-009: Kiểm tra lỗi khi thêm laptop với dữ liệu không hợp lệ
- **Mô tả**: Kiểm tra xử lý lỗi khi thêm laptop với dữ liệu không hợp lệ
- **Các bước kiểm tra**:
  1. Tạo đối tượng Laptop với dữ liệu không hợp lệ (ví dụ: không có tên, giá âm)
  2. Gọi phương thức AddLaptopAsync()
- **Kết quả mong đợi**: 
  - Phương thức trả về false
  - Laptop không được thêm vào database

## Test Case PS-010: Lấy danh sách thương hiệu (Brand)
- **Mô tả**: Kiểm tra việc lấy danh sách thương hiệu từ database
- **Các bước kiểm tra**:
  1. Gọi phương thức GetAllBrandsAsync()
- **Kết quả mong đợi**: 
  - Trả về danh sách các thương hiệu không bị xóa mềm
  - Danh sách được sắp xếp theo thứ tự tên thương hiệu

## Test Case PS-011: Lấy danh sách danh mục (Category)
- **Mô tả**: Kiểm tra việc lấy danh sách danh mục từ database
- **Các bước kiểm tra**:
  1. Gọi phương thức GetAllCategoriesAsync()
- **Kết quả mong đợi**: 
  - Trả về danh sách các danh mục không bị xóa mềm
  - Danh sách được sắp xếp theo thứ tự tên danh mục

## Test Case PS-012: Kiểm tra soft delete cho laptop
- **Mô tả**: Kiểm tra chức năng xóa mềm (soft delete) laptop
- **Tiền điều kiện**: Có laptop không liên kết với đơn hàng trong database
- **Các bước kiểm tra**:
  1. Gọi phương thức DeleteLaptopAsync() với laptop không có ràng buộc
  2. Truy vấn trực tiếp database để kiểm tra cờ IsDeleted
- **Kết quả mong đợi**: 
  - Phương thức trả về true
  - Laptop vẫn tồn tại trong database với IsDeleted = true
  - Tất cả cấu hình của laptop cũng bị đánh dấu IsDeleted = true

## Test Case PS-013: Xóa nhiều laptop cùng lúc
- **Mô tả**: Kiểm tra chức năng xóa hàng loạt laptop
- **Tiền điều kiện**: Có nhiều laptop trong database, một số có thể xóa và một số không thể xóa
- **Các bước kiểm tra**:
  1. Tạo danh sách ID laptop cần xóa (bao gồm cả laptop có đơn hàng và không có đơn hàng)
  2. Gọi phương thức DeleteMultipleLaptopsAsync() với danh sách ID
- **Kết quả mong đợi**: 
  - Phương thức trả về true nếu xóa được ít nhất một laptop
  - Chỉ các laptop không có đơn hàng mới bị đánh dấu IsDeleted = true
  - Các laptop có đơn hàng vẫn giữ nguyên IsDeleted = false
  - Các cấu hình của laptop bị xóa cũng bị đánh dấu IsDeleted = true

## Test Case PS-014: Quản lý tồn kho
- **Mô tả**: Kiểm tra chức năng cập nhật tồn kho
- **Tiền điều kiện**: Có laptop spec với số lượng tồn kho
- **Các bước kiểm tra**:
  1. Gọi phương thức UpdateStockQuantityAsync() với ID spec và số lượng mới
  2. Kiểm tra số lượng tồn kho trong database
- **Kết quả mong đợi**: 
  - Phương thức trả về true
  - Số lượng tồn kho được cập nhật chính xác

## Test Case PS-015: Lấy laptop theo ID
- **Mô tả**: Kiểm tra việc lấy thông tin chi tiết laptop theo ID
- **Tiền điều kiện**: Có laptop trong database
- **Các bước kiểm tra**:
  1. Gọi phương thức GetLaptopByIdAsync() với ID hợp lệ
- **Kết quả mong đợi**: 
  - Trả về thông tin chi tiết của laptop bao gồm Brand, Category và Specs
  - Chỉ trả về laptop chưa bị xóa mềm (IsDeleted = false)

## Test Case PS-016: Xử lý khi không tìm thấy laptop
- **Mô tả**: Kiểm tra xử lý khi truy vấn laptop không tồn tại
- **Các bước kiểm tra**:
  1. Gọi phương thức GetLaptopByIdAsync() với ID không tồn tại
- **Kết quả mong đợi**: 
  - Phương thức trả về null
  - Không xảy ra lỗi

## Test Case PS-017: Lấy laptop spec rẻ nhất
- **Mô tả**: Kiểm tra lấy cấu hình rẻ nhất của laptop
- **Tiền điều kiện**: Có laptop với nhiều cấu hình khác nhau
- **Các bước kiểm tra**:
  1. Gọi phương thức GetCheapestSpecForLaptopAsync() với ID laptop
- **Kết quả mong đợi**: 
  - Trả về cấu hình có giá thấp nhất trong số các cấu hình của laptop

## Test Case PS-018: Lấy số lượng biến thể của laptop
- **Mô tả**: Kiểm tra việc đếm số lượng biến thể (spec) của laptop
- **Tiền điều kiện**: Có laptop với nhiều cấu hình khác nhau
- **Các bước kiểm tra**:
  1. Gọi phương thức GetVariantsCountAsync() với ID laptop
- **Kết quả mong đợi**: 
  - Trả về đúng số lượng cấu hình chưa bị xóa mềm của laptop

## Test Case PS-019: Lọc laptop theo màu sắc
- **Mô tả**: Kiểm tra chức năng lọc laptop theo màu sắc
- **Tiền điều kiện**: Database có laptop với spec có các màu sắc khác nhau
- **Các bước kiểm tra**:
  1. Gọi phương thức FilterLaptopsByColorAsync() với một màu sắc
- **Kết quả mong đợi**: 
  - Trả về danh sách laptop có ít nhất một cấu hình với màu sắc đã chọn
  - Chỉ trả về laptop chưa bị xóa mềm (IsDeleted = false)

## Test Case PS-020: Lấy danh sách màu sắc
- **Mô tả**: Kiểm tra việc lấy danh sách các màu sắc có sẵn
- **Tiền điều kiện**: Database có laptop spec với các màu sắc khác nhau
- **Các bước kiểm tra**:
  1. Gọi phương thức GetAvailableColorsAsync()
- **Kết quả mong đợi**: 
  - Trả về danh sách các màu sắc duy nhất từ cấu hình laptop
  - Không bao gồm giá trị null hoặc rỗng
  - Chỉ lấy màu từ những cấu hình chưa bị xóa mềm

## Test Case PS-021: Cập nhật hàng loạt giảm giá
- **Mô tả**: Kiểm tra chức năng cập nhật giảm giá cho nhiều laptop cùng lúc
- **Tiền điều kiện**: Có nhiều laptop trong database
- **Các bước kiểm tra**:
  1. Tạo danh sách ID laptop cần cập nhật
  2. Gọi phương thức BatchUpdateLaptopsAsync() với danh sách ID và giá trị giảm giá mới
- **Kết quả mong đợi**: 
  - Phương thức trả về true
  - Tất cả laptop trong danh sách được cập nhật với giá trị giảm giá mới
  - Trường UpdatedAt được cập nhật với thời gian hiện tại

## Test Case PS-022: Kiểm tra ràng buộc khi chỉnh sửa laptop đã có đơn hàng
- **Mô tả**: Kiểm tra việc không cho phép chỉnh sửa laptop đã có trong đơn hàng
- **Tiền điều kiện**: 
  - Có laptop trong database
  - Laptop có liên kết với ít nhất một đơn hàng
- **Các bước kiểm tra**:
  1. Gọi phương thức CanEditLaptopAsync() với ID laptop đã có đơn hàng
- **Kết quả mong đợi**: 
  - Phương thức trả về false

## Test Case PS-023: Xử lý ngoại lệ trong các phương thức
- **Mô tả**: Kiểm tra xử lý ngoại lệ khi có lỗi trong các phương thức
- **Các bước kiểm tra**:
  1. Gọi phương thức với đối số không hợp lệ (null, giá trị âm, v.v.)
- **Kết quả mong đợi**: 
  - Phương thức xử lý ngoại lệ an toàn, không làm crash ứng dụng
  - Trả về giá trị mặc định phù hợp (false, null, empty list)