using System.Runtime.CompilerServices;
using Garage.Handlers;
using Garage.Managers;
using Garage.Models;
using Garage.UI;

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
            var manager = new GarageManager(ui);
            manager.Run();
        }
    }
}

