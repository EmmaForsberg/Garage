
using System.Reflection.Metadata;

namespace Garage
{
    internal class GarageManager
    {
        private readonly IHandler<Vehicle> handler;
        private readonly IUI ui;

        public GarageManager(int capacity)
        {
            handler = new GarageHandler<Vehicle>(capacity);
            ui = new UI(handler);
        }

        private void SeedGarage()
        {
            handler.AddVehicle(new Car(4, "Rosa", "EMA365", "diesel"));
            handler.AddVehicle(new Motorcycle(2, "Svart", "MOT123", 100));
            handler.AddVehicle(new Bus(30, "Blå", "BUS456", 55));
        }

        public void Run()
        {
            SeedGarage();

            ui.PrintMessage("\nFordon som finns i garaget:");
            ui.PrintVehicleList(handler.ListVehicles());

            bool running = true;
            while (running)
            {
                ui.PrintMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    // Visa alla fordon
                    case "1":
                        var vehicles = handler.ListVehicles();
                        ui.PrintVehicleList(vehicles);
                        break;

                        // Parkera fordon
                    case "2":
                    // Parkera fordon - hämta info från UI, skapa fordon, skicka till handler
                    // var newVehicle = GetVehicleFromUser();
                    //  if (handler.AddVehicle(newVehicle))
                    //    ui.PrintMessage("Fordon parkerat!");
                    //else
                    //    ui.PrintError("Garaget är fullt eller fordonet finns redan.");
                    //break;

                        // Ta bort fordon - hämta registreringsnummer, sök och ta bort
                    case "3":
                        var license = GetLicensePlateFromUser();
                        var vehicleToRemove = handler.FindVehicleByLicensePlate(license);
                        if (vehicleToRemove != null && handler.RemoveVehicle(vehicleToRemove))
                            ui.PrintMessage("Fordon borttaget.");
                        else
                            ui.PrintError("Fordon hittades inte.");
                        break;

                        //Visa statistik över fordonstyper
                    case "4":
                        break;

                    //Sök fordon
                    case "5":
                        break;

                        //Sök fordon via regnr
                    case "6":
                        break;

                    case "7":
                        running = false;
                        ui.PrintMessage("Avslutar programmet.");
                        break ;

                    default:
                        ui.PrintError("Ogiltigt val.");
                        break;
                }
            }
        }

        private string GetLicensePlateFromUser()
        {
            Console.Write("Ange registreringsnummer: ");
            return Console.ReadLine();
        }
    }
}
