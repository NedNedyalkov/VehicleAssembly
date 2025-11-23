using TireFittingShop.Abstractions;

namespace TireFittingShop.Services
{
    public sealed class RealTimeProvider : ITimeProvider
    {
        private DateTime _startTime = DateTime.UtcNow;
        public TimeSpan Elapsed => DateTime.UtcNow - _startTime;
        public void Reset() => _startTime = DateTime.UtcNow;
    }
}
