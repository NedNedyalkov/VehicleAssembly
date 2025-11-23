using System.Diagnostics;
using TireFittingShop.Abstractions;

namespace TireFittingShop.Services
{
    public class DebugLogger(ITimeProvider timeProvider) : ILogger
    {
        public void WriteLine(string message) => Debug.WriteLine($"{timeProvider.Elapsed.TotalSeconds:F1} {message}");
    }
}
