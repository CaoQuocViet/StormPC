'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    await queryInterface.bulkInsert('Cities', [
      {
        id: 1,
        CityCode: 'HCM',
        CityName: 'Hồ Chí Minh',
        createdAt: new Date('2025-01-03T09:00:00Z'),
        updatedAt: new Date('2025-01-03T09:00:00Z')
      },
      {
        id: 2,
        CityCode: 'HAN',
        CityName: 'Hà Nội',
        createdAt: new Date('2025-01-03T09:05:00Z'),
        updatedAt: new Date('2025-01-03T09:05:00Z')
      },
      {
        id: 3,
        CityCode: 'DAN',
        CityName: 'Đà Nẵng',
        createdAt: new Date('2025-01-03T09:10:00Z'),
        updatedAt: new Date('2025-01-03T09:10:00Z')
      },
      {
        id: 4,
        CityCode: 'CTO',
        CityName: 'Cần Thơ',
        createdAt: new Date('2025-01-03T09:15:00Z'),
        updatedAt: new Date('2025-01-03T09:15:00Z')
      },
      {
        id: 5,
        CityCode: 'HAP',
        CityName: 'Hải Phòng',
        createdAt: new Date('2025-01-03T09:20:00Z'),
        updatedAt: new Date('2025-01-03T09:20:00Z')
      },
      {
        id: 6,
        CityCode: 'NTH',
        CityName: 'Nha Trang',
        createdAt: new Date('2025-01-03T09:25:00Z'),
        updatedAt: new Date('2025-01-03T09:25:00Z')
      },
      {
        id: 7,
        CityCode: 'BMT',
        CityName: 'Buôn Ma Thuột',
        createdAt: new Date('2025-01-03T09:30:00Z'),
        updatedAt: new Date('2025-01-03T09:30:00Z')
      },
      {
        id: 8,
        CityCode: 'HUE',
        CityName: 'Huế',
        createdAt: new Date('2025-01-03T09:35:00Z'),
        updatedAt: new Date('2025-01-03T09:35:00Z')
      },
      {
        id: 9,
        CityCode: 'VUT',
        CityName: 'Vũng Tàu',
        createdAt: new Date('2025-01-03T09:40:00Z'),
        updatedAt: new Date('2025-01-03T09:40:00Z')
      },
      {
        id: 10,
        CityCode: 'QNH',
        CityName: 'Quy Nhơn',
        createdAt: new Date('2025-01-03T09:45:00Z'),
        updatedAt: new Date('2025-01-03T09:45:00Z')
      },
      {
        id: 11,
        CityCode: 'TNH',
        CityName: 'Thái Nguyên',
        createdAt: new Date('2025-01-03T09:50:00Z'),
        updatedAt: new Date('2025-01-03T09:50:00Z')
      },
      {
        id: 12,
        CityCode: 'BDG',
        CityName: 'Bình Dương',
        createdAt: new Date('2025-01-03T09:55:00Z'),
        updatedAt: new Date('2025-01-03T09:55:00Z')
      },
      {
        id: 13,
        CityCode: 'DLT',
        CityName: 'Đà Lạt',
        createdAt: new Date('2025-01-03T10:00:00Z'),
        updatedAt: new Date('2025-01-03T10:00:00Z')
      },
      {
        id: 14,
        CityCode: 'PTO',
        CityName: 'Phan Thiết',
        createdAt: new Date('2025-01-03T10:05:00Z'),
        updatedAt: new Date('2025-01-03T10:05:00Z')
      },
      {
        id: 15,
        CityCode: 'PLK',
        CityName: 'Pleiku',
        createdAt: new Date('2025-01-03T10:10:00Z'),
        updatedAt: new Date('2025-01-03T10:10:00Z')
      },
      {
        id: 16,
        CityCode: 'HUI',
        CityName: 'Hưng Yên',
        createdAt: new Date('2025-01-03T10:15:00Z'),
        updatedAt: new Date('2025-01-03T10:15:00Z')
      },
      {
        id: 17,
        CityCode: 'BNH',
        CityName: 'Bắc Ninh',
        createdAt: new Date('2025-01-03T10:20:00Z'),
        updatedAt: new Date('2025-01-03T10:20:00Z')
      },
      {
        id: 18,
        CityCode: 'AGG',
        CityName: 'An Giang',
        createdAt: new Date('2025-01-03T10:25:00Z'),
        updatedAt: new Date('2025-01-03T10:25:00Z')
      },
      {
        id: 19,
        CityCode: 'BRV',
        CityName: 'Bà Rịa - Vũng Tàu',
        createdAt: new Date('2025-01-03T10:30:00Z'),
        updatedAt: new Date('2025-01-03T10:30:00Z')
      },
      {
        id: 20,
        CityCode: 'BGG',
        CityName: 'Bắc Giang',
        createdAt: new Date('2025-01-03T10:35:00Z'),
        updatedAt: new Date('2025-01-03T10:35:00Z')
      },
      {
        id: 21,
        CityCode: 'BKN',
        CityName: 'Bắc Kạn',
        createdAt: new Date('2025-01-03T10:40:00Z'),
        updatedAt: new Date('2025-01-03T10:40:00Z')
      },
      {
        id: 22,
        CityCode: 'BLU',
        CityName: 'Bạc Liêu',
        createdAt: new Date('2025-01-03T10:45:00Z'),
        updatedAt: new Date('2025-01-03T10:45:00Z')
      },
      {
        id: 23,
        CityCode: 'BTR',
        CityName: 'Bến Tre',
        createdAt: new Date('2025-01-03T10:50:00Z'),
        updatedAt: new Date('2025-01-03T10:50:00Z')
      },
      {
        id: 24,
        CityCode: 'BDH',
        CityName: 'Bình Định',
        createdAt: new Date('2025-01-03T10:55:00Z'),
        updatedAt: new Date('2025-01-03T10:55:00Z')
      },
      {
        id: 25,
        CityCode: 'BPC',
        CityName: 'Bình Phước',
        createdAt: new Date('2025-01-03T11:00:00Z'),
        updatedAt: new Date('2025-01-03T11:00:00Z')
      },
      {
        id: 26,
        CityCode: 'BTN',
        CityName: 'Bình Thuận',
        createdAt: new Date('2025-01-03T11:05:00Z'),
        updatedAt: new Date('2025-01-03T11:05:00Z')
      },
      {
        id: 27,
        CityCode: 'CMU',
        CityName: 'Cà Mau',
        createdAt: new Date('2025-01-03T11:10:00Z'),
        updatedAt: new Date('2025-01-03T11:10:00Z')
      },
      {
        id: 28,
        CityCode: 'CBG',
        CityName: 'Cao Bằng',
        createdAt: new Date('2025-01-03T11:15:00Z'),
        updatedAt: new Date('2025-01-03T11:15:00Z')
      },
      {
        id: 29,
        CityCode: 'DLK',
        CityName: 'Đắk Lắk',
        createdAt: new Date('2025-01-03T11:20:00Z'),
        updatedAt: new Date('2025-01-03T11:20:00Z')
      },
      {
        id: 30,
        CityCode: 'DNO',
        CityName: 'Đắk Nông',
        createdAt: new Date('2025-01-03T11:25:00Z'),
        updatedAt: new Date('2025-01-03T11:25:00Z')
      },
      {
        id: 31,
        CityCode: 'DBN',
        CityName: 'Điện Biên',
        createdAt: new Date('2025-01-03T11:30:00Z'),
        updatedAt: new Date('2025-01-03T11:30:00Z')
      },
      {
        id: 32,
        CityCode: 'DNI',
        CityName: 'Đồng Nai',
        createdAt: new Date('2025-01-03T11:35:00Z'),
        updatedAt: new Date('2025-01-03T11:35:00Z')
      },
      {
        id: 33,
        CityCode: 'DTP',
        CityName: 'Đồng Tháp',
        createdAt: new Date('2025-01-03T11:40:00Z'),
        updatedAt: new Date('2025-01-03T11:40:00Z')
      },
      {
        id: 34,
        CityCode: 'GLA',
        CityName: 'Gia Lai',
        createdAt: new Date('2025-01-03T11:45:00Z'),
        updatedAt: new Date('2025-01-03T11:45:00Z')
      },
      {
        id: 35,
        CityCode: 'HGG',
        CityName: 'Hà Giang',
        createdAt: new Date('2025-01-03T11:50:00Z'),
        updatedAt: new Date('2025-01-03T11:50:00Z')
      },
      {
        id: 36,
        CityCode: 'HNM',
        CityName: 'Hà Nam',
        createdAt: new Date('2025-01-03T11:55:00Z'),
        updatedAt: new Date('2025-01-03T11:55:00Z')
      },
      {
        id: 37,
        CityCode: 'HTH',
        CityName: 'Hà Tĩnh',
        createdAt: new Date('2025-01-03T12:00:00Z'),
        updatedAt: new Date('2025-01-03T12:00:00Z')
      },
      {
        id: 38,
        CityCode: 'HDG',
        CityName: 'Hải Dương',
        createdAt: new Date('2025-01-03T12:05:00Z'),
        updatedAt: new Date('2025-01-03T12:05:00Z')
      },
      {
        id: 39,
        CityCode: 'HGG',
        CityName: 'Hậu Giang',
        createdAt: new Date('2025-01-03T12:10:00Z'),
        updatedAt: new Date('2025-01-03T12:10:00Z')
      },
      {
        id: 40,
        CityCode: 'HBH',
        CityName: 'Hòa Bình',
        createdAt: new Date('2025-01-03T12:15:00Z'),
        updatedAt: new Date('2025-01-03T12:15:00Z')
      },
      {
        id: 41,
        CityCode: 'KHA',
        CityName: 'Khánh Hòa',
        createdAt: new Date('2025-01-03T12:20:00Z'),
        updatedAt: new Date('2025-01-03T12:20:00Z')
      },
      {
        id: 42,
        CityCode: 'KGG',
        CityName: 'Kiên Giang',
        createdAt: new Date('2025-01-03T12:25:00Z'),
        updatedAt: new Date('2025-01-03T12:25:00Z')
      },
      {
        id: 43,
        CityCode: 'KTM',
        CityName: 'Kon Tum',
        createdAt: new Date('2025-01-03T12:30:00Z'),
        updatedAt: new Date('2025-01-03T12:30:00Z')
      },
      {
        id: 44,
        CityCode: 'LCU',
        CityName: 'Lai Châu',
        createdAt: new Date('2025-01-03T12:35:00Z'),
        updatedAt: new Date('2025-01-03T12:35:00Z')
      },
      {
        id: 45,
        CityCode: 'LDG',
        CityName: 'Lâm Đồng',
        createdAt: new Date('2025-01-03T12:40:00Z'),
        updatedAt: new Date('2025-01-03T12:40:00Z')
      },
      {
        id: 46,
        CityCode: 'LSN',
        CityName: 'Lạng Sơn',
        createdAt: new Date('2025-01-03T12:45:00Z'),
        updatedAt: new Date('2025-01-03T12:45:00Z')
      },
      {
        id: 47,
        CityCode: 'LCI',
        CityName: 'Lào Cai',
        createdAt: new Date('2025-01-03T12:50:00Z'),
        updatedAt: new Date('2025-01-03T12:50:00Z')
      },
      {
        id: 48,
        CityCode: 'LAN',
        CityName: 'Long An',
        createdAt: new Date('2025-01-03T12:55:00Z'),
        updatedAt: new Date('2025-01-03T12:55:00Z')
      },
      {
        id: 49,
        CityCode: 'NDH',
        CityName: 'Nam Định',
        createdAt: new Date('2025-01-03T13:00:00Z'),
        updatedAt: new Date('2025-01-03T13:00:00Z')
      },
      {
        id: 50,
        CityCode: 'NAN',
        CityName: 'Nghệ An',
        createdAt: new Date('2025-01-03T13:05:00Z'),
        updatedAt: new Date('2025-01-03T13:05:00Z')
      },
      {
        id: 51,
        CityCode: 'NBH',
        CityName: 'Ninh Bình',
        createdAt: new Date('2025-01-03T13:10:00Z'),
        updatedAt: new Date('2025-01-03T13:10:00Z')
      },
      {
        id: 52,
        CityCode: 'NTN',
        CityName: 'Ninh Thuận',
        createdAt: new Date('2025-01-03T13:15:00Z'),
        updatedAt: new Date('2025-01-03T13:15:00Z')
      },
      {
        id: 53,
        CityCode: 'PTO',
        CityName: 'Phú Thọ',
        createdAt: new Date('2025-01-03T13:20:00Z'),
        updatedAt: new Date('2025-01-03T13:20:00Z')
      },
      {
        id: 54,
        CityCode: 'PYN',
        CityName: 'Phú Yên',
        createdAt: new Date('2025-01-03T13:25:00Z'),
        updatedAt: new Date('2025-01-03T13:25:00Z')
      },
      {
        id: 55,
        CityCode: 'QBH',
        CityName: 'Quảng Bình',
        createdAt: new Date('2025-01-03T13:30:00Z'),
        updatedAt: new Date('2025-01-03T13:30:00Z')
      },
      {
        id: 56,
        CityCode: 'QNM',
        CityName: 'Quảng Nam',
        createdAt: new Date('2025-01-03T13:35:00Z'),
        updatedAt: new Date('2025-01-03T13:35:00Z')
      },
      {
        id: 57,
        CityCode: 'QNI',
        CityName: 'Quảng Ngãi',
        createdAt: new Date('2025-01-03T13:40:00Z'),
        updatedAt: new Date('2025-01-03T13:40:00Z')
      },
      {
        id: 58,
        CityCode: 'QNH',
        CityName: 'Quảng Ninh',
        createdAt: new Date('2025-01-03T13:45:00Z'),
        updatedAt: new Date('2025-01-03T13:45:00Z')
      },
      {
        id: 59,
        CityCode: 'QTI',
        CityName: 'Quảng Trị',
        createdAt: new Date('2025-01-03T13:50:00Z'),
        updatedAt: new Date('2025-01-03T13:50:00Z')
      },
      {
        id: 60,
        CityCode: 'STG',
        CityName: 'Sóc Trăng',
        createdAt: new Date('2025-01-03T13:55:00Z'),
        updatedAt: new Date('2025-01-03T13:55:00Z')
      },
      {
        id: 61,
        CityCode: 'SLA',
        CityName: 'Sơn La',
        createdAt: new Date('2025-01-03T14:00:00Z'),
        updatedAt: new Date('2025-01-03T14:00:00Z')
      },
      {
        id: 62,
        CityCode: 'TNH',
        CityName: 'Tây Ninh',
        createdAt: new Date('2025-01-03T14:05:00Z'),
        updatedAt: new Date('2025-01-03T14:05:00Z')
      },
      {
        id: 63,
        CityCode: 'TBH',
        CityName: 'Thái Bình',
        createdAt: new Date('2025-01-03T14:10:00Z'),
        updatedAt: new Date('2025-01-03T14:10:00Z')
      },
      {
        id: 64,
        CityCode: 'THA',
        CityName: 'Thanh Hóa',
        createdAt: new Date('2025-01-03T14:15:00Z'),
        updatedAt: new Date('2025-01-03T14:15:00Z')
      },
      {
        id: 65,
        CityCode: 'TTH',
        CityName: 'Thừa Thiên Huế',
        createdAt: new Date('2025-01-03T14:20:00Z'),
        updatedAt: new Date('2025-01-03T14:20:00Z')
      },
      {
        id: 66,
        CityCode: 'TGG',
        CityName: 'Tiền Giang',
        createdAt: new Date('2025-01-03T14:25:00Z'),
        updatedAt: new Date('2025-01-03T14:25:00Z')
      },
      {
        id: 67,
        CityCode: 'TVH',
        CityName: 'Trà Vinh',
        createdAt: new Date('2025-01-03T14:30:00Z'),
        updatedAt: new Date('2025-01-03T14:30:00Z')
      },
      {
        id: 68,
        CityCode: 'TQG',
        CityName: 'Tuyên Quang',
        createdAt: new Date('2025-01-03T14:35:00Z'),
        updatedAt: new Date('2025-01-03T14:35:00Z')
      },
      {
        id: 69,
        CityCode: 'VLG',
        CityName: 'Vĩnh Long',
        createdAt: new Date('2025-01-03T14:40:00Z'),
        updatedAt: new Date('2025-01-03T14:40:00Z')
      },
      {
        id: 70,
        CityCode: 'VPC',
        CityName: 'Vĩnh Phúc',
        createdAt: new Date('2025-01-03T14:45:00Z'),
        updatedAt: new Date('2025-01-03T14:45:00Z')
      },
      {
        id: 71,
        CityCode: 'YBI',
        CityName: 'Yên Bái',
        createdAt: new Date('2025-01-03T14:50:00Z'),
        updatedAt: new Date('2025-01-03T14:50:00Z')
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('Cities', null, {});
  }
};
