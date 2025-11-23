using TireFittingShop.Abstractions;

namespace TireFittingShop.Simulation
{
    /// <summary>
    /// Provides configuration settings for a tire fitting shop simulation.
    /// </summary>
    /// <remarks>
    /// This class validates all parameters during construction to ensure a valid simulation state.
    /// All factory delegates are invoked during simulation runtime to create necessary components.
    /// </remarks>
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


        /// <summary>
        /// Initializes a new instance of the <see cref="TireFittingShopConfiguration"/> class.
        /// </summary>
        /// <param name="totalCustomers">The total number of customers. Must be greater than zero.</param>
        /// <param name="concurrentMechanics">The number of concurrent mechanics. Must be greater than zero.</param>
        /// <param name="minArrival">The minimum customer arrival time. Must be non-negative.</param>
        /// <param name="maxArrival">The maximum customer arrival time. Must be greater than or equal to <paramref name="minArrival"/>.</param>
        /// <param name="minChange">The minimum tire change duration. Must be non-negative.</param>
        /// <param name="maxChange">The maximum tire change duration. Must be greater than or equal to <paramref name="minChange"/>.</param>
        /// <param name="customerGeneratorFactory">Factory for creating customer generators. Must be present.</param>
        /// <param name="loggerFactory">Factory for creating loggers. Must be present.</param>
        /// <param name="randomProviderFactory">Factory for creating random providers. Must be present.</param>
        /// <param name="workSimulatorFactory">Factory for creating work simulators. Must be present.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when any numeric parameter is out of valid range or when min values exceed max values.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when any factory parameter is null.
        /// </exception>
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
