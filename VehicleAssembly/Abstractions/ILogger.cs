namespace VehicleAssembly.Abstractions
{
    /// <summary>
    /// Defines a generic logging interface for messages generated during simulation.
    /// </summary>
    internal interface ILogger
    {
        /// <summary>
        /// Writes a message to the log or output.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void WriteLine(string message);
    }
}
