using System.Diagnostics;
using TireFittingShop.Services;
using TireFittingShop.Tests.Utilities;

namespace TireFittingShop.Tests.Tests
{
    [TestClass]
    public sealed class TireFittingShopTests
    {
        [DataTestMethod]
        //Task cases
        [DataRow(5, 2, 0, 1, 2, 5)]
        [DataRow(100, 4, 0, 1, 2, 5)]
        //Edge cases for mechanics and customers
        [DataRow(4, 1, 0, 1, 2, 5)]
        [DataRow(4, 4, 0, 1, 2, 5)]
        [DataRow(1, 4, 0, 1, 2, 5)]
        [DataRow(1, 1, 0, 1, 2, 5)]
        //With zero duration arrival time
        [DataRow(5, 2, 0, 0, 2, 5)]
        [DataRow(16, 4, 0, 0, 2, 5)]
        [DataRow(4, 1, 0, 0, 2, 5)]
        [DataRow(4, 4, 0, 0, 2, 5)]
        [DataRow(1, 4, 0, 0, 2, 5)]
        [DataRow(1, 1, 0, 0, 2, 5)]
        //With zero duration change time
        [DataRow(5, 2, 0, 1, 0, 0)]
        [DataRow(16, 4, 0, 1, 0, 0)]
        [DataRow(4, 1, 0, 1, 0, 0)]
        [DataRow(4, 4, 0, 1, 0, 0)]
        [DataRow(1, 4, 0, 1, 0, 0)]
        [DataRow(1, 1, 0, 1, 0, 0)]
        //With zero duration arrival and change time
        [DataRow(5, 2, 0, 0, 0, 0)]
        [DataRow(16, 4, 0, 0, 0, 0)]
        [DataRow(4, 1, 0, 0, 0, 0)]
        [DataRow(4, 4, 0, 0, 0, 0)]
        [DataRow(1, 4, 0, 0, 0, 0)]
        [DataRow(1, 1, 0, 0, 0, 0)]

        //With short duration arrival time
        [DataRow(5, 2, 0, 0.1, 2, 5)]
        [DataRow(16, 4, 0, 0.1, 2, 5)]
        [DataRow(4, 1, 0, 0.1, 2, 5)]
        [DataRow(4, 4, 0, 0.1, 2, 5)]
        [DataRow(1, 4, 0, 0.1, 2, 5)]
        [DataRow(1, 1, 0, 0.1, 2, 5)]
        //With short duration change time
        [DataRow(5, 2, 0, 1, 0.1, 0.2)]
        [DataRow(16, 4, 0, 1, 0.1, 0.2)]
        [DataRow(4, 1, 0, 1, 0.1, 0.2)]
        [DataRow(4, 4, 0, 1, 0.1, 0.2)]
        [DataRow(1, 4, 0, 1, 0.1, 0.2)]
        [DataRow(1, 1, 0, 1, 0.1, 0.2)]
        //With short duration arrival and change time
        [DataRow(5, 2, 0, 0.1, 0.1, 0.2)]
        [DataRow(16, 4, 0, 0.1, 0.1, 0.2)]
        [DataRow(4, 1, 0, 0.1, 0.1, 0.2)]
        [DataRow(4, 4, 0, 0.1, 0.1, 0.2)]
        [DataRow(1, 4, 0, 0.1, 0.1, 0.2)]
        [DataRow(1, 1, 0, 0.1, 0.1, 0.2)]

