using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    [Collection("Sequential")]
    public class Aufgabe1Test
    {
        /// <summary>
        /// Generates database in C:\Scratch\SPG_Fachtheorie.Aufgabe1.Test\Debug\net6.0\appointments.db
        /// </summary>
        private AppointmentContext GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite(@"Data Source=appointments.db")
                .Options;

            var db = new AppointmentContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void CreateDatabaseTest()
        {
            using var db = GetEmptyDbContext();
        }

        [Fact]
        public void AddPatientSuccessTest()
        {
            using var db = GetEmptyDbContext();

            Patient patient = new Patient("Duy", "Do", new Address("Spengergasse", 1050, "Wien"), "do210070@spengeergasse.at", "+43123456789");
            db.Add(patient);
            db.SaveChanges();

            Assert.True(db.Patients.Count() == 1);
        }

        [Fact]
        public void AddAppointmentSuccessTest()
        {
            using var db = GetEmptyDbContext();

            Patient patient = new Patient("Duy", "Do", new Address("Spengergasse", 1050, "Wien"), "do210070@spengeergasse.at", "+43123456789");
            Appointment appointment = new Appointment(new DateTime(2023, 11, 30), new TimeSpan(30), DateTime.Now, patient, null);
            db.Add(appointment);
            db.SaveChanges();

            Assert.True(db.Appointments.Count() == 1);
        }

        [Fact]
        public void ChangeAppointmentStateToConfirmedSuccessTest()
        {
            using var db = GetEmptyDbContext();

            Patient patient = new Patient("Duy", "Do", new Address("Spengergasse", 1050, "Wien"), "do210070@spengeergasse.at", "+43123456789");
            Appointment appointment = new Appointment(new DateTime(2023, 11, 30), new TimeSpan(30), DateTime.Now, patient, null);
            db.Add(appointment);
            db.SaveChanges();

            ConfirmedAppointmentState confirmedAppointmentState = new ConfirmedAppointmentState(DateTime.Now, appointment, new TimeSpan(30), "Kommen Sie bitte pünktlich");
            //Appointment appointment = new Appointment(new DateTime(2023, 11, 30), new TimeSpan(30), DateTime.Now, patient, confirmedAppointmentState);
            db.Update(confirmedAppointmentState);
            db.SaveChanges();

            Assert.True(db.Appointments.Count() == 1);
        }
    }
}