using TireFittingShop.Abstractions;
using TireFittingShop.Domain;

namespace TireFittingShop.Simulation
{
    internal class CustomerProducer(
        TimeSpan minCustomerArrivalTime,
        TimeSpan maxCustomerArrivalTime,
        IRandomProvider randomProvider,
        IWorkSimulator delayProvider,
        ILogger logger,
        ICustomerFactory customerGenerator)
    {
        internal async Task<Customer> ProduceCustomerAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var waitForCustomer = randomProvider.NextDuration(minCustomerArrivalTime, maxCustomerArrivalTime);
            await delayProvider.DoWork(waitForCustomer, cancellationToken);

            var customer = customerGenerator.Create();
            logger.WriteLine($"Customer {customer.Id} has arrived. Driving a car from {customer.Vehicle.Manufacturer}...");

            return customer;
        }
    }
}