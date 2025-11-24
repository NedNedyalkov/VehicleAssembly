namespace VehicleAssembly.Domain.Tires
{
    public sealed record SummerTires : Tires
    {
        public static Lazy<SummerTires> Default { get; } = new(() => new SummerTires());

        /// <summary>
        /// Maximum operating temperature of the tires in Celsius degrees
        /// </summary>
        public float MaxTemperatureC { get; }

        internal SummerTires(float maxTemperatureC = 50, float pressureBar = 2.5f) : base(pressureBar)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(maxTemperatureC, nameof(maxTemperatureC));
            MaxTemperatureC = maxTemperatureC;
        }

        public override string ToString() => $"Summer Tires: MaxTemperature = {MaxTemperatureC} °C, Pressure = {PressureBar} bar";
    }
}
