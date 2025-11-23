namespace VehicleAssembly.Domain.Manufacturers
{
    internal class CarManufacturers
    {
        internal static Lazy<Manufacturer> Honda { get; } = new(() => Manufacturers.Honda.Instance.Value);
        internal static Lazy<Manufacturer> Toyota { get; } = new(() => Manufacturers.Toyota.Instance.Value);
    }
}
