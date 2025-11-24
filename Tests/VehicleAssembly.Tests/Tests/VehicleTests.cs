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
            var car = new Car(Manufacturers.CarManufacturers[CarManufacturersEnum.Honda], logger);

            car.ShowInformation();

            var log = logger.Log.ToString();
            log.ShouldContain("Driving a Car from Honda");
            log.ShouldContain("Summer Tires");
        }

        [TestMethod]
        public void Vehicle_Motorcycles_ShowInformation()
        {
            var logger = new MemoryLogger();
            var motorcycle = new Motorcycle(Manufacturers.MotorcycleManufacturers[MotorcycleManufacturersEnum.Honda], logger);

            motorcycle.ShowInformation();

            logger.Log.ToString().ShouldContain("Driving a Motorcycle from Honda");
        }

        [TestMethod]
        public void Vehicle_ShowInformationReflectsReplacedTires()
        {
            var logger = new MemoryLogger();
            var car = new Car(Manufacturers.CarManufacturers[CarManufacturersEnum.Honda], logger);

            car.ReplaceTires(new WinterTires(-10, 3.1f, 2.0f));
            car.ShowInformation();

            logger.Log.ToString().ShouldContain("Winter Tires: MinTemperature = -10 °C, Thickness = 3.1 cm, Pressure=2 bar");
        }


        [TestMethod]
        public void Vehicle_CreatingADefaultCar_HasDefaultSummerTires()
        {
            var car = new Car(Manufacturers.CarManufacturers[CarManufacturersEnum.Honda]);

            Assert.IsInstanceOfType<SummerTires>(car.Tires);
            Assert.AreSame(SummerTires.Default.Value, car.Tires);
        }

        [TestMethod]
        public void Vehicle_CreatingACarWithSpecificSummerTires_HasTheExactSummerTires()
        {
            TiresFactory.TryCreateSummerTires(pressureBar: 2.2f, maxTemperatureC: 44, out var expectedSummerTires);
            var car = new Car(Manufacturers.CarManufacturers[CarManufacturersEnum.Honda], expectedSummerTires!);

            Assert.IsInstanceOfType<SummerTires>(car.Tires);
            Assert.AreEqual(expectedSummerTires, car.Tires);
        }

        [TestMethod]
        public void Vehicle_ReplaceCarTires_WorksCorrectly()
        {
            var car = new Car(Manufacturers.CarManufacturers[CarManufacturersEnum.Toyota]);
            var expectedWinterTires = new WinterTires(minTemperatureC: -20, thicknessCm: 3.2f, pressureBar: 2.3f);

            car.ReplaceTires(expectedWinterTires);

            Assert.IsInstanceOfType<WinterTires>(car.Tires);
            Assert.AreEqual(expectedWinterTires, car.Tires);
        }

        [TestMethod]
        public void Vehicle_WithMissingManufacturer_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Car(manufacturer: null!));
            Assert.ThrowsException<ArgumentNullException>(() => new Motorcycle(manufacturer: null!));
        }

        [TestMethod]
        public void Vehicle_CarWithNullTires_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Car(Manufacturers.CarManufacturers[CarManufacturersEnum.Honda], tires: null!));
        }

        [TestMethod]
        public void Vehicle_ToString_WorksCorrectly()
        {
            var car = new Car(Manufacturers.CarManufacturers[CarManufacturersEnum.Honda]);
            var motorcycle = new Motorcycle(Manufacturers.MotorcycleManufacturers[MotorcycleManufacturersEnum.KTM]);

            var carString = car.ToString();
            var motorcycleString = motorcycle.ToString();

            carString.ShouldContain("Car: Honda");
            carString.ShouldContain("Summer Tires");
            motorcycleString.ShouldContain("Motorcycle: KTM");
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
