namespace Vehicle.Interfaces
{
    /// <summary>
    /// Represents a vehicle with associated manufacturer details and the ability to display its information.
    /// </summary>
    public interface IVehicle
    {
        IManufacturer Manufacturer { get; }

        void ShowInformation();
    }
}
