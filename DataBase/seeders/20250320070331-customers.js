'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    const now = new Date();
    await queryInterface.bulkInsert('Customers', [
      // Giữ nguyên 10 khách hàng hiện có
      {
        CustomerID: 1,
        FullName: 'Nguyễn Văn Anh',
        Email: 'vananh@email.com',
        Phone: '0901234567',
        Address: '123 Lê Lợi, Phường Bến Nghé, Quận 1',
        CityCode: 'HCM',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        CustomerID: 2,
        FullName: 'Trần Thị Mai',
        Email: 'maitr@email.com',
        Phone: '0912345678',
        Address: '45 Nguyễn Huệ, Phường Bến Nghé, Quận 1',
        CityCode: 'HCM',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        CustomerID: 3,
        FullName: 'Lê Hoàng Nam',
        Email: 'namlh@email.com',
        Phone: '0923456789',
        Address: '67 Trần Hưng Đạo, Phường Cầu Ông Lãnh, Quận 1',
        CityCode: 'HCM',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        CustomerID: 4,
        FullName: 'Phạm Minh Tuấn',
        Email: 'tuanpm@email.com',
        Phone: '0934567890',
        Address: '89 Lý Tự Trọng, Hoàn Kiếm',
        CityCode: 'HAN',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        CustomerID: 5,
        FullName: 'Hoàng Thị Lan',
        Email: 'lanht@email.com',
        Phone: '0945678901',
        Address: '234 Trần Phú, Hải Châu',
        CityCode: 'DAN',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        CustomerID: 6,
        FullName: 'Vũ Đức Minh',
        Email: 'minhvd@email.com',
        Phone: '0956789012',
        Address: '56 Nguyễn Văn Linh, Hải Châu',
        CityCode: 'DAN',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        CustomerID: 7,
        FullName: 'Đặng Thu Hà',
        Email: 'hadt@email.com',
        Phone: '0967890123',
        Address: '78 Phan Chu Trinh, Ninh Kiều',
        CityCode: 'CTO',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        CustomerID: 8,
        FullName: 'Bùi Quang Huy',
        Email: 'huybq@email.com',
        Phone: '0978901234',
        Address: '90 Lê Duẩn, Hải An',
        CityCode: 'HAP',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        CustomerID: 9,
        FullName: 'Ngô Thị Thanh',
        Email: 'thanhnt@email.com',
        Phone: '0989012345',
        Address: '123 Trần Phú, Lộc Thọ',
        CityCode: 'NTH',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        CustomerID: 10,
        FullName: 'Đỗ Văn Hùng',
        Email: 'hungdv@email.com',
        Phone: '0990123456',
        Address: '45 Nguyễn Huệ, Thành phố Huế',
        CityCode: 'HUE',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      
      // Thêm 5 khách hàng mới
      {
        CustomerID: 11,
        FullName: 'Trần Đức Hiếu',
        Email: 'hieutd@email.com',
        Phone: '0912345987',
        Address: '28 Phan Xích Long, Phường 2, Phú Nhuận',
        CityCode: 'HCM',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        CustomerID: 12,
        FullName: 'Nguyễn Thị Kiều Trang',
        Email: 'trangntk@email.com',
        Phone: '0923456987',
        Address: '175 Tây Sơn, Đống Đa',
        CityCode: 'HAN',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        CustomerID: 13,
        FullName: 'Lê Minh Hoàng',
        Email: 'hoanglm@email.com',
        Phone: '0934567891',
        Address: '45 Nguyễn Thị Minh Khai, Hải Châu',
        CityCode: 'DAN',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        CustomerID: 14,
        FullName: 'Phạm Thanh Hương',
        Email: 'huongpt@email.com',
        Phone: '0945678902',
        Address: '101 Lê Hồng Phong, Ngô Quyền',
        CityCode: 'HAP',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      },
      {
        CustomerID: 15,
        FullName: 'Trịnh Văn Công',
        Email: 'congtv@email.com',
        Phone: '0956789198',
        Address: '65 Lê Duẩn, Quận 1',
        CityCode: 'HCM',
        IsDeleted: false,
        createdAt: now,
        updatedAt: now
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('Customers', null, {});
  }
};
