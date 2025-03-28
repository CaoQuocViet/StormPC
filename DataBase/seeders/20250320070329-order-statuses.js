'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    await queryInterface.bulkInsert('OrderStatuses', [
      {
        StatusID: 1,
        StatusName: 'Pending',
        createdAt: new Date('2025-01-09T13:00:00Z'),
        updatedAt: new Date('2025-01-09T13:00:00Z')
      },
      {
        StatusID: 2,
        StatusName: 'Processing',
        createdAt: new Date('2025-01-09T13:05:00Z'),
        updatedAt: new Date('2025-01-09T13:05:00Z')
      },
      {
        StatusID: 3,
        StatusName: 'Shipped',
        createdAt: new Date('2025-01-09T13:10:00Z'),
        updatedAt: new Date('2025-01-09T13:10:00Z')
      },
      {
        StatusID: 4,
        StatusName: 'Delivered',
        createdAt: new Date('2025-01-09T13:15:00Z'),
        updatedAt: new Date('2025-01-09T13:15:00Z')
      },
      {
        StatusID: 5,
        StatusName: 'Cancelled',
        createdAt: new Date('2025-01-09T13:20:00Z'),
        updatedAt: new Date('2025-01-09T13:20:00Z')
      },
      {
        StatusID: 6,
        StatusName: 'Refunded',
        createdAt: new Date('2025-01-09T13:25:00Z'),
        updatedAt: new Date('2025-01-09T13:25:00Z')
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('OrderStatuses', null, {});
  }
};