        //With same duration arrival time
        [DataRow(5, 2, 0.5, 0.5, 0, 0)]
        [DataRow(16, 4, 0.5, 0.5, 0, 0)]
        [DataRow(4, 1, 0.5, 0.5, 0, 0)]
        [DataRow(4, 4, 0.5, 0.5, 0, 0)]
        [DataRow(1, 4, 0.5, 0.5, 0, 0)]
        [DataRow(1, 1, 0.5, 0.5, 0, 0)]
        //With same duration change time
        [DataRow(5, 2, 0, 0, 0.1, 0.1)]
        [DataRow(16, 4, 0, 0, 0.1, 0.1)]
        [DataRow(4, 1, 0, 0, 0.1, 0.1)]
        [DataRow(4, 4, 0, 0, 0.1, 0.1)]
        [DataRow(1, 4, 0, 0, 0.1, 0.1)]
        [DataRow(1, 1, 0, 0, 0.1, 0.1)]
        //With same duration arrival and change time
        [DataRow(5, 2, 0.5, 0.5, 0.5, 0.5)]
        [DataRow(16, 4, 0.5, 0.5, 0.5, 0.5)]
        [DataRow(4, 1, 0.5, 0.5, 0.5, 0.5)]
        [DataRow(4, 4, 0.5, 0.5, 0.5, 0.5)]
        [DataRow(1, 4, 0.5, 0.5, 0.5, 0.5)]
        [DataRow(1, 1, 0.5, 0.5, 0.5, 0.5)]

        public async Task TireFittingShop_SuccessCases(
            int customers,
            int mechanics,
            double minArrivalTimeRangeSec,
            double maxArrivalTimeRangeSec,
            double minChangeTireTimeRangeSec,
            double maxChangeTireTimeRangeSec)
        {
            Random rnd = new(42);
            MemoryLogger logger = null!;
            var tireFittingShop = new Simulation.TireFittingShop(
                totalCustomers: customers,
                concurrentMechanics: mechanics,
                minArrival: TimeSpan.FromSeconds(minArrivalTimeRangeSec),
                maxArrival: TimeSpan.FromSeconds(maxArrivalTimeRangeSec),
                minChange: TimeSpan.FromSeconds(minChangeTireTimeRangeSec),
                maxChange: TimeSpan.FromSeconds(maxChangeTireTimeRangeSec),
                customerGeneratorFactory: () => new RandomCustomerFactory(new SystemRandomProvider(seed: rnd.Next())),
                loggerFactory: () => logger = new MemoryLogger(new RealTimeProvider()),
                randomProviderFactory: () => new SystemRandomProvider(seed: rnd.Next()),
                workSimulatorFactory: () => new TaskDelayWorkSimulator()
            );

            Debug.WriteLine($"Starting simulation with {customers} customers and {mechanics} mechanics.");
            Debug.WriteLine($"Arrival time range: {minArrivalTimeRangeSec}s - {maxArrivalTimeRangeSec}s.");
            Debug.WriteLine($"Tire change time range: {minChangeTireTimeRangeSec}s - {maxChangeTireTimeRangeSec}s.");

            await tireFittingShop.RunAsync(cancellationToken: CancellationToken.None);

            CalculateMinAndMaxExpectedDurations(
                customers,
                mechanics,
                minArrivalTimeRangeSec,
                maxArrivalTimeRangeSec,
                minChangeTireTimeRangeSec,
                maxChangeTireTimeRangeSec,
                out var minExpectedDuration,
                out var maxExpectedDuration);

            // Add a small buffer for timing inaccuracies of Task.Delay
            minExpectedDuration -= TimeSpan.FromSeconds(0.1);
            // Add a small buffer for the actual work done
            maxExpectedDuration += TimeSpan.FromSeconds(Math.Max(1, maxExpectedDuration.TotalSeconds * 0.1));

            Debug.WriteLine($"Expected duration between {minExpectedDuration} and {maxExpectedDuration}.");

            var loggedDuration = logger.Logs.Max(log => log.Elapsed);
            Assert.IsTrue(
                loggedDuration >= minExpectedDuration && loggedDuration <= maxExpectedDuration,
                $"Expected duration between {minExpectedDuration} and {maxExpectedDuration}, but was {loggedDuration}.");
        }

