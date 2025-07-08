namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class InvoiceItem
    {
        public int Id { get; private set; }
        //public int Position { get; set; }
        public int Anzahl { get; set; }
        public decimal Einzelpreis { get; set; }
        public decimal Gesamtpreis { get; set; }
        public int InvoiceId { get; set; }
        public Invoice InvoiceNavigation { get; set; }
        public int ArticleId { get; set; }
        public Article ArticleNavigation { get; set; }

    }
}
