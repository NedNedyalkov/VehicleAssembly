using TireFittingShop.Abstractions;
using TireFittingShop.Simulation;
namespace TireFittingShop.Domain
{
    internal class Mechanic(
        TimeSpan minChangeTiresTime,
        TimeSpan maxChangeTiresTime,
        IRandomProvider randomProvider,
        IWorkSimulator delayProvider,
        ILogger logger)
        : CustomerConsumer(
            minChangeTiresTime,
            maxChangeTiresTime,
            randomProvider,
            delayProvider,
            logger)
    {
    }
}
