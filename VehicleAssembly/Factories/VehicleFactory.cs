using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Domain.Tires;
using VehicleAssembly.Domain.Vehicles;

namespace VehicleAssembly.Factories
{
    public static class VehicleFactory
    {
        /// <summary>
        /// Attempts to create a vehicle instance from the specified manufacturer.
        /// </summary>
        /// <remarks>This method determines the type of vehicle to create based on the type of the
        /// provided manufacturer. If the manufacturer is not recognized or an error occurs during creation, the method
        /// returns <see langword="false"/> and sets <paramref name="vehicle"/> to <see langword="null"/>.</remarks>
        /// <param name="manufacturer">The manufacturer used to create the vehicle.</param>
        /// <param name="vehicle">When this method returns, contains the created vehicle if the operation succeeded; otherwise, <see
        /// langword="null"/>.</param>
        /// <returns><see langword="true"/> if the vehicle was successfully created; otherwise, <see langword="false"/>.</returns>
        public static bool TryCreateVehicle(CarManufacturersEnum manufacturer, out Vehicle? vehicle)
        {
            try
            {
                TryCreateCar(manufacturer, out var car);
                vehicle = car;
            }
            catch
            {
                vehicle = null;
            }
            return vehicle is not null;
        }
        /// <summary>
        /// Attempts to create a vehicle instance from the specified manufacturer.
        /// </summary>
        /// <remarks>This method determines the type of vehicle to create based on the type of the
        /// provided manufacturer. If the manufacturer is not recognized or an error occurs during creation, the method
        /// returns <see langword="false"/> and sets <paramref name="vehicle"/> to <see langword="null"/>.</remarks>
        /// <param name="manufacturer">The manufacturer used to create the vehicle.</param>
        /// <param name="vehicle">When this method returns, contains the created vehicle if the operation succeeded; otherwise, <see
        /// langword="null"/>.</param>
        /// <returns><see langword="true"/> if the vehicle was successfully created; otherwise, <see langword="false"/>.</returns>
        public static bool TryCreateVehicle(MotorcycleManufacturersEnum manufacturer, out Vehicle? vehicle)
        {
            try
            {
                TryCreateMotorcycle(manufacturer, out var motorcycle);
                vehicle = motorcycle;
            }
            catch
            {
                vehicle = null;
            }
            return vehicle is not null;
        }

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
            catch { }
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
            catch { }
            return motorcycle is not null;
        }

        /// <summary>
        /// Attempts to create a vehicle using the specified manufacturer and tire.
        /// </summary>
        /// <remarks>This method supports creating vehicles for manufacturers that implement <see
        /// cref="ICarManufacturer"/>. Manufacturers of other vehicle types, such as motorcycles, are not currently
        /// supported. If the manufacturer is <see langword="null"/> or an exception occurs during the creation process,
        /// the method returns <see langword="false"/>.
        /// <para/>
        /// If you want to create a vehicle without specifying tires, use the overload of <see cref="TryCreateVehicle(CarManufacturersEnum manufaccturer, out var vehicle)"/> instead.
        /// </remarks>
        /// <param name="manufacturer">The manufacturer used to create the vehicle.</param>
        /// <param name="tires">The tires to be used in the vehicle creation process. Must not be <see langword="null"/>.</param>
        /// <param name="vehicle">When this method returns, contains the created vehicle if the operation was successful; otherwise, <see
        /// langword="null"/>.</param>
        /// <returns><see langword="true"/> if the vehicle was successfully created; otherwise, <see langword="false"/>.</returns>
        public static bool TryCreateVehicle(CarManufacturersEnum manufacturer, Tires tires, out Vehicle? vehicle)
        {
            try
            {
                TryCreateCar(manufacturer, tires, out var car);
                vehicle = car;
            }
            catch
            {
                vehicle = null;
            }
            return vehicle is not null;
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
            catch { }
            return car is not null;
        }
    }
}
