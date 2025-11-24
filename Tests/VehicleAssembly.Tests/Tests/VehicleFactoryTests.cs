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
        public void VehicleFactory_CreatingCar_IsSuccessfull()
        {
            var result = VehicleFactory.TryCreateCar(manufacturer: CarManufacturersEnum.Toyota, out var car);
            Assert.IsTrue(result);
            Assert.IsInstanceOfType<Car>(car);
        }

        [TestMethod]
        public void VehicleFactory_CreatingMotorcycle_IsSuccessfull()
        {
            var result = VehicleFactory.TryCreateMotorcycle(manufacturer: MotorcycleManufacturersEnum.KTM, out var motorcycle);
            Assert.IsTrue(result);
            Assert.IsInstanceOfType<Motorcycle>(motorcycle);
        }

        [TestMethod]
        public void VehicleFactory_CreatingCarWithTires_IsSuccessfull()
        {
            var result = VehicleFactory.TryCreateCar(manufacturer: CarManufacturersEnum.Toyota, tires: SummerTires.Default.Value, out var car);
            Assert.IsTrue(result);
            Assert.IsInstanceOfType<Car>(car);
        }

        [TestMethod]
        public void VehicleFactory_CreatingCarWithNoTires_DoesNotThrowButReturnsFalse()
        {
            var result = VehicleFactory.TryCreateCar(manufacturer: CarManufacturersEnum.Toyota, tires: null!, out var car);
            Assert.IsFalse(result);
            Assert.IsNull(car);
        }
    }
}
