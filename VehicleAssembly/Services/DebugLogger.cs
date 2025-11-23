using System.Diagnostics;
using VehicleAssembly.Abstractions;

namespace VehicleAssembly.Services
{
    /// <summary>
    /// Provides a logger implementation that writes messages to the debug output.
    /// </summary>
    /// <remarks>This logger is a record intentionly. This way it will not be considered when doing
    /// comparison between other records, that keep loggers</remarks>
    public record DebugLogger() : ILogger
    {
        public void WriteLine(string message) => Debug.WriteLine(message);
    }
}
