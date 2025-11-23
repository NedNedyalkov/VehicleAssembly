using VehicleAssembly.Interfaces;

namespace VehicleAssembly.Models.Tires
{
    internal abstract class Tire : ITire
    {
        /// <summary>
        /// Recommended pressure of the tire in bars
        /// </summary>
        public float PressureBar { get; set; }

        public Tire(float pressureBar)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(pressureBar, nameof(pressureBar));
            PressureBar = pressureBar;
        }

        public override bool Equals(object? obj)
            => obj is Tire other
            && PressureBar == other.PressureBar;
        public override int GetHashCode() => HashCode.Combine(PressureBar);
    }
}
