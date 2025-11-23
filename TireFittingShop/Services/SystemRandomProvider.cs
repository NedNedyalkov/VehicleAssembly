using TireFittingShop.Abstractions;

namespace TireFittingShop.Services
{
    public class SystemRandomProvider(int? seed = null) : IRandomProvider
    {
        private readonly Random _rnd = seed.HasValue? new(seed.Value) : new();
        public int NextInt() => _rnd.Next();
        public TimeSpan NextDuration(TimeSpan min, TimeSpan max)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(min, max, nameof(min));
            var range = max - min;
            return min + TimeSpan.FromTicks((long)(_rnd.NextDouble() * range.Ticks));
        }
    }
}
