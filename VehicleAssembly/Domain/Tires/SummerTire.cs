namespace VehicleAssembly.Domain.Tires
{
    public sealed record SummerTire : Tire
    {
        public static Lazy<SummerTire> Default { get; } = new(() => new SummerTire());

        /// <summary>
        /// Maximum operating temperature of the tire in Celsius degrees
        /// </summary>
        public float MaxTemperatureC { get; }

        internal SummerTire(float maxTemperatureC = 50, float pressureBar = 2.5f) : base(pressureBar)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(maxTemperatureC, nameof(maxTemperatureC));
            MaxTemperatureC = maxTemperatureC;
        }

        public override string ToString() => $"Summer Tire: MaxTemperature = {MaxTemperatureC} °C, Pressure = {PressureBar} bar";
    }
}
