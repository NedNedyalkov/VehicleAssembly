namespace VehicleAssembly.Domain.Tires
{
    internal class SummerTire : Tire
    {
        public static Lazy<SummerTire> Default { get; } = new(() => new SummerTire());

        /// <summary>
        /// Maximum operating temperature of the tire in Celsius degrees
        /// </summary>
        public float MaxTemperatureC { get; set; }

        public SummerTire(float maxTemperatureC = 50, float pressureBar = 2.5f) : base(pressureBar)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(maxTemperatureC, nameof(maxTemperatureC));
            MaxTemperatureC = maxTemperatureC;
        }

        public override string ToString() => $"Summer Tire: MaxTemperature = {MaxTemperatureC} °C, Pressure = {PressureBar} bar";
        public override bool Equals(object? obj)
            => base.Equals(obj)
            && obj is SummerTire other
            && MaxTemperatureC == other.MaxTemperatureC;
        public override int GetHashCode() => HashCode.Combine(MaxTemperatureC, PressureBar);
    }
}
