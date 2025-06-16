using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    internal class UI : IUI
    {
        private readonly IHandler<Vehicle> handler;

        public UI(IHandler<Vehicle> handler)
        {
            this.handler = handler;
        }
        
        public void ReadInput()
        {
        //    bool running = true;

        //    while (running)
        //    {
        //        PrintMenu();
        //        Console.Write("Välj ett alternativ: ");
        //        var input = Console.ReadLine();

        //        switch (input)
        //        {
        //            case "1":
        //                // Skapa garage - du kan fråga efter kapacitet och skapa ny handler
        //                PrintMessage("Funktion för att skapa garage kommer här.");
        //                break;
        //            case "2":
        //                // Parkera fordon
        //                PrintMessage("Funktion för att parkera fordon kommer här.");
        //                break;
        //            case "3":
        //                // Ta bort fordon
        //                PrintMessage("Funktion för att ta bort fordon kommer här.");
        //                break;
        //            case "4":
        //                // Visa alla fordon
        //                var vehicles = handler.ListVehicles();
        //                PrintVehicleList(vehicles);
        //                break;
        //            case "8":
        //                running = false;
        //                PrintMessage("Avslutar programmet.");
        //                break;
        //            default:
        //                PrintError("Felaktigt val, försök igen.");
        //                break;
        //        }
        //    }
        }



        public void PrintError(string errorMsg)
        {
            throw new NotImplementedException();
        }

        public void PrintMenu()
        {
            Console.WriteLine("1. Visa alla fordon ");
            Console.WriteLine("2. Parkera ett fordon.");
            Console.WriteLine("3. Ta bort ett fordon.");
            Console.WriteLine("4. Visa statistik över fordonstyper");
            Console.WriteLine("5. Sök fordon");
            Console.WriteLine("6. Sök fordon via registeringsnummer");
            Console.WriteLine("7. Avsluta programmet");
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

    }
}
