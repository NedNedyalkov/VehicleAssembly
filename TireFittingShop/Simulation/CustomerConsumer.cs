using TireFittingShop.Abstractions;
using TireFittingShop.Domain;

namespace TireFittingShop.Simulation
{
    internal class CustomerConsumer(
        TimeSpan minChangeTireTime,
        TimeSpan maxChangeTireTime,
        IRandomProvider randomProvider,
        IWorkSimulator delayProvider,
        ILogger logger)
    {
        internal async Task ChangeCustomerTiresAsync(Customer customer, CancellationToken cancellationToken)
        {
            var tireChangeRandomDuration = randomProvider.NextDuration(minChangeTireTime, maxChangeTireTime);
            logger.WriteLine($"Customer {customer.Id} car tires are being changed and it will take {tireChangeRandomDuration.TotalSeconds:F1} seconds.");

            await delayProvider.DoWork(tireChangeRandomDuration, cancellationToken);

            logger.WriteLine($"Customer {customer.Id} has left.");
        }
    }
}
