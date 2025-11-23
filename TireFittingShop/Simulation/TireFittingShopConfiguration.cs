using TireFittingShop.Abstractions;

namespace TireFittingShop.Simulation
{
    public class TireFittingShopConfiguration
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

        public TireFittingShopConfiguration(
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
    }
}
