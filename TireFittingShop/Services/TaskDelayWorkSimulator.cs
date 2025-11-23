using TireFittingShop.Abstractions;

namespace TireFittingShop.Services
{
    public sealed class TaskDelayWorkSimulator : IWorkSimulator
    {
        private DateTime _startTime = DateTime.UtcNow;
        public TimeSpan WorkedDuration
        {
            get => DateTime.UtcNow - _startTime;
            set => _startTime = DateTime.UtcNow - value;
        }

        public Task DoWork(TimeSpan delay, CancellationToken cancellationToken) => Task.Delay(delay, cancellationToken);
    }
}