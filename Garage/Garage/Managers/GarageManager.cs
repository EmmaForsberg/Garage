using System.Text.RegularExpressions;

namespace Garage.Managers
{
    internal class GarageManager
    {
        private IHandler<Vehicle> handler;
        private readonly IUI ui;

        public GarageManager(IUI ui, IHandler<Vehicle> handler)
        {
            this.ui = ui;

            // om handler är null kastas ett undantag direkt, annars tilldelas det inkommande handler objektet till klassens privata fält
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler));
            SeedGarage();
        }

        private void SeedGarage()
        {
            handler.AddVehicle(new Car(4, "Rosa", "EMA365", "diesel"));
            handler.AddVehicle(new Motorcycle(2, "Svart", "MOT123", 100));
            handler.AddVehicle(new Bus(30, "Blå", "BUS456", 55));
        }

        public void Run()
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
            var license = GetValidLicensePlate();
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
            var regnr = GetValidLicensePlate();
            var foundVehicle = handler.FindVehicleByLicensePlate(regnr);

            if (foundVehicle != null)
                ui.PrintMessage(foundVehicle.ToString());
            else
                ui.PrintError("Inget fordon hittades med det registreringsnumret.");
        }

        //Metod för att lägga till ett nytt fordon
        private Vehicle GetVehicleFromUser()
        {
            string type = GetVehicleTypeFromUser();
            int wheels = GetPositiveIntFromUser("Ange antal hjul: ");
            string color = GetTextInput("Ange färg");
            string licensePlate = GetValidLicensePlate();

            return type switch
            {
                "car" => CreateCar(wheels, color, licensePlate),
                "motorcycle" => CreateMc(wheels, color, licensePlate),
                "bus" => CreateBus(wheels, color, licensePlate),
                "airplane" => CreateAirplane(wheels, color, licensePlate),
                "boat" => CreateBoat(color, licensePlate),
                _ => throw new InvalidOperationException("Ogiltig fordonstyp.")
            };
        }

        private Vehicle CreateBoat(string color, string licensePlate)
        {
            int length = GetPositiveIntFromUser("Ange längden på båten: ");
            return new Boat(color, licensePlate, length);
        }

        private Vehicle CreateAirplane(int wheels, string color, string licensePlate)
        {
            int numberofengines = GetPositiveIntFromUser("Ange antal motorer: ");
            return new Airplane(wheels, color, licensePlate, numberofengines);
        }

        private Vehicle CreateBus(int wheels, string color, string licensePlate)
        {
            int seats = GetPositiveIntFromUser("Ange antal säten: ");
            return new Bus(wheels, color, licensePlate, seats);
        }

        private Vehicle CreateCar(int wheels, string color, string licensePlate)
        {
            string fuel = GetTextInput("Ange bränsletyp: ");
            return new Car(wheels, color, licensePlate, fuel);
        }

        private Vehicle CreateMc(int wheels, string color, string licensePlate)
        {
            int enginevolyme = GetPositiveIntFromUser("Ange enginevolyme: ");
            return new Motorcycle(wheels, color, licensePlate, enginevolyme);
        }


        // Hjälpfunktion för att läsa in tal
        private int GetPositiveIntFromUser(string prompt)
        {
            while (true)
            {
                ui.PrintMessage(prompt);
                var input = ui.ReadInput();

                if (int.TryParse(input, out int number) && number > 0)
                    return number;

                ui.PrintError("Ange ett giltigt positivt heltal.");
            }
        }

        //hjälpfunktion för att läsa in sträng
        private string GetTextInput(string prompt)
        {
            while (true)
            {
                ui.PrintMessage(prompt);
                var input = ui.ReadInput().Trim();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;

                ui.PrintError("Fältet får inte vara tomt. Försök igen.");
            }
        }

        private string GetVehicleTypeFromUser()
        {
            var validTypes = new[] { "car", "motorcycle", "bus", "airplane", "boat" };

            while (true)
            {
                ui.PrintMessage("Ange fordonstyp (Car, Motorcycle, Bus, Airplane, Boat): ");
                var input = ui.ReadInput().Trim().ToLower();

                if (validTypes.Contains(input))
                    return input;

                ui.PrintError("Okänd fordonstyp. Försök igen.");
            }
        }

        private string GetValidLicensePlate()
        {
            var regex = new Regex("^[A-Za-z]{3}[0-9]{3}$");
            while (true)
            {
                ui.PrintMessage("Ange registreringsnummer (3 bokstäver + 3 siffror): ");
                var input = ui.ReadInput().ToUpper();

                if (regex.IsMatch(input))
                    return input;
                else
                    ui.PrintError("Ogiltigt registreringsnummer. Försök igen.");
            }
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
