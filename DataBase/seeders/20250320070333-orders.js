'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    await queryInterface.bulkInsert('Orders', [
      {
        OrderID: 1,
        CustomerID: 1,
        OrderDate: '2025-02-01T09:30:00Z',
        StatusID: 4,
        TotalAmount: 390000000,
        PaymentMethodID: 2,
        ShipCityID: 1,  // HCM
        ShippingAddress: '123 Lê Lợi, Phường Bến Nghé, Quận 1',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: new Date('2025-02-01T09:30:00Z'),
        updatedAt: new Date('2025-02-01T09:30:00Z')
      },
      {
        OrderID: 2,
        CustomerID: 2,
        OrderDate: '2025-02-03T14:15:00Z',
        StatusID: 3,
        TotalAmount: 579000000,
        PaymentMethodID: 1,
        ShipCityID: 1,  // HCM
        ShippingAddress: '45 Nguyễn Huệ, Phường Bến Nghé, Quận 1',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: new Date('2025-02-03T14:15:00Z'),
        updatedAt: new Date('2025-02-03T14:15:00Z')
      },
      {
        OrderID: 3,
        CustomerID: 4,
        OrderDate: '2025-02-05T11:20:00Z',
        StatusID: 2,
        TotalAmount: 866000000,
        PaymentMethodID: 3,
        ShipCityID: 2,  // HAN
        ShippingAddress: '89 Lý Tự Trọng, Hoàn Kiếm',
        ShippingCity: 'Hà Nội',
        ShippingPostalCode: '100000',
        IsDeleted: false,
        createdAt: new Date('2025-02-05T11:20:00Z'),
        updatedAt: new Date('2025-02-05T11:20:00Z')
      },
      {
        OrderID: 4,
        CustomerID: 5,
        OrderDate: '2025-02-07T16:45:00Z',
        StatusID: 1,
        TotalAmount: 382999988,
        PaymentMethodID: 7,
        ShipCityID: 3,  // DAN
        ShippingAddress: '234 Trần Phú, Hải Châu',
        ShippingCity: 'Đà Nẵng',
        ShippingPostalCode: '550000',
        IsDeleted: false,
        createdAt: new Date('2025-02-07T16:45:00Z'),
        updatedAt: new Date('2025-02-07T16:45:00Z')
      },
      {
        OrderID: 5,
        CustomerID: 7,
        OrderDate: '2025-02-10T10:00:00Z',
        StatusID: 5,
        TotalAmount: 1187800000,
        PaymentMethodID: 5,
        ShipCityID: 4,  // CTO
        ShippingAddress: '78 Phan Chu Trinh, Ninh Kiều',
        ShippingCity: 'Cần Thơ',
        ShippingPostalCode: '900000',
        IsDeleted: false,
        createdAt: new Date('2025-02-10T10:00:00Z'),
        updatedAt: new Date('2025-02-10T10:00:00Z')
      },
      {
        OrderID: 6,
        CustomerID: 8,
        OrderDate: '2025-02-12T13:30:00Z',
        StatusID: 2,
        TotalAmount: 484600000,
        PaymentMethodID: 4,
        ShipCityID: 5,  // HAP
        ShippingAddress: '90 Lê Duẩn, Hải An',
        ShippingCity: 'Hải Phòng',
        ShippingPostalCode: '180000',
        IsDeleted: false,
        createdAt: new Date('2025-02-12T13:30:00Z'),
        updatedAt: new Date('2025-02-12T13:30:00Z')
      },
      {
        OrderID: 7,
        CustomerID: 3,
        OrderDate: '2025-02-15T09:15:00Z',
        StatusID: 1,
        TotalAmount: 689000000,
        PaymentMethodID: 8,
        ShipCityID: 1,  // HCM
        ShippingAddress: '67 Trần Hưng Đạo, Phường Cầu Ông Lãnh, Quận 1',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: new Date('2025-02-15T09:15:00Z'),
        updatedAt: new Date('2025-02-15T09:15:00Z')
      },
      {
        OrderID: 8,
        CustomerID: 9,
        OrderDate: '2025-02-17T11:45:00Z',
        StatusID: 2,
        TotalAmount: 512000000,
        PaymentMethodID: 6,
        ShipCityID: 6,  // NTH
        ShippingAddress: '123 Trần Phú, Lộc Thọ',
        ShippingCity: 'Nha Trang',
        ShippingPostalCode: '650000',
        IsDeleted: false,
        createdAt: new Date('2025-02-17T11:45:00Z'),
        updatedAt: new Date('2025-02-17T11:45:00Z')
      },
      {
        OrderID: 9,
        CustomerID: 10,
        OrderDate: '2025-02-20T14:20:00Z',
        StatusID: 1,
        TotalAmount: 398200000,
        PaymentMethodID: 2,
        ShipCityID: 8,  // HUE
        ShippingAddress: '45 Nguyễn Huệ, Thành phố Huế',
        ShippingCity: 'Huế',
        ShippingPostalCode: '530000',
        IsDeleted: false,
        createdAt: new Date('2025-02-20T14:20:00Z'),
        updatedAt: new Date('2025-02-20T14:20:00Z')
      },
      {
        OrderID: 10,
        CustomerID: 6,
        OrderDate: '2025-02-22T15:50:00Z',
        StatusID: 3,
        TotalAmount: 862000000,
        PaymentMethodID: 1,
        ShipCityID: 3,  // DAN
        ShippingAddress: '56 Nguyễn Văn Linh, Hải Châu',
        ShippingCity: 'Đà Nẵng',
        ShippingPostalCode: '550000',
        IsDeleted: false,
        createdAt: new Date('2025-02-22T15:50:00Z'),
        updatedAt: new Date('2025-02-22T15:50:00Z')
      },
      {
        OrderID: 11,
        CustomerID: 11,
        OrderDate: '2025-02-25T10:15:00Z',
        StatusID: 1,
        TotalAmount: 639000000,
        PaymentMethodID: 2,
        ShipCityID: 1,  // HCM
        ShippingAddress: '28 Phan Xích Long, Phường 2, Phú Nhuận',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: new Date('2025-02-25T10:15:00Z'),
        updatedAt: new Date('2025-02-25T10:15:00Z')
      },
      {
        OrderID: 12,
        CustomerID: 12,
        OrderDate: '2025-03-01T14:30:00Z',
        StatusID: 2,
        TotalAmount: 558000000,
        PaymentMethodID: 3,
        ShipCityID: 2,  // HAN
        ShippingAddress: '175 Tây Sơn, Đống Đa',
        ShippingCity: 'Hà Nội',
        ShippingPostalCode: '100000',
        IsDeleted: false,
        createdAt: new Date('2025-03-01T14:30:00Z'),
        updatedAt: new Date('2025-03-01T14:30:00Z')
      },
      {
        OrderID: 13,
        CustomerID: 13,
        OrderDate: '2025-03-05T09:45:00Z',
        StatusID: 1,
        TotalAmount: 576000000,
        PaymentMethodID: 1,
        ShipCityID: 3,  // DAN
        ShippingAddress: '45 Nguyễn Thị Minh Khai, Hải Châu',
        ShippingCity: 'Đà Nẵng',
        ShippingPostalCode: '550000',
        IsDeleted: false,
        createdAt: new Date('2025-03-05T09:45:00Z'),
        updatedAt: new Date('2025-03-05T09:45:00Z')
      },
      {
        OrderID: 14,
        CustomerID: 14,
        OrderDate: '2025-03-10T15:20:00Z',
        StatusID: 3,
        TotalAmount: 846000000,
        PaymentMethodID: 4,
        ShipCityID: 5,  // HAP
        ShippingAddress: '101 Lê Hồng Phong, Ngô Quyền',
        ShippingCity: 'Hải Phòng',
        ShippingPostalCode: '180000',
        IsDeleted: false,
        createdAt: new Date('2025-03-10T15:20:00Z'),
        updatedAt: new Date('2025-03-10T15:20:00Z')
      },
      {
        OrderID: 15,
        CustomerID: 15,
        OrderDate: '2025-03-15T11:30:00Z',
        StatusID: 2,
        TotalAmount: 387000000,
        PaymentMethodID: 2,
        ShipCityID: 1,  // HCM
        ShippingAddress: '65 Lê Duẩn, Quận 1',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: new Date('2025-03-15T11:30:00Z'),
        updatedAt: new Date('2025-03-15T11:30:00Z')
      },
      {
        OrderID: 16,
        CustomerID: 11,
        OrderDate: '2025-03-18T10:45:00Z',
        StatusID: 1,
        TotalAmount: 405000000,
        PaymentMethodID: 5,
        ShipCityID: 1,  // HCM
        ShippingAddress: '28 Phan Xích Long, Phường 2, Phú Nhuận',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: new Date('2025-03-18T10:45:00Z'),
        updatedAt: new Date('2025-03-18T10:45:00Z')
      },
      {
        OrderID: 17,
        CustomerID: 12,
        OrderDate: '2025-03-22T09:15:00Z',
        StatusID: 2,
        TotalAmount: 567000000,
        PaymentMethodID: 1,
        ShipCityID: 2,  // HAN
        ShippingAddress: '175 Tây Sơn, Đống Đa',
        ShippingCity: 'Hà Nội',
        ShippingPostalCode: '100000',
        IsDeleted: false,
        createdAt: new Date('2025-03-22T09:15:00Z'),
        updatedAt: new Date('2025-03-22T09:15:00Z')
      },
      {
        OrderID: 18,
        CustomerID: 1,
        OrderDate: '2025-03-23T09:30:00Z',
        StatusID: 1,
        TotalAmount: 495000000,
        PaymentMethodID: 5,
        ShipCityID: 1,  // HCM
        ShippingAddress: '123 Lê Lợi, Phường Bến Nghé, Quận 1',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: new Date('2025-03-23T09:30:00Z'),
        updatedAt: new Date('2025-03-23T09:30:00Z')
      },
      {
        OrderID: 19,
        CustomerID: 3,
        OrderDate: '2025-03-24T14:15:00Z',
        StatusID: 2,
        TotalAmount: 570000000,
        PaymentMethodID: 1,
        ShipCityID: 1,  // HCM
        ShippingAddress: '67 Trần Hưng Đạo, Phường Cầu Ông Lãnh, Quận 1',
        ShippingCity: 'Hồ Chí Minh',
        ShippingPostalCode: '700000',
        IsDeleted: false,
        createdAt: new Date('2025-03-24T14:15:00Z'),
        updatedAt: new Date('2025-03-24T14:15:00Z')
      },
      {
        OrderID: 20,
        CustomerID: 4,
        OrderDate: '2025-03-25T10:20:00Z',
        StatusID: 3,
        TotalAmount: 670000000,
        PaymentMethodID: 2,
        ShipCityID: 2,  // HAN
        ShippingAddress: '89 Lý Tự Trọng, Hoàn Kiếm',
        ShippingCity: 'Hà Nội',
        ShippingPostalCode: '100000',
        IsDeleted: false,
        createdAt: new Date('2025-03-25T10:20:00Z'),
        updatedAt: new Date('2025-03-25T10:20:00Z')
      },
      {
        OrderID: 21,
        CustomerID: 13,
        OrderDate: '2025-03-27T16:45:00Z',
        StatusID: 1,
        TotalAmount: 1520000000,
        PaymentMethodID: 3,
        ShipCityID: 3,  // DAN
        ShippingAddress: '45 Nguyễn Thị Minh Khai, Hải Châu',
        ShippingCity: 'Đà Nẵng',
        ShippingPostalCode: '550000',
        IsDeleted: false,
        createdAt: new Date('2025-03-27T16:45:00Z'),
        updatedAt: new Date('2025-03-27T16:45:00Z')
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('Orders', null, {});
  }
};
