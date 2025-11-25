using VehicleAssembly.Abstractions;
using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Services;

namespace VehicleAssembly.Domain.Vehicles
{
    public abstract record Vehicle
    {
        internal Vehicle(Manufacturer manufacturer, IVehicleShowInformationHandler? showInfoHandler = null)
        {
            Manufacturer = manufacturer ?? throw new ArgumentNullException(paramName: nameof(manufacturer));
            ShowInformationHandler = showInfoHandler ?? new DebugVehicleInformationHandler();
        }

        public Manufacturer Manufacturer { get; init; }
        internal IVehicleShowInformationHandler ShowInformationHandler { get; }

        public void ShowInformation() => ShowInformationHandler.ShowInformation($"Driving a {GetInformation()}");
        public abstract string GetInformation();
    }
}
