namespace VehicleAssembly.Abstractions
{
    /// <summary>
    /// Defines a generic logging interface for messages generated during simulation.
    /// </summary>
    internal interface IVehicleShowInformationHandler
    {
        /// <summary>
        /// Displays information about the vehicle.
        /// </summary>
        /// <param name="information">The information to show</param>
        void ShowInformation(string information);
    }
}
