using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class UserPremium : User
    {
        public decimal Mitgliedsbeitrag { get; set; }
        public int Mindestvertragsdauer { get; set; }
        protected UserPremium() { }
        //public UserPremium(User user, decimal mitgliedsbeitrag, int mindestvertragsdauer) : base(user) {
        //    Mitgliedsbeitrag= mitgliedsbeitrag;
        //    Mindestvertragsdauer= mindestvertragsdauer;
        //}
        public UserPremium(
            string vorname,
            string zuname,
            string email,
            DateTime startDatum,
            DateTime? endDatum,
            decimal mitgliedsbeitrag,
            int mindestvertragsdauer) : base(vorname: vorname,
            zuname: zuname,
            email: email,
            startDatum: startDatum,
            endDatum: endDatum) {
            Mitgliedsbeitrag = mitgliedsbeitrag;
            Mindestvertragsdauer = mindestvertragsdauer;
        }

    }
}
