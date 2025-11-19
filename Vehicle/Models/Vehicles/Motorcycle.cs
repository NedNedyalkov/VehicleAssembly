using Vehicle.Interfaces;

namespace Vehicle.Models.Vehicles
{
    internal class Motorcycle(IMotorcycleManufacturer manufacturer) : Vehicle(manufacturer)
    {
        public override string ToString() => $"Motorcycle: {Manufacturer}";
        public override void ShowInformation() => Console.WriteLine($"Driving a motorcycle from {Manufacturer}");
    }
}
