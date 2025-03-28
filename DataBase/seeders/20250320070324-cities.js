'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    await queryInterface.bulkInsert('Cities', [
      {
        CityCode: 'HCM',
        CityName: 'Hồ Chí Minh',
        createdAt: new Date('2025-01-03T09:00:00Z'),
        updatedAt: new Date('2025-01-03T09:00:00Z')
      },
      {
        CityCode: 'HAN',
        CityName: 'Hà Nội',
        createdAt: new Date('2025-01-03T09:05:00Z'),
        updatedAt: new Date('2025-01-03T09:05:00Z')
      },
      {
        CityCode: 'DAN',
        CityName: 'Đà Nẵng',
        createdAt: new Date('2025-01-03T09:10:00Z'),
        updatedAt: new Date('2025-01-03T09:10:00Z')
      },
      {
        CityCode: 'CTO',
        CityName: 'Cần Thơ',
        createdAt: new Date('2025-01-03T09:15:00Z'),
        updatedAt: new Date('2025-01-03T09:15:00Z')
      },
      {
        CityCode: 'HAP',
        CityName: 'Hải Phòng',
        createdAt: new Date('2025-01-03T09:20:00Z'),
        updatedAt: new Date('2025-01-03T09:20:00Z')
      },
      {
        CityCode: 'NTH',
        CityName: 'Nha Trang',
        createdAt: new Date('2025-01-03T09:25:00Z'),
        updatedAt: new Date('2025-01-03T09:25:00Z')
      },
      {
        CityCode: 'BMT',
        CityName: 'Buôn Ma Thuột',
        createdAt: new Date('2025-01-03T09:30:00Z'),
        updatedAt: new Date('2025-01-03T09:30:00Z')
      },
      {
        CityCode: 'HUE',
        CityName: 'Huế',
        createdAt: new Date('2025-01-03T09:35:00Z'),
        updatedAt: new Date('2025-01-03T09:35:00Z')
      },
      {
        CityCode: 'VUT',
        CityName: 'Vũng Tàu',
        createdAt: new Date('2025-01-03T09:40:00Z'),
        updatedAt: new Date('2025-01-03T09:40:00Z')
      },
      {
        CityCode: 'QNH',
        CityName: 'Quy Nhơn',
        createdAt: new Date('2025-01-03T09:45:00Z'),
        updatedAt: new Date('2025-01-03T09:45:00Z')
      },
      {
        CityCode: 'TNH',
        CityName: 'Thái Nguyên',
        createdAt: new Date('2025-01-03T09:50:00Z'),
        updatedAt: new Date('2025-01-03T09:50:00Z')
      },
      {
        CityCode: 'BDG',
        CityName: 'Bình Dương',
        createdAt: new Date('2025-01-03T09:55:00Z'),
        updatedAt: new Date('2025-01-03T09:55:00Z')
      },
      {
        CityCode: 'DLT',
        CityName: 'Đà Lạt',
        createdAt: new Date('2025-01-03T10:00:00Z'),
        updatedAt: new Date('2025-01-03T10:00:00Z')
      },
      {
        CityCode: 'PTO',
        CityName: 'Phan Thiết',
        createdAt: new Date('2025-01-03T10:05:00Z'),
        updatedAt: new Date('2025-01-03T10:05:00Z')
      },
      {
        CityCode: 'PLK',
        CityName: 'Pleiku',
        createdAt: new Date('2025-01-03T10:10:00Z'),
        updatedAt: new Date('2025-01-03T10:10:00Z')
      },
      {
        CityCode: 'HUI',
        CityName: 'Hưng Yên',
        createdAt: new Date('2025-01-03T10:15:00Z'),
        updatedAt: new Date('2025-01-03T10:15:00Z')
      },
      {
        CityCode: 'BNH',
        CityName: 'Bắc Ninh',
        createdAt: new Date('2025-01-03T10:20:00Z'),
        updatedAt: new Date('2025-01-03T10:20:00Z')
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('Cities', null, {});
  }
};
