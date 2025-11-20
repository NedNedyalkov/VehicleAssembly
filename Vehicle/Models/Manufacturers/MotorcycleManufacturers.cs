using Vehicle.Interfaces;

namespace Vehicle.Models.Manufacturers
{
    public class MotorcycleManufacturers
    {
        public static Lazy<IMotorcycleManufacturer> Honda { get; } = new(() => Manufacturers.Honda.Instance.Value);
        public static Lazy<IMotorcycleManufacturer> Ktm { get; } = new(() => Manufacturers.KTM.Instance.Value);
    }
}
