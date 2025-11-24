namespace VehicleAssembly.Domain.Manufacturers
{
    public class Manufacturer
    {
        internal string Name { get; init; }

        private Manufacturer(string name) => Name = name;

        internal static Manufacturer Create<T>(T enumValue)
            where T : Enum
            => new(enumValue.ToString());

        public override string ToString() => Name;
    }
}
