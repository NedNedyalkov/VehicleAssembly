using System.Data;
using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Domain.Vehicles;
using VehicleAssembly.Factories;

namespace VehicleAssembly.Tests.Tests
{
    [TestClass]
    public class ManufacturerFactoryTests
    {
        private CarManufacturersEnum invalidManufacturer = (CarManufacturersEnum)999;
        [DataTestMethod]
        [DataRow(CarManufacturersEnum.Toyota)]
        [DataRow(CarManufacturersEnum.Honda)]
        public void TryCreateCarManufacturer_ValidManufacturer_ReturnsTrueAndManufacturer(CarManufacturersEnum carManufacturer)
        {
            var result = ManufacturerFactory.TryCreateCarManufacturer(carManufacturer, out var manufacturer);

            Assert.IsTrue(result);
            Assert.IsNotNull(manufacturer);
        }

        [DataTestMethod]
        [DataRow(MotorcycleManufacturersEnum.Honda)]
        [DataRow(MotorcycleManufacturersEnum.KTM)]
        public void TryCreateMotorcycleManufacturer_ValidManufacturer_ReturnsTrueAndManufacturer(MotorcycleManufacturersEnum motorcycleManufacturer)
        {
            var result = ManufacturerFactory.TryCreateMotorcycleManufacturer(motorcycleManufacturer, out var manufacturer);

            Assert.IsTrue(result);
            Assert.IsNotNull(manufacturer);
        }

        [TestMethod]
        public void TryCreateCarManufacturer_InvalidManufacturer_ReturnsFalseAndNull()
        {
            bool? result = null;
            Manufacturer? manufacturer = null;
            try
            {
                result = ManufacturerFactory.TryCreateCarManufacturer(invalidManufacturer, out manufacturer);
            }
            catch
            {
                Assert.Fail("ManufacturerFactory.TryCreateCarManufacturer threw an exception for an invalid manufacturer.");
            }

            Assert.IsFalse(result);
            Assert.IsNull(manufacturer);
        }
    }
}
