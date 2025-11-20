using Vehicle.Interfaces;

namespace Vehicle.Models.Vehicles
{
    public abstract class Vehicle(IManufacturer manufacturer) : IVehicle
    {
        public IManufacturer Manufacturer { get; init; } = manufacturer ?? throw new ArgumentNullException(paramName: nameof(manufacturer));

        public abstract void ShowInformation();
    }
}
