'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    await queryInterface.bulkInsert('OrderItems', [
      // OrderID 1
      {
        OrderID: 1,
        VariantID: '12768-2',
        Quantity: 2,
        UnitPrice: 138000000,
        createdAt: new Date('2025-02-01T09:30:00Z'),
        updatedAt: new Date('2025-02-01T09:30:00Z')
      },
      {
        OrderID: 1,
        VariantID: '12768-1',
        Quantity: 1,
        UnitPrice: 108000000,
        createdAt: new Date('2025-02-01T09:30:00Z'),
        updatedAt: new Date('2025-02-01T09:30:00Z')
      },
      {
        OrderID: 1,
        VariantID: '12768-3',
        Quantity: 3,
        UnitPrice: 144000000,
        createdAt: new Date('2025-02-01T09:30:00Z'),
        updatedAt: new Date('2025-02-01T09:30:00Z')
      },
      
      // OrderID 2
      {
        OrderID: 2,
        VariantID: '16354-2',
        Quantity: 1,
        UnitPrice: 228000000,
        createdAt: new Date('2025-02-03T14:15:00Z'),
        updatedAt: new Date('2025-02-03T14:15:00Z')
      },
      {
        OrderID: 2,
        VariantID: '16354-1',
        Quantity: 2,
        UnitPrice: 153000000,
        createdAt: new Date('2025-02-03T14:15:00Z'),
        updatedAt: new Date('2025-02-03T14:15:00Z')
      },
      {
        OrderID: 2, 
        VariantID: '18351-2',
        Quantity: 2,
        UnitPrice: 63000000,
        createdAt: new Date('2025-02-03T14:15:00Z'),
        updatedAt: new Date('2025-02-03T14:15:00Z')
      },
      {
        OrderID: 2,
        VariantID: '20388-1',
        Quantity: 3,
        UnitPrice: 72000000,
        createdAt: new Date('2025-02-03T14:15:00Z'),
        updatedAt: new Date('2025-02-03T14:15:00Z')
      },
      
      // OrderID 3
      {
        OrderID: 3,
        VariantID: '19927-2',
        Quantity: 1,
        UnitPrice: 326000000,
        createdAt: new Date('2025-02-05T11:20:00Z'),
        updatedAt: new Date('2025-02-05T11:20:00Z')
      },
      {
        OrderID: 3,
        VariantID: '19927-1',
        Quantity: 2,
        UnitPrice: 243000000,
        createdAt: new Date('2025-02-05T11:20:00Z'),
        updatedAt: new Date('2025-02-05T11:20:00Z')
      },
      {
        OrderID: 3,
        VariantID: '20269-1',
        Quantity: 3,
        UnitPrice: 67500000,
        createdAt: new Date('2025-02-05T11:20:00Z'),
        updatedAt: new Date('2025-02-05T11:20:00Z')
      },
      {
        OrderID: 3,
        VariantID: '21056-1',
        Quantity: 2,
        UnitPrice: 81000000,
        createdAt: new Date('2025-02-05T11:20:00Z'),
        updatedAt: new Date('2025-02-05T11:20:00Z')
      },
      {
        OrderID: 3,
        VariantID: '21302-1',
        Quantity: 1,
        UnitPrice: 81000000,
        createdAt: new Date('2025-02-05T11:20:00Z'),
        updatedAt: new Date('2025-02-05T11:20:00Z')
      },
      
      // OrderID 4
      {
        OrderID: 4,
        VariantID: '18351-1',
        Quantity: 3,
        UnitPrice: 97000000,
        createdAt: new Date('2025-02-07T16:45:00Z'),
        updatedAt: new Date('2025-02-07T16:45:00Z')
      },
      {
        OrderID: 4,
        VariantID: '21340-1',
        Quantity: 2,
        UnitPrice: 69999988,
        createdAt: new Date('2025-02-07T16:45:00Z'),
        updatedAt: new Date('2025-02-07T16:45:00Z')
      },
      {
        OrderID: 4,
        VariantID: '18351-2',
        Quantity: 1,
        UnitPrice: 126000000,
        createdAt: new Date('2025-02-07T16:45:00Z'),
        updatedAt: new Date('2025-02-07T16:45:00Z')
      },
      {
        OrderID: 4,
        VariantID: '21175-1',
        Quantity: 2,
        UnitPrice: 90000000,
        createdAt: new Date('2025-02-07T16:45:00Z'),
        updatedAt: new Date('2025-02-07T16:45:00Z')
      },
      
      // OrderID 5
      {
        OrderID: 5,
        VariantID: '20692-1',
        Quantity: 1,
        UnitPrice: 467800000,
        createdAt: new Date('2025-02-10T10:00:00Z'),
        updatedAt: new Date('2025-02-10T10:00:00Z')
      },
      {
        OrderID: 5,
        VariantID: '21253-1',
        Quantity: 1,
        UnitPrice: 396000000,
        createdAt: new Date('2025-02-10T10:00:00Z'),
        updatedAt: new Date('2025-02-10T10:00:00Z')
      },
      {
        OrderID: 5,
        VariantID: '20626-2',
        Quantity: 2,
        UnitPrice: 324000000,
        createdAt: new Date('2025-02-10T10:00:00Z'),
        updatedAt: new Date('2025-02-10T10:00:00Z')
      },
      
      // OrderID 6
      {
        OrderID: 6,
        VariantID: '21042-1',
        Quantity: 3,
        UnitPrice: 138200000,
        createdAt: new Date('2025-02-12T13:30:00Z'),
        updatedAt: new Date('2025-02-12T13:30:00Z')
      },
      {
        OrderID: 6,
        VariantID: '20993-1',
        Quantity: 2,
        UnitPrice: 58400000,
        createdAt: new Date('2025-02-12T13:30:00Z'),
        updatedAt: new Date('2025-02-12T13:30:00Z')
      },
      {
        OrderID: 6,
        VariantID: '21042-2',
        Quantity: 1,
        UnitPrice: 189000000,
        createdAt: new Date('2025-02-12T13:30:00Z'),
        updatedAt: new Date('2025-02-12T13:30:00Z')
      },
      {
        OrderID: 6,
        VariantID: '20993-2',
        Quantity: 2,
        UnitPrice: 99000000,
        createdAt: new Date('2025-02-12T13:30:00Z'),
        updatedAt: new Date('2025-02-12T13:30:00Z')
      },
      
      // OrderID 7
      {
        OrderID: 7,
        VariantID: '16354-1',
        Quantity: 3,
        UnitPrice: 158000000,
        createdAt: new Date('2025-02-15T09:15:00Z'),
        updatedAt: new Date('2025-02-15T09:15:00Z')
      },
      {
        OrderID: 7,
        VariantID: '20225-1',
        Quantity: 1,
        UnitPrice: 162000000,
        createdAt: new Date('2025-02-15T09:15:00Z'),
        updatedAt: new Date('2025-02-15T09:15:00Z')
      },
      {
        OrderID: 7,
        VariantID: '21108-1',
        Quantity: 2,
        UnitPrice: 171000000,
        createdAt: new Date('2025-02-15T09:15:00Z'),
        updatedAt: new Date('2025-02-15T09:15:00Z')
      },
      {
        OrderID: 7,
        VariantID: '21171-1',
        Quantity: 1,
        UnitPrice: 117000000,
        createdAt: new Date('2025-02-15T09:15:00Z'),
        updatedAt: new Date('2025-02-15T09:15:00Z')
      },
      {
        OrderID: 7,
        VariantID: '20577-1',
        Quantity: 3,
        UnitPrice: 81000000,
        createdAt: new Date('2025-02-15T09:15:00Z'),
        updatedAt: new Date('2025-02-15T09:15:00Z')
      },
      
      // OrderID 8
      {
        OrderID: 8,
        VariantID: '20225-2',
        Quantity: 2,
        UnitPrice: 215000000,
        createdAt: new Date('2025-02-17T11:45:00Z'),
        updatedAt: new Date('2025-02-17T11:45:00Z')
      },
      {
        OrderID: 8,
        VariantID: '20225-1',
        Quantity: 1,
        UnitPrice: 162000000,
        createdAt: new Date('2025-02-17T11:45:00Z'),
        updatedAt: new Date('2025-02-17T11:45:00Z')
      },
      {
        OrderID: 8,
        VariantID: '20269-1',
        Quantity: 3,
        UnitPrice: 135000000,
        createdAt: new Date('2025-02-17T11:45:00Z'),
        updatedAt: new Date('2025-02-17T11:45:00Z')
      },
      
      // OrderID 9
      {
        OrderID: 9,
        VariantID: '21183-2',
        Quantity: 2,
        UnitPrice: 146200000,
        createdAt: new Date('2025-02-20T14:20:00Z'),
        updatedAt: new Date('2025-02-20T14:20:00Z')
      },
      {
        OrderID: 9,
        VariantID: '21183-1',
        Quantity: 1,
        UnitPrice: 108000000,
        createdAt: new Date('2025-02-20T14:20:00Z'),
        updatedAt: new Date('2025-02-20T14:20:00Z')
      },
      {
        OrderID: 9,
        VariantID: '20388-1',
        Quantity: 3,
        UnitPrice: 72000000,
        createdAt: new Date('2025-02-20T14:20:00Z'),
        updatedAt: new Date('2025-02-20T14:20:00Z')
      },
      {
        OrderID: 9,
        VariantID: '21340-1',
        Quantity: 2,
        UnitPrice: 72000000,
        createdAt: new Date('2025-02-20T14:20:00Z'),
        updatedAt: new Date('2025-02-20T14:20:00Z')
      },
      
      // OrderID 10
      {
        OrderID: 10,
        VariantID: '20626-1',
        Quantity: 1,
        UnitPrice: 259000000,
        createdAt: new Date('2025-02-22T15:50:00Z'),
        updatedAt: new Date('2025-02-22T15:50:00Z')
      },
      {
        OrderID: 10,
        VariantID: '20626-2',
        Quantity: 2,
        UnitPrice: 324000000,
        createdAt: new Date('2025-02-22T15:50:00Z'),
        updatedAt: new Date('2025-02-22T15:50:00Z')
      },
      {
        OrderID: 10,
        VariantID: '20627-1',
        Quantity: 3,
        UnitPrice: 126000000,
        createdAt: new Date('2025-02-22T15:50:00Z'),
        updatedAt: new Date('2025-02-22T15:50:00Z')
      },
      {
        OrderID: 10,
        VariantID: '21037-1',
        Quantity: 1,
        UnitPrice: 153000000,
        createdAt: new Date('2025-02-22T15:50:00Z'),
        updatedAt: new Date('2025-02-22T15:50:00Z')
      },
      
      // OrderID 11
      {
        OrderID: 11,
        VariantID: '18950-1',
        Quantity: 2,
        UnitPrice: 108000000,
        createdAt: new Date('2025-02-25T10:15:00Z'),
        updatedAt: new Date('2025-02-25T10:15:00Z')
      },
      {
        OrderID: 11,
        VariantID: '18950-2',
        Quantity: 3,
        UnitPrice: 144000000,
        createdAt: new Date('2025-02-25T10:15:00Z'),
        updatedAt: new Date('2025-02-25T10:15:00Z')
      },
      {
        OrderID: 11,
        VariantID: '18950-3',
        Quantity: 1,
        UnitPrice: 189000000,
        createdAt: new Date('2025-02-25T10:15:00Z'),
        updatedAt: new Date('2025-02-25T10:15:00Z')
      },
      {
        OrderID: 11,
        VariantID: '20353-1',
        Quantity: 2,
        UnitPrice: 198000000,
        createdAt: new Date('2025-02-25T10:15:00Z'),
        updatedAt: new Date('2025-02-25T10:15:00Z')
      },
      
      // OrderID 12
      {
        OrderID: 12,
        VariantID: '19325-1',
        Quantity: 3,
        UnitPrice: 171000000,
        createdAt: new Date('2025-03-01T14:30:00Z'),
        updatedAt: new Date('2025-03-01T14:30:00Z')
      },
      {
        OrderID: 12,
        VariantID: '19325-2',
        Quantity: 1,
        UnitPrice: 216000000,
        createdAt: new Date('2025-03-01T14:30:00Z'),
        updatedAt: new Date('2025-03-01T14:30:00Z')
      },
      {
        OrderID: 12,
        VariantID: '20269-2',
        Quantity: 2,
        UnitPrice: 171000000,
        createdAt: new Date('2025-03-01T14:30:00Z'),
        updatedAt: new Date('2025-03-01T14:30:00Z')
      },
      
      // OrderID 13
      {
        OrderID: 13,
        VariantID: '20353-2',
        Quantity: 1,
        UnitPrice: 261000000,
        createdAt: new Date('2025-03-05T09:45:00Z'),
        updatedAt: new Date('2025-03-05T09:45:00Z')
      },
      {
        OrderID: 13,
        VariantID: '20577-2',
        Quantity: 3,
        UnitPrice: 117000000,
        createdAt: new Date('2025-03-05T09:45:00Z'),
        updatedAt: new Date('2025-03-05T09:45:00Z')
      },
      {
        OrderID: 13,
        VariantID: '21037-2',
        Quantity: 2,
        UnitPrice: 198000000,
        createdAt: new Date('2025-03-05T09:45:00Z'),
        updatedAt: new Date('2025-03-05T09:45:00Z')
      },
      
      // OrderID 14
      {
        OrderID: 14,
        VariantID: '20724-1',
        Quantity: 2,
        UnitPrice: 225000000,
        createdAt: new Date('2025-03-10T15:20:00Z'),
        updatedAt: new Date('2025-03-10T15:20:00Z')
      },
      {
        OrderID: 14,
        VariantID: '20724-2',
        Quantity: 1,
        UnitPrice: 288000000,
        createdAt: new Date('2025-03-10T15:20:00Z'),
        updatedAt: new Date('2025-03-10T15:20:00Z')
      },
      {
        OrderID: 14,
        VariantID: '21056-2',
        Quantity: 3,
        UnitPrice: 117000000,
        createdAt: new Date('2025-03-10T15:20:00Z'),
        updatedAt: new Date('2025-03-10T15:20:00Z')
      },
      {
        OrderID: 14,
        VariantID: '21108-2',
        Quantity: 2,
        UnitPrice: 216000000,
        createdAt: new Date('2025-03-10T15:20:00Z'),
        updatedAt: new Date('2025-03-10T15:20:00Z')
      },
      
      // OrderID 15
      {
        OrderID: 15,
        VariantID: '21175-2',
        Quantity: 3,
        UnitPrice: 108000000,
        createdAt: new Date('2025-03-15T11:30:00Z'),
        updatedAt: new Date('2025-03-15T11:30:00Z')
      },
      {
        OrderID: 15,
        VariantID: '21310-1',
        Quantity: 2,
        UnitPrice: 117000000,
        createdAt: new Date('2025-03-15T11:30:00Z'),
        updatedAt: new Date('2025-03-15T11:30:00Z')
      },
      {
        OrderID: 15,
        VariantID: '21310-2',
        Quantity: 1,
        UnitPrice: 162000000,
        createdAt: new Date('2025-03-15T11:30:00Z'),
        updatedAt: new Date('2025-03-15T11:30:00Z')
      },
      
      // OrderID 16
      {
        OrderID: 16,
        VariantID: '21330-1',
        Quantity: 2,
        UnitPrice: 126000000,
        createdAt: new Date('2025-03-18T10:45:00Z'),
        updatedAt: new Date('2025-03-18T10:45:00Z')
      },
      {
        OrderID: 16,
        VariantID: '21330-2',
        Quantity: 3,
        UnitPrice: 153000000,
        createdAt: new Date('2025-03-18T10:45:00Z'),
        updatedAt: new Date('2025-03-18T10:45:00Z')
      },
      {
        OrderID: 16,
        VariantID: '21345-1',
        Quantity: 1,
        UnitPrice: 108000000,
        createdAt: new Date('2025-03-18T10:45:00Z'),
        updatedAt: new Date('2025-03-18T10:45:00Z')
      },
      
      // OrderID 17
      {
        OrderID: 17,
        VariantID: '21345-2',
        Quantity: 3,
        UnitPrice: 135000000,
        createdAt: new Date('2025-03-22T09:15:00Z'),
        updatedAt: new Date('2025-03-22T09:15:00Z')
      },
      {
        OrderID: 17,
        VariantID: '18351-3',
        Quantity: 2,
        UnitPrice: 153000000,
        createdAt: new Date('2025-03-22T09:15:00Z'),
        updatedAt: new Date('2025-03-22T09:15:00Z')
      },
      {
        OrderID: 17,
        VariantID: '16354-3',
        Quantity: 1,
        UnitPrice: 279000000,
        createdAt: new Date('2025-03-22T09:15:00Z'),
        updatedAt: new Date('2025-03-22T09:15:00Z')
      },
      
      // OrderID 18 - MacBook Pro 16-inch
      {
        OrderID: 18,
        VariantID: '21355-1',
        Quantity: 1,
        UnitPrice: 495000000,
        createdAt: new Date('2025-03-23T09:30:00Z'),
        updatedAt: new Date('2025-03-23T09:30:00Z')
      },
      
      // OrderID 19 - MacBook Pro 16-inch high-spec
      {
        OrderID: 19,
        VariantID: '21355-2',
        Quantity: 1,
        UnitPrice: 570000000,
        createdAt: new Date('2025-03-24T14:15:00Z'),
        updatedAt: new Date('2025-03-24T14:15:00Z')
      },
      
      // OrderID 20 - MacBook Pro 14-inch
      {
        OrderID: 20,
        VariantID: '21360-1',
        Quantity: 1,
        UnitPrice: 670000000,
        createdAt: new Date('2025-03-25T10:20:00Z'),
        updatedAt: new Date('2025-03-25T10:20:00Z')
      },
      
      // OrderID 21 - Bundle order
      {
        OrderID: 21,
        VariantID: '21360-2',
        Quantity: 1,
        UnitPrice: 770000000,
        createdAt: new Date('2025-03-27T16:45:00Z'),
        updatedAt: new Date('2025-03-27T16:45:00Z')
      },
      {
        OrderID: 21,
        VariantID: '21355-1',
        Quantity: 1,
        UnitPrice: 495000000,
        createdAt: new Date('2025-03-27T16:45:00Z'),
        updatedAt: new Date('2025-03-27T16:45:00Z')
      },
      {
        OrderID: 21,
        VariantID: '21355-2',
        Quantity: 1,
        UnitPrice: 255000000,
        createdAt: new Date('2025-03-27T16:45:00Z'),
        updatedAt: new Date('2025-03-27T16:45:00Z')
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('OrderItems', null, {});
  }
};
