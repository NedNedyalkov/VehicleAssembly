using TireFittingShop.Abstractions;

namespace TireFittingShop.Services
{
    public sealed class TaskDelayWorkSimulator : IWorkSimulator
    {
        public Task DoWork(TimeSpan delay, CancellationToken cancellationToken) => Task.Delay(delay, cancellationToken);
    }
}