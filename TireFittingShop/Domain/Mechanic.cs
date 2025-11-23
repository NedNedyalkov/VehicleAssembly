using TireFittingShop.Abstractions;
using TireFittingShop.Simulation;
namespace TireFittingShop.Domain
{
    internal class Mechanic(
        TimeSpan minChangeTireTime,
        TimeSpan maxChangeTireTime,
        IRandomProvider randomProvider,
        IWorkSimulator delayProvider,
        ILogger logger)
        : CustomerConsumer(
            minChangeTireTime,
            maxChangeTireTime,
            randomProvider,
            delayProvider,
            logger)
    {
    }
}
