'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class Order extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      // define association here
    }
  }
  Order.init({
    OrderID: {
      type: DataTypes.INTEGER,
      primaryKey: true,
      autoIncrement: true
    },
    CustomerID: DataTypes.INTEGER,
    OrderDate: DataTypes.DATE,
    StatusID: DataTypes.INTEGER,
    TotalAmount: DataTypes.DECIMAL,
    PaymentMethodID: DataTypes.INTEGER,
    ShipCityCode: DataTypes.STRING(3),
    ShippingAddress: DataTypes.TEXT,
    ShippingCity: DataTypes.STRING(50),
    ShippingPostalCode: DataTypes.STRING(10),
    IsDeleted: {
      type: DataTypes.BOOLEAN,
      defaultValue: false
    }
  }, {
    sequelize,
    modelName: 'Order',
  });
  return Order;
};