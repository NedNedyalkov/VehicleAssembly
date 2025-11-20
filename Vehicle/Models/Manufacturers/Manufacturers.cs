using Vehicle.Interfaces;

namespace Vehicle.Models.Manufacturers
{
    // Honda manufactures both cars and motorcycles.
    internal sealed class Honda : ICarManufacturer, IMotorcycleManufacturer
    {
        private Honda() { }
        internal static Lazy<Honda> Instance { get; } = new(() => new Honda());
        public override string ToString() => nameof(Honda);
    }

    internal sealed class Toyota : ICarManufacturer
    {
        private Toyota() { }
        internal static Lazy<Toyota> Instance { get; } = new(() => new Toyota());
        public override string ToString() => nameof(Toyota);
    }

    internal sealed class KTM : IMotorcycleManufacturer
    {
        private KTM() { }
        internal static Lazy<KTM> Instance { get; } = new(() => new KTM());
        public override string ToString() => nameof(KTM);
    }
}
