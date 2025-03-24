'use strict';
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('Orders', {
      id: {
        allowNull: true,
        autoIncrement: true,
        primaryKey: true,
        type: Sequelize.INTEGER
      },
      OrderID: {
        type: Sequelize.INTEGER
      },
      CustomerID: {
        type: Sequelize.INTEGER
      },
      OrderDate: {
        type: Sequelize.DATE
      },
      StatusID: {
        type: Sequelize.INTEGER
      },
      TotalAmount: {
        type: Sequelize.DECIMAL
      },
      PaymentMethodID: {
        type: Sequelize.INTEGER
      },
      ShipCityCode: {
        type: Sequelize.STRING
      },
      ShippingAddress: {
        type: Sequelize.TEXT
      },
      ShippingCity: {
        type: Sequelize.STRING
      },
      ShippingPostalCode: {
        type: Sequelize.STRING(10),
        allowNull: true
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
    await queryInterface.dropTable('Orders');
  }
};