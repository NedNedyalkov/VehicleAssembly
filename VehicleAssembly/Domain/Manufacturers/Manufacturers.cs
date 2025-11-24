namespace VehicleAssembly.Domain.Manufacturers
{
    public static class Manufacturers
    {
        public static Dictionary<CarManufacturersEnum, Manufacturer> CarManufacturers { get; } = [];
        public static Dictionary<MotorcycleManufacturersEnum, Manufacturer> MotorcycleManufacturers { get; } = [];

        static Manufacturers()
        {
            foreach (var carManufacturer in Enum.GetValues<CarManufacturersEnum>())
                CarManufacturers[carManufacturer] = Manufacturer.Create(carManufacturer);

            foreach (var motorcycleManufacturer in Enum.GetValues<MotorcycleManufacturersEnum>())
                MotorcycleManufacturers[motorcycleManufacturer] = Manufacturer.Create(motorcycleManufacturer);
        }
    }
}
