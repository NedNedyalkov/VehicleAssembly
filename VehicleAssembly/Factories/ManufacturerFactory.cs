using VehicleAssembly.Domain.Manufacturers;

namespace VehicleAssembly.Factories
{
    public static class ManufacturerFactory
    {
        public static bool TryCreateCarManufacturer(CarManufacturersEnum carManufacturer, out Manufacturer? manufacturer)
        {
            try
            {
                manufacturer = carManufacturer switch
                {
                    CarManufacturersEnum.Honda => CarManufacturers.Honda.Value,
                    CarManufacturersEnum.Toyota => CarManufacturers.Toyota.Value,
                    _ => null
                };
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
                manufacturer = motorcycleManufacturer switch
                {
                    MotorcycleManufacturersEnum.Honda => MotorcycleManufacturers.Honda.Value,
                    MotorcycleManufacturersEnum.Ktm => MotorcycleManufacturers.Ktm.Value,
                    _ => null
                };
            }
            catch
            {
                manufacturer = null;
            }
            return manufacturer is not null;
        }
    }
}
