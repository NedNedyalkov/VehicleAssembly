using TireFittingShop.Services;
using TireFittingShop.Tests.Utilities;
using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Factories;

namespace TireFittingShop.Tests.Tests
{
    [TestClass]
    public class CancellationTests
    {
        [TestMethod]
        public async Task TireFittingShop_ProcessOrderAsync_CancelsOperationWhenRequested()
        {
            Random rnd = new(42);

            var tireFittingShop = new Simulation.TireFittingShop(
                totalCustomers: 100,
                concurrentMechanics: 4,
                minArrival: TimeSpan.FromSeconds(0),
                maxArrival: TimeSpan.FromSeconds(1),
                minChange: TimeSpan.FromSeconds(2),
                maxChange: TimeSpan.FromSeconds(5),
                customerGeneratorFactory: () => new RandomCustomerFactory(new SystemRandomProvider(seed: rnd.Next())),
                loggerFactory: () => new MemoryLogger(new RealTimeProvider()),
                randomProviderFactory: () => new SystemRandomProvider(seed: rnd.Next()),
                delayProviderFactory: () => new TaskDelayWorkSimulator()
            );

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(1000));

            try
            {
                await tireFittingShop.RunAsync(cts.Token);
                Assert.Fail();
            }
            catch (OperationCanceledException)
            {
            }
        }

        [TestMethod]
        public async Task TaskDelaySimulator_Cancellation_CancelsOperationWhenRequested()
        {
            var delaySimulator = new TaskDelayWorkSimulator();

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(100));

            await Assert.ThrowsExceptionAsync<TaskCanceledException>(async () =>
            {
                await delaySimulator.DoWork(TimeSpan.FromSeconds(5), cts.Token);
            });
        }

        [TestMethod]
        public async Task CustomerConsumer_Cancellation_CancelsOperationWhenRequested()
        {
            var randomProvider = new SystemRandomProvider(seed: 42);
            var delaySimulator = new TaskDelayWorkSimulator();
            var logger = new MemoryLogger(new RealTimeProvider());

            var customerConsumer = new Simulation.CustomerConsumer(
                minChangeTireTime: TimeSpan.FromSeconds(2),
                maxChangeTireTime: TimeSpan.FromSeconds(5),
                randomProvider: randomProvider,
                delayProvider: delaySimulator,
                logger: logger
            );

            VehicleFactory.TryCreateCar(CarManufacturersEnum.Toyota, out var vehicle);
            var customer = new Domain.Customer(Id: 1, Vehicle: vehicle!);

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(100));

            await Assert.ThrowsExceptionAsync<TaskCanceledException>(async () =>
            {
                await customerConsumer.ChangeCustomerTiresAsync(customer, cts.Token);
            });
        }

        [TestMethod]
        public async Task CustomerProducer_Cancellation_CancelsOperationWhenRequested()
        {
            var randomProvider = new SystemRandomProvider();
            var delaySimulator = new TaskDelayWorkSimulator();
            var logger = new MemoryLogger(new RealTimeProvider());
            var customerFactory = new RandomCustomerFactory(randomProvider);

            var customerProducer = new Simulation.CustomerProducer(
                minCustomerArrivalTime: TimeSpan.FromSeconds(1),
                maxCustomerArrivalTime: TimeSpan.FromSeconds(1),
                randomProvider: randomProvider,
                delayProvider: delaySimulator,
                logger: logger,
                customerGenerator: customerFactory
            );

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(100));

            await Assert.ThrowsExceptionAsync<TaskCanceledException>(async () =>
            {
                await customerProducer.ProduceCustomerAsync(cts.Token);
            });
        }
    }
}
