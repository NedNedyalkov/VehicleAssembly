using Vehicle.Interfaces;

namespace Vehicle.Models.Vehicles
{
    public abstract class Vehicle
    {
        internal Vehicle(IManufacturer manufacturer)
        {
            Manufacturer = manufacturer ?? throw new ArgumentNullException(paramName: nameof(manufacturer));
        }

        public IManufacturer Manufacturer { get; init; }

        public abstract void ShowInformation();
    }
}
