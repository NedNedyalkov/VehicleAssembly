using TireFittingShop.Abstractions;

namespace TireFittingShop.Tests.Utilities
{
    internal class FixedRandomProvider : IRandomProvider
    {
        private readonly TimeSpan? fixedTimeSpan;
        private readonly int? fixedInt;

        public FixedRandomProvider(int fixedInt) => this.fixedInt = fixedInt;
        public FixedRandomProvider(TimeSpan fixedValue) => fixedTimeSpan = fixedValue;
        public TimeSpan NextDuration(TimeSpan from, TimeSpan to) => fixedTimeSpan ?? throw new InvalidOperationException("Fixed value for TimeSpan is not set.");
        public int NextInt() => fixedInt ?? throw new InvalidOperationException("Fixed value for int is not set.");
    }
}
