using System.Diagnostics;
using System.Text;
using VehicleAssembly.Abstractions;

namespace VehicleAssembly.Tests.Utilities
{
    public class MemoryLogger : ILogger
    {
        public StringBuilder Log { get; } = new();

        public void WriteLine(string message)
        {
            Debug.WriteLine(message);
            Log.AppendLine(message);
        }
    }
}
