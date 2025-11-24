using System.Collections.Concurrent;
using TireFittingShop.Domain;

namespace TireFittingShop.Simulation
{
    /// <summary>
    /// Orchestrates a tire fitting shop simulation with concurrent customer arrivals and mechanic processing.
    /// </summary>
    /// <remarks>
    /// This class implements a producer-consumer pattern where customers arrive at random intervals
    /// and are processed by multiple mechanics working concurrently. The simulation continues until
    /// all customers have been serviced or a cancellation is requested.
    /// </remarks>
    /// <param name="config">The configuration settings for the simulation.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="config"/> is null.</exception>
    public class TireFittingShop(TireFittingShopConfiguration config)
    {
        private readonly TireFittingShopConfiguration _config = config ?? throw new ArgumentNullException(nameof(config));

        public event Action? SimulationStarted;

        /// <summary>
        /// Runs the tire fitting shop simulation asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the simulation.</param>
        /// <returns>A task that completes when all customers have been processed or cancellation is requested.</returns>
        /// <exception cref="OperationCanceledException">Thrown when the operation is canceled via <paramref name="cancellationToken"/>.</exception>
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            SimulationStarted?.Invoke();
            using var waitingCustomers = new BlockingCollection<Customer>();

            var customerGenerator = new CustomerProducer(
                _config.MinCustomerArrivalTime,
                _config.MaxCustomerArrivalTime,
                _config.RandomProviderFactory(),
                _config.WorkSimulatorFactory(),
                _config.LoggerFactory(),
                _config.CustomerGeneratorFactory());

            // Start customer producer
            var producerTask = Task.Run(() => ProduceCustomersAsync(waitingCustomers, _config.TotalCustomers, customerGenerator, cancellationToken), cancellationToken);

            // Start mechanics
            var mechanicTasks = Enumerable.Range(0, _config.ConcurrentMechanics)
                .Select(_ => Task.Run(async () =>
                {
                    var mechanic = new Mechanic(
                        _config.MinChangeTiresTime,
                        _config.MaxChangeTiresTime,
                        _config.RandomProviderFactory(),
                        _config.WorkSimulatorFactory(),
                        _config.LoggerFactory());
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
