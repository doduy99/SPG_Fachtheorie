using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe2.Dto {
    public class AdvertisementDto {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Production { get; set; }
        public int Length { get; set; }
        public int? MinPlayTime { get; set; }
        public decimal CostsPerPlay { get; set; }
        public int NumberOfListenedItems { get; set; }
    }
}
