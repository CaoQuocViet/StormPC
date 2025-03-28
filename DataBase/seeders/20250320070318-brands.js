'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    // Sử dụng các ngày cố định thay vì now()
    const baseDate = new Date('2025-01-05');
    
    await queryInterface.bulkInsert('Brands', [
      {
        BrandID: 1,
        BrandName: 'Dell',
        createdAt: new Date('2025-01-05T09:30:00Z'),
        updatedAt: new Date('2025-01-05T09:30:00Z')
      },
      {
        BrandID: 2,
        BrandName: 'Asus',
        createdAt: new Date('2025-01-05T10:15:00Z'),
        updatedAt: new Date('2025-01-05T10:15:00Z')
      },
      {
        BrandID: 3,
        BrandName: 'Lenovo',
        createdAt: new Date('2025-01-05T11:20:00Z'),
        updatedAt: new Date('2025-01-05T11:20:00Z')
      },
      {
        BrandID: 4,
        BrandName: 'HP',
        createdAt: new Date('2025-01-05T13:45:00Z'),
        updatedAt: new Date('2025-01-05T13:45:00Z')
      },
      {
        BrandID: 5,
        BrandName: 'Razer',
        createdAt: new Date('2025-01-06T09:10:00Z'),
        updatedAt: new Date('2025-01-06T09:10:00Z')
      },
      {
        BrandID: 6,
        BrandName: 'LG',
        createdAt: new Date('2025-01-06T10:30:00Z'),
        updatedAt: new Date('2025-01-06T10:30:00Z')
      },
      {
        BrandID: 7,
        BrandName: 'Acer',
        createdAt: new Date('2025-01-06T14:25:00Z'),
        updatedAt: new Date('2025-01-06T14:25:00Z')
      },
      {
        BrandID: 8,
        BrandName: 'MSI',
        createdAt: new Date('2025-01-07T08:50:00Z'),
        updatedAt: new Date('2025-01-07T08:50:00Z')
      },
      {
        BrandID: 9,
        BrandName: 'Gigabyte',
        createdAt: new Date('2025-01-07T11:15:00Z'),
        updatedAt: new Date('2025-01-07T11:15:00Z')
      },
      {
        BrandID: 10,
        BrandName: 'Apple',
        createdAt: new Date('2025-01-07T15:40:00Z'),
        updatedAt: new Date('2025-01-07T15:40:00Z')
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('Brands', null, {});
  }
};
