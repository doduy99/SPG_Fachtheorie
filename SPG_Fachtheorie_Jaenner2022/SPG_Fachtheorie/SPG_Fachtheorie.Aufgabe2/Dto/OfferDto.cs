using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Dto {
    public class OfferDto {
        public Guid Id { get; set; }
        public int Term { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EducationType { get; set; } = string.Empty;
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy}")]
        public DateTime To { get; set; }
        public int Anzahl { get; set; }
    }
}
