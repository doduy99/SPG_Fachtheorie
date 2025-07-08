using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Diagnostics;
using System.Threading;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Rating
    {
        public int Id { get; private set; }
        public int Bewertung {
            get { return _bewertung; }
            set {
                if (value < 1 || value > 5) {
                    throw new ArgumentOutOfRangeException("Bewertung muss zwischen 1 und 5 liegen!");
                }
                _bewertung = value;
            }
        }

        public int _bewertung;
        public string? BewertungText { get; set; } = string.Empty;
        public int UserNavigationId { get; private set; }
        public virtual User UserNavigation { get; private set; } = default!;
        public int PodcastNavigationId { get; private set; }
        public virtual Podcast PodcastNavigation { get; private set; } = default!;
        protected Rating() { }
        public Rating(User user, Podcast podcast, int bewertung, string? bewertungText = null) {
            UserNavigation = user;
            PodcastNavigation = podcast;
            Bewertung = bewertung;
            BewertungText = bewertungText;
        }

    }
}
