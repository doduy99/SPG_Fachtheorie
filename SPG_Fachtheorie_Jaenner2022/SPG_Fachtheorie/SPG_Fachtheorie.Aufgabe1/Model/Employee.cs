namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Employee
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public List<Invoice> Invoices { get; set; } = new();

    }
}
