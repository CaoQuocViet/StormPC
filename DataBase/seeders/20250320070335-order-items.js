'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    await queryInterface.bulkInsert('OrderItems', [
      // OrderID 1
      {
        OrderID: 1,
        VariantID: '127682',
        Quantity: 2,
        UnitPrice: 138000000,
        createdAt: new Date('2025-02-01T09:30:00Z'),
        updatedAt: new Date('2025-02-01T09:30:00Z')
      },
      {
        OrderID: 1,
        VariantID: '127681',
        Quantity: 1,
        UnitPrice: 108000000,
        createdAt: new Date('2025-02-01T09:30:00Z'),
        updatedAt: new Date('2025-02-01T09:30:00Z')
      },
      {
        OrderID: 1,
        VariantID: '127683',
        Quantity: 3,
        UnitPrice: 144000000,
        createdAt: new Date('2025-02-01T09:30:00Z'),
        updatedAt: new Date('2025-02-01T09:30:00Z')
      },
      
      // OrderID 2
      {
        OrderID: 2,
        VariantID: '163542',
        Quantity: 1,
        UnitPrice: 228000000,
        createdAt: new Date('2025-02-03T14:15:00Z'),
        updatedAt: new Date('2025-02-03T14:15:00Z')
      },
      {
        OrderID: 2,
        VariantID: '163541',
        Quantity: 2,
        UnitPrice: 153000000,
        createdAt: new Date('2025-02-03T14:15:00Z'),
        updatedAt: new Date('2025-02-03T14:15:00Z')
      },
      {
        OrderID: 2, 
        VariantID: '183512',
        Quantity: 2,
        UnitPrice: 63000000,
        createdAt: new Date('2025-02-03T14:15:00Z'),
        updatedAt: new Date('2025-02-03T14:15:00Z')
      },
      {
        OrderID: 2,
        VariantID: '203881',
        Quantity: 3,
        UnitPrice: 72000000,
        createdAt: new Date('2025-02-03T14:15:00Z'),
        updatedAt: new Date('2025-02-03T14:15:00Z')
      },
      
      // OrderID 3
      {
        OrderID: 3,
        VariantID: '199272',
        Quantity: 1,
        UnitPrice: 326000000,
        createdAt: new Date('2025-02-05T11:20:00Z'),
        updatedAt: new Date('2025-02-05T11:20:00Z')
      },
      {
        OrderID: 3,
        VariantID: '199271',
        Quantity: 2,
        UnitPrice: 243000000,
        createdAt: new Date('2025-02-05T11:20:00Z'),
        updatedAt: new Date('2025-02-05T11:20:00Z')
      },
      {
        OrderID: 3,
        VariantID: '202691',
        Quantity: 3,
        UnitPrice: 67500000,
        createdAt: new Date('2025-02-05T11:20:00Z'),
        updatedAt: new Date('2025-02-05T11:20:00Z')
      },
      {
        OrderID: 3,
        VariantID: '210561',
        Quantity: 2,
        UnitPrice: 81000000,
        createdAt: new Date('2025-02-05T11:20:00Z'),
        updatedAt: new Date('2025-02-05T11:20:00Z')
      },
      {
        OrderID: 3,
        VariantID: '213021',
        Quantity: 1,
        UnitPrice: 81000000,
        createdAt: new Date('2025-02-05T11:20:00Z'),
        updatedAt: new Date('2025-02-05T11:20:00Z')
      },
      
      // OrderID 4
      {
        OrderID: 4,
        VariantID: '183511',
        Quantity: 3,
        UnitPrice: 97000000,
        createdAt: new Date('2025-02-07T16:45:00Z'),
        updatedAt: new Date('2025-02-07T16:45:00Z')
      },
      {
        OrderID: 4,
        VariantID: '213401',
        Quantity: 2,
        UnitPrice: 69999988,
        createdAt: new Date('2025-02-07T16:45:00Z'),
        updatedAt: new Date('2025-02-07T16:45:00Z')
      },
      {
        OrderID: 4,
        VariantID: '183512',
        Quantity: 1,
        UnitPrice: 126000000,
        createdAt: new Date('2025-02-07T16:45:00Z'),
        updatedAt: new Date('2025-02-07T16:45:00Z')
      },
      {
        OrderID: 4,
        VariantID: '211751',
        Quantity: 2,
        UnitPrice: 90000000,
        createdAt: new Date('2025-02-07T16:45:00Z'),
        updatedAt: new Date('2025-02-07T16:45:00Z')
      },
      
      // OrderID 5
      {
        OrderID: 5,
        VariantID: '206921',
        Quantity: 1,
        UnitPrice: 467800000,
        createdAt: new Date('2025-02-10T10:00:00Z'),
        updatedAt: new Date('2025-02-10T10:00:00Z')
      },
      {
        OrderID: 5,
        VariantID: '212531',
        Quantity: 1,
        UnitPrice: 396000000,
        createdAt: new Date('2025-02-10T10:00:00Z'),
        updatedAt: new Date('2025-02-10T10:00:00Z')
      },
      {
        OrderID: 5,
        VariantID: '206262',
        Quantity: 2,
        UnitPrice: 324000000,
        createdAt: new Date('2025-02-10T10:00:00Z'),
        updatedAt: new Date('2025-02-10T10:00:00Z')
      },
      
      // OrderID 6
      {
        OrderID: 6,
        VariantID: '210421',
        Quantity: 3,
        UnitPrice: 138200000,
        createdAt: new Date('2025-02-12T13:30:00Z'),
        updatedAt: new Date('2025-02-12T13:30:00Z')
      },
      {
        OrderID: 6,
        VariantID: '209931',
        Quantity: 2,
        UnitPrice: 58400000,
        createdAt: new Date('2025-02-12T13:30:00Z'),
        updatedAt: new Date('2025-02-12T13:30:00Z')
      },
      {
        OrderID: 6,
        VariantID: '210422',
        Quantity: 1,
        UnitPrice: 189000000,
        createdAt: new Date('2025-02-12T13:30:00Z'),
        updatedAt: new Date('2025-02-12T13:30:00Z')
      },
      {
        OrderID: 6,
        VariantID: '209932',
        Quantity: 2,
        UnitPrice: 99000000,
        createdAt: new Date('2025-02-12T13:30:00Z'),
        updatedAt: new Date('2025-02-12T13:30:00Z')
      },
      
      // OrderID 7
      {
        OrderID: 7,
        VariantID: '163541',
        Quantity: 3,
        UnitPrice: 158000000,
        createdAt: new Date('2025-02-15T09:15:00Z'),
        updatedAt: new Date('2025-02-15T09:15:00Z')
      },
      {
        OrderID: 7,
        VariantID: '202251',
        Quantity: 1,
        UnitPrice: 162000000,
        createdAt: new Date('2025-02-15T09:15:00Z'),
        updatedAt: new Date('2025-02-15T09:15:00Z')
      },
      {
        OrderID: 7,
        VariantID: '211081',
        Quantity: 2,
        UnitPrice: 171000000,
        createdAt: new Date('2025-02-15T09:15:00Z'),
        updatedAt: new Date('2025-02-15T09:15:00Z')
      },
      {
        OrderID: 7,
        VariantID: '211711',
        Quantity: 1,
        UnitPrice: 117000000,
        createdAt: new Date('2025-02-15T09:15:00Z'),
        updatedAt: new Date('2025-02-15T09:15:00Z')
      },
      {
        OrderID: 7,
        VariantID: '205771',
        Quantity: 3,
        UnitPrice: 81000000,
        createdAt: new Date('2025-02-15T09:15:00Z'),
        updatedAt: new Date('2025-02-15T09:15:00Z')
      },
      
      // OrderID 8
      {
        OrderID: 8,
        VariantID: '202252',
        Quantity: 2,
        UnitPrice: 215000000,
        createdAt: new Date('2025-02-17T11:45:00Z'),
        updatedAt: new Date('2025-02-17T11:45:00Z')
      },
      {
        OrderID: 8,
        VariantID: '202251',
        Quantity: 1,
        UnitPrice: 162000000,
        createdAt: new Date('2025-02-17T11:45:00Z'),
        updatedAt: new Date('2025-02-17T11:45:00Z')
      },
      {
        OrderID: 8,
        VariantID: '202691',
        Quantity: 3,
        UnitPrice: 135000000,
        createdAt: new Date('2025-02-17T11:45:00Z'),
        updatedAt: new Date('2025-02-17T11:45:00Z')
      },
      
      // OrderID 9
      {
        OrderID: 9,
        VariantID: '211832',
        Quantity: 2,
        UnitPrice: 146200000,
        createdAt: new Date('2025-02-20T14:20:00Z'),
        updatedAt: new Date('2025-02-20T14:20:00Z')
      },
      {
        OrderID: 9,
        VariantID: '211831',
        Quantity: 1,
        UnitPrice: 108000000,
        createdAt: new Date('2025-02-20T14:20:00Z'),
        updatedAt: new Date('2025-02-20T14:20:00Z')
      },
      {
        OrderID: 9,
        VariantID: '203881',
        Quantity: 3,
        UnitPrice: 72000000,
        createdAt: new Date('2025-02-20T14:20:00Z'),
        updatedAt: new Date('2025-02-20T14:20:00Z')
      },
      {
        OrderID: 9,
        VariantID: '213401',
        Quantity: 2,
        UnitPrice: 72000000,
        createdAt: new Date('2025-02-20T14:20:00Z'),
        updatedAt: new Date('2025-02-20T14:20:00Z')
      },
      
      // OrderID 10
      {
        OrderID: 10,
        VariantID: '206261',
        Quantity: 1,
        UnitPrice: 259000000,
        createdAt: new Date('2025-02-22T15:50:00Z'),
        updatedAt: new Date('2025-02-22T15:50:00Z')
      },
      {
        OrderID: 10,
        VariantID: '206262',
        Quantity: 2,
        UnitPrice: 324000000,
        createdAt: new Date('2025-02-22T15:50:00Z'),
        updatedAt: new Date('2025-02-22T15:50:00Z')
      },
      {
        OrderID: 10,
        VariantID: '206271',
        Quantity: 3,
        UnitPrice: 126000000,
        createdAt: new Date('2025-02-22T15:50:00Z'),
        updatedAt: new Date('2025-02-22T15:50:00Z')
      },
      {
        OrderID: 10,
        VariantID: '210371',
        Quantity: 1,
        UnitPrice: 153000000,
        createdAt: new Date('2025-02-22T15:50:00Z'),
        updatedAt: new Date('2025-02-22T15:50:00Z')
      },
      
      // OrderID 11
      {
        OrderID: 11,
        VariantID: '189501',
        Quantity: 2,
        UnitPrice: 108000000,
        createdAt: new Date('2025-02-25T10:15:00Z'),
        updatedAt: new Date('2025-02-25T10:15:00Z')
      },
      {
        OrderID: 11,
        VariantID: '189502',
        Quantity: 3,
        UnitPrice: 144000000,
        createdAt: new Date('2025-02-25T10:15:00Z'),
        updatedAt: new Date('2025-02-25T10:15:00Z')
      },
      {
        OrderID: 11,
        VariantID: '189503',
        Quantity: 1,
        UnitPrice: 189000000,
        createdAt: new Date('2025-02-25T10:15:00Z'),
        updatedAt: new Date('2025-02-25T10:15:00Z')
      },
      {
        OrderID: 11,
        VariantID: '203531',
        Quantity: 2,
        UnitPrice: 198000000,
        createdAt: new Date('2025-02-25T10:15:00Z'),
        updatedAt: new Date('2025-02-25T10:15:00Z')
      },
      
      // OrderID 12
      {
        OrderID: 12,
        VariantID: '193251',
        Quantity: 3,
        UnitPrice: 171000000,
        createdAt: new Date('2025-03-01T14:30:00Z'),
        updatedAt: new Date('2025-03-01T14:30:00Z')
      },
      {
        OrderID: 12,
        VariantID: '193252',
        Quantity: 1,
        UnitPrice: 216000000,
        createdAt: new Date('2025-03-01T14:30:00Z'),
        updatedAt: new Date('2025-03-01T14:30:00Z')
      },
      {
        OrderID: 12,
        VariantID: '202692',
        Quantity: 2,
        UnitPrice: 171000000,
        createdAt: new Date('2025-03-01T14:30:00Z'),
        updatedAt: new Date('2025-03-01T14:30:00Z')
      },
      
      // OrderID 13
      {
        OrderID: 13,
        VariantID: '203532',
        Quantity: 1,
        UnitPrice: 261000000,
        createdAt: new Date('2025-03-05T09:45:00Z'),
        updatedAt: new Date('2025-03-05T09:45:00Z')
      },
      {
        OrderID: 13,
        VariantID: '205772',
        Quantity: 3,
        UnitPrice: 117000000,
        createdAt: new Date('2025-03-05T09:45:00Z'),
        updatedAt: new Date('2025-03-05T09:45:00Z')
      },
      {
        OrderID: 13,
        VariantID: '210372',
        Quantity: 2,
        UnitPrice: 198000000,
        createdAt: new Date('2025-03-05T09:45:00Z'),
        updatedAt: new Date('2025-03-05T09:45:00Z')
      },
      
      // OrderID 14
      {
        OrderID: 14,
        VariantID: '207241',
        Quantity: 2,
        UnitPrice: 225000000,
        createdAt: new Date('2025-03-10T15:20:00Z'),
        updatedAt: new Date('2025-03-10T15:20:00Z')
      },
      {
        OrderID: 14,
        VariantID: '207242',
        Quantity: 1,
        UnitPrice: 288000000,
        createdAt: new Date('2025-03-10T15:20:00Z'),
        updatedAt: new Date('2025-03-10T15:20:00Z')
      },
      {
        OrderID: 14,
        VariantID: '210562',
        Quantity: 3,
        UnitPrice: 117000000,
        createdAt: new Date('2025-03-10T15:20:00Z'),
        updatedAt: new Date('2025-03-10T15:20:00Z')
      },
      {
        OrderID: 14,
        VariantID: '211082',
        Quantity: 2,
        UnitPrice: 216000000,
        createdAt: new Date('2025-03-10T15:20:00Z'),
        updatedAt: new Date('2025-03-10T15:20:00Z')
      },
      
      // OrderID 15
      {
        OrderID: 15,
        VariantID: '211752',
        Quantity: 3,
        UnitPrice: 108000000,
        createdAt: new Date('2025-03-15T11:30:00Z'),
        updatedAt: new Date('2025-03-15T11:30:00Z')
      },
      {
        OrderID: 15,
        VariantID: '213101',
        Quantity: 2,
        UnitPrice: 117000000,
        createdAt: new Date('2025-03-15T11:30:00Z'),
        updatedAt: new Date('2025-03-15T11:30:00Z')
      },
      {
        OrderID: 15,
        VariantID: '213102',
        Quantity: 1,
        UnitPrice: 162000000,
        createdAt: new Date('2025-03-15T11:30:00Z'),
        updatedAt: new Date('2025-03-15T11:30:00Z')
      },
      
      // OrderID 16
      {
        OrderID: 16,
        VariantID: '213301',
        Quantity: 2,
        UnitPrice: 126000000,
        createdAt: new Date('2025-03-18T10:45:00Z'),
        updatedAt: new Date('2025-03-18T10:45:00Z')
      },
      {
        OrderID: 16,
        VariantID: '213302',
        Quantity: 3,
        UnitPrice: 153000000,
        createdAt: new Date('2025-03-18T10:45:00Z'),
        updatedAt: new Date('2025-03-18T10:45:00Z')
      },
      {
        OrderID: 16,
        VariantID: '213451',
        Quantity: 1,
        UnitPrice: 108000000,
        createdAt: new Date('2025-03-18T10:45:00Z'),
        updatedAt: new Date('2025-03-18T10:45:00Z')
      },
      
      // OrderID 17
      {
        OrderID: 17,
        VariantID: '213452',
        Quantity: 3,
        UnitPrice: 135000000,
        createdAt: new Date('2025-03-22T09:15:00Z'),
        updatedAt: new Date('2025-03-22T09:15:00Z')
      },
      {
        OrderID: 17,
        VariantID: '183513',
        Quantity: 2,
        UnitPrice: 153000000,
        createdAt: new Date('2025-03-22T09:15:00Z'),
        updatedAt: new Date('2025-03-22T09:15:00Z')
      },
      {
        OrderID: 17,
        VariantID: '163543',
        Quantity: 1,
        UnitPrice: 279000000,
        createdAt: new Date('2025-03-22T09:15:00Z'),
        updatedAt: new Date('2025-03-22T09:15:00Z')
      },
      
      // OrderID 18 - MacBook Pro 16-inch
      {
        OrderID: 18,
        VariantID: '213551',
        Quantity: 1,
        UnitPrice: 495000000,
        createdAt: new Date('2025-03-23T09:30:00Z'),
        updatedAt: new Date('2025-03-23T09:30:00Z')
      },
      
      // OrderID 19 - MacBook Pro 16-inch high-spec
      {
        OrderID: 19,
        VariantID: '213552',
        Quantity: 1,
        UnitPrice: 570000000,
        createdAt: new Date('2025-03-24T14:15:00Z'),
        updatedAt: new Date('2025-03-24T14:15:00Z')
      },
      
      // OrderID 20 - MacBook Pro 14-inch
      {
        OrderID: 20,
        VariantID: '213601',
        Quantity: 1,
        UnitPrice: 670000000,
        createdAt: new Date('2025-03-25T10:20:00Z'),
        updatedAt: new Date('2025-03-25T10:20:00Z')
      },
      
      // OrderID 21 - Bundle order
      {
        OrderID: 21,
        VariantID: '213602',
        Quantity: 1,
        UnitPrice: 770000000,
        createdAt: new Date('2025-03-27T16:45:00Z'),
        updatedAt: new Date('2025-03-27T16:45:00Z')
      },
      {
        OrderID: 21,
        VariantID: '213551',
        Quantity: 1,
        UnitPrice: 495000000,
        createdAt: new Date('2025-03-27T16:45:00Z'),
        updatedAt: new Date('2025-03-27T16:45:00Z')
      },
      {
        OrderID: 21,
        VariantID: '213552',
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
