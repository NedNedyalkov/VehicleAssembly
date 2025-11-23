namespace TireFittingShop.Abstractions
{
    /// <summary>
    /// Provides an abstraction over elapsed simulation time.
    /// </summary>
    public interface ITimeProvider
    {
        /// <summary>
        /// Gets the elapsed time since the last <see cref="Reset"/> call.
        /// </summary>
        TimeSpan Elapsed { get; }

        /// <summary>
        /// Resets the elapsed time to zero.
        /// </summary>
        void Reset();
    }
}
