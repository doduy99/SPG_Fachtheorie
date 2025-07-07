using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Model;
using SPG_Fachtheorie.Aufgabe2.Services;
using System;
using System.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe2.Test
{
    [Collection("Sequential")]
    public class StickerServiceTests
    {
        /// <summary>
        /// Generates database in C:\Scratch\SPG_Fachtheorie.Aufgabe2.Test\Debug\net6.0\sticker.db
        /// </summary>
        private StickerContext GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite(@"Data Source=sticker.db")
                .Options;

            var db = new StickerContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        private StickerContext GetSeededDbContext()
        {
            var db = GetEmptyDbContext();
            db.Seed();
            return db;
        }

        [Fact]
        public void CreateDatabaseTest()
        {
            using var db = GetSeededDbContext();
            db.ChangeTracker.Clear();
            Assert.True(db.StickerTypes.Any());
        }

        [Fact]
        public void HasPermissionReturnsFalseIfNumberplateIsInvalidTest()
        {
            using(var db = GetSeededDbContext()) {
                db.ChangeTracker.Clear();

                var service = new StickerService(db);
                bool numberplate = service.HasPermission("OW 11360E", new DateTime(2023, 10, 25), VehicleType.PassengerCar);

                Assert.True(numberplate == false);
            }
        }

        [Fact]
        public void HasPermissionReturnsFalseIfCarTypeIsInvalidTest()
        {
            using(var db = GetSeededDbContext()) {
                db.ChangeTracker.Clear();

                var service = new StickerService(db);
                bool vehicle = service.HasPermission("OW 11360I", new DateTime(2023, 10, 25), VehicleType.Motorcycle);

                Assert.True(vehicle == false);
            }
        }

        [Fact]
        public void HasPermissionReturnsFalseIfDateTimeNotInValidTimespanTest()
        {
            using (var db = GetSeededDbContext()) {
                db.ChangeTracker.Clear();

                var service = new StickerService(db);
                bool dt = service.HasPermission("OW 11360I", new DateTime(2023, 09, 25), VehicleType.PassengerCar);

                Assert.True(dt == false);
            }
        }

        [Fact]
        public void HasPermissionReturnsTrueIfSuccessTest()
        {
            using (var db = GetSeededDbContext()) {
                db.ChangeTracker.Clear();

                var service = new StickerService(db);
                bool  sticker = service.HasPermission("OW 11360I", new DateTime(2023, 10, 27), VehicleType.PassengerCar);

                Assert.True(sticker == true);
            }
        }

        /// <summary>
        /// Vorgegebener Test für die Methode CalcSaleStatistics.
        /// Hier muss nichts geändert werden, sie ist zur Kontrolle deiner Implementierung
        /// von CalcSaleStatistics da.
        /// </summary>
        [Fact]
        public void CalcSaleStatisticsTest()
        {
            // SELECT st.Name AS StickerTypeName, SUM(s.Price) AS TotalRevenue
            // FROM Stickers s INNER JOIN StickerTypes st ON(s.StickerTypeId = st.Id)
            // WHERE strftime('%Y', s.PurchaseDate) == '2023'
            // GROUP BY st.Name;
            using var db = GetSeededDbContext();
            var service = new StickerService(db);
            var saleStatistics = service.CalcSaleStatistics(2023);
            Assert.True(saleStatistics.Count == 6);
            Assert.True(saleStatistics.First(s => s.StickerTypeName == "10-Tages-Vignette Motorrad").TotalRevenue == 11.6M);
            Assert.True(saleStatistics.First(s => s.StickerTypeName == "10-Tages-Vignette PKW").TotalRevenue == 9.9M);
            Assert.True(saleStatistics.First(s => s.StickerTypeName == "2-Monats-Vignette Motorrad").TotalRevenue == 29.0M);
            Assert.True(saleStatistics.First(s => s.StickerTypeName == "2-Monats-Vignette PKW").TotalRevenue == 116.0M);
            Assert.True(saleStatistics.First(s => s.StickerTypeName == "Jahres-Vignette Motorrad").TotalRevenue == 38.2M);
            Assert.True(saleStatistics.First(s => s.StickerTypeName == "Jahres-Vignette PKW").TotalRevenue == 192.8M);
        }
    }
}