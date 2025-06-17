
using System.Text.RegularExpressions;

namespace Garage
{
    internal class GarageManager
    {
        private IHandler<Vehicle> handler;
        private readonly IUI ui;

        public GarageManager(IUI ui, IHandler<Vehicle> handler)
        {
            this.ui = ui;
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
                    //// Visa alla fordon
                    case "1": HandleListVehicles(); break;                  

                    // Parkera fordon
                    case "2": HandleAddVehicle(); break;

                    case "3":
                        HandleRemoveVehicle();
                        break;
                    case "4":
                        HandleShowStatistics();
                        break;
                    case "5":
                        HandleSearchVehicle();
                        break;
                    case "6":
                        HandleFindVehicleByLicense();
                        break;

                    case "7":
                        if (!IsGarageInitialized()) break;
                        running = false;
                        ui.PrintMessage("Avslutar programmet.");
                        break ;

                    default:
                        ui.PrintError("Ogiltigt val.");
                        break;
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
            if (!IsGarageInitialized()) return;
            var newVehicle = GetVehicleFromUser();
            if (handler.AddVehicle(newVehicle))
                ui.PrintMessage("Fordon parkerat!");
            else
                ui.PrintError("Garaget är fullt eller fordonet finns redan.");
        }


        private void HandleRemoveVehicle()
        {
            if (!IsGarageInitialized()) return;
            var license = GetLicensePlateFromUser();
            var vehicleToRemove = handler.FindVehicleByLicensePlate(license);
            if (vehicleToRemove != null && handler.RemoveVehicle(vehicleToRemove))
                ui.PrintMessage("Fordon borttaget.");
            else
                ui.PrintError("Fordon hittades inte.");
        }

        private void HandleShowStatistics()
        {
            if (!IsGarageInitialized()) return;
            GetStatistics();
        }

        private void HandleSearchVehicle()
        {
            if (!IsGarageInitialized()) return;
            SearchVehicle();
        }

        private void HandleFindVehicleByLicense()
        {
            if (!IsGarageInitialized()) return;
            var regnr = GetLicensePlateFromUser();
            var foundVehicle = handler.FindVehicleByLicensePlate(regnr);

            if (foundVehicle != null)
                ui.PrintMessage(foundVehicle.ToString());
            else
                ui.PrintError("Inget fordon hittades med det registreringsnumret.");
        }

        private void SearchVehicle()
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

        private Vehicle GetVehicleFromUser()
        {
            string type;
            while (true)
            {
                ui.PrintMessage("Ange fordonstyp (Car, Motorcycle, Bus, Airplane, Boat): ");
                type = ui.ReadInput().Trim().ToLower();
                if (type == "car" || type == "motorcycle" || type == "bus" || type =="airplane" || type == "boat")
                    break;
                ui.PrintError("Okänd fordonstyp. Försök igen.");
            }

            int wheels;
            while (true)
            {
                ui.PrintMessage("Ange antal hjul: ");
                var input = ui.ReadInput();
                if (int.TryParse(input, out wheels) && wheels > 0)
                    break;
                ui.PrintError("Ange ett giltigt positivt heltal för hjul.");
            }

            ui.PrintMessage("Ange färg: ");
            var color = ui.ReadInput();

            ui.PrintMessage("Ange registreringsnummer (3 bokstäver + 3 siffror):  ");
            string licensePlate = GetValidLicensePlate();


            switch (type)
            {
                case "car":
                    string fuel;
                    while (true)
                    {
                        ui.PrintMessage("Ange bränsletyp: ");
                        fuel = ui.ReadInput().Trim();
                        if (!string.IsNullOrWhiteSpace(fuel))
                            break;

                        ui.PrintError("Bränsletyp kan inte vara tom.");
                    }
                    return new Car(wheels, color, licensePlate, fuel);

                case "motorcycle":
                    int hp;
                    while (true)
                    {
                        ui.PrintMessage("Ange hästkrafter: ");
                        var input = ui.ReadInput();
                        if (int.TryParse(input, out hp) && hp > 0)
                            break;
                        ui.PrintError("Ange ett giltigt positivt heltal för hästkrafter.");
                    }
                    return new Motorcycle(wheels, color, licensePlate, hp);

                case "bus":
                    int seats;
                    while (true)
                    {
                        ui.PrintMessage("Ange antal sittplatser: ");
                        var input = ui.ReadInput();
                        if (int.TryParse(input, out seats) && seats > 0)
                            break;
                        ui.PrintError("Ange ett giltigt positivt heltal för antal sittplatser.");
                    }
                    return new Bus(seats, color, licensePlate, wheels);

                case "airplane":
                    int engines;
                    while(true)
                    {
                        ui.PrintMessage("Ange antal motorer: ");
                        var input = ui.ReadInput();
                        if(int.TryParse(input,out engines) && engines > 0)
                            break;
                        ui.PrintError("Ange ett giltigt positivt heltal för antal motorer.");
                    }
                    return new Airplane(wheels, color, licensePlate, engines);

                case "boat":
                    int length;
                    while(true)
                    {
                        ui.PrintMessage("Ange längden: ");
                        var input = ui.ReadInput();
                        if (int.TryParse(input, out length) && length > 0)
                            break;
                        ui.PrintError("Ange ett giltigt positivt heltal för längden.");
                    }
                    return new Airplane(wheels, color, licensePlate, length);

                default:
                    ui.PrintError("Okänd fordonstyp. Försök igen.");
                    return GetVehicleFromUser();
            }
        }

        private string GetLicensePlateFromUser()
        {
            Console.Write("Ange registreringsnummer: ");
            return ui.ReadInput();
        }

        private void GetStatistics()
        {
           var allvehicles = handler.ListVehicles();

            var x = allvehicles.GroupBy(v => v.GetType().Name);

            foreach (var item in x)
            {
                ui.PrintMessage($"{item.Count()} {item.Key}");

            }
        }

        private bool IsGarageInitialized()
        {
            if (handler == null)
            {
                ui.PrintError("Du måste skapa ett garage först (val 1 i menyn).");
                return false;
            }
            return true;
        }

        private string GetValidLicensePlate()
        {
            var regex = new Regex("^[A-Za-z]{3}[0-9]{3}$");
            while (true)
            {
                var input = ui.ReadInput().ToUpper();

                if (regex.IsMatch(input))
                    return input;
                else
                    ui.PrintError("Ogiltigt registreringsnummer. Försök igen.");
            }
        }
    }
}
