using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    internal interface IHandler<T> where T : Vehicle
    {
        bool AddVehicle(T vehicle);
        bool RemoveVehicle(T vehicle);
        IEnumerable<T> ListVehicles();
        T? FindVehicleByLicensePlate(string licensePlate);    

    }
}
