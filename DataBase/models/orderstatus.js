'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class OrderStatus extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      // define association here
    }
  }
  OrderStatus.init({
    StatusID: DataTypes.INTEGER,
    StatusName: DataTypes.STRING
  }, {
    sequelize,
    modelName: 'OrderStatus',
  });
  return OrderStatus;
};