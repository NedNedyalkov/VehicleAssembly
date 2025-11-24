using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Domain.Tires;
using VehicleAssembly.Domain.Vehicles;

namespace VehicleAssembly.Factories
{
    public static class VehicleFactory
    {
        /// <summary>
        /// Attempts to create a <see cref="Car"/> instance based on the specified car manufacturer.
        /// </summary>
        /// <remarks>This method uses the specified <paramref name="manufacturer"/> to determine the type
        /// of <see cref="Car"/> to create. If the manufacturer is not recognized or an error occurs during creation,
        /// the method returns <see langword="false"/> and sets <paramref name="car"/> to <see
        /// langword="null"/>.</remarks>
        /// <param name="manufacturer">The manufacturer used to create the car.</param>
        /// <param name="car">When this method returns, contains the created <see cref="Car"/> instance if the operation succeeds;
        /// otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the <see cref="Car"/> was successfully created; otherwise, <see
        /// langword="false"/>.</returns>
        public static bool TryCreateCar(CarManufacturersEnum manufacturer, out Car? car)
        {
            car = null;
            try
            {
                if (!ManufacturerFactory.TryCreateCarManufacturer(manufacturer, out var result))
                    return false;

                car = new Car(result!);
            }
            catch (ArgumentException) { }
            return car is not null;
        }

        /// <summary>
        /// Attempts to create a <see cref="Car"/> instance using the specified manufacturer and tire.
        /// </summary>
        /// <remarks>This method does not throw exceptions for invalid input or creation failures.
        /// Instead, it returns <see langword="false"/> and sets <paramref name="car"/> to <see langword="null"/> in
        /// such cases.
        /// <para/>
        /// If you want to create a car without specifying tires, use the overload of <see cref="TryCreateCar(CarManufacturersEnum manufaccturer, out var car)"/> instead.
        /// </remarks>
        /// <param name="manufacturer">The manufacturer used to create the car.</param>
        /// <param name="tires">The tires to use for the car. Must not be <see langword="null"/>.</param>
        /// <param name="car">When this method returns, contains the created <see cref="Car"/> instance if the operation succeeds;
        /// otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the car was successfully created; otherwise, <see langword="false"/>.</returns>
        public static bool TryCreateCar(CarManufacturersEnum manufacturer, Tires tires, out Car? car)
        {
            car = null;
            try
            {
                if (!ManufacturerFactory.TryCreateCarManufacturer(manufacturer, out var result))
                    return false;

                car = new Car(result!, tires);
            }
            catch (ArgumentException) { }
            return car is not null;
        }

        /// <summary>
        /// Attempts to create a <see cref="Motorcycle"/> instance based on the specified motorcycle manufacturer.
        /// </summary>
        /// <remarks>This method will return <see langword="false"/> if the specified manufacturer is <see
        /// langword="null"/>  or if an error occurs during the creation process.</remarks>
        /// <param name="manufacturer">The manufacturer used to create the motorcycle.</param>
        /// <param name="motorcycle">When this method returns, contains the created <see cref="Motorcycle"/> instance if the operation succeeds; 
        /// otherwise, <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if the <see cref="Motorcycle"/> was successfully created; otherwise, <see
        /// langword="false"/>.</returns>
        public static bool TryCreateMotorcycle(MotorcycleManufacturersEnum manufacturer, out Motorcycle? motorcycle)
        {
            motorcycle = null;
            try
            {
                if (!ManufacturerFactory.TryCreateMotorcycleManufacturer(manufacturer, out var result))
                    return false;

                motorcycle = new Motorcycle(result!);
            }
            catch (ArgumentException) { }
            return motorcycle is not null;
        }
    }
}
