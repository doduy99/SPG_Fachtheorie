using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Dto {
    public class VehicleStickerDto {
        public Guid VehicleGuid { get; set; }
        public Guid StickerTypeGuid { get; set; }
        public DateTime ValidFrom { get; set; }
    }
}
