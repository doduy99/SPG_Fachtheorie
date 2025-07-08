using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    public class Aufgabe1Test
    {
        public UserContext CreateDb() {
            var options = new DbContextOptionsBuilder()
                .UseSqlite(@"Data Source=User.db")                
                .Options;

            var db = new UserContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            return db;
        }
        [Fact]
        public void CreateDatabaseTest()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite(@"Data Source=User.db")                
                .Options;

            var db = new UserContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Assert.True(true);
        }

        [Fact]
        public void AddUserStandardSuccessTest() {
            using(var db = CreateDb()) {
                //User user = new User(vorname: "Duy", zuname: "DO", email:"doduy99@yahoo.com", startDatum: new DateTime(2023, 08, 08));
                //UserStandard userStandard = new UserStandard(user, informed : true);
                UserStandard userStandard = new UserStandard(vorname: "Duy", zuname: "DO", email: "doduy99@yahoo.com", startDatum: new DateTime(2023, 08, 08), null, true);
                UserStandard userStandard2 = new UserStandard(vorname: "Duy", zuname: "DO", email: "doduy99@yahoo.com", startDatum: new DateTime(2023, 08, 08), endDatum: new DateTime(2023, 09, 08),true);
                db.Users.Add(userStandard);
                db.SaveChanges();                  
                //Assert.True(db.Users.Select(u => u.Id).First() == 1);
                var users = db.Users;
                Assert.True(users.Count() == 1);
            }
        }

        [Fact]
        public void AddUserPremiumSuccessTest() {
            using(var db = CreateDb()) {
                UserPremium userPremium = new UserPremium(vorname: "Duy", zuname: "DO", email: "doduy99@yahoo.com", startDatum: new DateTime(2023, 08, 08), null, 35.0M, 3);
                db.Users.Add(userPremium);
                db.SaveChanges();

                //Assert.True(db.Users.Select(u => u.Id).First() == 1);
                var users = db.Users;
                Assert.True(users.Count() == 1);
            }
        }
    }
}