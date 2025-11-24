using System.Collections.Concurrent;
using System.Diagnostics;
using TireFittingShop.Abstractions;

namespace TireFittingShop.Tests.Utilities
{
    internal class MemoryLogger(ITimeProvider timeProvider) : ILogger
    {
        public record struct LogEntry(TimeSpan Elapsed, string Message);
        public ConcurrentQueue<LogEntry> Logs { get; } = [];

        public void WriteLine(string message)
        {
            var elapsed = timeProvider.Elapsed;

            Debug.WriteLine($"{elapsed.TotalSeconds:F1} {message}");
            Logs.Enqueue(new LogEntry(elapsed, message));
        }
    }
}
