CREATE TABLE "Categories" (
  "CategoryID" integer PRIMARY KEY NOT NULL,
  "CategoryName" varchar(50) NOT NULL,
  "Description" text
);

CREATE TABLE "Brands" (
  "BrandID" integer PRIMARY KEY NOT NULL,
  "BrandName" varchar(100) NOT NULL
);

CREATE TABLE "Laptops" (
  "LaptopID" varchar PRIMARY KEY NOT NULL,
  "BrandID" integer NOT NULL,
  "CategoryID" integer NOT NULL,
  "Picture" varchar,
  "ModelName" varchar(200) NOT NULL,
  "ScreenSize" decimal(3,1),
  "OperatingSystem" varchar(50),
  "ReleaseYear" integer
  "Description" text,
  "Discount" decimal NOT NULL,
  "DiscountEndDate" datetime,
  "DiscountStartDate" datetime
);

CREATE TABLE "LaptopSpecs" (
  "VariantID" varchar PRIMARY KEY NOT NULL,
  "LaptopID" varchar NOT NULL,
  "CPU" varchar(100) NOT NULL,
  "GPU" varchar(100),
  "RAM" integer NOT NULL,
  "Storage" integer NOT NULL,
  "StorageType" varchar(20) NOT NULL,
  "Color" varchar(50),
  "Price" decimal NOT NULL,
  "StockQuantity" integer NOT NULL
);

CREATE TABLE "Customers" (
  "CustomerID" integer PRIMARY KEY NOT NULL,
  "FullName" varchar(100) NOT NULL,
  "Email" varchar(100) NOT NULL,
  "Phone" varchar(20),
  "Address" text NOT NULL,
  "CityCode" varchar(3) NOT NULL
);

CREATE TABLE "Orders" (
  "OrderID" integer PRIMARY KEY NOT NULL,
  "CustomerID" integer NOT NULL,
  "OrderDate" datetime NOT NULL,
  "StatusID" integer NOT NULL,
  "TotalAmount" decimal NOT NULL,
  "PaymentMethodID" integer NOT NULL,
  "ShipCityCode" varchar(3) NOT NULL,
  "ShippingAddress" text NOT NULL,
  "ShippingCity" varchar(50) NOT NULL,
  "ShippingPostalCode" varchar(10)
);

CREATE TABLE "OrderItems" (
  "OrderID" integer NOT NULL,
  "VariantID" varchar NOT NULL,
  "Quantity" integer NOT NULL,
  "UnitPrice" decimal NOT NULL,
  PRIMARY KEY ("OrderID", "VariantID")
);

CREATE TABLE "PaymentMethods" (
  "PaymentMethodID" integer PRIMARY KEY NOT NULL,
  "MethodName" varchar(50) NOT NULL
);

CREATE TABLE "OrderStatuses" (
  "StatusID" integer PRIMARY KEY NOT NULL,
  "StatusName" varchar(50) NOT NULL
);

CREATE TABLE "Cities" (
  "CityCode" varchar(3) PRIMARY KEY NOT NULL,
  "CityName" varchar(100) NOT NULL
);

CREATE TABLE "SoftwareVersion" (
  "Version" varchar PRIMARY KEY NOT NULL
);

ALTER TABLE "Laptops" ADD FOREIGN KEY ("BrandID") REFERENCES "Brands" ("BrandID");

ALTER TABLE "Laptops" ADD FOREIGN KEY ("CategoryID") REFERENCES "Categories" ("CategoryID");

ALTER TABLE "LaptopSpecs" ADD FOREIGN KEY ("LaptopID") REFERENCES "Laptops" ("LaptopID");

ALTER TABLE "Customers" ADD FOREIGN KEY ("CityCode") REFERENCES "Cities" ("CityCode");

ALTER TABLE "Orders" ADD FOREIGN KEY ("CustomerID") REFERENCES "Customers" ("CustomerID");

ALTER TABLE "Orders" ADD FOREIGN KEY ("PaymentMethodID") REFERENCES "PaymentMethods" ("PaymentMethodID");

ALTER TABLE "Orders" ADD FOREIGN KEY ("StatusID") REFERENCES "OrderStatuses" ("StatusID");

ALTER TABLE "Orders" ADD FOREIGN KEY ("ShipCityCode") REFERENCES "Cities" ("CityCode");

ALTER TABLE "OrderItems" ADD FOREIGN KEY ("OrderID") REFERENCES "Orders" ("OrderID");

ALTER TABLE "OrderItems" ADD FOREIGN KEY ("VariantID") REFERENCES "LaptopSpecs" ("VariantID");
