namespace TireFittingShop.Abstractions
{
    /// <summary>
    /// Provides methods to generate reproducible or seeded random values for the simulation.
    /// </summary>
    public interface IRandomProvider
    {
        /// <summary>Returns a non-negative random integer.</summary>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than <see cref="int.MaxValue"/>.</returns>
        int NextInt();

        /// <summary>
        /// Returns a random <see cref="TimeSpan"/> within the specified range.
        /// </summary>
        /// <param name="min">The inclusive minimum duration.</param>
        /// <param name="max">The exclusive maximum duration. Must be greater than <paramref name="min"/>.</param>
        /// <returns>A random <see cref="TimeSpan"/> such that <paramref name="min"/> ≤ value &lt; <paramref name="max"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="max"/> is less than <paramref name="min"/>.</exception>
        TimeSpan NextDuration(TimeSpan min, TimeSpan max);
    }
}
