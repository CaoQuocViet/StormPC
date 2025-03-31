'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    await queryInterface.bulkInsert('Cities', [
      {
        id: 1,
        CityCode: 'HCM',
        CityName: 'Hồ Chí Minh',
        createdAt: new Date('2025-01-03T09:00:00Z'),
        updatedAt: new Date('2025-01-03T09:00:00Z')
      },
      {
        id: 2,
        CityCode: 'HAN',
        CityName: 'Hà Nội',
        createdAt: new Date('2025-01-03T09:05:00Z'),
        updatedAt: new Date('2025-01-03T09:05:00Z')
      },
      {
        id: 3,
        CityCode: 'DAN',
        CityName: 'Đà Nẵng',
        createdAt: new Date('2025-01-03T09:10:00Z'),
        updatedAt: new Date('2025-01-03T09:10:00Z')
      },
      {
        id: 4,
        CityCode: 'CTO',
        CityName: 'Cần Thơ',
        createdAt: new Date('2025-01-03T09:15:00Z'),
        updatedAt: new Date('2025-01-03T09:15:00Z')
      },
      {
        id: 5,
        CityCode: 'HAP',
        CityName: 'Hải Phòng',
        createdAt: new Date('2025-01-03T09:20:00Z'),
        updatedAt: new Date('2025-01-03T09:20:00Z')
      },
      {
        id: 6,
        CityCode: 'NTH',
        CityName: 'Nha Trang',
        createdAt: new Date('2025-01-03T09:25:00Z'),
        updatedAt: new Date('2025-01-03T09:25:00Z')
      },
      {
        id: 7,
        CityCode: 'BMT',
        CityName: 'Buôn Ma Thuột',
        createdAt: new Date('2025-01-03T09:30:00Z'),
        updatedAt: new Date('2025-01-03T09:30:00Z')
      },
      {
        id: 8,
        CityCode: 'HUE',
        CityName: 'Huế',
        createdAt: new Date('2025-01-03T09:35:00Z'),
        updatedAt: new Date('2025-01-03T09:35:00Z')
      },
      {
        id: 9,
        CityCode: 'VUT',
        CityName: 'Vũng Tàu',
        createdAt: new Date('2025-01-03T09:40:00Z'),
        updatedAt: new Date('2025-01-03T09:40:00Z')
      },
      {
        id: 10,
        CityCode: 'QNH',
        CityName: 'Quy Nhơn',
        createdAt: new Date('2025-01-03T09:45:00Z'),
        updatedAt: new Date('2025-01-03T09:45:00Z')
      },
      {
        id: 11,
        CityCode: 'TNH',
        CityName: 'Thái Nguyên',
        createdAt: new Date('2025-01-03T09:50:00Z'),
        updatedAt: new Date('2025-01-03T09:50:00Z')
      },
      {
        id: 12,
        CityCode: 'BDG',
        CityName: 'Bình Dương',
        createdAt: new Date('2025-01-03T09:55:00Z'),
        updatedAt: new Date('2025-01-03T09:55:00Z')
      },
      {
        id: 13,
        CityCode: 'DLT',
        CityName: 'Đà Lạt',
        createdAt: new Date('2025-01-03T10:00:00Z'),
        updatedAt: new Date('2025-01-03T10:00:00Z')
      },
      {
        id: 14,
        CityCode: 'PTO',
        CityName: 'Phan Thiết',
        createdAt: new Date('2025-01-03T10:05:00Z'),
        updatedAt: new Date('2025-01-03T10:05:00Z')
      },
      {
        id: 15,
        CityCode: 'PLK',
        CityName: 'Pleiku',
        createdAt: new Date('2025-01-03T10:10:00Z'),
        updatedAt: new Date('2025-01-03T10:10:00Z')
      },
      {
        id: 16,
        CityCode: 'HUI',
        CityName: 'Hưng Yên',
        createdAt: new Date('2025-01-03T10:15:00Z'),
        updatedAt: new Date('2025-01-03T10:15:00Z')
      },
      {
        id: 17,
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
