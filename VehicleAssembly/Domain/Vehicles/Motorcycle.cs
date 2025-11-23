using VehicleAssembly.Domain.Manufacturers;

namespace VehicleAssembly.Domain.Vehicles
{
    public sealed class Motorcycle : Vehicle
    {
        internal Motorcycle(Manufacturer manufacturer) : base(manufacturer)
        {
        }

        public override string ToString() => $"Motorcycle: {Manufacturer}";
        public override void ShowInformation() => Console.WriteLine($"Driving a motorcycle from {Manufacturer}");
    }
}
