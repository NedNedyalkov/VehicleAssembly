using TireFittingShop.Abstractions;

namespace ConsoleApp.Services
{
    public class ConsoleLogger(ITimeProvider timeProvider) : ILogger
    {
        public void WriteLine(string message) => Console.WriteLine($"{timeProvider.Elapsed.TotalSeconds:F1} {message}");
    }
}
