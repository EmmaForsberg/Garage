using Garage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Garage.Handlers
{
    //Klass för att abstrahera ett lager så det inte finns någon direkt kontakt mellan användargränssnittet och garageklassen
    internal class GarageHandler<T> : IHandler<T> where T : Vehicle

    {
        private Garage<T> garage;

        public GarageHandler(int capacity)
        {
            garage = new Garage<T>(capacity);
        }

        //mellanhand som tar emot fordon från ui eller manager och sedan anropar motsvarande metod i Garage klassen för att faktiskt lägga till fordonet i den interna samlingen
        public bool AddVehicle(T vehicle)
        {
            // Kontrollera om regnummer redan finns — oavsett case
            var existing = garage.FirstOrDefault(v =>
                v.Licenseplate.Equals(vehicle.Licenseplate, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
                return false;

            return garage.Add(vehicle);
        }

        public T? FindVehicleByLicensePlate(string licensePlate)
        {
            return garage.FirstOrDefault(vehicle => vehicle.Licenseplate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
        }

        public bool RemoveVehicle(T vehicle)
        {
            return garage.Remove(vehicle);
        }

        public IEnumerable<T> ListVehicles()
        {
            return garage;
        }
    }

}