        [DataTestMethod]
        [DataRow(0, 1, 0, 1, 2, 5)] // 0 customers
        [DataRow(1, 0, 0, 1, 2, 5)] // 0 mechanics
        [DataRow(1, 1, 1, 0, 5, 2)] // min arrival time > max arrival time
        [DataRow(1, 1, 1, 0, 5, 2)] // min change time > max change time
        public void TireFittingShop_InvalidSimulationOptions_ThrowExceptions(
            int customers,
            int mechanics,
            double minArrivalTimeRangeSec,
            double maxArrivalTimeRangeSec,
            double minChangeTireTimeRangeSec,
            double maxChangeTireTimeRangeSec)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new Simulation.TireFittingShop(
                    totalCustomers: customers,
                    concurrentMechanics: mechanics,
                    minArrival: TimeSpan.FromSeconds(minArrivalTimeRangeSec),
                    maxArrival: TimeSpan.FromSeconds(maxArrivalTimeRangeSec),
                    minChange: TimeSpan.FromSeconds(minChangeTireTimeRangeSec),
                    maxChange: TimeSpan.FromSeconds(maxChangeTireTimeRangeSec)
            ));
        }

        [DataTestMethod]
        [DataRow(0, 1, 0, 1, 2, 5, 0, 0)]
        [DataRow(1, 0, 0, 1, 2, 5, 0, 1)]
        [DataRow(1, 1, 0, 0, 2, 5, 2, 5)]
        [DataRow(1, 1, 0, 1, 0, 0, 0, 1)]
        [DataRow(1, 1, 0, 0, 0, 0, 0, 0)]
        [DataRow(5, 1, 0, 0, 0, 0, 0, 0)]
        public void Test_CalculateMinAndMaxExpectedDurations(
            int customers,
            int mechanics,
            double minArrivalTimeRangeSec,
            double maxArrivalTimeRangeSec,
            double minChangeTireTimeRangeSec,
            double maxChangeTireTimeRangeSec,
            double expectedMinDurationSec,
            double expectedMaxDurationSec
            )
        {
            CalculateMinAndMaxExpectedDurations(
                customers: customers,
                mechanics: mechanics,
                minArrivalTimeRangeSec: minArrivalTimeRangeSec,
                maxArrivalTimeRangeSec: maxArrivalTimeRangeSec,
                minChangeTireTimeRangeSec: minChangeTireTimeRangeSec,
                maxChangeTireTimeRangeSec: maxChangeTireTimeRangeSec,
                out var minExpectedDuration,
                out var maxExpectedDuration);
            Assert.AreEqual(expectedMinDurationSec, minExpectedDuration.TotalSeconds);
            Assert.AreEqual(expectedMaxDurationSec, maxExpectedDuration.TotalSeconds);
        }

        private static void CalculateMinAndMaxExpectedDurations(
            int customers,
            int mechanics,
            double minArrivalTimeRangeSec,
            double maxArrivalTimeRangeSec,
            double minChangeTireTimeRangeSec,
            double maxChangeTireTimeRangeSec,
            out TimeSpan minExpectedDuration,
            out TimeSpan maxExpectedDuration)
        {
            int batches = 0;
            if (customers > 0 && mechanics > 0)
                batches = (int)Math.Ceiling((double)customers / mechanics);

            // arrival times
            double totalArrivalMin = customers * minArrivalTimeRangeSec;
            double totalArrivalMax = customers * maxArrivalTimeRangeSec;

            double firstArrivalMin = minArrivalTimeRangeSec;
            double firstArrivalMax = maxArrivalTimeRangeSec;

            // tire change total per batch
            double totalChangeMin = batches * minChangeTireTimeRangeSec;
            double totalChangeMax = batches * maxChangeTireTimeRangeSec;

            double minExpectedSeconds;
            double maxExpectedSeconds;

            if (mechanics >= customers)
            {
                // Everyone gets served immediately once they arrive
                minExpectedSeconds = firstArrivalMin + totalChangeMin;
                maxExpectedSeconds = totalArrivalMax + totalChangeMax;
            }
            else
            {
                // Some have to wait for a mechanic slot
                minExpectedSeconds = Math.Max(totalArrivalMin, firstArrivalMin + totalChangeMin);
                maxExpectedSeconds = Math.Max(totalArrivalMax, firstArrivalMax + totalChangeMax);
            }

            minExpectedDuration = TimeSpan.FromSeconds(minExpectedSeconds);
            maxExpectedDuration = TimeSpan.FromSeconds(maxExpectedSeconds);
        }
    }
}
