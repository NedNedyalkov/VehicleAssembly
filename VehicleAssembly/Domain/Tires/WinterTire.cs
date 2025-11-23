namespace VehicleAssembly.Domain.Tires
{
    public sealed record WinterTire : Tire
    {
        /// <summary>
        /// Minimum operating temperature of the tire in Celsius degrees
        /// </summary>
        public float MinTemperatureC { get; }
        /// <summary>
        /// Thickness of the winter tire in centimeters
        /// </summary>
        public float ThicknessCm { get; }

        internal WinterTire(float minTemperatureC, float thicknessCm, float pressureBar) : base(pressureBar)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(thicknessCm, nameof(thicknessCm));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(minTemperatureC, 0, nameof(minTemperatureC));

            MinTemperatureC = minTemperatureC;
            ThicknessCm = thicknessCm;
        }

        public override string ToString() => $"Winter Tire: MinTemperature = {MinTemperatureC} °C, Thickness = {ThicknessCm} cm, Pressure={PressureBar} bar";
    }
}
