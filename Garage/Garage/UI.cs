using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    internal class UI : IUI
    {
        private IHandler<Vehicle> handler;
        
        public string ReadInput()
        {
            return Console.ReadLine() ?? string.Empty;
        }



        public void PrintError(string errorMsg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMsg);
            Console.ResetColor();
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. Skapa ett garage");
            Console.WriteLine("2. Visa alla fordon ");
            Console.WriteLine("3. Parkera ett fordon.");
            Console.WriteLine("4. Ta bort ett fordon.");
            Console.WriteLine("5. Visa statistik över fordonstyper");
            Console.WriteLine("6. Sök fordon");
            Console.WriteLine("7. Sök fordon via registeringsnummer");
            Console.WriteLine("8. Avsluta programmet");
        }

        public void PrintMessage(string msg)
        {
           Console.WriteLine(msg);
        }

        public void PrintVehicleList(IEnumerable<Vehicle> vehicles)
        {
            foreach (var item in vehicles)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void SetHandler(IHandler<Vehicle> handler)
        {
            this.handler = handler;
        }
    }
}
