using System.Collections.Concurrent;
using System.Diagnostics;
using TireFittingShop.Abstractions;
using VehicleAssembly.Abstractions;

namespace TireFittingShop.Tests.Utilities
{
    internal class MemoryLogger(ITimeProvider timeProvider) : ILogger
    {
        public record struct LogEntry(TimeSpan Elapsed, string Message);
        public ConcurrentQueue<LogEntry> Logs { get; } = [];
        public ITimeProvider TimeProvider { get; } = timeProvider;

        public void WriteLine(string message)
        {
            Debug.WriteLine($"{TimeProvider.Elapsed.TotalSeconds:F1} {message}");
            Logs.Enqueue(new LogEntry(TimeProvider.Elapsed, message));
        }
    }
}
