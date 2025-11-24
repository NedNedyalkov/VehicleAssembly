using System.Diagnostics;
using TireFittingShop.Abstractions;
using TireFittingShop.Domain;
using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Domain.Vehicles;
using VehicleAssembly.Factories;

namespace TireFittingShop.Services
{
    /// <summary>
    /// Provides functionality to create customers with randomly assigned vehicles.
    /// </summary>
    /// <remarks>This factory generates customers with unique IDs and assigns each customer a vehicle 
    /// selected randomly from a predefined set of vehicle manufacturers. The randomness is  determined by the provided
    /// <see cref="IRandomProvider"/> implementation.
    /// <para/>
    /// This class it not thread safe! It's assumed only a single instance of it will run concurrently!</remarks>
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
