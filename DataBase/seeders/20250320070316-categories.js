'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    await queryInterface.bulkInsert('Categories', [
      {
        CategoryID: 1,
        CategoryName: 'Văn phòng',
        Description: 'Dành cho công việc văn phòng, pin lâu, nhẹ',
        IsDeleted: false,
        createdAt: new Date('2025-01-10T08:30:00Z'),
        updatedAt: new Date('2025-01-10T08:30:00Z')
      },
      {
        CategoryID: 2,
        CategoryName: 'Gaming',
        Description: 'Dành cho game thủ, hiệu năng cao, tản nhiệt tốt',
        IsDeleted: false,
        createdAt: new Date('2025-01-10T09:15:00Z'),
        updatedAt: new Date('2025-01-10T09:15:00Z')
      },
      {
        CategoryID: 3,
        CategoryName: 'Mỏng nhẹ',
        Description: 'Thiết kế mỏng nhẹ, di động, phù hợp cho doanh nhân',
        IsDeleted: false,
        createdAt: new Date('2025-01-10T10:45:00Z'),
        updatedAt: new Date('2025-01-10T10:45:00Z')
      },
      {
        CategoryID: 4,
        CategoryName: 'Đồ họa',
        Description: 'Dành cho dân thiết kế, đồ họa, cấu hình mạnh',
        IsDeleted: false,
        createdAt: new Date('2025-01-10T14:20:00Z'),
        updatedAt: new Date('2025-01-10T14:20:00Z')
      },
      {
        CategoryID: 5,
        CategoryName: 'Cao cấp',
        Description: 'Dòng cao cấp, thiết kế sang trọng, hiệu năng mạnh',
        IsDeleted: false,
        createdAt: new Date('2025-01-10T16:30:00Z'),
        updatedAt: new Date('2025-01-10T16:30:00Z')
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('Categories', null, {});
  }
};
