namespace Vehicle.Tests
{
    [TestClass]
    public sealed class VehicleFactoryTests
    {
        [TestMethod]
        public void VehicleFactory_CreatingVehicleWithNoManufacturer_DoesNotThrowButReturnsFalse()
        {
            var result = VehicleFactory.TryCreateVehicle(manufacturer: null!, out var vehicle);
            Assert.IsFalse(result);
            Assert.IsNull(vehicle);
        }

        [TestMethod]
        public void VehicleFactory_CreatingCarWithNoManufacturer_DoesNotThrowButReturnsFalse()
        {
            var result = VehicleFactory.TryCreateCar(manufacturer: null!, out var car);
            Assert.IsFalse(result);
            Assert.IsNull(car);
        }

        [TestMethod]
        public void VehicleFactory_CreatingMotorcycleWithNoManufacturer_DoesNotThrowButReturnsFalse()
        {
            var result = VehicleFactory.TryCreateMotorcycle(manufacturer: null!, out var motorcycle);
            Assert.IsFalse(result);
            Assert.IsNull(motorcycle);
        }

        [TestMethod]
        public void VehicleFactory_CreatingVehicleWithNoTire_DoesNotThrowButReturnsFalse()
        {
            var result = VehicleFactory.TryCreateVehicle(manufacturer: CarManufacturers.Toyota.Value, null!, out var vehicle);
            Assert.IsFalse(result);
            Assert.IsNull(vehicle);
        }

        [TestMethod]
        public void VehicleFactory_CreatingCarWithNoTire_DoesNotThrowButReturnsFalse()
        {
            var result = VehicleFactory.TryCreateCar(manufacturer: CarManufacturers.Toyota.Value, null!, out var car);
            Assert.IsFalse(result);
            Assert.IsNull(car);
        }

        [TestMethod]
        public void VehicleFactory_CreatingVehicleWithNoTireAndMotorcycleManufacturer_DoesNotThrowButReturnsFalse()
        {
            var result = VehicleFactory.TryCreateVehicle(manufacturer: MotorcycleManufacturers.Ktm.Value, null!, out var motorcycle);
            Assert.IsFalse(result);
            Assert.IsNull(motorcycle);
        }
    }
}
