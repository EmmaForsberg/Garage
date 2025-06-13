namespace Garage
{
    internal class Airplane : Vehicle
    {
        public int NumberofEngines { get; set; }

        public Airplane(int wheel, string color, string licensePlate, int numberofengines) : base(wheel, color, licensePlate)
        {
            NumberofEngines = numberofengines;
        }
    }
}