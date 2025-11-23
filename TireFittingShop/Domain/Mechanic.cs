using TireFittingShop.Abstractions;
using TireFittingShop.Simulation;

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
