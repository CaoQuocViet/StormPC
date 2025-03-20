'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    const now = new Date();
    await queryInterface.bulkInsert('OrderItems', [
      {
        OrderID: 1,
        VariantID: '12768-2',
        Quantity: 1,
        UnitPrice: 1599.99,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 2,
        VariantID: '16354-2',
        Quantity: 1,
        UnitPrice: 2499.99,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 3,
        VariantID: '19927-2',
        Quantity: 1,
        UnitPrice: 3499.99,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 4,
        VariantID: '18351-1',
        Quantity: 1,
        UnitPrice: 1199.99,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 4,
        VariantID: '21340-1',
        Quantity: 1,
        UnitPrice: 99.99,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 5,
        VariantID: '20692-1',
        Quantity: 1,
        UnitPrice: 4999.99,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 6,
        VariantID: '21042-1',
        Quantity: 1,
        UnitPrice: 1699.99,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 6,
        VariantID: '20993-1',
        Quantity: 1,
        UnitPrice: 500.00,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 7,
        VariantID: '16354-1',
        Quantity: 1,
        UnitPrice: 1799.99,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 8,
        VariantID: '20225-2',
        Quantity: 1,
        UnitPrice: 2399.99,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 9,
        VariantID: '21183-2',
        Quantity: 1,
        UnitPrice: 1699.99,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 10,
        VariantID: '20626-1',
        Quantity: 1,
        UnitPrice: 2899.99,
        createdAt: now,
        updatedAt: now
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('OrderItems', null, {});
  }
};
