using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Cars
{
    public class Bakkie : Car
    {
        public int BakVolumeLiters { get; set; }
        public int HorsePower { get;set; }
    }
}
