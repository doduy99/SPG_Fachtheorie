namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Invoice
    {
        public int Rechnungsnummer { get; private set; }
        public DateTime Rechnungsdatum { get; set; }
        public int Position { get; set; }
        public int Rabatt { get; set; }
        public decimal Endpreis { get; set; }
        public int CustomerKundennummer { get; set; }
        public Customer CustomerNavigation { get; set; }
        public int EmployeeId { get; set; }
        public Employee EmployeeNavigation { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; } = new();



    }
}
