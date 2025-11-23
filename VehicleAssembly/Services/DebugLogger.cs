using System.Diagnostics;
using VehicleAssembly.Abstractions;

namespace VehicleAssembly.Services
{
    public record DebugLogger() : ILogger
    {
        public void WriteLine(string message) => Debug.WriteLine(message);
    }
}
