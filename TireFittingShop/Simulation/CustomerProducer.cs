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

        /// <summary>
        /// Continuously produces customers at random intervals until the total count is reached.
        /// Customers are added to the queue for mechanics to process.
        /// </summary>
        /// <remarks>
        /// This method simulates customer arrivals by:
        /// 1. Waiting a random duration between min/max arrival times
        /// 2. Creating a new customer via the factory
        /// 3. Adding them to the shared queue
        /// 4. Marking the queue complete when all customers have arrived
        /// </remarks>
        internal async Task<Customer> ProduceCustomerAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var waitForCustomer = randomProvider.NextDuration(minCustomerArrivalTime, maxCustomerArrivalTime);
            await delayProvider.DoWork(waitForCustomer, cancellationToken);

            var customer = customerGenerator.Create();
            logger.WriteLine($"Customer {customer.Id} has arrived. Driving a {customer.Vehicle.GetInformation()}...");

            return customer;
        }
    }
}