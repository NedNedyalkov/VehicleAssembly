using System.Reflection;
using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Domain.Tires;
using VehicleAssembly.Domain.Vehicles;
using VehicleAssembly.Factories;
using VehicleAssembly.Tests.Utilities;

namespace VehicleAssembly.Tests.Tests
{
    [TestClass]
    public sealed class VehicleTests
    {
        [TestMethod]
        public void Vehicle_Cars_ShowInformation()
        {
            var logger = new MemoryLogger();
            var car = new Car(CarManufacturers.Honda.Value, logger);

            car.ShowInformation();

            var log = logger.Log.ToString();
            log.ShouldContain("Driving a car from Honda");
            log.ShouldContain("Summer Tire");
        }

        [TestMethod]
        public void Vehicle_Motorcycles_ShowInformation()
        {
            var logger = new MemoryLogger();
            var motorcycle = new Motorcycle(MotorcycleManufacturers.Honda.Value, logger);

            motorcycle.ShowInformation();

            logger.Log.ToString().ShouldContain("Driving a motorcycle from Honda");
        }

        [TestMethod]
        public void Vehicle_ShowInformationReflectsReplacedTires()
        {
            var logger = new MemoryLogger();
            var car = new Car(CarManufacturers.Honda.Value, logger);

            car.ReplaceTires(new WinterTire(-10, 3.1f, 2.0f));
            car.ShowInformation();

            logger.Log.ToString().ShouldContain("Winter Tire: MinTemperature = -10 °C, Thickness = 3.1 cm, Pressure=2 bar");
        }


        [TestMethod]
        public void Vehicle_CreatingADefaultCar_HasDefaultSummerTires()
        {
            var car = new Car(CarManufacturers.Honda.Value);

            Assert.IsInstanceOfType<SummerTire>(car.Tire);
            Assert.AreSame(SummerTire.Default.Value, car.Tire);
        }

        [TestMethod]
        public void Vehicle_CreatingACarWithSpecificSummerTires_HasTheExactSummerTires()
        {
            TireFactory.TryCreateSummerTire(pressureBar: 2.2f, maxTemperatureC: 44, out var expectedSummerTire);
            var car = new Car(CarManufacturers.Honda.Value, expectedSummerTire!);

            Assert.IsInstanceOfType<SummerTire>(car.Tire);
            Assert.AreEqual(expectedSummerTire, car.Tire);
        }

        [TestMethod]
        public void Vehicle_ReplaceCarTires_WorksCorrectly()
        {
            var car = new Car(CarManufacturers.Toyota.Value);
            var expectedWinterTire = new WinterTire(minTemperatureC: -20, thicknessCm: 3.2f, pressureBar: 2.3f);

            car.ReplaceTires(expectedWinterTire);

            Assert.IsInstanceOfType<WinterTire>(car.Tire);
            Assert.AreEqual(expectedWinterTire, car.Tire);
        }

        [TestMethod]
        public void Vehicle_WithMissingManufacturer_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Car(manufacturer: null!));
            Assert.ThrowsException<ArgumentNullException>(() => new Motorcycle(manufacturer: null!));
        }

        [TestMethod]
        public void Vehicle_CarWithNullTire_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Car(CarManufacturers.Honda.Value, tire: null!));
        }

        [TestMethod]
        public void Vehicle_ToString_WorksCorrectly()
        {
            var car = new Car(CarManufacturers.Honda.Value);
            var motorcycle = new Motorcycle(MotorcycleManufacturers.Honda.Value);

            var carString = car.ToString();
            var motorcycleString = motorcycle.ToString();

            carString.ShouldContain("Car: Honda");
            carString.ShouldContain("Summer Tire");
            motorcycleString.ShouldContain("Motorcycle: Honda");
        }

        [TestMethod]
        public void Vehicles_DontHavePublicConstructors()
        {
            var assembly = Assembly.GetAssembly(typeof(Vehicle));
            Assert.IsNotNull(assembly, "Could not get assembly containing IVehicle.");

            var vehicleTypes = assembly.GetTypes().Where(t => typeof(Vehicle).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            Assert.IsTrue(vehicleTypes.Any());

            foreach (var vehicleType in vehicleTypes)
            {
                var constructors = vehicleType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                Assert.IsTrue(constructors.Length >= 1);

                foreach (var constructor in constructors)
                {
                    Assert.IsFalse(constructor.IsPublic, $"Vehicle type {vehicleType.FullName} has a public constructor.");
                }
            }
        }

        [TestMethod]
        public void Vehicles_CreateOverlappingVehicleTypes_ShouldCreateDifferentVehicles()
        {
            VehicleFactory.TryCreateVehicle(CarManufacturersEnum.Honda, out var hondaCar);
            VehicleFactory.TryCreateVehicle(MotorcycleManufacturersEnum.Honda, out var hondaMotorcycle);

            Assert.IsInstanceOfType<Car>(hondaCar);
            Assert.IsInstanceOfType<Motorcycle>(hondaMotorcycle);
        }
    }
}
