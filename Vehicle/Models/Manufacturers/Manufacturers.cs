using Vehicle.Interfaces;

namespace Vehicle.Models.Manufacturers
{
    // Honda manufactures both cars and motorcycles.
    internal sealed class Honda : ICarManufacturer, IMotorcycleManufacturer
    {
        public override string ToString() => nameof(Honda);
    }

    internal sealed class Toyota : ICarManufacturer
    {
        public override string ToString() => nameof(Toyota);
    }

    internal sealed class KTM : IMotorcycleManufacturer
    {
        public override string ToString() => nameof(KTM);
    }
}
