using Vehicle.Interfaces;

namespace Vehicle.Models.Vehicles
{
    public sealed class Motorcycle : Vehicle
    {
        internal Motorcycle(IMotorcycleManufacturer manufacturer) : base(manufacturer)
        {
        }

        public override string ToString() => $"Motorcycle: {Manufacturer}";
        public override void ShowInformation() => Console.WriteLine($"Driving a motorcycle from {Manufacturer}");
    }
}
