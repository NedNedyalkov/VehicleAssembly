namespace VehicleAssembly.Domain.Tires
{
    public sealed record WinterTires : Tires
    {
        /// <summary>
        /// Minimum operating temperature of the tires in Celsius degrees
        /// </summary>
        public float MinTemperatureC { get; }
        /// <summary>
        /// Thickness of the winter tires in centimeters
        /// </summary>
        public float ThicknessCm { get; }

        internal WinterTires(float minTemperatureC, float thicknessCm, float pressureBar) : base(pressureBar)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(thicknessCm, nameof(thicknessCm));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(minTemperatureC, 0, nameof(minTemperatureC));

            MinTemperatureC = minTemperatureC;
            ThicknessCm = thicknessCm;
        }

        public override string ToString() => $"Winter Tires: MinTemperature = {MinTemperatureC} °C, Thickness = {ThicknessCm} cm, Pressure={PressureBar} bar";
    }
}
