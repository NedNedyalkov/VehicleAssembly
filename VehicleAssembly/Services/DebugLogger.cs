using System.Diagnostics;
using VehicleAssembly.Abstractions;

namespace VehicleAssembly.Services
{
    public class DebugLogger() : ILogger
    {
        public void WriteLine(string message) => Debug.WriteLine(message);
    }
}
