namespace Vehicle.Models.Tires
{
    internal class WinterTire(float minTemperatureC, float thicknessCm, float pressureBar) : Tire(pressureBar)
    {
        // TODO: Add some validation for value.
        /// <summary>
        /// Minimum operating temperature of the tire in Celsius degrees
        /// </summary>
        public float MinTemperatureC { get; set; } = minTemperatureC;
        /// <summary>
        /// Thickness of the winter tire in centimeters
        /// </summary>
        public float ThicknessCm { get; set; } = thicknessCm;

        public override string ToString() => $"Winter Tire: MinTemperature = {MinTemperatureC} °C, Thickness = {ThicknessCm} cm, Pressure={PressureBar} bar";
    }
}
