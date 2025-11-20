using System.Reflection;

namespace Vehicle.Tests
{
    [TestClass]
    public sealed class VehicleTests
    {
        [TestMethod]
        [DoNotParallelize] // Do not parallelize to avoid console output conflicts
        public void Vehicle_Cars_ShowInformation()
        {
            var car = new Car(CarManufacturers.Honda.Value);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            car.ShowInformation();

            var output = sw.ToString();

            output.ShouldContain("Driving a car from Honda");
            output.ShouldContain("Summer Tire");
        }

        [TestMethod]
        [DoNotParallelize] // Do not parallelize to avoid console output conflicts
        public void Vehicle_Motorcycles_ShowInformation()
        {
            var motorcycle = new Motorcycle(MotorcycleManufacturers.Honda.Value);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            motorcycle.ShowInformation();

            var output = sw.ToString();
            var expected = "Driving a motorcycle from Honda";
            output.ShouldContain(expected);
        }

        [TestMethod]
        [DoNotParallelize] // Do not parallelize to avoid console output conflicts
        public void Vehicle_ShowInformationReflectsReplacedTires()
        {
            var car = new Car(CarManufacturers.Honda.Value);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            car.ReplaceTires(new WinterTire(-10, 3.1f, 2.0f));
            car.ShowInformation();
            var output = sw.ToString();

            output.ShouldContain("Winter Tire: MinTemperature = -10 °C, Thickness = 3.1 cm, Pressure=2 bar");
        }


        [TestMethod]
        public void Vehicle_CreatingADefaultCar_HasDefaultSummerTires()
        {
            var car = new Car(CarManufacturers.Honda.Value);

            Assert.IsInstanceOfType<SummerTire>(car.Tire);
            Assert.AreEqual(new SummerTire(), car.Tire);
        }

        [TestMethod]
        public void Vehicle_CreatingACarWithSpecificSummerTires_HasTheExactSummerTires()
        {
            var expectedSummerTire = new SummerTire(maxTemperatureC: 44, pressureBar: 2.2f);
            var car = new Car(CarManufacturers.Honda.Value, expectedSummerTire);

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
            var assembly = Assembly.GetAssembly(typeof(IVehicle));
            Assert.IsNotNull(assembly, "Could not get assembly containing IVehicle.");

            var vehicleTypes = assembly.GetTypes().Where(t => typeof(IVehicle).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            Assert.IsTrue(vehicleTypes.Count() >= 1);

            foreach (var vehicleType in vehicleTypes)
            {
                var constructors = vehicleType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                Assert.IsTrue(constructors.Count() >= 1);

                foreach (var constructor in constructors)
                {
                    Assert.IsFalse(constructor.IsPublic, $"Vehicle type {vehicleType.FullName} has a public constructor.");
                }
            }
        }
    }
}
