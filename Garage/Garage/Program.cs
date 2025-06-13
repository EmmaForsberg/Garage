namespace Garage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            IHandler<Vehicle> ie = new GarageHandler<Vehicle>(10);

        }
    }
}
