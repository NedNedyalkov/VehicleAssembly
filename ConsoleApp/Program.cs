using ConsoleApp.Services;
using ConsoleApp.Utilities;
using TireFittingShop.Services;

namespace ConsoleApp
{
    internal partial class Program
    {

        private static readonly List<Parameter> requiredParameters = [
            new Parameter("Customers", typeof(int)),
            new Parameter("Mechanics", typeof(int)),
            new Parameter("Minimum Arrival Time", typeof(float)),
            new Parameter("Maximum Arrival Time", typeof(float)),
            new Parameter("Minimum Tire Change Time", typeof(float)),
            new Parameter("Maximum Tire Change Time", typeof(float))];

        static async Task Main(string[] _)
        {
            Console.WriteLine("Tire Fitting simulation");
            Console.WriteLine("-----------------------");

            await RunMultipleTimes();
        }

        private static async Task RunMultipleTimes()
        {
            while (true)
            {
                ConsoleParametersParser.AskForParameters(requiredParameters);

                try
                {
                    await RunSimulation();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during the simulation: {ex.Message}");
                }

                Console.WriteLine("Do you want to run the simulation again? (y/n):");
                var input = Console.ReadLine();
                if (input == null || !input.Equals("y", StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }

        private static async Task RunSimulation()
        {
            using var cts = new CancellationTokenSource();
            using var consoleCts = new CancellationTokenSource();
            RealTimeProvider timeProvider = null!;
            ConsoleLogger logger = null!;

            var tireFittingShop = new TireFittingShop.Simulation.TireFittingShop(
                totalCustomers: (int)requiredParameters[0].Value,
                concurrentMechanics: (int)requiredParameters[1].Value,
                minArrival: TimeSpan.FromSeconds((float)requiredParameters[2].Value),
                maxArrival: TimeSpan.FromSeconds((float)requiredParameters[3].Value),
                minChange: TimeSpan.FromSeconds((float)requiredParameters[4].Value),
                maxChange: TimeSpan.FromSeconds((float)requiredParameters[5].Value),
                customerGeneratorFactory: () => new RandomCustomerFactory(new SystemRandomProvider()),
                loggerFactory: () => logger = new ConsoleLogger(timeProvider = new RealTimeProvider()),
                randomProviderFactory: () => new SystemRandomProvider(),
                workSimulatorFactory: () => new TaskDelayWorkSimulator());

            var consoleTask = Task.Run(() =>
            {
                while (true)
                {
                    cts.Token.ThrowIfCancellationRequested();
                    consoleCts.Token.ThrowIfCancellationRequested();

                    if (!Console.KeyAvailable)
                    {
                        Thread.Sleep(20);
                    }
                    else if (!string.IsNullOrEmpty(Console.ReadKey(intercept: true).Key.ToString()))
                    {
                        cts.Cancel();
                        break;
                    }
                }
            });

            try
            {
                await tireFittingShop.RunAsync(cts.Token);

                Console.WriteLine();
                Console.WriteLine($"Simulation completed successfully for {timeProvider.Elapsed}");
                Console.WriteLine();
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine();
                Console.WriteLine($"Simulation cancelled after {timeProvider.Elapsed}");
                Console.WriteLine();
            }
            finally
            {
                consoleCts.Cancel();
                try
                {
                    await consoleTask;
                }
                catch (OperationCanceledException)
                {
                    // Ignore cancellation exception of the console task
                }
            }
        }
    }
}
