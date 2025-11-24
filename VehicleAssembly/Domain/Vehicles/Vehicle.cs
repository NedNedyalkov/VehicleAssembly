using VehicleAssembly.Abstractions;
using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Services;

namespace VehicleAssembly.Domain.Vehicles
{
    public abstract record Vehicle
    {
        internal Vehicle(Manufacturer manufacturer, IVehicleShowInformationHandler? logger = null)
        {
            Manufacturer = manufacturer ?? throw new ArgumentNullException(paramName: nameof(manufacturer));
            Logger = logger ?? new DebugLogger();
        }

        public Manufacturer Manufacturer { get; init; }
        internal IVehicleShowInformationHandler Logger { get; }

        public abstract void ShowInformation();
        public abstract string GetInformation();
    }
}
