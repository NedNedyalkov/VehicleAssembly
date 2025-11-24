using VehicleAssembly.Domain.Manufacturers;

namespace VehicleAssembly.Factories
{
    public static class ManufacturerFactory
    {
        public static bool TryCreateCarManufacturer(CarManufacturersEnum carManufacturer, out Manufacturer? manufacturer)
        {
            try
            {
                Manufacturers.CarManufacturers.TryGetValue(carManufacturer, out manufacturer);
            }
            catch
            {
                manufacturer = null;
            }
            return manufacturer is not null;
        }

        public static bool TryCreateMotorcycleManufacturer(MotorcycleManufacturersEnum motorcycleManufacturer, out Manufacturer? manufacturer)
        {
            try
            {
                Manufacturers.MotorcycleManufacturers.TryGetValue(motorcycleManufacturer, out manufacturer);
            }
            catch
            {
                manufacturer = null;
            }
            return manufacturer is not null;
        }
    }
}
