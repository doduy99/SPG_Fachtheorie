using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Patient
    {
        public int Id { get; private set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public Address Address { get; set; } = default!;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<Appointment> Appointments { get; set; } = new();
        protected Patient() { }
        public Patient(string firstname, string lastname, Address address, string email, string phone) {
            Firstname= firstname;
            Lastname= lastname;
            Address= address;
            Email= email;
            Phone= phone;
        }

    }
}