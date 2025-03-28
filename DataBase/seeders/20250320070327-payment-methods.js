'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    await queryInterface.bulkInsert('PaymentMethods', [
      {
        PaymentMethodID: 1,
        MethodName: 'Thẻ ATM nội địa',
        createdAt: new Date('2025-01-08T11:00:00Z'),
        updatedAt: new Date('2025-01-08T11:00:00Z')
      },
      {
        PaymentMethodID: 2,
        MethodName: 'Ví MoMo',
        createdAt: new Date('2025-01-08T11:05:00Z'),
        updatedAt: new Date('2025-01-08T11:05:00Z')
      },
      {
        PaymentMethodID: 3,
        MethodName: 'VNPay',
        createdAt: new Date('2025-01-08T11:10:00Z'),
        updatedAt: new Date('2025-01-08T11:10:00Z')
      },
      {
        PaymentMethodID: 4,
        MethodName: 'ZaloPay',
        createdAt: new Date('2025-01-08T11:15:00Z'),
        updatedAt: new Date('2025-01-08T11:15:00Z')
      },
      {
        PaymentMethodID: 5,
        MethodName: 'Thẻ tín dụng/ghi nợ',
        createdAt: new Date('2025-01-08T11:20:00Z'),
        updatedAt: new Date('2025-01-08T11:20:00Z')
      },
      {
        PaymentMethodID: 6,
        MethodName: 'Chuyển khoản ngân hàng',
        createdAt: new Date('2025-01-08T11:25:00Z'),
        updatedAt: new Date('2025-01-08T11:25:00Z')
      },
      {
        PaymentMethodID: 7,
        MethodName: 'Thanh toán khi nhận hàng (COD)',
        createdAt: new Date('2025-01-08T11:30:00Z'),
        updatedAt: new Date('2025-01-08T11:30:00Z')
      },
      {
        PaymentMethodID: 8,
        MethodName: 'ShopeePay',
        createdAt: new Date('2025-01-08T11:35:00Z'),
        updatedAt: new Date('2025-01-08T11:35:00Z')
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('PaymentMethods', null, {});
  }
};
