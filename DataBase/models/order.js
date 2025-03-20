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
    OrderID: DataTypes.INTEGER,
    CustomerID: DataTypes.INTEGER,
    OrderDate: DataTypes.DATE,
    StatusID: DataTypes.INTEGER,
    TotalAmount: DataTypes.DECIMAL,
    PaymentMethodID: DataTypes.INTEGER,
    ShipCityCode: DataTypes.STRING,
    ShippingAddress: DataTypes.TEXT,
    ShippingCity: DataTypes.STRING,
    ShippingPostalCode: DataTypes.STRING
  }, {
    sequelize,
    modelName: 'Order',
  });
  return Order;
};