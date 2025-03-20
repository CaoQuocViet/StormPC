'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    const now = new Date();
    await queryInterface.bulkInsert('PaymentMethods', [
      {
        PaymentMethodID: 1,
        MethodName: 'Thẻ ATM nội địa',
        createdAt: now,
        updatedAt: now
      },
      {
        PaymentMethodID: 2,
        MethodName: 'Ví MoMo',
        createdAt: now,
        updatedAt: now
      },
      {
        PaymentMethodID: 3,
        MethodName: 'VNPay',
        createdAt: now,
        updatedAt: now
      },
      {
        PaymentMethodID: 4,
        MethodName: 'ZaloPay',
        createdAt: now,
        updatedAt: now
      },
      {
        PaymentMethodID: 5,
        MethodName: 'Thẻ tín dụng/ghi nợ',
        createdAt: now,
        updatedAt: now
      },
      {
        PaymentMethodID: 6,
        MethodName: 'Chuyển khoản ngân hàng',
        createdAt: now,
        updatedAt: now
      },
      {
        PaymentMethodID: 7,
        MethodName: 'Thanh toán khi nhận hàng (COD)',
        createdAt: now,
        updatedAt: now
      },
      {
        PaymentMethodID: 8,
        MethodName: 'ShopeePay',
        createdAt: now,
        updatedAt: now
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('PaymentMethods', null, {});
  }
};
