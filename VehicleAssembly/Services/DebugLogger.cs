using System.Diagnostics;
using VehicleAssembly.Abstractions;

namespace VehicleAssembly.Services
{
    /// <summary>
    /// Provides a logger implementation that writes messages to the debug output.
    /// </summary>
    /// <remarks>
    /// This logger is a record intentionally. This ensures it will not affect equality 
    /// comparisons for other records that contain logger instances.
    /// </remarks>
    public record DebugLogger() : ILogger
    {
        public void WriteLine(string message) => Debug.WriteLine(message);
    }
}
