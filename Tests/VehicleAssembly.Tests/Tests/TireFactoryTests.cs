using VehicleAssembly.Domain.Tires;
using VehicleAssembly.Factories;

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
        public void TireFactory_CreateSummerTires_ReturnsSummerTires()
        {
            var result = TiresFactory.TryCreateSummerTires(validPressureBar, validMaxTemperatureC, out var tire);

            Assert.IsTrue(result);
            Assert.IsNotNull(tire);
            Assert.IsInstanceOfType<SummerTires>(tire);
        }

        [TestMethod]
        public void TireFactory_CreateWinterTires_ReturnsWinterTires()
        {
            var result = TiresFactory.TryCreateWinterTires(validPressureBar, validMinTemperatureC, validThicknessCm, out var tire);

            Assert.IsTrue(result);
            Assert.IsNotNull(tire);
            Assert.IsInstanceOfType<WinterTires>(tire);
        }

        [TestMethod]
        public void TiresFactory_CreateTiresWithNegativePressure_ReturnsFalse()
        {
            var summerResult = TiresFactory.TryCreateSummerTires(invalidPressureBar, validMaxTemperatureC, out var summerTires);
            var winterResult = TiresFactory.TryCreateWinterTires(invalidPressureBar, validMinTemperatureC, validThicknessCm, out var winterTires);

            Assert.IsFalse(summerResult);
            Assert.IsFalse(winterResult);
            Assert.IsNull(summerTires);
            Assert.IsNull(winterTires);
        }

        [TestMethod]
        public void TireFactory_CreateSummerTiresWithNegativeMaxTemperature_ReturnsFalse()
        {
            var result = TiresFactory.TryCreateSummerTires(validPressureBar, invalidMaxTemperatureC, out var tire);

            Assert.IsFalse(result);
            Assert.IsNull(tire);
        }

        [TestMethod]
        public void TireFactory_CreateWinterTiresWithPositiveMinTemperature_ReturnsFalse()
        {
            var result = TiresFactory.TryCreateWinterTires(validPressureBar, invalidMinTemperatureC, validThicknessCm, out var tire);

            Assert.IsFalse(result);
            Assert.IsNull(tire);
        }

        [TestMethod]
        public void TireFactory_CreateWinterTiresWithNegativeThickness_ReturnsFalse()
        {
            var result = TiresFactory.TryCreateWinterTires(validPressureBar, validMinTemperatureC, invalidThicknessCm, out var tire);

            Assert.IsFalse(result);
            Assert.IsNull(tire);
        }
    }
}
