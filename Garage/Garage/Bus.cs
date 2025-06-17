namespace Garage
{
    internal class Bus : Vehicle
    {
        public int NumberOfSeats { get; set; }
        public Bus(int wheel, string color, string licensePlate, int numberofseats) : base(wheel, color, licensePlate)
        {
            NumberOfSeats = numberofseats;
        }

        public override string ToString()
        {
            return base.ToString() + $", NumberofSeats: {NumberOfSeats}";
        }

    }
}