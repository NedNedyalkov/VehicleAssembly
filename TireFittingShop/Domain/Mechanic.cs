using TireFittingShop.Abstractions;
using TireFittingShop.Simulation;
using VehicleAssembly.Abstractions;

namespace TireFittingShop.Domain
{
    internal class Mechanic(
        TimeSpan minCustomerArrivalTime,
        TimeSpan maxCustomerArrivalTime,
        IRandomProvider randomProvider,
        IWorkSimulator delayProvider,
        ILogger logger)
        : CustomerConsumer(
            minCustomerArrivalTime,
            maxCustomerArrivalTime,
            randomProvider,
            delayProvider,
            logger)
    {
    }
}
