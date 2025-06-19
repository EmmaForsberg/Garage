using Garage.Handlers;
using Garage.Helpers;
using Garage.Models;
using Garage.UI;

namespace Garage.Managers
{
    internal class GarageManager
    {
        private IHandler<Vehicle> handler;
        private readonly IUI ui;
        private readonly InputHelper inputHelper;
        private readonly VehicleFactory vehicleFactory;


        public GarageManager(IUI ui)
        {
            this.ui = ui;
            this.inputHelper = new InputHelper(ui);
            this.vehicleFactory = new VehicleFactory(inputHelper);
        }

        private List<Vehicle> GetSeedVehicles()
        {
            var vehicles = new List<Vehicle>();
            vehicles.Add(new Car(4, "Rosa", "EMA365", "diesel"));
            vehicles.Add(new Motorcycle(2, "Svart", "MOT123", 100));
            vehicles.Add(new Bus(30, "Blå", "BUS456", 55));
            vehicles.Add(new Airplane(6, "Vit", "FLY123", 6));
            vehicles.Add(new Boat("Grön", "BOA319", 3));
            return vehicles;
        }

        public void Run()
        {
            var seedVehicles = GetSeedVehicles();

            ShowSeedVehicles(seedVehicles);

            int capacity = GetValidCapacity(seedVehicles.Count);

            // Skapa handler och garage
            handler = new GarageHandler<Vehicle>(capacity);

            // Lägg in seedade fordon
            foreach (var v in seedVehicles)
            {
                handler.AddVehicle(v);
            }

            MainMenuLoop();
        }

        private void MainMenuLoop()
        {
            bool running = true;
            while (running)
            {
                ui.PrintMenu();
                var choice = ui.ReadInput();

                switch (choice)
                {
                    case "1": HandleListVehicles(); break;
                    case "2": HandleAddVehicle(); break;
                    case "3": HandleRemoveVehicle(); break;
                    case "4": HandleShowStatistics(); break;
                    case "5": HandleSearchVehicle(); break;
                    case "6": HandleFindVehicleByLicense(); break;
                    case "7": running = false; ui.PrintMessage("Avslutar programmet."); break;
                    default: ui.PrintError("Ogiltigt val."); break;
                }
            }
        }


        private void ShowSeedVehicles(List<Vehicle> seedVehicles)
        {
            ui.PrintMessage("Följande fordon kommer att finnas i garaget:\n");
            foreach (var vehicle in seedVehicles)
            {
                ui.PrintMessage(vehicle.ToString());
            }
        }

        private int GetValidCapacity(int minCapacity)
        {
            int capacity = 0;
            bool valid = false;

            do
            {
                ui.PrintMessage($"Ange garagekapacitet (måste vara minst {minCapacity}):");
                string input = ui.ReadInput();

                if (int.TryParse(input, out capacity) && capacity >= minCapacity)
                    valid = true;
                else
                    ui.PrintError("Felaktig inmatning. Ange ett giltigt heltal som är tillräckligt stort.");

            } while (!valid);

            return capacity;
        }


        /// <summary>
        /// Metod för att visa alla fordon
        /// </summary>
        private void HandleListVehicles()
        {
            if (!IsGarageInitialized()) return;
            var vehicles = handler.ListVehicles();
            ui.PrintVehicleList(vehicles);
        }

        /// <summary>
        /// Metod för att parkera fordon
        /// </summary>
        private void HandleAddVehicle()
        {
            var newVehicle = GetVehicleFromUser();
            if (handler.AddVehicle(newVehicle))
                ui.PrintMessage("Fordon parkerat!");
            else
                ui.PrintError("Garaget är fullt eller fordonet finns redan.");
        }

        //Metod för att ta bort fordon
        private void HandleRemoveVehicle()
        {
            var license = inputHelper.GetValidLicense();
            var vehicleToRemove = handler.FindVehicleByLicensePlate(license);
            if (vehicleToRemove != null && handler.RemoveVehicle(vehicleToRemove))
                ui.PrintMessage("Fordon borttaget.");
            else
                ui.PrintError("Fordon hittades inte.");
        }

        // Metod för att visa statistik över fordonstyper
        private void HandleShowStatistics()
        {
            var allvehicles = handler.ListVehicles();

            var x = allvehicles.GroupBy(v => v.GetType().Name);

            foreach (var item in x)
            {
                ui.PrintMessage($"{item.Count()} {item.Key}");
            }
        }

        //Metod för att söka fordon
        private void HandleSearchVehicle()
        {
            var allVehicles = handler.ListVehicles();

            ui.PrintMessage("Vill du söka på färg? (j/n): ");
            var searchColor = ui.ReadInput().Trim().ToLower() == "j";
            string color = "";
            if (searchColor)
            {
                ui.PrintMessage("Ange färg: ");
                color = ui.ReadInput().Trim();
            }

            ui.PrintMessage("Vill du söka på antal hjul? (j/n): ");
            var searchWheels = ui.ReadInput().Trim().ToLower() == "j";
            int wheels = 0;
            if (searchWheels)
            {
                ui.PrintMessage("Ange antal hjul: ");
                int.TryParse(ui.ReadInput(), out wheels);
            }

            ui.PrintMessage("Vill du söka på fordonstyp? (j/n): ");
            var searchType = ui.ReadInput().Trim().ToLower() == "j";
            string type = "";
            if (searchType)
            {
                ui.PrintMessage("Ange fordonstyp (Car, Motorcycle, Bus, Boat, Airplane): ");
                type = ui.ReadInput().Trim().ToLower();
            }

            var result = allVehicles.Where(v =>
                (!searchColor || v.Color.Equals(color, StringComparison.OrdinalIgnoreCase)) &&
                (!searchWheels || v.Wheel == wheels) &&
                (!searchType || v.GetType().Name.ToLower() == type)
            );


            if (!result.Any())
            {
                ui.PrintMessage("Inga fordon matchade din sökning.");
            }
            else
            {
                ui.PrintVehicleList(result);
            }

        }

        //Metod för att söka fordon genom registeringsplåt
        private void HandleFindVehicleByLicense()
        {
            var regnr = inputHelper.GetValidLicense();
            var foundVehicle = handler.FindVehicleByLicensePlate(regnr);

            if (foundVehicle != null)
                ui.PrintMessage(foundVehicle.ToString());
            else
                ui.PrintError("Inget fordon hittades med det registreringsnumret.");
        }

        //Metod för att lägga till ett nytt fordon
        private Vehicle GetVehicleFromUser()
        {
            string type = inputHelper.GetVehicleTypeFromUser();
            return vehicleFactory.CreateVehicle(type);
        }

        private bool IsGarageInitialized()
        {
            var vehicles = handler.ListVehicles();

            if (vehicles == null || !vehicles.Any())
            {
                ui.PrintError("Garaget är tomt. Lägg till ett fordon först.");
                return false;
            }
            return true;
        }
    }
}
