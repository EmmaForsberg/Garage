using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    internal class Garage<T> : IEnumerable<T> where T : Vehicle
    {
        //privat array att hålla fordonen
        private T[] vehicles;

        //räknare som jag uppdaterar när jag lägger till fordon eller tar bort fordon
        int count;

        public Garage(int capacity)
        {
            vehicles = new T[capacity];
        }

        //Denna ger en enumerator
        public IEnumerator<T> GetEnumerator()
        {
            //implementera går igenom min interna array och returnerar varje icke null fordon

            foreach (var item in vehicles)
            {
                if (item != null)
                {
                    yield return item;
                }
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //metod för att lägga till
        public bool Add(T vehicle)
        {
            for (var i = 0; i < vehicles.Length; i++)
            {
                vehicles[i] = vehicle;
                return true;
            }
            return false;
        }

        public bool Remove(T vehicle)
        {
            for (var i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i].Licenseplate == vehicle.Licenseplate)
                {
                    vehicles[i] = null;
                    return true;
                }
            }
            return false;
        }
    }
}
