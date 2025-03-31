'use strict';
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('LaptopSpecs', {
      id: {
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
        type: Sequelize.INTEGER
      },
      SKU: {
        type: Sequelize.STRING,
        allowNull: false,
        unique: true
      },
      VariantID: {
        type: Sequelize.STRING
      },
      LaptopID: {
        type: Sequelize.STRING
      },
      CPU: {
        type: Sequelize.STRING
      },
      GPU: {
        type: Sequelize.STRING
      },
      RAM: {
        type: Sequelize.INTEGER
      },
      Storage: {
        type: Sequelize.INTEGER
      },
      StorageType: {
        type: Sequelize.STRING
      },
      Color: {
        type: Sequelize.STRING
      },
      ImportPrice: {
        type: Sequelize.DECIMAL,
        allowNull: false
      },
      Price: {
        type: Sequelize.DECIMAL
      },
      StockQuantity: {
        type: Sequelize.INTEGER
      },
      IsDeleted: {
        type: Sequelize.BOOLEAN,
        allowNull: false,
        defaultValue: false
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
    await queryInterface.dropTable('LaptopSpecs');
  }
};