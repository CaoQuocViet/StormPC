npm init -y
npm install sequelize pg pg-hstore
npm install --save-dev sequelize-cli

===============================================

npx sequelize-cli init

===============================================

# Categories
npx sequelize-cli model:generate --name Category --attributes CategoryID:integer,CategoryName:string,Description:text

# Brands
npx sequelize-cli model:generate --name Brand --attributes BrandID:integer,BrandName:string

# Laptops
npx sequelize-cli model:generate --name Laptop --attributes LaptopID:string,BrandID:integer,CategoryID:integer,Picture:string,ModelName:string,ScreenSize:decimal,OperatingSystem:string,ReleaseYear:integer

# LaptopSpecs
npx sequelize-cli model:generate --name LaptopSpec --attributes VariantID:string,LaptopID:string,CPU:string,GPU:string,RAM:integer,Storage:integer,StorageType:string,Color:string,Price:decimal,StockQuantity:integer

# Cities
npx sequelize-cli model:generate --name City --attributes CityCode:string,CityName:string

# Customers
npx sequelize-cli model:generate --name Customer --attributes CustomerID:integer,FullName:string,Email:string,Phone:string,Address:text,CityCode:string

# PaymentMethods
npx sequelize-cli model:generate --name PaymentMethod --attributes PaymentMethodID:integer,MethodName:string

# OrderStatuses
npx sequelize-cli model:generate --name OrderStatus --attributes StatusID:integer,StatusName:string

# Orders
npx sequelize-cli model:generate --name Order --attributes OrderID:integer,CustomerID:integer,OrderDate:date,StatusID:integer,TotalAmount:decimal,PaymentMethodID:integer,ShipCityCode:string,ShippingAddress:text,ShippingCity:string,ShippingPostalCode:string

# OrderItems
npx sequelize-cli model:generate --name OrderItem --attributes OrderID:integer,VariantID:string,Quantity:integer,UnitPrice:decimal

# SoftwareVersion
npx sequelize-cli model:generate --name SoftwareVersion --attributes Version:string

===============================================

npx sequelize-cli seed:generate --name categories
npx sequelize-cli seed:generate --name brands
npx sequelize-cli seed:generate --name laptops
npx sequelize-cli seed:generate --name laptop-specs
npx sequelize-cli seed:generate --name cities
npx sequelize-cli seed:generate --name payment-methods
npx sequelize-cli seed:generate --name order-statuses
npx sequelize-cli seed:generate --name customers
npx sequelize-cli seed:generate --name orders
npx sequelize-cli seed:generate --name order-items
npx sequelize-cli seed:generate --name software-versions

===============================================

# Tạo database
npx sequelize-cli db:create

# Xóa database
npx sequelize-cli db:drop

===============================================

# Chạy tất cả migration
npx sequelize-cli db:migrate

# Rollback migration gần nhất
npx sequelize-cli db:migrate:undo

# Rollback tất cả migration
npx sequelize-cli db:migrate:undo:all

# Rollback đến một migration cụ thể
npx sequelize-cli db:migrate:undo:all --to XXXXXXXXXXXXXX-create-posts.js

===============================================

# Chạy tất cả seeder
npx sequelize-cli db:seed:all

# Undo seeder gần nhất
npx sequelize-cli db:seed:undo

# Undo một seeder cụ thể
npx sequelize-cli db:seed:undo --seed name-of-seed-as-in-data

# Undo tất cả seeder
npx sequelize-cli db:seed:undo:all


===============================================

# Kết nối DB thông qua terminal
psql postgresql://vietcq:123456789000@localhost:5444/stormpc_db

# dump database
pg_dump -U vietcq -h localhost -p 5444 -d stormpc_db -s > schema.sql

===============================================

# Thường dùng khi debug - khởi động lại trạng thái data ban đầu, thêm trường mới vào header line
# Thứ duy nhất cần làm là copy đống này dán 1 phát vào terminal
npx sequelize-cli db:drop
npx sequelize-cli db:create
npx sequelize-cli db:migrate
npx sequelize-cli db:seed:all