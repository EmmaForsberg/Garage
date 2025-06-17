namespace Garage
{
    internal class GarageManager
    {
        private IHandler<Vehicle> handler;
        private readonly IUI ui;

        public GarageManager(IUI ui)
        {
            this.ui = ui;
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
                    case "1":
                        int capacity = AskForCapacity(); // Metod som frågar användaren om kapacitet
                        handler = new GarageHandler<Vehicle>(capacity);
                        ui.SetHandler(handler);
                        SeedGarage(); // Lägg in dina förseedade fordon i det nya garaget
                        ui.PrintMessage($"Nytt garage skapat med kapacitet {capacity}.");
                        break;

                    //// Visa alla fordon
                    case "2":

                        var vehicles = handler.ListVehicles();
                        ui.PrintVehicleList(vehicles);
                        break;

                    // Parkera fordon
                    case "3":
                        // Parkera fordon - hämta info från UI, skapa fordon, skicka till handler
                        var newVehicle = GetVehicleFromUser();
                        if (handler.AddVehicle(newVehicle))
                            ui.PrintMessage("Fordon parkerat!");
                        else
                            ui.PrintError("Garaget är fullt eller fordonet finns redan.");
                        break;

                    // Ta bort fordon - hämta registreringsnummer, sök och ta bort
                    case "4":
                        var license = GetLicensePlateFromUser();
                        var vehicleToRemove = handler.FindVehicleByLicensePlate(license);
                        if (vehicleToRemove != null && handler.RemoveVehicle(vehicleToRemove))
                            ui.PrintMessage("Fordon borttaget.");
                        else
                            ui.PrintError("Fordon hittades inte.");
                        break;

                        //Visa statistik över fordonstyper
                    case "5":
                        break;

                    //Sök fordon
                    case "6":
                        break;

                        //Sök fordon via regnr
                    case "7":
                        break;

                    case "8":
                        running = false;
                        ui.PrintMessage("Avslutar programmet.");
                        break ;

                    default:
                        ui.PrintError("Ogiltigt val.");
                        break;
                }
            }
        }

        private int AskForCapacity()
        {
            while (true)
            {
                ui.PrintMessage("Ange kapacitet för garaget (antal platser): ");
                var input = ui.ReadInput();
                if (int.TryParse(input, out int capacity) && capacity > 0)
                    return capacity;
                else
                    ui.PrintError("Ange ett giltigt positivt heltal.");
            }
        }

        private Vehicle GetVehicleFromUser()
        {
            Console.Write("Ange fordonstyp (Car, Motorcycle, Bus): ");
            string type = ui.ReadInput().Trim();

            Console.Write("Ange antal hjul: ");
            int wheels = int.Parse(ui.ReadInput());

            Console.Write("Ange färg: ");
            string color = ui.ReadInput();

            Console.Write("Ange registreringsnummer: ");
            string licensePlate = ui.ReadInput();

            switch (type.ToLower())
            {
                case "car":
                    Console.Write("Ange bränsletyp: ");
                    string fuel = ui.ReadInput();
                    return new Car(wheels, color, licensePlate, fuel);

                case "motorcycle":
                    Console.Write("Ange hästkrafter: ");
                    int hp = int.Parse(ui.ReadInput());
                    return new Motorcycle(wheels, color, licensePlate, hp);

                case "bus":
                    Console.Write("Ange antal sittplatser: ");
                    int seats = int.Parse(ui.ReadInput());
                    return new Bus(seats, color, licensePlate, wheels);

                default:
                    ui.PrintError("Okänd fordonstyp. Försök igen.");
                    return GetVehicleFromUser(); // Loopar tills rätt typ anges
            }
        }


        private string GetLicensePlateFromUser()
        {
            Console.Write("Ange registreringsnummer: ");
            return ui.ReadInput();
        }
    }
}
