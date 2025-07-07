using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class AppointmentState
    {
        public int Id { get; private set; }
        //public int? AppointmentId { get; private set; }
        public Appointment? AppointmentNavigation { get; set; } 
        public DateTime Created { get; set; }
        public string Type { get; private set; } = default!;
        protected AppointmentState() { }
        public AppointmentState(DateTime created, Appointment? appointment = null) {            
            Created = created;
            AppointmentNavigation = appointment;
        }    
    }
}