using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class ConfirmedAppointmentState : AppointmentState
    {
        public TimeSpan Duration { get; set; }
        public string? Infotext { get; set; } = string.Empty;
        protected ConfirmedAppointmentState() { }
        public ConfirmedAppointmentState(DateTime created, Appointment appointment, TimeSpan duration, string? infotext) : base(created, appointment) {
            Duration = duration;
            Infotext = infotext;
        }
    }
}