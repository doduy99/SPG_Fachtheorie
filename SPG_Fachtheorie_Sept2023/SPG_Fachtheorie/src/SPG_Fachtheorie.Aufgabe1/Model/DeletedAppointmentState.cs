using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class DeletedAppointmentState : AppointmentState
    {
        protected DeletedAppointmentState() { }
        public DeletedAppointmentState(DateTime created, Appointment appointment) : base(created, appointment) {

        }
    }
}