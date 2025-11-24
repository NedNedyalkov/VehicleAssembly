using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Domain.Tires;
using VehicleAssembly.Domain.Vehicles;
using VehicleAssembly.Factories;

namespace VehicleAssembly.Tests.Tests
{
    [TestClass]
    public sealed class VehicleFactoryTests
    {
        [TestMethod]
        public void VehicleFactory_CreatingVehicle_IsSuccessfull()
        {
            var result = VehicleFactory.TryCreateVehicle(manufacturer: CarManufacturersEnum.Toyota, out var vehicle);
            Assert.IsTrue(result);
            Assert.IsInstanceOfType<Vehicle>(vehicle);
        }

        [TestMethod]
        public void VehicleFactory_CreatingCar_IsSuccessfull()
        {
            var result = VehicleFactory.TryCreateCar(manufacturer: CarManufacturersEnum.Toyota, out var car);
            Assert.IsTrue(result);
            Assert.IsInstanceOfType<Car>(car);
        }

        [TestMethod]
        public void VehicleFactory_CreatingMotorcycle_IsSuccessfull()
        {
            var result = VehicleFactory.TryCreateMotorcycle(manufacturer: MotorcycleManufacturersEnum.Ktm, out var motorcycle);
            Assert.IsTrue(result);
            Assert.IsInstanceOfType<Motorcycle>(motorcycle);
        }


        [TestMethod]
        public void VehicleFactory_CreatingVehicleWithTires_IsSuccessfull()
        {
            var result = VehicleFactory.TryCreateVehicle(manufacturer: CarManufacturersEnum.Toyota, tires: SummerTires.Default.Value, out var vehicle);
            Assert.IsTrue(result);
            Assert.IsInstanceOfType<Vehicle>(vehicle);
        }

        [TestMethod]
        public void VehicleFactory_CreatingCarWithTires_IsSuccessfull()
        {
            var result = VehicleFactory.TryCreateCar(manufacturer: CarManufacturersEnum.Toyota, tires: SummerTires.Default.Value, out var car);
            Assert.IsTrue(result);
            Assert.IsInstanceOfType<Car>(car);
        }

        [TestMethod]
        public void VehicleFactory_CreatingVehicleWithNoTire_DoesNotThrowButReturnsFalse()
        {
            var result = VehicleFactory.TryCreateVehicle(manufacturer: CarManufacturersEnum.Toyota, null!, out var vehicle);
            Assert.IsFalse(result);
            Assert.IsNull(vehicle);
        }

        [TestMethod]
        public void VehicleFactory_CreatingCarWithNoTire_DoesNotThrowButReturnsFalse()
        {
            var result = VehicleFactory.TryCreateCar(manufacturer: CarManufacturersEnum.Toyota, null!, out var car);
            Assert.IsFalse(result);
            Assert.IsNull(car);
        }
    }
}
