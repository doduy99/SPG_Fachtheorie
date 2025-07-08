using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2
{
    public class AppointmentService
    {
        private readonly AppointmentContext _db;

        public AppointmentService(AppointmentContext db)
        {
            _db = db;
        }

        public bool AskForAppointment(Guid offerId, Guid studentId, DateTime date)
        {
            // TOTO: Implementiere die Methode
            var offer = _db.Offers.SingleOrDefault(o => o.Id== offerId);
            if (offer == null) {
                return false;
            }

            if(date > offer.To || date < offer.From) {
                return false;
            }

            Appointment appointment = new Appointment() {
                Id = new Guid(), 
                OfferId = offerId,
                StudentId = studentId,
                Date = date,
                Location = "",
                State = AppointmentState.AskedFor
            };

            _db.Appointments.Add(appointment);
            _db.SaveChanges();           

            //_db.Offers.Add(offer);
            //_db.SaveChanges();            

            return true;
            //return default;
        }

        public bool ConfirmAppointment(Guid appointmentId)
        {
            // TOTO: Implementiere die Methode
            var appointment = _db.Appointments.SingleOrDefault(a => a.Id == appointmentId);
            if (appointment == null) {
                return false;
            }

            if(appointment.State == AppointmentState.Confirmed 
                || appointment.State == AppointmentState.Cancelled
                || appointment.State == AppointmentState.TookPlace) {
                return false;
            }

            appointment.State = AppointmentState.Confirmed;
            _db.Update(appointment);
            _db.SaveChanges();

            return true;
            //return default;
        }

        public bool CancelAppointment(Guid appointmentId, Guid studentId)
        {
            // TOTO: Implementiere die Methode
            var appointment = _db.Appointments.SingleOrDefault(a =>a.Id== appointmentId);
            if (appointment == null) {
                return false;
            }
            if(appointment.State == AppointmentState.AskedFor 
                && appointment.StudentId == studentId) {
                appointment.State= AppointmentState.Cancelled;
            }
            if(appointment.State== AppointmentState.AskedFor ||
                appointment.State == AppointmentState.Confirmed &&
                appointment.Offer.TeacherId == studentId) { 
                appointment.State= AppointmentState.Cancelled;
            }
            return true;
            //return default;
        }
    }
}
