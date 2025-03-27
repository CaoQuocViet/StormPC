'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    const now = new Date();
    await queryInterface.bulkInsert('Brands', [
      {
        BrandID: 1,
        BrandName: 'Dell',
        createdAt: now,
        updatedAt: now
      },
      {
        BrandID: 2,
        BrandName: 'Asus',
        createdAt: now,
        updatedAt: now
      },
      {
        BrandID: 3,
        BrandName: 'Lenovo',
        createdAt: now,
        updatedAt: now
      },
      {
        BrandID: 4,
        BrandName: 'HP',
        createdAt: now,
        updatedAt: now
      },
      {
        BrandID: 5,
        BrandName: 'Razer',
        createdAt: now,
        updatedAt: now
      },
      {
        BrandID: 6,
        BrandName: 'LG',
        createdAt: now,
        updatedAt: now
      },
      {
        BrandID: 7,
        BrandName: 'Acer',
        createdAt: now,
        updatedAt: now
      },
      {
        BrandID: 8,
        BrandName: 'MSI',
        createdAt: now,
        updatedAt: now
      },
      {
        BrandID: 9,
        BrandName: 'Gigabyte',
        createdAt: now,
        updatedAt: now
      },
      {
        BrandID: 10,
        BrandName: 'Apple',
        createdAt: now,
        updatedAt: now
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('Brands', null, {});
  }
};
