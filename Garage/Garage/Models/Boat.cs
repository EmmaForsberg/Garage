using Garage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    internal class Boat : Vehicle
    {
        public int Length { get; set; }

        public Boat(string color, string licensePlate, int length) : base(0, color, licensePlate)
        {
            Length = length;
        }

        public override string ToString()
        {
            return base.ToString() + $", Length: {Length}";
        }

    }
}
