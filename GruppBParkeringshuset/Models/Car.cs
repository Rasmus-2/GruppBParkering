using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppBParkeringshuset.Models
{
    internal class Car
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public string Make { get; set; }
        public string Colour { get; set; }
        public int? ParkingSlotId { get; set; }
    }
}
