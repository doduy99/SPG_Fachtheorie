using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Domain
{
    public class Advertisement : Item
    {
        [MinLength(3, ErrorMessage = "ProductName muss mindest 3 Zeichen sein!")]
        public string ProductName { get; set; } = string.Empty;
        public int? MinPlayTime { get; set; }
        [Required(ErrorMessage = "CostsPerPlay ist notwendig!")]
        public decimal CostsPerPlay { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;
    }
}
