namespace Garage
{
    internal class Bus : Vehicle
    {
        public int NumberOFSeats { get; set; }
        public Bus(int wheel, string color, string licensePlate, int numberofseats) : base(wheel, color, licensePlate)
        {
            NumberOFSeats = numberofseats;
        }
    }
}