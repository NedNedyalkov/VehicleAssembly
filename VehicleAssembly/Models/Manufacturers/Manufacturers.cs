namespace VehicleAssembly.Models.Manufacturers
{
    internal sealed class Honda : Manufacturer
    {
        private Honda() { }
        internal static Lazy<Honda> Instance { get; } = new(() => new Honda());
        public override string ToString() => nameof(Honda);
    }

    internal sealed class Toyota : Manufacturer
    {
        private Toyota() { }
        internal static Lazy<Toyota> Instance { get; } = new(() => new Toyota());
        public override string ToString() => nameof(Toyota);
    }

    internal sealed class KTM : Manufacturer
    {
        private KTM() { }
        internal static Lazy<KTM> Instance { get; } = new(() => new KTM());
        public override string ToString() => nameof(KTM);
    }
}
