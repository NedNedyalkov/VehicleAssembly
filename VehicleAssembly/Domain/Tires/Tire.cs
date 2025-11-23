namespace VehicleAssembly.Domain.Tires
{
    public abstract record Tire
    {
        /// <summary>
        /// Recommended pressure of the tire in bars
        /// </summary>
        public float PressureBar { get; }

        internal Tire(float pressureBar)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(pressureBar, nameof(pressureBar));
            PressureBar = pressureBar;
        }
    }
}
