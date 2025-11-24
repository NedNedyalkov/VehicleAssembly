namespace VehicleAssembly.Domain.Tires
{
    public abstract record Tires
    {
        /// <summary>
        /// Recommended pressure of the tires in bars
        /// </summary>
        public float PressureBar { get; }

        internal Tires(float pressureBar)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(pressureBar, nameof(pressureBar));
            PressureBar = pressureBar;
        }
    }
}
