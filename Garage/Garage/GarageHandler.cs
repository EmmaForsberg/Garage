using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Garage
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
            return garage.Add(vehicle);
        }

        //todo Bra att du använder StringComparison.OrdinalIgnoreCase i GarageHandler för att hitta fordon oavsett versaler/gemener. Kontrollera att detta också gäller när du lägger till fordon — så att du inte får dubbletter med samma regnr fast olika case.
        public T FindVehicleByLicensePlate(string licensePlate)
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
