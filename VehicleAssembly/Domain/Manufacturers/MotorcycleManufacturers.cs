namespace VehicleAssembly.Domain.Manufacturers
{
    public class MotorcycleManufacturers
    {
        public static Lazy<Manufacturer> Honda { get; } = new(() => Manufacturers.Honda.Instance.Value);
        public static Lazy<Manufacturer> Ktm { get; } = new(() => KTM.Instance.Value);
    }
}
