namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Company
    {
        public int Id { get; private set; }
        public string FirmenName { get; set; } = string.Empty;
        public string Anschrift { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Telefonnummer { get; set; }

    }
}
