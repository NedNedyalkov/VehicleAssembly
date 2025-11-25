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
            ShowInformationHandler = logger ?? new DebugVehicleInformationHandler();
        }

        public Manufacturer Manufacturer { get; init; }
        internal IVehicleShowInformationHandler ShowInformationHandler { get; }

        public abstract void ShowInformation();
        public abstract string GetInformation();
    }
}
