'use strict';
/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up(queryInterface, Sequelize) {
    await queryInterface.createTable('Laptops', {
      LaptopID: {
        allowNull: false,
        autoIncrement: true,
        primaryKey: true,
        type: Sequelize.INTEGER
      },
      BrandID: {
        type: Sequelize.INTEGER,
        references: {
          model: 'Brands',
          key: 'BrandID'
        }
      },
      CategoryID: {
        type: Sequelize.INTEGER,
        references: {
          model: 'Categories',
          key: 'CategoryID'
        }
      },
      Picture: {
        type: Sequelize.STRING
      },
      ModelName: {
        type: Sequelize.STRING
      },
      ScreenSize: {
        type: Sequelize.DECIMAL
      },
      OperatingSystem: {
        type: Sequelize.STRING
      },
      ReleaseYear: {
        type: Sequelize.INTEGER
      },
      Description: {
        type: Sequelize.TEXT
      },
      Discount: {
        type: Sequelize.DECIMAL,
        allowNull: false
      },
      DiscountEndDate: {
        type: Sequelize.DATE
      },
      DiscountStartDate: {
        type: Sequelize.DATE,
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
    await queryInterface.dropTable('Laptops');
  }
};