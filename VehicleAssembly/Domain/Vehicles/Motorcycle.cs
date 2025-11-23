using VehicleAssembly.Abstractions;
using VehicleAssembly.Domain.Manufacturers;

namespace VehicleAssembly.Domain.Vehicles
{
    public sealed record Motorcycle : Vehicle
    {
        internal Motorcycle(Manufacturer manufacturer, ILogger? logger = null) : base(manufacturer, logger)
        {
        }

        public override string ToString() => $"Motorcycle: {Manufacturer}";
        public override void ShowInformation() => Logger.WriteLine($"Driving a motorcycle from {Manufacturer}");
    }
}
