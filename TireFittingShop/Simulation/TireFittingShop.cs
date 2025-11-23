using System.Collections.Concurrent;
using TireFittingShop.Abstractions;
using TireFittingShop.Domain;

namespace TireFittingShop.Simulation
{
    public class TireFittingShop
    {
        public int TotalCustomers { get; }
        public int ConcurrentMechanics { get; }
        public TimeSpan MinCustomerArrivalTime { get; }
        public TimeSpan MaxCustomerArrivalTime { get; }
        public TimeSpan MinChangeTireTime { get; }
        public TimeSpan MaxChangeTireTime { get; }
        public Func<ICustomerFactory> CustomerGeneratorFactory { get; }
        public Func<ILogger> LoggerFactory { get; }
        public Func<IRandomProvider> RandomProviderFactory { get; }
        public Func<IWorkSimulator> WorkSimulatorFactory { get; }

        public TireFittingShop(
            int totalCustomers,
            int concurrentMechanics,
            TimeSpan minArrival,
            TimeSpan maxArrival,
            TimeSpan minChange,
            TimeSpan maxChange,
            Func<ICustomerFactory> customerGeneratorFactory,
            Func<ILogger> loggerFactory,
            Func<IRandomProvider> randomProviderFactory,
            Func<IWorkSimulator> workSimulatorFactory)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(totalCustomers, nameof(totalCustomers));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(concurrentMechanics, nameof(concurrentMechanics));
            ArgumentOutOfRangeException.ThrowIfNegative(minArrival.TotalSeconds, nameof(minArrival));
            ArgumentOutOfRangeException.ThrowIfNegative(maxArrival.TotalSeconds, nameof(maxArrival));
            ArgumentOutOfRangeException.ThrowIfNegative(minChange.TotalSeconds, nameof(minChange));
            ArgumentOutOfRangeException.ThrowIfNegative(maxChange.TotalSeconds, nameof(maxChange));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(minArrival, maxArrival);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(minChange, maxChange);
            ArgumentNullException.ThrowIfNull(customerGeneratorFactory, nameof(customerGeneratorFactory));
            ArgumentNullException.ThrowIfNull(loggerFactory, nameof(loggerFactory));
            ArgumentNullException.ThrowIfNull(randomProviderFactory, nameof(randomProviderFactory));
            ArgumentNullException.ThrowIfNull(workSimulatorFactory, nameof(workSimulatorFactory));

            TotalCustomers = totalCustomers;
            ConcurrentMechanics = concurrentMechanics;
            MinCustomerArrivalTime = minArrival;
            MaxCustomerArrivalTime = maxArrival;
            MinChangeTireTime = minChange;
            MaxChangeTireTime = maxChange;
            RandomProviderFactory = randomProviderFactory;
            WorkSimulatorFactory = workSimulatorFactory;
            LoggerFactory = loggerFactory;
            CustomerGeneratorFactory = customerGeneratorFactory;
        }

        /// <summary>
        /// Runs the full tire fitting simulation.
        /// Spawns a producer that creates customers and multiple mechanics processing them concurrently.
        /// </summary>
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            using var waitingCustomers = new BlockingCollection<Customer>();
            var sharedWorkSimulator = WorkSimulatorFactory();
            var logger = LoggerFactory();

            var customerGenerator = new CustomerProducer(
                MinCustomerArrivalTime,
                MaxCustomerArrivalTime,
                RandomProviderFactory(),
                sharedWorkSimulator,
                logger,
                CustomerGeneratorFactory());

            // Start customer producer
            var producerTask = Task.Run(() => ProduceCustomersAsync(waitingCustomers, TotalCustomers, customerGenerator, cancellationToken), cancellationToken);

            // Start mechanics
            var mechanicTasks = Enumerable.Range(0, ConcurrentMechanics)
                .Select(_ => Task.Run(async () =>
                {
                    var mechanic = new Mechanic(
                        MinChangeTireTime,
                        MaxChangeTireTime,
                        RandomProviderFactory(),
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
