using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Address
    {
        public string Street { get; set; } = string.Empty;
        public int Zip { get; set; }
        public string City { get; set; } = string.Empty;
        public Address(string street, int zip, string city) {
            Street = street;
            Zip = zip;
            City = city;
        }
    }
}