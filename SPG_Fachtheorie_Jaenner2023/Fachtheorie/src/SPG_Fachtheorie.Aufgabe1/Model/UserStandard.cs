using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class UserStandard : User
    {
        public bool Informed { get; set; }
        protected UserStandard() { }
        //public UserStandard(User user, bool informed) : base(user) {
        //    Informed = informed;
        //}
        public UserStandard(string vorname,
            string zuname,
            string email,
            DateTime startDatum, 
            DateTime? endDatum,
            bool informed) : base(vorname: vorname,
            zuname: zuname,
            email: email,
            startDatum: startDatum,
            endDatum: endDatum
            ) {
            Informed = informed;
        }
    }
}
