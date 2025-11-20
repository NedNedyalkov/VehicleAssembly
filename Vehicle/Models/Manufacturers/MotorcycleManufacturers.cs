using Vehicle.Interfaces;

namespace Vehicle.Models.Manufacturers
{
    public class MotorcycleManufacturers
    {
        public static Lazy<Manufacturer> Honda { get; } = new(() => Manufacturers.Honda.Instance.Value);
        public static Lazy<Manufacturer> Ktm { get; } = new(() => Manufacturers.KTM.Instance.Value);
    }
}
