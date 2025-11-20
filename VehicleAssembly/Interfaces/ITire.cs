namespace VehicleAssembly.Interfaces
{
    /// <summary>
    /// Represents a type of tire used by a vehicle.
    /// For simplicity, a single ITire instance represents the complete tire set.
    /// </summary>
    public interface ITire
    {
        /// <summary>
        /// Recommended pressure of the tire in bars
        /// </summary>
        float PressureBar { get; }
    }
}
