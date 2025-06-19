using Garage.Models;

namespace Garage
{
    public class Car : Vehicle
    {
        public string Fuel { get; set; }
        public Car(int wheel, string color, string licensePlate, string fuel) : base(wheel, color, licensePlate)
        {
            Fuel = fuel;
        }

        public override string ToString()
        {
            return base.ToString() + $", Fuel: {Fuel}";
        }
    }
}