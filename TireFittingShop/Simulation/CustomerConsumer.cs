using TireFittingShop.Abstractions;
using TireFittingShop.Domain;

namespace TireFittingShop.Simulation
{
    internal class CustomerConsumer(
        TimeSpan minChangeTiresTime,
        TimeSpan maxChangeTiresTime,
        IRandomProvider randomProvider,
        IWorkSimulator delayProvider,
        ILogger logger)
    {
        internal async Task ChangeCustomerTiresAsync(Customer customer, CancellationToken cancellationToken)
        {
            var tiresChangeRandomDuration = randomProvider.NextDuration(minChangeTiresTime, maxChangeTiresTime);
            logger.WriteLine($"Customer {customer.Id} car tires are being changed and it will take {tiresChangeRandomDuration.TotalSeconds:F1} seconds.");

            await delayProvider.DoWork(tiresChangeRandomDuration, cancellationToken);

            logger.WriteLine($"Customer {customer.Id} has left.");
        }
    }
}
