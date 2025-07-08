using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Favorite
    {
        public int Id { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime? EndDatum { get; set; }
        public int UserNavigationId { get; private set; }
        public User UserNavigation { get; private set; } = default!;
        public int CategoryNavigationId { get; private set; }
        public Category CategoryNavigation { get; private set; } = default!;
        protected Favorite() { }
        public Favorite(DateTime startDatum, DateTime? endDatum, int userNavigationId, User userNavigation, int categoryNavigationId, Category categoryNavigation) {
            StartDatum = startDatum;
            EndDatum = endDatum;
            UserNavigationId = userNavigationId;
            UserNavigation = userNavigation;
            CategoryNavigationId = categoryNavigationId;
            CategoryNavigation = categoryNavigation;
        }
    }
}
