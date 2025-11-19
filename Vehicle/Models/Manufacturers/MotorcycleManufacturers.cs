using Vehicle.Interfaces;

namespace Vehicle.Models.Manufacturers
{
    internal class MotorcycleManufacturers
    {
        internal static readonly Lazy<IMotorcycleManufacturer> Honda = new(() => new Honda());
        internal static readonly Lazy<IMotorcycleManufacturer> Ktm = new(() => new KTM());
    }
}
