'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    const now = new Date();
    await queryInterface.bulkInsert('Cities', [
      {
        CityCode: 'HCM',
        CityName: 'Hồ Chí Minh',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'HAN',
        CityName: 'Hà Nội',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'DAN',
        CityName: 'Đà Nẵng',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'CTO',
        CityName: 'Cần Thơ',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'HAP',
        CityName: 'Hải Phòng',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'NTH',
        CityName: 'Nha Trang',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'BMT',
        CityName: 'Buôn Ma Thuột',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'HUE',
        CityName: 'Huế',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'VUT',
        CityName: 'Vũng Tàu',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'QNH',
        CityName: 'Quy Nhơn',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'TNH',
        CityName: 'Thái Nguyên',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'BDG',
        CityName: 'Bình Dương',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'DLT',
        CityName: 'Đà Lạt',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'PTO',
        CityName: 'Phan Thiết',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'PLK',
        CityName: 'Pleiku',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'HUI',
        CityName: 'Hưng Yên',
        createdAt: now,
        updatedAt: now
      },
      {
        CityCode: 'BNH',
        CityName: 'Bắc Ninh',
        createdAt: now,
        updatedAt: now
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('Cities', null, {});
  }
};
