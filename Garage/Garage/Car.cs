namespace Garage
{
    internal class Car : Vehicle
    {
        public string Fuel { get; set; }
        public Car(int wheel, string color, string licensePlate, string fuel) : base(wheel, color, licensePlate)
        {
            Fuel = fuel;
        }
    }
}