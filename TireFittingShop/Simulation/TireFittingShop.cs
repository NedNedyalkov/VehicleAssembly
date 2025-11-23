using System.Collections.Concurrent;
using TireFittingShop.Abstractions;
using TireFittingShop.Domain;

namespace TireFittingShop.Simulation
{
    public class TireFittingShop(TireFittingShopConfiguration config)
    {
        /// <summary>
        /// Runs the full tire fitting simulation.
        /// Spawns a producer that creates customers and multiple mechanics processing them concurrently.
        /// </summary>
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            using var waitingCustomers = new BlockingCollection<Customer>();
            var sharedWorkSimulator = config.WorkSimulatorFactory();
            var logger = config.LoggerFactory();

            var customerGenerator = new CustomerProducer(
                config.MinCustomerArrivalTime,
                config.MaxCustomerArrivalTime,
                config.RandomProviderFactory(),
                sharedWorkSimulator,
                logger,
                config.CustomerGeneratorFactory());

            // Start customer producer
            var producerTask = Task.Run(() => ProduceCustomersAsync(waitingCustomers, config.TotalCustomers, customerGenerator, cancellationToken), cancellationToken);

            // Start mechanics
            var mechanicTasks = Enumerable.Range(0, config.ConcurrentMechanics)
                .Select(_ => Task.Run(async () =>
                {
                    var mechanic = new Mechanic(
                        config.MinChangeTireTime,
                        config.MaxChangeTireTime,
                        config.RandomProviderFactory(),
                        sharedWorkSimulator,
                        logger);
                    await MechanicWorkLoopAsync(waitingCustomers, mechanic, cancellationToken);
                }, cancellationToken));

            List<Task> allTasks = [producerTask, .. mechanicTasks];
            await Task.WhenAll(allTasks);
        }

        /// <summary>
        /// Continuously produces customers until the total is reached.
        /// When done, it marks the queue as complete.
        /// </summary>
        private static async Task ProduceCustomersAsync(
            BlockingCollection<Customer> waitingCustomers,
            int totalCustomers,
            CustomerProducer customerProducer,
            CancellationToken cancellationToken)
        {
            for (int i = 0; i < totalCustomers; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var customer = await customerProducer.ProduceCustomerAsync(cancellationToken);
                waitingCustomers.Add(customer, cancellationToken);
            }

            waitingCustomers.CompleteAdding();
        }

        /// <summary>
        /// Each mechanic continuously attempts to take customers from the queue.
        /// When the queue is empty and completed, the loop exits.
        /// </summary>
        private static async Task MechanicWorkLoopAsync(
            BlockingCollection<Customer> waitingCustomers,
            Mechanic mechanic,
            CancellationToken cancellationToken)
        {
            foreach (var customer in waitingCustomers.GetConsumingEnumerable(cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();

                await mechanic.ChangeCustomerTiresAsync(customer, cancellationToken);
            }
        }
    }
}
