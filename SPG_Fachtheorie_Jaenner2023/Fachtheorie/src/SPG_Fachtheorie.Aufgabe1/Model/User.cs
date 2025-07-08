using System;
using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class User
    {
        public int Id { get; private set; }
        public string Vorname { get; set; } = string.Empty;
        public string Zuname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime StartDatum { get; set; }
        public DateTime? EndDatum { get; set; }
        public string UserType { get; private set; } = default!;

        //public List<Favorite> Favorite { get; set; } = new();

        protected List<Favorite> _favoriten  = new();
        public virtual IReadOnlyCollection<Favorite> Favoriten => _favoriten;
        protected User() { }
        //protected User(User user) {
        //    Id = user.Id;
        //    Vorname = user.Vorname;
        //    Zuname = user.Zuname;
        //    Email = user.Email;
        //    StartDatum= user.StartDatum;
        //    EndDatum= user.EndDatum;
        //}
        public User(
            string vorname, 
            string zuname, 
            string email,
            DateTime startDatum,
            DateTime? endDatum = null) {
            Vorname= vorname;
            Zuname= zuname;
            Email= email;
            StartDatum= startDatum;
            EndDatum= endDatum;
        }

    }
}
