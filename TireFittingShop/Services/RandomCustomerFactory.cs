using System.Diagnostics;
using TireFittingShop.Abstractions;
using TireFittingShop.Domain;
using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Domain.Vehicles;
using VehicleAssembly.Factories;

namespace TireFittingShop.Services
{
    public class RandomCustomerFactory : ICustomerFactory
    {
        private readonly List<Func<Vehicle>> _vehicleFactories = [];
        private readonly IRandomProvider _randomProvider;
        private int _nextCustomerId = 1;

        public RandomCustomerFactory(IRandomProvider randomProvider)
        {
            _randomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));

            foreach (var carManufacturer in Enum.GetValues<CarManufacturersEnum>())
            {
                _vehicleFactories.Add(() =>
                {
                    VehicleFactory.TryCreateVehicle(carManufacturer, out var car);
                    Debug.Assert(car is not null, $"Failed to create car for manufacturer {carManufacturer}.");
                    return car!;
                });
            }
        }

        public Customer Create()
        {
            var randomId = _randomProvider.NextInt();
            var vehicleFactory = _vehicleFactories[randomId % _vehicleFactories.Count];
            return new Customer(_nextCustomerId++, vehicleFactory.Invoke());
        }
    }
}
