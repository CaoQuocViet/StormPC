'use strict';

/** @type {import('sequelize-cli').Migration} */
module.exports = {
  async up (queryInterface, Sequelize) {
    const now = new Date();
    await queryInterface.bulkInsert('Laptops', [
      {
        LaptopID: '12768',
        BrandID: 6,
        CategoryID: 3,
        Picture: '/abc/xyz/01.jpg',
        ModelName: 'LG Gram 2023 16Z90RS',
        ScreenSize: 16.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2023,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '16354',
        BrandID: 3,
        CategoryID: 2,
        Picture: '/abc/xyz/02.jpg',
        ModelName: 'Lenovo Legion Pro 5 16IRX9 2024',
        ScreenSize: 16.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '18351',
        BrandID: 3,
        CategoryID: 3,
        Picture: '/abc/xyz/03.jpg',
        ModelName: 'Lenovo Yoga 7 2-in-1 14IML9',
        ScreenSize: 14.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '18950',
        BrandID: 2,
        CategoryID: 2,
        Picture: '/abc/xyz/04.jpg',
        ModelName: 'Asus TUF Gaming A16 Advantage Edition FA617NT',
        ScreenSize: 16.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '19325',
        BrandID: 1,
        CategoryID: 2,
        Picture: '/abc/xyz/05.jpg',
        ModelName: 'Dell Alienware X14 R2',
        ScreenSize: 14.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '19927',
        BrandID: 2,
        CategoryID: 2,
        Picture: '/abc/xyz/06.jpg',
        ModelName: 'Asus ROG Strix G16 G614 Per Key RGB',
        ScreenSize: 16.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '20225',
        BrandID: 2,
        CategoryID: 2,
        Picture: '/abc/xyz/07.jpg',
        ModelName: 'Asus ROG Zephyrus G14 GA403UV',
        ScreenSize: 14.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '20269',
        BrandID: 2,
        CategoryID: 2,
        Picture: '/abc/xyz/08.jpg',
        ModelName: 'Asus ROG Strix G16 G614JU',
        ScreenSize: 16.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '20353',
        BrandID: 1,
        CategoryID: 4,
        Picture: '/abc/xyz/09.jpg',
        ModelName: 'Dell Mobile Precision Workstation 3581',
        ScreenSize: 15.6,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '20388',
        BrandID: 3,
        CategoryID: 1,
        Picture: '/abc/xyz/10.jpg',
        ModelName: 'Lenovo LOQ Essential 15IAX9E',
        ScreenSize: 15.6,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '20577',
        BrandID: 4,
        CategoryID: 2,
        Picture: '/abc/xyz/11.jpg',
        ModelName: 'HP Victus 16 R0223TX',
        ScreenSize: 16.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '20626',
        BrandID: 9,
        CategoryID: 2,
        Picture: '/abc/xyz/12.jpg',
        ModelName: 'Gigabyte AORUS 16X 9KG',
        ScreenSize: 16.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '20627',
        BrandID: 9,
        CategoryID: 2,
        Picture: '/abc/xyz/13.jpg',
        ModelName: 'Gigabyte G6 KF',
        ScreenSize: 15.6,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '20692',
        BrandID: 8,
        CategoryID: 2,
        Picture: '/abc/xyz/14.jpg',
        ModelName: 'MSI Titan 18 HX AI A2XWJG 034VN Dragon Edition',
        ScreenSize: 18.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '20724',
        BrandID: 2,
        CategoryID: 2,
        Picture: '/abc/xyz/15.jpg',
        ModelName: 'Asus ROG Zephyrus G16 GA605',
        ScreenSize: 16.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '20993',
        BrandID: 3,
        CategoryID: 1,
        Picture: '/abc/xyz/16.jpg',
        ModelName: 'Lenovo Ideapad 5 Xiaoxin Pro 14 AMD',
        ScreenSize: 14.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '21037',
        BrandID: 4,
        CategoryID: 5,
        Picture: '/abc/xyz/17.jpg',
        ModelName: 'HP Omen Transcend 14',
        ScreenSize: 14.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '21042',
        BrandID: 7,
        CategoryID: 5,
        Picture: '/abc/xyz/18.jpg',
        ModelName: 'Acer Predator Helios Neo 14 PHN14 51',
        ScreenSize: 14.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '21056',
        BrandID: 4,
        CategoryID: 2,
        Picture: '/abc/xyz/19.jpg',
        ModelName: 'HP Victus 16 R1190TX',
        ScreenSize: 16.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '21108',
        BrandID: 7,
        CategoryID: 5,
        Picture: '/abc/xyz/20.jpg',
        ModelName: 'Acer Predator Helios Neo 16S AI',
        ScreenSize: 16.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '21171',
        BrandID: 3,
        CategoryID: 3,
        Picture: '/abc/xyz/21.jpg',
        ModelName: 'Lenovo Yoga Slim 9 14ILL10',
        ScreenSize: 14.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '21175',
        BrandID: 3,
        CategoryID: 3,
        Picture: '/abc/xyz/22.jpg',
        ModelName: 'Lenovo Yoga Slim 7 14ILL10',
        ScreenSize: 14.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '21183',
        BrandID: 3,
        CategoryID: 2,
        Picture: '/abc/xyz/23.jpg',
        ModelName: 'Lenovo Legion 5 R7000 APH9',
        ScreenSize: 15.6,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '21253',
        BrandID: 8,
        CategoryID: 2,
        Picture: '/abc/xyz/24.jpg',
        ModelName: 'MSI Titan 18 HX AI A2XW',
        ScreenSize: 18.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '21302',
        BrandID: 8,
        CategoryID: 2,
        Picture: '/abc/xyz/25.jpg',
        ModelName: 'MSI Raider GE78 HX AI A2XW',
        ScreenSize: 17.3,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '21310',
        BrandID: 8,
        CategoryID: 2,
        Picture: '/abc/xyz/26.jpg',
        ModelName: 'MSI Katana A15 AI B8V',
        ScreenSize: 15.6,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '21330',
        BrandID: 1,
        CategoryID: 2,
        Picture: '/abc/xyz/27.jpg',
        ModelName: 'Dell Gaming G15 5530 i7H165W11GR4060',
        ScreenSize: 15.6,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '21340',
        BrandID: 1,
        CategoryID: 3,
        Picture: '/abc/xyz/28.jpg',
        ModelName: 'Dell Latitude 3450',
        ScreenSize: 14.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      },
      {
        LaptopID: '21345',
        BrandID: 3,
        CategoryID: 3,
        Picture: '/abc/xyz/29.jpg',
        ModelName: 'Lenovo ThinkPad T14s Gen 5',
        ScreenSize: 14.0,
        OperatingSystem: 'Windows 11',
        ReleaseYear: 2024,
        createdAt: now,
        updatedAt: now
      }
    ], {});
  },

  async down (queryInterface, Sequelize) {
    await queryInterface.bulkDelete('Laptops', null, {});
  }
};
