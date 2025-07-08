using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Domain
{
    public class Item
    {
        public int Id { get; set; }
        public DateTime Production { get; set; }
        [Required(ErrorMessage = "Length ist notwendig!")]
        public int Length { get; set; }

        public List<ListenedItem> ListenedItems { get; set; } = new();

        public string ItemType { get; set; } = default!;
    }
}
