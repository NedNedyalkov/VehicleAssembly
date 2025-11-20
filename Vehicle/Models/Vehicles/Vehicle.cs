using Vehicle.Models.Manufacturers;

namespace Vehicle.Models.Vehicles
{
    public abstract class Vehicle
    {
        internal Vehicle(Manufacturer manufacturer)
        {
            Manufacturer = manufacturer ?? throw new ArgumentNullException(paramName: nameof(manufacturer));
        }

        public Manufacturer Manufacturer { get; init; }

        public abstract void ShowInformation();
    }
}
