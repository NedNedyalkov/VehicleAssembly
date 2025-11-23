using TireFittingShop.Abstractions;
using TireFittingShop.Domain;
using VehicleAssembly.Abstractions;

namespace TireFittingShop.Simulation
{
    internal class CustomerConsumer(
        TimeSpan minChangeTireTime,
        TimeSpan maxChangeTireTime,
        IRandomProvider randomProvider,
        IWorkSimulator delayProvider,
        ILogger logger)
    {
        public TimeSpan MinChangeTireTime { get; } = minChangeTireTime;
        public TimeSpan MaxChangeTireTime { get; } = maxChangeTireTime;
        public IRandomProvider RandomProvider { get; } = randomProvider;
        public IWorkSimulator DelayProvider { get; } = delayProvider;
        public ILogger Logger { get; } = logger;

        internal async Task ChangeCustomerTiresAsync(Customer customer, CancellationToken cancellationToken)
        {
            var tireChangeRandomDuration = RandomProvider.NextDuration(MinChangeTireTime, MaxChangeTireTime);
            Logger.WriteLine($"Customer {customer.Id} car tires are being changed and it will take {tireChangeRandomDuration.TotalSeconds:F1} seconds.");

            await DelayProvider.DoWork(tireChangeRandomDuration, cancellationToken);

            Logger.WriteLine($"Customer {customer.Id} has left.");
        }
    }
}
