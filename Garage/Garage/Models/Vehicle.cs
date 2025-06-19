using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models
{
    public abstract class Vehicle : IVehicle
    {
        public int Wheel { get; private set; }
        public string Color { get; private set; }
        public string Licenseplate { get; private set; }

        protected Vehicle(int wheel, string color, string licensePlate)
        {
            Wheel = wheel;
            Color = color;
            Licenseplate = licensePlate.ToUpper(); ;
        }

        public override string ToString()
        {
            return $"{GetType().Name}: {Licenseplate}, Color: {Color}, Wheels: {Wheel}";
        }
    }
}
