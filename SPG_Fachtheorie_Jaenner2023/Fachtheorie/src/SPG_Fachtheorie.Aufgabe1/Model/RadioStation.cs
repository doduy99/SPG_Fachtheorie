using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public enum RadioType { online, offline, beide }
    public class RadioStation
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public RadioType RadioType { get; set; }
        public string? Link { get; set; } = string.Empty;
        public decimal? RadioFrequenz { get; set; }

        public List<Podcast> Podcasts { get; set; } = new();

        protected RadioStation() { }
        public RadioStation(
            string name,
            string adresse,
            RadioType radioType,
            string? link = null,
            decimal? radioFrequenz = null) {
            Name = name;
            Adresse = adresse;
            RadioType = radioType;
            Link = link;
            RadioFrequenz = radioFrequenz;
        }



    }
}
