'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    const now = new Date();
    await queryInterface.bulkInsert('Orders', [
      // Giữ nguyên 10 đơn hàng hiện có
      {
        OrderID: 1,
        CustomerID: 1,
        OrderDate: '2024-03-15 09:30:00',
        StatusID: 4,
        TotalAmount: 390000000, // 138000000 + 108000000 + 144000000
        PaymentMethodID: 2,
        ShipCityCode: 'HCM',
        ShippingAddress: '123 Lê Lợi, Phường Bến Nghé, Quận 1',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 2,
        CustomerID: 2,
        OrderDate: '2024-03-16 14:15:00',
        StatusID: 3,
        TotalAmount: 579000000, // 228000000 + 153000000 + 2*63000000 + 72000000
        PaymentMethodID: 1,
        ShipCityCode: 'HCM',
        ShippingAddress: '45 Nguyễn Huệ, Phường Bến Nghé, Quận 1',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 3,
        CustomerID: 4,
        OrderDate: '2024-03-17 11:20:00',
        StatusID: 2,
        TotalAmount: 866000000, // 326000000 + 243000000 + 2*67500000 + 81000000 + 81000000
        PaymentMethodID: 3,
        ShipCityCode: 'HAN',
        ShippingAddress: '89 Lý Tự Trọng, Hoàn Kiếm',
        ShippingCity: 'Hà Nội',
        ShippingPostalCode: '100000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 4,
        CustomerID: 5,
        OrderDate: '2024-03-18 16:45:00',
        StatusID: 1,
        TotalAmount: 382999988, // 97000000 + 69999988 + 126000000 + 90000000
        PaymentMethodID: 7,
        ShipCityCode: 'DAN',
        ShippingAddress: '234 Trần Phú, Hải Châu',
        ShippingCity: 'Đà Nẵng',
        ShippingPostalCode: '550000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 5,
        CustomerID: 7,
        OrderDate: '2024-03-19 10:00:00',
        StatusID: 5,
        TotalAmount: 1187800000, // 467800000 + 396000000 + 324000000
        PaymentMethodID: 5,
        ShipCityCode: 'CTO',
        ShippingAddress: '78 Phan Chu Trinh, Ninh Kiều',
        ShippingCity: 'Cần Thơ',
        ShippingPostalCode: '900000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 6,
        CustomerID: 8,
        OrderDate: '2024-03-19 13:30:00',
        StatusID: 2,
        TotalAmount: 484600000, // 138200000 + 58400000 + 189000000 + 99000000
        PaymentMethodID: 4,
        ShipCityCode: 'HAP',
        ShippingAddress: '90 Lê Duẩn, Hải An',
        ShippingCity: 'Hải Phòng',
        ShippingPostalCode: '180000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 7,
        CustomerID: 3,
        OrderDate: '2024-03-20 09:15:00',
        StatusID: 1,
        TotalAmount: 689000000, // 158000000 + 162000000 + 171000000 + 117000000 + 81000000
        PaymentMethodID: 8,
        ShipCityCode: 'HCM',
        ShippingAddress: '67 Trần Hưng Đạo, Phường Cầu Ông Lãnh, Quận 1',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 8,
        CustomerID: 9,
        OrderDate: '2024-03-20 11:45:00',
        StatusID: 2,
        TotalAmount: 512000000, // 215000000 + 162000000 + 135000000
        PaymentMethodID: 6,
        ShipCityCode: 'NTH',
        ShippingAddress: '123 Trần Phú, Lộc Thọ',
        ShippingCity: 'Nha Trang',
        ShippingPostalCode: '650000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 9,
        CustomerID: 10,
        OrderDate: '2024-03-20 14:20:00',
        StatusID: 1,
        TotalAmount: 398200000, // 146200000 + 108000000 + 72000000 + 72000000
        PaymentMethodID: 2,
        ShipCityCode: 'HUE',
        ShippingAddress: '45 Nguyễn Huệ, Thành phố Huế',
        ShippingCity: 'Huế',
        ShippingPostalCode: '530000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 10,
        CustomerID: 6,
        OrderDate: '2024-03-20 15:50:00',
        StatusID: 3,
        TotalAmount: 862000000, // 259000000 + 324000000 + 126000000 + 153000000
        PaymentMethodID: 1,
        ShipCityCode: 'DAN',
        ShippingAddress: '56 Nguyễn Văn Linh, Hải Châu',
        ShippingCity: 'Đà Nẵng',
        ShippingPostalCode: '550000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      
      // Thêm 7 đơn hàng mới
      {
        OrderID: 11,
        CustomerID: 11,
        OrderDate: '2024-03-21 10:15:00',
        StatusID: 1,
        TotalAmount: 639000000, // 108000000 + 144000000 + 189000000 + 198000000
        PaymentMethodID: 2,
        ShipCityCode: 'HCM',
        ShippingAddress: '28 Phan Xích Long, Phường 2, Phú Nhuận',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 12,
        CustomerID: 12,
        OrderDate: '2024-03-21 14:30:00',
        StatusID: 2,
        TotalAmount: 558000000, // 171000000 + 216000000 + 171000000
        PaymentMethodID: 3,
        ShipCityCode: 'HAN',
        ShippingAddress: '175 Tây Sơn, Đống Đa',
        ShippingCity: 'Hà Nội',
        ShippingPostalCode: '100000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 13,
        CustomerID: 13,
        OrderDate: '2024-03-22 09:45:00',
        StatusID: 1,
        TotalAmount: 576000000, // 261000000 + 117000000 + 198000000
        PaymentMethodID: 1,
        ShipCityCode: 'DAN',
        ShippingAddress: '45 Nguyễn Thị Minh Khai, Hải Châu',
        ShippingCity: 'Đà Nẵng',
        ShippingPostalCode: '550000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 14,
        CustomerID: 14,
        OrderDate: '2024-03-22 15:20:00',
        StatusID: 3,
        TotalAmount: 846000000, // 225000000 + 288000000 + 117000000 + 216000000
        PaymentMethodID: 4,
        ShipCityCode: 'HAP',
        ShippingAddress: '101 Lê Hồng Phong, Ngô Quyền',
        ShippingCity: 'Hải Phòng',
        ShippingPostalCode: '180000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 15,
        CustomerID: 15,
        OrderDate: '2024-03-23 11:30:00',
        StatusID: 2,
        TotalAmount: 387000000, // 108000000 + 117000000 + 162000000
        PaymentMethodID: 2,
        ShipCityCode: 'HCM',
        ShippingAddress: '65 Lê Duẩn, Quận 1',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 16,
        CustomerID: 11,
        OrderDate: '2024-03-24 10:45:00',
        StatusID: 1,
        TotalAmount: 405000000, // 2*126000000 + 153000000
        PaymentMethodID: 5,
        ShipCityCode: 'HCM',
        ShippingAddress: '28 Phan Xích Long, Phường 2, Phú Nhuận',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 17,
        CustomerID: 12,
        OrderDate: '2024-03-25 09:15:00',
        StatusID: 2,
        TotalAmount: 567000000, // 135000000 + 153000000 + 279000000
        PaymentMethodID: 1,
        ShipCityCode: 'HAN',
        ShippingAddress: '175 Tây Sơn, Đống Đa',
        ShippingCity: 'Hà Nội',
        ShippingPostalCode: '100000',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('Orders', null, {});
  }
};
