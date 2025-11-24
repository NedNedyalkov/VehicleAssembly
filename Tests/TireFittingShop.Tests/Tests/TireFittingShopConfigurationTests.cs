using TireFittingShop.Abstractions;
using TireFittingShop.Services;
using TireFittingShop.Tests.Utilities;

namespace TireFittingShop.Tests.Tests
{
    [TestClass]
    public class TireFittingShopConfigurationTests
    {
        private const int TestSeed = 42;

        private static RandomCustomerFactory CustomerGeneratorFactory() => new(new SystemRandomProvider(seed: TestSeed));
        private static MemoryLogger LoggerFactory() => new(new RealTimeProvider());
        private static SystemRandomProvider RandomProviderFactory() => new(seed: TestSeed);
        private static TaskDelayWorkSimulator WorkSimulatorFactory() => new();

        [TestMethod]
        public void TireFittingShopConfiguration_NoCustomerGeneratorFactory_ThrowException()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() =>
                new Simulation.TireFittingShopConfiguration(
                    totalCustomers: 1,
                    concurrentMechanics: 1,
                    minArrival: TimeSpan.FromSeconds(1),
                    maxArrival: TimeSpan.FromSeconds(1),
                    minChange: TimeSpan.FromSeconds(1),
                    maxChange: TimeSpan.FromSeconds(1),
                    customerGeneratorFactory: null!,
                    loggerFactory: LoggerFactory,
                    randomProviderFactory: RandomProviderFactory,
                    workSimulatorFactory: WorkSimulatorFactory));

            Assert.AreEqual("customerGeneratorFactory", ex.ParamName);
        }

        [TestMethod]
        public void TireFittingShopConfiguration_NoLoggerFactory_ThrowException()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() =>
                new Simulation.TireFittingShopConfiguration(
                    totalCustomers: 1,
                    concurrentMechanics: 1,
                    minArrival: TimeSpan.FromSeconds(1),
                    maxArrival: TimeSpan.FromSeconds(1),
                    minChange: TimeSpan.FromSeconds(1),
                    maxChange: TimeSpan.FromSeconds(1),
                    customerGeneratorFactory: CustomerGeneratorFactory,
                    loggerFactory: null!,
                    randomProviderFactory: RandomProviderFactory,
                    workSimulatorFactory: WorkSimulatorFactory));

            Assert.AreEqual("loggerFactory", ex.ParamName);
        }

        [TestMethod]
        public void TireFittingShopConfiguration_NoRandomProviderFactory_ThrowException()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() =>
                new Simulation.TireFittingShopConfiguration(
                    totalCustomers: 1,
                    concurrentMechanics: 1,
                    minArrival: TimeSpan.FromSeconds(1),
                    maxArrival: TimeSpan.FromSeconds(1),
                    minChange: TimeSpan.FromSeconds(1),
                    maxChange: TimeSpan.FromSeconds(1),
                    customerGeneratorFactory: CustomerGeneratorFactory,
                    loggerFactory: LoggerFactory,
                    randomProviderFactory: null!,
                    workSimulatorFactory: WorkSimulatorFactory));

            Assert.AreEqual("randomProviderFactory", ex.ParamName);
        }

        [TestMethod]
        public void TireFittingShopConfiguration_NoWorkSimulatorFactory_ThrowException()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() =>
                new Simulation.TireFittingShopConfiguration(
                    totalCustomers: 1,
                    concurrentMechanics: 1,
                    minArrival: TimeSpan.FromSeconds(1),
                    maxArrival: TimeSpan.FromSeconds(1),
                    minChange: TimeSpan.FromSeconds(1),
                    maxChange: TimeSpan.FromSeconds(1),
                    customerGeneratorFactory: CustomerGeneratorFactory,
                    loggerFactory: LoggerFactory,
                    randomProviderFactory: RandomProviderFactory,
                    workSimulatorFactory: null!));

            Assert.AreEqual("workSimulatorFactory", ex.ParamName);
        }

        [DataTestMethod]
        [DataRow(0, 1, 0, 1, 2, 5, "totalCustomers")] // 0 customers
        [DataRow(1, 0, 0, 1, 2, 5, "concurrentMechanics")] // 0 mechanics
        [DataRow(1, 1, -1, 0, 2, 5, "minArrival")] // negative min arrival time
        [DataRow(1, 1, 0, -1, 2, 5, "maxArrival")] // negative max arrival time
        [DataRow(1, 1, 1, 0, 5, 2, "minArrival")] // min arrival time > max arrival time
        [DataRow(1, 1, 0, 1, -1, 2, "minChange")] // negative min change time
        [DataRow(1, 1, 0, 1, 2, -1, "maxChange")] // negative max change time
        [DataRow(1, 1, 0, 1, 5, 2, "minChange")] // min change time > max change time
        public void TireFittingShopConfiguration_InvalidSimulationOptions_ThrowException(
            int customers,
            int mechanics,
            double minArrivalTimeRangeSec,
            double maxArrivalTimeRangeSec,
            double minChangeTiresTimeRangeSec,
            double maxChangeTiresTimeRangeSec,
            string expectedExceptionParameterName)
        {
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new Simulation.TireFittingShopConfiguration(
                    totalCustomers: customers,
                    concurrentMechanics: mechanics,
                    minArrival: TimeSpan.FromSeconds(minArrivalTimeRangeSec),
                    maxArrival: TimeSpan.FromSeconds(maxArrivalTimeRangeSec),
                    minChange: TimeSpan.FromSeconds(minChangeTiresTimeRangeSec),
                    maxChange: TimeSpan.FromSeconds(maxChangeTiresTimeRangeSec),
                    customerGeneratorFactory: CustomerGeneratorFactory,
                    loggerFactory: LoggerFactory,
                    randomProviderFactory: RandomProviderFactory,
                    workSimulatorFactory: WorkSimulatorFactory));

            Assert.AreEqual(expectedExceptionParameterName, ex.ParamName);
        }
    }
}
