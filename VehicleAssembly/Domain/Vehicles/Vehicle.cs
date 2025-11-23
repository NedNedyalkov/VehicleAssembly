using VehicleAssembly.Abstractions;
using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Services;

namespace VehicleAssembly.Domain.Vehicles
{
    public abstract record Vehicle
    {
        internal Vehicle(Manufacturer manufacturer, ILogger? logger = null)
        {
            Manufacturer = manufacturer ?? throw new ArgumentNullException(paramName: nameof(manufacturer));
            Logger = logger ?? new DebugLogger();
        }

        public Manufacturer Manufacturer { get; init; }
        internal ILogger Logger { get; }

        public abstract void ShowInformation();
    }
}
