'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    const now = new Date();
    await queryInterface.bulkInsert('Categories', [
      {
        CategoryID: 1,
        CategoryName: 'Văn phòng',
        Description: 'Dành cho công việc văn phòng, pin lâu, nhẹ',
        createdAt: now,
        updatedAt: now
      },
      {
        CategoryID: 2,
        CategoryName: 'Gaming',
        Description: 'Dành cho game thủ, hiệu năng cao, tản nhiệt tốt',
        createdAt: now,
        updatedAt: now
      },
      {
        CategoryID: 3,
        CategoryName: 'Mỏng nhẹ',
        Description: 'Thiết kế mỏng nhẹ, di động, phù hợp cho doanh nhân',
        createdAt: now,
        updatedAt: now
      },
      {
        CategoryID: 4,
        CategoryName: 'Đồ họa',
        Description: 'Dành cho dân thiết kế, đồ họa, cấu hình mạnh',
        createdAt: now,
        updatedAt: now
      },
      {
        CategoryID: 5,
        CategoryName: 'Cao cấp',
        Description: 'Dòng cao cấp, thiết kế sang trọng, hiệu năng mạnh',
        createdAt: now,
        updatedAt: now
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('Categories', null, {});
  }
};
