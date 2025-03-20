'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    const now = new Date();
    await queryInterface.bulkInsert('Orders', [
      {
        OrderID: 1,
        CustomerID: 1,
        OrderDate: '2024-03-15 09:30:00',
        StatusID: 4,
        TotalAmount: 1599.99,
        PaymentMethodID: 2,
        ShipCityCode: 'HCM',
        ShippingAddress: '123 Lê Lợi, Phường Bến Nghé, Quận 1',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 2,
        CustomerID: 2,
        OrderDate: '2024-03-16 14:15:00',
        StatusID: 3,
        TotalAmount: 2499.99,
        PaymentMethodID: 1,
        ShipCityCode: 'HCM',
        ShippingAddress: '45 Nguyễn Huệ, Phường Bến Nghé, Quận 1',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 3,
        CustomerID: 4,
        OrderDate: '2024-03-17 11:20:00',
        StatusID: 2,
        TotalAmount: 3499.99,
        PaymentMethodID: 3,
        ShipCityCode: 'HAN',
        ShippingAddress: '89 Lý Tự Trọng, Hoàn Kiếm',
        ShippingCity: 'Hà Nội',
        ShippingPostalCode: '100000',
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 4,
        CustomerID: 5,
        OrderDate: '2024-03-18 16:45:00',
        StatusID: 1,
        TotalAmount: 1299.99,
        PaymentMethodID: 7,
        ShipCityCode: 'DAN',
        ShippingAddress: '234 Trần Phú, Hải Châu',
        ShippingCity: 'Đà Nẵng',
        ShippingPostalCode: '550000',
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 5,
        CustomerID: 7,
        OrderDate: '2024-03-19 10:00:00',
        StatusID: 5,
        TotalAmount: 4999.99,
        PaymentMethodID: 5,
        ShipCityCode: 'CTO',
        ShippingAddress: '78 Phan Chu Trinh, Ninh Kiều',
        ShippingCity: 'Cần Thơ',
        ShippingPostalCode: '900000',
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 6,
        CustomerID: 8,
        OrderDate: '2024-03-19 13:30:00',
        StatusID: 2,
        TotalAmount: 2199.99,
        PaymentMethodID: 4,
        ShipCityCode: 'HAP',
        ShippingAddress: '90 Lê Duẩn, Hải An',
        ShippingCity: 'Hải Phòng',
        ShippingPostalCode: '180000',
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 7,
        CustomerID: 3,
        OrderDate: '2024-03-20 09:15:00',
        StatusID: 1,
        TotalAmount: 1799.99,
        PaymentMethodID: 8,
        ShipCityCode: 'HCM',
        ShippingAddress: '67 Trần Hưng Đạo, Phường Cầu Ông Lãnh, Quận 1',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 8,
        CustomerID: 9,
        OrderDate: '2024-03-20 11:45:00',
        StatusID: 2,
        TotalAmount: 2399.99,
        PaymentMethodID: 6,
        ShipCityCode: 'NTH',
        ShippingAddress: '123 Trần Phú, Lộc Thọ',
        ShippingCity: 'Nha Trang',
        ShippingPostalCode: '650000',
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 9,
        CustomerID: 10,
        OrderDate: '2024-03-20 14:20:00',
        StatusID: 1,
        TotalAmount: 1699.99,
        PaymentMethodID: 2,
        ShipCityCode: 'HUE',
        ShippingAddress: '45 Nguyễn Huệ, Thành phố Huế',
        ShippingCity: 'Huế',
        ShippingPostalCode: '530000',
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 10,
        CustomerID: 6,
        OrderDate: '2024-03-20 15:50:00',
        StatusID: 3,
        TotalAmount: 2899.99,
        PaymentMethodID: 1,
        ShipCityCode: 'DAN',
        ShippingAddress: '56 Nguyễn Văn Linh, Hải Châu',
        ShippingCity: 'Đà Nẵng',
        ShippingPostalCode: '550000',
        createdAt: now,
        updatedAt: now
      },
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('Orders', null, {});
  }
};
