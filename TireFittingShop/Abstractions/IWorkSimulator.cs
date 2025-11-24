namespace TireFittingShop.Abstractions
{
    /// <summary>
    /// Represents a simulator that performs units of work asynchronously over a given duration.
    /// </summary>
    public interface IWorkSimulator
    {
        /// <summary>
        /// Performs a simulated unit of work for the specified duration, supporting cancellation.
        /// </summary>
        /// <param name="workDuration">The amount of time to simulate work. Must be positive.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can cancel the work operation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous work operation.</returns>
        /// <remarks>
        /// The operation will complete after <paramref name="workDuration"/>, unless canceled by <paramref name="cancellationToken"/>.
        /// If canceled, the returned task will be in a canceled state. The total <see cref="WorkedDuration"/> will
        /// include work that completed before cancellation.
        /// </remarks>
        Task DoWork(TimeSpan workDuration, CancellationToken cancellationToken);
    }
}
