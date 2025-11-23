using TireFittingShop.Abstractions;

namespace TireFittingShop.Services
{
    public sealed class TaskDelayWorkSimulator : IWorkSimulator
    {
        private readonly DateTime _startTime = DateTime.UtcNow;
        public TimeSpan WorkedDuration => DateTime.UtcNow - _startTime;

        public Task DoWork(TimeSpan delay, CancellationToken cancellationToken) => Task.Delay(delay, cancellationToken);
    }
}