namespace VehicleAssembly.Models.Manufacturers
{
    public class CarManufacturers
    {
        public static Lazy<Manufacturer> Honda { get; } = new(() => Manufacturers.Honda.Instance.Value);
        public static Lazy<Manufacturer> Toyota { get; } = new(() => Manufacturers.Toyota.Instance.Value);
    }
}
