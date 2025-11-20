namespace Vehicle.Models.Tires
{
    internal class SummerTire(float maxTemperatureC = 50, float pressureBar = 2.5f) : Tire(pressureBar)
    {
        // TODO: Add some validation for value.
        /// <summary>
        /// Maximum operating temperature of the tire in Celsius degrees
        /// </summary>
        public float MaxTemperatureC { get; set; } = maxTemperatureC;

        public override string ToString() => $"Summer Tire: MaxTemperature = {MaxTemperatureC} °C, Pressure = {PressureBar} bar";
        public override bool Equals(object? obj)
            => base.Equals(obj)
            && (obj is SummerTire other)
            && MaxTemperatureC == other.MaxTemperatureC;
        public override int GetHashCode() => HashCode.Combine(MaxTemperatureC, PressureBar);
    }
}
