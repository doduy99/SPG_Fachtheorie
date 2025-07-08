namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Article
    {
        //public int Id { get; private set; }
        public int Arktikelnummer { get; private set; }
        public string Bezeichnung { get; set; } = string.Empty;
        public decimal Preis { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; } = new();

    }
}
