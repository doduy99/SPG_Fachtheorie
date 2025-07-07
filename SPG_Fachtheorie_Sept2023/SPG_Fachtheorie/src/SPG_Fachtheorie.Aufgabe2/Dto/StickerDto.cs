using SPG_Fachtheorie.Aufgabe2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Dto {
    public class StickerDto {        
        public string Numberplate { get; set; } = string.Empty;
        public string StickerType { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        
        public DateTime ValidFrom { get; set; }
        
        public DateTime ValidUntil { get; set; }
        public decimal Price { get; set; }
    }
}
