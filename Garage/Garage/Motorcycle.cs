namespace Garage
{
    internal class Motorcycle : Vehicle
    {
        public int EngineVolume { get; set; }
        public Motorcycle(int wheel, string color, string licensePlate, int enginevolume) : base(wheel, color, licensePlate)
        {
            EngineVolume = enginevolume;
        }
    }
}