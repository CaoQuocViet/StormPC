'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    await queryInterface.bulkInsert('Customers', [
      {
        CustomerID: 1,
        FullName: 'Nguyễn Văn Anh',
        Email: 'vananh@email.com',
        Phone: '0901234567',
        Address: '123 Lê Lợi, Phường Bến Nghé, Quận 1',
        CityCode: 1,  // HCM
        IsDeleted: false,
        createdAt: new Date('2025-01-15T09:30:00Z'),
        updatedAt: new Date('2025-01-15T09:30:00Z')
      },
      {
        CustomerID: 2,
        FullName: 'Trần Thị Mai',
        Email: 'maitr@email.com',
        Phone: '0912345678',
        Address: '45 Nguyễn Huệ, Phường Bến Nghé, Quận 1',
        CityCode: 1,  // HCM
        IsDeleted: false,
        createdAt: new Date('2025-01-15T14:15:00Z'),
        updatedAt: new Date('2025-01-15T14:15:00Z')
      },
      {
        CustomerID: 3,
        FullName: 'Lê Hoàng Nam',
        Email: 'namlh@email.com',
        Phone: '0923456789',
        Address: '67 Trần Hưng Đạo, Phường Cầu Ông Lãnh, Quận 1',
        CityCode: 1,  // HCM
        IsDeleted: false,
        createdAt: new Date('2025-01-16T10:20:00Z'),
        updatedAt: new Date('2025-01-16T10:20:00Z')
      },
      {
        CustomerID: 4,
        FullName: 'Phạm Minh Tuấn',
        Email: 'tuanpm@email.com',
        Phone: '0934567890',
        Address: '89 Lý Tự Trọng, Hoàn Kiếm',
        CityCode: 2,  // HAN
        IsDeleted: false,
        createdAt: new Date('2025-01-17T11:45:00Z'),
        updatedAt: new Date('2025-01-17T11:45:00Z')
      },
      {
        CustomerID: 5,
        FullName: 'Hoàng Thị Lan',
        Email: 'lanht@email.com',
        Phone: '0945678901',
        Address: '234 Trần Phú, Hải Châu',
        CityCode: 3,  // DAN
        IsDeleted: false,
        createdAt: new Date('2025-01-18T13:30:00Z'),
        updatedAt: new Date('2025-01-18T13:30:00Z')
      },
      {
        CustomerID: 6,
        FullName: 'Vũ Đức Minh',
        Email: 'minhvd@email.com',
        Phone: '0956789012',
        Address: '56 Nguyễn Văn Linh, Hải Châu',
        CityCode: 3,  // DAN
        IsDeleted: false,
        createdAt: new Date('2025-01-19T10:15:00Z'),
        updatedAt: new Date('2025-01-19T10:15:00Z')
      },
      {
        CustomerID: 7,
        FullName: 'Đặng Thu Hà',
        Email: 'hadt@email.com',
        Phone: '0967890123',
        Address: '78 Phan Chu Trinh, Ninh Kiều',
        CityCode: 4,  // CTO
        IsDeleted: false,
        createdAt: new Date('2025-01-20T14:45:00Z'),
        updatedAt: new Date('2025-01-20T14:45:00Z')
      },
      {
        CustomerID: 8,
        FullName: 'Bùi Quang Huy',
        Email: 'huybq@email.com',
        Phone: '0978901234',
        Address: '90 Lê Duẩn, Hải An',
        CityCode: 5,  // HAP
        IsDeleted: false,
        createdAt: new Date('2025-01-21T11:30:00Z'),
        updatedAt: new Date('2025-01-21T11:30:00Z')
      },
      {
        CustomerID: 9,
        FullName: 'Ngô Thị Thanh',
        Email: 'thanhnt@email.com',
        Phone: '0989012345',
        Address: '123 Trần Phú, Lộc Thọ',
        CityCode: 6,  // NTH
        IsDeleted: false,
        createdAt: new Date('2025-01-22T09:20:00Z'),
        updatedAt: new Date('2025-01-22T09:20:00Z')
      },
      {
        CustomerID: 10,
        FullName: 'Đỗ Văn Hùng',
        Email: 'hungdv@email.com',
        Phone: '0990123456',
        Address: '45 Nguyễn Huệ, Thành phố Huế',
        CityCode: 8,  // HUE
        IsDeleted: false,
        createdAt: new Date('2025-01-23T15:10:00Z'),
        updatedAt: new Date('2025-01-23T15:10:00Z')
      },
      {
        CustomerID: 11,
        FullName: 'Trần Đức Hiếu',
        Email: 'hieutd@email.com',
        Phone: '0912345987',
        Address: '28 Phan Xích Long, Phường 2, Phú Nhuận',
        CityCode: 1,  // HCM
        IsDeleted: false,
        createdAt: new Date('2025-01-24T08:40:00Z'),
        updatedAt: new Date('2025-01-24T08:40:00Z')
      },
      {
        CustomerID: 12,
        FullName: 'Nguyễn Thị Kiều Trang',
        Email: 'trangntk@email.com',
        Phone: '0923456987',
        Address: '175 Tây Sơn, Đống Đa',
        CityCode: 2,  // HAN
        IsDeleted: false,
        createdAt: new Date('2025-01-25T13:15:00Z'),
        updatedAt: new Date('2025-01-25T13:15:00Z')
      },
      {
        CustomerID: 13,
        FullName: 'Lê Minh Hoàng',
        Email: 'hoanglm@email.com',
        Phone: '0934567891',
        Address: '45 Nguyễn Thị Minh Khai, Hải Châu',
        CityCode: 3,  // DAN
        IsDeleted: false,
        createdAt: new Date('2025-01-26T11:30:00Z'),
        updatedAt: new Date('2025-01-26T11:30:00Z')
      },
      {
        CustomerID: 14,
        FullName: 'Phạm Thanh Hương',
        Email: 'huongpt@email.com',
        Phone: '0945678902',
        Address: '101 Lê Hồng Phong, Ngô Quyền',
        CityCode: 5,  // HAP
        IsDeleted: false,
        createdAt: new Date('2025-01-27T10:20:00Z'),
        updatedAt: new Date('2025-01-27T10:20:00Z')
      },
      {
        CustomerID: 15,
        FullName: 'Trịnh Văn Công',
        Email: 'congtv@email.com',
        Phone: '0956789198',
        Address: '65 Lê Duẩn, Quận 1',
        CityCode: 1,  // HCM
        IsDeleted: false,
        createdAt: new Date('2025-01-28T14:40:00Z'),
        updatedAt: new Date('2025-01-28T14:40:00Z')
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('Customers', null, {});
  }
};
