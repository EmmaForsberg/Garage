using Garage.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Garage.Helpers
{
    internal class InputHelper
    {
        private readonly IUI ui;

        public InputHelper(IUI ui)
        {
            this.ui = ui;
        }

        public int GetPositiveInt(string prompt)
        {
            while (true)
            {
                ui.PrintMessage(prompt);
                var input = ui.ReadInput();

                if (int.TryParse(input, out int value) && value > 0)
                    return value;

                ui.PrintError("Ange ett giltigt positivt heltal.");
            }
        }

        public string GetText(string prompt)
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

        public string GetValidLicense()
        {
            var regex = new Regex("^[A-Za-z]{3}[0-9]{3}$");
            while (true)
            {
                ui.PrintMessage("Ange registreringsnummer (3 bokstäver + 3 siffror): ");
                var input = ui.ReadInput().ToUpper();

                if (regex.IsMatch(input))
                    return input;

                ui.PrintError("Ogiltigt registreringsnummer.");
            }
        }

        public string GetVehicleTypeFromUser()
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

    }
}
