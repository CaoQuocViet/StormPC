'use strict';
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('OrderItems', {
      id: {
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
        type: Sequelize.INTEGER
      },
      OrderID: {
        type: Sequelize.INTEGER,
        references: {
          model: 'Orders',
          key: 'OrderID'
        }
      },
      VariantID: {
        type: Sequelize.INTEGER,
        references: {
          model: 'LaptopSpecs',
          key: 'VariantID'
        }
      },
      Quantity: {
        type: Sequelize.INTEGER
      },
      UnitPrice: {
        type: Sequelize.DECIMAL
      },
      createdAt: {
        allowNull: true,
        type: Sequelize.DATE
      },
      updatedAt: {
        allowNull: true,
        type: Sequelize.DATE
      }
    });
  },
  async down(queryInterface, Sequelize) {
    await queryInterface.dropTable('OrderItems');
  }
};