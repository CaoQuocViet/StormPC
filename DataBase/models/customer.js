'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class Customer extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      // define association here
    }
  }
  Customer.init({
    CustomerID: {
      type: DataTypes.INTEGER,
      primaryKey: true,
      autoIncrement: true
    },
    FullName: DataTypes.STRING(100),
    Email: DataTypes.STRING(100),
    Phone: DataTypes.STRING(20),
    Address: DataTypes.TEXT,
    CityCode: DataTypes.STRING(3),
    IsDeleted: {
      type: DataTypes.BOOLEAN,
      defaultValue: false
    }
  }, {
    sequelize,
    modelName: 'Customer',
  });
  return Customer;
};