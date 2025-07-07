using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Dto {
    public class CustomerStickerDto {
        public string Name { get; set; } = string.Empty;
        //public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<StickerDto> StickerList { get; set; } = new();
    }

}
