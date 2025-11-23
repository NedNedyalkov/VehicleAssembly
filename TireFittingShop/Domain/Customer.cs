using VehicleAssembly.Domain.Vehicles;

namespace TireFittingShop.Domain
{
    public record Customer(int Id, Vehicle Vehicle)
    {
        public Vehicle Vehicle { get; } = Vehicle ?? throw new ArgumentNullException(nameof(Vehicle));
    }
}
