'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class LaptopSpec extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      // define association here
    }
  }
  LaptopSpec.init({
    SKU: {
      type: DataTypes.STRING,
      allowNull: false,
      unique: true
    },
    VariantID: DataTypes.STRING,
    LaptopID: DataTypes.STRING,
    CPU: DataTypes.STRING,
    GPU: DataTypes.STRING,
    RAM: DataTypes.INTEGER,
    Storage: DataTypes.INTEGER,
    StorageType: DataTypes.STRING,
    Color: DataTypes.STRING,
    ImportPrice: {
      type: DataTypes.DECIMAL,
      allowNull: false
    },
    Price: DataTypes.DECIMAL,
    StockQuantity: DataTypes.INTEGER
  }, {
    sequelize,
    modelName: 'LaptopSpec',
  });
  return LaptopSpec;
};