using SPG_Fachtheorie.Aufgabe2.Infrastructure;
using SPG_Fachtheorie.Aufgabe2.Services;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe2.Test
{
    public class DatabaseContextTest
    {
        private PodcastContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite(@"Data Source=Podcast.db")
                .Options;

            var db = new PodcastContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Seed();
            return db;
        }


        [Fact()]
        public void CalcTotalCosts_Invalid_CustomerId()
        {
            using(var db = GetDbContext()) {
                var service = new PodcastService(db);
                bool calc = service.CalcTotalCosts(31, new DateTime(2023, 08, 08), new DateTime(2023, 08, 11));
                Assert.True(calc == false);
            }
            //throw new NotImplementedException("Noch keine Implementierung vorhanden");
        }
        [Fact()]
        public void CalcTotalCosts_TotalCosts_Already_Calculated()
        {
            using (var db = GetDbContext()) {
                var service = new PodcastService(db);
                bool calc = service.CalcTotalCosts(7, new DateTime(2022, 08, 04), new DateTime(2022, 12, 18));
                Assert.True(calc == false);
            }
            //throw new NotImplementedException("Noch keine Implementierung vorhanden");
        }
        [Fact()]
        public void CalcTotalCosts_Invalid_TimePeriod()
        {
            using (var db = GetDbContext()) {
                var service = new PodcastService(db);
                bool calc = service.CalcTotalCosts(14, new DateTime(2022, 08, 04), new DateTime(2022, 12, 05));
                Assert.True(calc == false);
            }
            //throw new NotImplementedException("Noch keine Implementierung vorhanden");
        }
        [Fact()]
        public void CalcTotalCosts_No_Advertisements()
        {
            using (var db = GetDbContext()) {
                var service = new PodcastService(db);
                bool calc = service.CalcTotalCosts(14, new DateTime(2022, 12, 05), new DateTime(2022, 12, 07));
                Assert.True(calc == false);
            }
            //throw new NotImplementedException("Noch keine Implementierung vorhanden");
        }
        [Fact()]
        public void CalcTotalCosts_Success()
        {
            using (var db = GetDbContext()) {
                var service = new PodcastService(db);
                bool calc = service.CalcTotalCosts(14, new DateTime(2022, 08, 01), new DateTime(2022, 12, 05));
                Assert.True(calc == true);
            }
            //throw new NotImplementedException("Noch keine Implementierung vorhanden");
        }
    }
}