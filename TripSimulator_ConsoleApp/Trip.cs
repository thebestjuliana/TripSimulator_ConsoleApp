using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripSimulator_ConsoleApp
{
    class Trip
    {
        public string destination { get; set; }
        public int nightsThere { get; set; }
        public int distance { get; set; }
        public double hotelPrice { get; set; }
        public Car car { get; set; }
        public double gasPrice { get; set; }
        public double hotelCost { get; set; } = 0;
        public double foodCost { get; set; } = 0;
        public double fuelCost { get; set; } = 0;
        public double souvenierCost { get; set; } = 0;
        public double businessCost { get; set; } = 0;
        public int stops { get; set; } = 0;

    }
}
