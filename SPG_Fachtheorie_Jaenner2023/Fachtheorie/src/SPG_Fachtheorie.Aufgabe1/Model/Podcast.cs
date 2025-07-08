using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Podcast
    {
        public int Id { get; private set; }
        public string Titel { get; set; } = string.Empty;
        public int Laenge { get; set; }

        public int CategoryNavigationId { get; private set; }
        public virtual Category CategoryNavigation { get; private set; } = default!; 
        public int RadioStationNaviagationId { get; private set; }
        public virtual RadioStation RadioStationNavigation { get; private set; } = default!;
        //public List<Rating> Ratings { get; set; } = new();
        protected List<Rating> _ratings = new();
        public virtual IReadOnlyCollection<Rating> Ratings => _ratings;
        protected Podcast() { }
        public Podcast(string titel, int laenge, Category categoryNavigation, RadioStation radioStationNavigation) {
            Titel = titel;
            Laenge = laenge;
            CategoryNavigation= categoryNavigation;
            RadioStationNavigation= radioStationNavigation;
        }

    }
}
