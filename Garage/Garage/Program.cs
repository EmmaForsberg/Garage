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
            IUI ui = new UI();
            var manager = new GarageManager(ui);
            manager.Run();
        }
    }
}

