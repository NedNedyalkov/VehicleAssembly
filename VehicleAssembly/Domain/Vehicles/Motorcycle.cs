using VehicleAssembly.Abstractions;
using VehicleAssembly.Domain.Manufacturers;

namespace VehicleAssembly.Domain.Vehicles
{
    public sealed record Motorcycle : Vehicle
    {
        internal Motorcycle(Manufacturer manufacturer, IVehicleShowInformationHandler? logger = null) : base(manufacturer, logger)
        {
        }

        public override string ToString() => $"Motorcycle: {Manufacturer}";
        public override string GetInformation() => $"Motorcycle from {Manufacturer}";
    }
}
