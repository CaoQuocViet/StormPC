'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    const now = new Date();
    await queryInterface.bulkInsert('OrderItems', [
      // OrderID 1 - thêm thành 3 items
      {
        OrderID: 1,
        VariantID: '12768-2',
        Quantity: 1,
        UnitPrice: 138000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 1,
        VariantID: '12768-1',
        Quantity: 1,
        UnitPrice: 108000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 1,
        VariantID: '12768-3',
        Quantity: 1,
        UnitPrice: 144000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 2 - thêm thành 4 items
      {
        OrderID: 2,
        VariantID: '16354-2',
        Quantity: 1,
        UnitPrice: 228000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 2,
        VariantID: '16354-1',
        Quantity: 1,
        UnitPrice: 153000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 2, 
        VariantID: '18351-2',
        Quantity: 2,
        UnitPrice: 63000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 2,
        VariantID: '20388-1',
        Quantity: 1,
        UnitPrice: 72000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 3 - thêm thành 5 items
      {
        OrderID: 3,
        VariantID: '19927-2',
        Quantity: 1,
        UnitPrice: 326000000, 
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 3,
        VariantID: '19927-1',
        Quantity: 1,
        UnitPrice: 243000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 3,
        VariantID: '20269-1',
        Quantity: 2,
        UnitPrice: 67500000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 3,
        VariantID: '21056-1',
        Quantity: 1,
        UnitPrice: 81000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 3,
        VariantID: '21302-1',
        Quantity: 1,
        UnitPrice: 81000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 4 - thêm thành 4 items
      {
        OrderID: 4,
        VariantID: '18351-1',
        Quantity: 1,
        UnitPrice: 97000000,  
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 4,
        VariantID: '21340-1',
        Quantity: 1,
        UnitPrice: 69999988,   
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 4,
        VariantID: '18351-2',
        Quantity: 1,
        UnitPrice: 126000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 4,
        VariantID: '21175-1',
        Quantity: 1,
        UnitPrice: 90000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 5 - thêm thành 3 items
      {
        OrderID: 5,
        VariantID: '20692-1',
        Quantity: 1,
        UnitPrice: 467800000, 
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 5,
        VariantID: '21253-1',
        Quantity: 1,
        UnitPrice: 396000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 5,
        VariantID: '20626-2',
        Quantity: 1,
        UnitPrice: 324000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 6 - thêm thành 4 items
      {
        OrderID: 6,
        VariantID: '21042-1',
        Quantity: 1,
        UnitPrice: 138200000, 
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 6,
        VariantID: '20993-1',
        Quantity: 1,
        UnitPrice: 58400000,  
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 6,
        VariantID: '21042-2',
        Quantity: 1,
        UnitPrice: 189000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 6,
        VariantID: '20993-2',
        Quantity: 1,
        UnitPrice: 99000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 7 - thêm thành 5 items
      {
        OrderID: 7,
        VariantID: '16354-1',
        Quantity: 1,
        UnitPrice: 158000000, 
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 7,
        VariantID: '20225-1',
        Quantity: 1,
        UnitPrice: 162000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 7,
        VariantID: '21108-1',
        Quantity: 1,
        UnitPrice: 171000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 7,
        VariantID: '21171-1',
        Quantity: 1,
        UnitPrice: 117000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 7,
        VariantID: '20577-1',
        Quantity: 1,
        UnitPrice: 81000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 8 - sửa UnitPrice hiện tại và thêm thành 3 items
      {
        OrderID: 8,
        VariantID: '20225-2',
        Quantity: 1,
        UnitPrice: 215000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 8,
        VariantID: '20225-1',
        Quantity: 1,
        UnitPrice: 162000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 8,
        VariantID: '20269-1',
        Quantity: 1,
        UnitPrice: 135000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 9 - thêm thành 4 items
      {
        OrderID: 9,
        VariantID: '21183-2',
        Quantity: 1,
        UnitPrice: 146200000,  
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 9,
        VariantID: '21183-1',
        Quantity: 1,
        UnitPrice: 108000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 9,
        VariantID: '20388-1',
        Quantity: 1,
        UnitPrice: 72000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 9,
        VariantID: '21340-1',
        Quantity: 1,
        UnitPrice: 72000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 10 - thêm thành 4 items
      {
        OrderID: 10,
        VariantID: '20626-1',
        Quantity: 1,
        UnitPrice: 259000000, 
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 10,
        VariantID: '20626-2',
        Quantity: 1,
        UnitPrice: 324000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 10,
        VariantID: '20627-1',
        Quantity: 1,
        UnitPrice: 126000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 10,
        VariantID: '21037-1',
        Quantity: 1,
        UnitPrice: 153000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 11 - Đơn hàng mới
      {
        OrderID: 11,
        VariantID: '18950-1',
        Quantity: 1,
        UnitPrice: 108000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 11,
        VariantID: '18950-2',
        Quantity: 1,
        UnitPrice: 144000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 11,
        VariantID: '18950-3',
        Quantity: 1,
        UnitPrice: 189000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 11,
        VariantID: '20353-1',
        Quantity: 1,
        UnitPrice: 198000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 12 - Đơn hàng mới
      {
        OrderID: 12,
        VariantID: '19325-1',
        Quantity: 1,
        UnitPrice: 171000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 12,
        VariantID: '19325-2',
        Quantity: 1,
        UnitPrice: 216000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 12,
        VariantID: '20269-2',
        Quantity: 1,
        UnitPrice: 171000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 13 - Đơn hàng mới
      {
        OrderID: 13,
        VariantID: '20353-2',
        Quantity: 1,
        UnitPrice: 261000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 13,
        VariantID: '20577-2',
        Quantity: 1,
        UnitPrice: 117000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 13,
        VariantID: '21037-2',
        Quantity: 1,
        UnitPrice: 198000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 14 - Đơn hàng mới
      {
        OrderID: 14,
        VariantID: '20724-1',
        Quantity: 1,
        UnitPrice: 225000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 14,
        VariantID: '20724-2',
        Quantity: 1,
        UnitPrice: 288000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 14,
        VariantID: '21056-2',
        Quantity: 1,
        UnitPrice: 117000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 14,
        VariantID: '21108-2',
        Quantity: 1,
        UnitPrice: 216000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 15 - Đơn hàng mới
      {
        OrderID: 15,
        VariantID: '21175-2',
        Quantity: 1,
        UnitPrice: 108000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 15,
        VariantID: '21310-1',
        Quantity: 1,
        UnitPrice: 117000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 15,
        VariantID: '21310-2',
        Quantity: 1,
        UnitPrice: 162000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 16 - Đơn hàng mới bổ sung
      {
        OrderID: 16,
        VariantID: '21330-1',
        Quantity: 2,
        UnitPrice: 126000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 16,
        VariantID: '21330-2',
        Quantity: 1,
        UnitPrice: 153000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 16,
        VariantID: '21345-1',
        Quantity: 1,
        UnitPrice: 108000000,
        createdAt: now,
        updatedAt: now
      },
      
      // OrderID 17 - Đơn hàng mới bổ sung
      {
        OrderID: 17,
        VariantID: '21345-2',
        Quantity: 1,
        UnitPrice: 135000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 17,
        VariantID: '18351-3',
        Quantity: 1,
        UnitPrice: 153000000,
        createdAt: now,
        updatedAt: now
      },
      {
        OrderID: 17,
        VariantID: '16354-3',
        Quantity: 1,
        UnitPrice: 279000000,
        createdAt: now,
        updatedAt: now
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('OrderItems', null, {});
  }
};
