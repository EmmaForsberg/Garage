using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    abstract class Vehicle
    {
        public int Wheel { get; private set; }
        public string Color { get; private set; }
        public string Licenseplate { get; private set; }

        protected Vehicle()
        {

        }

    }
}
