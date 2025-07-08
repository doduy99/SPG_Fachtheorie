using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SPG_Fachtheorie.Aufgabe2;
using Microsoft.EntityFrameworkCore;

namespace SPG_Fachtheorie.Aufgabe2.Test
{
    [Collection("Sequential")]
    public class AppointmentServiceTests
    {
        /// <summary>
        /// Legt die Datenbank an und befüllt sie mit Musterdaten. Die Datenbank ist
        /// nach Ausführen des Tests ServiceClassSuccessTest in
        /// SPG_Fachtheorie\SPG_Fachtheorie.Aufgabe2.Test\bin\Debug\net6.0\Appointment.db
        /// und kann mit SQLite Manager, DBeaver, ... betrachtet werden.
        /// </summary>
        private AppointmentContext GetAppointmentContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite("Data Source=Appointment.db")
                .Options;

            var db = new AppointmentContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Seed();
            return db;
        }
        [Fact]
        public void ServiceClassSuccessTest()
        {
            using var db = GetAppointmentContext();
            Assert.True(db.Students.Count() > 0);
            var service = new AppointmentService(db);
        }
        [Fact]
        public void AskForAppointmentSuccessTest()
        {
            using (var db = GetAppointmentContext()) {
                var service = new AppointmentService(db);
                var result = service.AskForAppointment(new Guid("0285DE34EDCC033040E2306B827C3B1A"),
                    new Guid("22C407046FC267CC9BB58A98E11C845B"), new DateTime(2021, 12, 20));

                Assert.True(db.Appointments.Count() == 41);
            }
        }
        [Fact]
        public void AskForAppointmentReturnsFalseIfNoOfferExists()
        {
            using(var db = GetAppointmentContext()) {
                var service = new AppointmentService(db);
                var result = service.AskForAppointment(new Guid("0285DE34EDCC033040E2306A827C3B1A"),
                    new Guid("22C407046FC267CC9BB58A98E11C845B"), new DateTime(2021, 12, 20));

                Assert.True(result == false);

            }
        }
        [Fact]
        public void AskForAppointmentReturnsFalseIfOutOfDate() {
            using (var db = GetAppointmentContext()) {
                var service = new AppointmentService(db);
                var result = service.AskForAppointment(new Guid("0285DE34EDCC033040E2306B827C3B1A"),
                    new Guid("22C407046FC267CC9BB58A98E11C845B"), new DateTime(2021, 11, 20));

                Assert.True(result == false);

            }
        }
        [Fact]
        public void ConfirmAppointmentSuccessTest()
        {
            using(var db = GetAppointmentContext()) {
                var service = new AppointmentService(db);
                var result = service.ConfirmAppointment(new Guid("F68B5B4224EB69BA05CAC964A9F1BF1D"));

                Assert.True(result == true);
            }
        }
        [Fact]
        public void ConfirmAppointmentReturnsFalseIfStateIsInvalid()
        {
            using (var db = GetAppointmentContext()) {
                var service = new AppointmentService(db);
                var result = service.ConfirmAppointment(new Guid("33A7313D10AA3EE7CE393E3A6BF72E53"));

                Assert.True(result == false);
            }
        }
        [Fact]
        public void CancelAppointmentStudentSuccessTest()
        {
            using(var db = GetAppointmentContext()) {
                var service = new AppointmentService(db);
                var result = service.CancelAppointment(new Guid("012E1154DD8974C19E62869F3AAD2975"),
                    new Guid("610007DA9AB03AF171D86E273282F63F"));

                Assert.True(result == true);
            }
        }
        [Fact]
        public void CancelAppointmentCoachSuccessTest()
        {
            using (var db = GetAppointmentContext()) {
                var service = new AppointmentService(db);
                var result = service.CancelAppointment(new Guid("11CC14E241929A3F9E20005186ED8273"),
                    new Guid("D48663A6B51ACDF2F78984188E4CE05E"));

                Assert.True(result == true);
            }
        }
        [Fact]
        public void ConfirmAppointmentStudentReturnsFalseIfStateIsInvalid()
        {
            using(var db = GetAppointmentContext()) {
                var service = new AppointmentService(db);
                var result = service.CancelAppointment(new Guid("11CC14E241929A3F9E20005186ED8273"),
                    new Guid("C5B4B3A0EF45D12B0471C9C387C286D4"));

                Assert.False(result == false);
            }
        }
        [Fact]
        public void ConfirmAppointmentCoachReturnsFalseIfStateIsInvalid()
        {
            using(var db = GetAppointmentContext()) {
                var service = new AppointmentService(db);
                var result = service.CancelAppointment(new Guid("11F0D56632E540744003BE651297C1EA"),
                    new Guid("5C2B10B7E7DB6DC0D85FB026E236AD88"));

                Assert.False(result == false);
            }

        }
    }
}
