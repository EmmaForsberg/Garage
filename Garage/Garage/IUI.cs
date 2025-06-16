using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    internal interface IUI
    {
        void PrintMenu();
        void ReadInput();
        void PrintMessage(string msg);
        void PrintError(string errorMsg);
        void PrintVehicleList(IEnumerable<Vehicle> vehicles);
    }
}
