using Vehicle.Interfaces;

namespace Vehicle.Models.Manufacturers
{
    internal class CarManufacturers
    {
        internal static readonly Lazy<ICarManufacturer> Honda = new(() => new Honda());
        internal static readonly Lazy<ICarManufacturer> Toyota = new(() => new Toyota());
    }
}
