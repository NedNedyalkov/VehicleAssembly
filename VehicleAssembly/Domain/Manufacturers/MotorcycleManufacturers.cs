namespace VehicleAssembly.Domain.Manufacturers
{
    internal class MotorcycleManufacturers
    {
        internal static Lazy<Manufacturer> Honda { get; } = new(() => Manufacturers.Honda.Instance.Value);
        internal static Lazy<Manufacturer> Ktm { get; } = new(() => KTM.Instance.Value);
    }
}
