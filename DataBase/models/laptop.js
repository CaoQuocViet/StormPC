'use strict';
const {
  Model
} = require('sequelize');
module.exports = (sequelize, DataTypes) => {
  class Laptop extends Model {
    /**
     * Helper method for defining associations.
     * This method is not a part of Sequelize lifecycle.
     * The `models/index` file will call this method automatically.
     */
    static associate(models) {
      // define association here
    }
  }
  Laptop.init({
    LaptopID: DataTypes.STRING,
    BrandID: DataTypes.INTEGER,
    CategoryID: DataTypes.INTEGER,
    Picture: DataTypes.STRING,
    ModelName: DataTypes.STRING,
    ScreenSize: DataTypes.DECIMAL,
    OperatingSystem: DataTypes.STRING,
    ReleaseYear: DataTypes.INTEGER
  }, {
    sequelize,
    modelName: 'Laptop',
  });
  return Laptop;
};