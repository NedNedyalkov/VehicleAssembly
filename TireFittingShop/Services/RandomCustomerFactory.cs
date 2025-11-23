using TireFittingShop.Abstractions;
using TireFittingShop.Domain;
using VehicleAssembly.Models.Manufacturers;
using VehicleAssembly.Models.Vehicles;

namespace TireFittingShop.Services
{
    public class RandomCustomerFactory : ICustomerFactory
    {
        private readonly List<Vehicle> _vehicleTypes = [];
        private readonly IRandomProvider _randomProvider;
        private int _nextCustomerId = 1;

        public RandomCustomerFactory(IRandomProvider randomProvider)
        {
            _randomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));

            foreach (var carManufacturer in Enum.GetValues<CarManufacturersEnum>())
            {
                if (VehicleFactory.TryCreateVehicle(carManufacturer, out var car))
                {
                    _vehicleTypes.Add(car!);
                }
            }
        }

        public Customer Create()
        {
            var randomId = _randomProvider.NextInt();
            var vehicle = _vehicleTypes[randomId % _vehicleTypes.Count];
            return new Customer(_nextCustomerId++, vehicle);
        }
    }
}
