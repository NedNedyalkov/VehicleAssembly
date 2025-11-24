using System.Diagnostics;
using System.Text;
using VehicleAssembly.Abstractions;

namespace VehicleAssembly.Tests.Utilities
{
    public class MemoryLogger : IVehicleShowInformationHandler
    {
        public StringBuilder Log { get; } = new();

        public void ShowInformation(string message)
        {
            Debug.WriteLine(message);
            Log.AppendLine(message);
        }
    }
}
