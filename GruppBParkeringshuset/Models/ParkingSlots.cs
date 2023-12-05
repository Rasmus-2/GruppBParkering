using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GruppBParkeringshuset.Models
{
    internal class ParkingSlots
    {
        public int Id { get; set; }
        public int SlotNumber { get; set; }
        public bool ElectricOutlet {  get; set; } //gör till int om bool inte funkar
        public int ParkingHouseId { get; set; }
    }
}
