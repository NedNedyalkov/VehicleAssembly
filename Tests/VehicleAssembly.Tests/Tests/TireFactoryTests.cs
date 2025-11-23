using VehicleAssembly.Domain.Tires;
using VehicleAssembly.Domain.Vehicles;

namespace VehicleAssembly.Tests.Tests
{
    [TestClass]
    public class TireFactoryTests
    {
        private const float validPressureBar = 2.2f;
        private const float validMaxTemperatureC = 35f;
        private const float validMinTemperatureC = -20f;
        private const float validThicknessCm = 2.0f;
        private const float invalidPressureBar = -1.0f;
        private const float invalidMaxTemperatureC = -5f;
        private const float invalidMinTemperatureC = 20f;
        private const float invalidThicknessCm = -2.0f;

        [TestMethod]
        public void TireFactory_CreateSummerTire_ReturnsASummerTire()
        {
            var result = TireFactory.TryCreateSummerTire(validPressureBar, validMaxTemperatureC, out var tire);

            Assert.IsTrue(result);
            Assert.IsNotNull(tire);
            Assert.IsInstanceOfType<SummerTire>(tire);
        }

        [TestMethod]
        public void TireFactory_CreateWinterTire_ReturnsAWinterTire()
        {
            var result = TireFactory.TryCreateWinterTire(validPressureBar, validMinTemperatureC, validThicknessCm, out var tire);

            Assert.IsTrue(result);
            Assert.IsNotNull(tire);
            Assert.IsInstanceOfType<WinterTire>(tire);
        }

        [TestMethod]
        public void TireFactory_CreateTiresWithNegativePressure_ReturnsFalse()
        {
            var summerResult = TireFactory.TryCreateSummerTire(invalidPressureBar, validMaxTemperatureC, out var summerTire);
            var winterResult = TireFactory.TryCreateWinterTire(invalidPressureBar, validMinTemperatureC, validThicknessCm, out var winterTire);

            Assert.IsFalse(summerResult);
            Assert.IsFalse(winterResult);
            Assert.IsNull(summerTire);
            Assert.IsNull(winterTire);
        }

        [TestMethod]
        public void TireFactory_CreateSummerTireWithNegativeMaxTemperature_ReturnsFalse()
        {
            var result = TireFactory.TryCreateSummerTire(validPressureBar, invalidMaxTemperatureC, out var tire);

            Assert.IsFalse(result);
            Assert.IsNull(tire);
        }

        [TestMethod]
        public void TireFactory_CreateWinterTireWithPositiveMinTemperature_ReturnsFalse()
        {
            var result = TireFactory.TryCreateWinterTire(validPressureBar, invalidMinTemperatureC, validThicknessCm, out var tire);

            Assert.IsFalse(result);
            Assert.IsNull(tire);
        }

        [TestMethod]
        public void TireFactory_CreateWinterTireWithNegativeThickness_ReturnsFalse()
        {
            var result = TireFactory.TryCreateWinterTire(validPressureBar, validMinTemperatureC, invalidThicknessCm, out var tire);

            Assert.IsFalse(result);
            Assert.IsNull(tire);
        }
    }
}
