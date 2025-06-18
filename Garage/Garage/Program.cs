using System.Runtime.CompilerServices;

namespace Garage
{
    internal class Program
    {
        /// <summary>
        /// startpunkt för applikationen. skapar instanser av manager, ui och handler
        /// anropar startmetod i manager klassen
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IUI ui = new ConsoleUI();

            int capacity;
            ui.PrintMessage("Ange kapacitet för garaget: ");
            while (!int.TryParse(ui.ReadInput(), out capacity) || capacity <= 0)
            {
                ui.PrintMessage("Felaktig inmatning. Ange ett positivt heltal för kapaciteten:");
            }

            IHandler<Vehicle> handler = new GarageHandler<Vehicle>(capacity);
            var manager = new GarageManager(ui, handler);
            manager.Run();
        }
    }
}

