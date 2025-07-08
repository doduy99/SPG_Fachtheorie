using System.Collections.Generic;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public bool Top { get; set; }
        public bool OnlyPremium { get; set; }

        public List<Podcast> Podcasts { get; set; } = new();
        public List<Favorite> Favorites { get; set; } = new();
        protected Category() { }
        public Category(string name, bool top, bool onlyPremium) {
            Name = name;
            Top = top;
            OnlyPremium = onlyPremium;
        }

    }
}
