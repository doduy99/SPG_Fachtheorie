using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    /// <summary>
    /// Der Termin des Patienten.
    /// </summary>
    public class Appointment
    {
        public int Id { get; private set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int PatientId { get; private set; }
        public Patient PatientNavigation { get; set; } = default!;
        public DateTime Created { get; set; }
        public int? AppointmentStateId { get; private set; }
        public AppointmentState? AppointmentStateNavigation { get; set; } = default!;
        protected Appointment() { }
        public Appointment(DateTime date, TimeSpan time, DateTime created, Patient patient, AppointmentState? appointmentState = null) {
            Date = date;
            Time = time;
            Created = created;
            PatientNavigation = patient;
            AppointmentStateNavigation = appointmentState;
            //PatientId = patient.Id;
            //AppointmentStateId = appointmentState.Id;        

        }

    }
}