using Vehicle.Interfaces;

namespace Vehicle.Models.Tires
{
    internal abstract class Tire(float pressureBar) : ITire
    {
        // TODO: Add some validation for value.
        /// <summary>
        /// Recommended pressure of the tire in bars
        /// </summary>
        public float PressureBar { get; set; } = pressureBar;

        public override bool Equals(object? obj)
            => (obj is Tire other)
            && PressureBar == other.PressureBar;
        public override int GetHashCode() => HashCode.Combine(PressureBar);
    }
}
