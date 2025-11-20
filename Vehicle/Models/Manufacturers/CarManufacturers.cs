using Vehicle.Interfaces;

namespace Vehicle.Models.Manufacturers
{
    public class CarManufacturers
    {
        public static Lazy<ICarManufacturer> Honda { get; } = new(() => Manufacturers.Honda.Instance.Value);
        public static Lazy<ICarManufacturer> Toyota { get; } = new(() => Manufacturers.Toyota.Instance.Value);
    }
}
