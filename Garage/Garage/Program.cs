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
            Console.Write("Ange garagekapacitet: ");
            int capacity = int.Parse(Console.ReadLine());

            var manager = new GarageManager(capacity); // sätter garagekapacitet här
            manager.Run();
        }
    }
}

