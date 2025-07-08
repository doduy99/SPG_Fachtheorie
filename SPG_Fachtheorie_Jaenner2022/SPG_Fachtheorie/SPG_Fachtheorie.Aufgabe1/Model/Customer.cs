namespace SPG_Fachtheorie.Aufgabe1.Model
{ 
    public enum Anrede { HERR, FRAU }
    public class Customer
    {
        public int Kundennummer { get; private set; }
        public Anrede Anrede { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Anschrift { get; set; } = string.Empty;
        public List<Invoice> Invoices { get; set; } = new();
        protected Customer() { }
        public Customer(int kundennummer, Anrede anrede, string name, string anschrift) {
            Kundennummer = kundennummer;
            Anrede = anrede;
            Name = name;
            Anschrift = anschrift;
        }

    }
}
