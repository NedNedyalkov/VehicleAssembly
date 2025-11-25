namespace VehicleAssembly.Domain.Manufacturers
{
    public class Manufacturer
    {
        public string Name { get; private init; }

        private Manufacturer(string name) => Name = name;

        internal static Manufacturer Create<T>(T enumValue)
            where T : Enum
            => new(enumValue.ToString());

        public override string ToString() => Name;
    }
}
