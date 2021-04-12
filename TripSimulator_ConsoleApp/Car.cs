using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripSimulator_ConsoleApp
{
    class Car
    {
        public string CarName { get; set; }
        public int mpg { get; set; }
        public int tankCapacity { get; set; }
        public int milesUntilEmpty { get; set; }

        public Car(string CarName, int mpg, int tankCapacity, int milesUntilEmpty)
        {
            this.CarName = CarName;
            this.mpg = mpg;
            this.tankCapacity = tankCapacity;
            this.milesUntilEmpty = milesUntilEmpty;
        }

    }
}
