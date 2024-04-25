using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Cars
{
    public class SchoolBus : Car
    {
        public int NumberOfSeatedPassengers { get; set; }
        public DateTime FirstPickupTime { get; set; } = DateTime.Now;
    }
}
