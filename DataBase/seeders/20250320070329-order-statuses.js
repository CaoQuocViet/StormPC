'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    const now = new Date();
    await queryInterface.bulkInsert('OrderStatuses', [
      {
        StatusID: 1,
        StatusName: 'Pending',
        createdAt: now,
        updatedAt: now
      },
      {
        StatusID: 2,
        StatusName: 'Processing',
        createdAt: now,
        updatedAt: now
      },
      {
        StatusID: 3,
        StatusName: 'Shipped',
        createdAt: now,
        updatedAt: now
      },
      {
        StatusID: 4,
        StatusName: 'Delivered',
        createdAt: now,
        updatedAt: now
      },
      {
        StatusID: 5,
        StatusName: 'Cancelled',
        createdAt: now,
        updatedAt: now
      },
      {
        StatusID: 6,
        StatusName: 'Refunded',
        createdAt: now,
        updatedAt: now
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('OrderStatuses', null, {});
  }
};
