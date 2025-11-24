using VehicleAssembly.Domain.Tires;

namespace VehicleAssembly.Tests.Tests
{
    [TestClass]
    public class TiresTests
    {
        [TestMethod]
        public void SummerTires_WithNegativePressure_ThrowsException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SummerTires(maxTemperatureC: 50, pressureBar: -1.0f));
        }

        [TestMethod]
        public void SummerTires_WithNegativeMaxTemperature_ThrowsException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SummerTires(maxTemperatureC: -5, pressureBar: 2.5f));
        }

        [TestMethod]
        public void WinterTires_WithNegativePressure_ThrowsException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new WinterTires(minTemperatureC: -10, thicknessCm: 3.0f, pressureBar: -2.0f));
        }

        [TestMethod]
        public void WinterTires_WithPositiveMinTemperature_ThrowsException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new WinterTires(minTemperatureC: 5, thicknessCm: 3.0f, pressureBar: 2.0f));
        }

        [TestMethod]
        public void WinterTires_WithNegativeThickness_ThrowsException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new WinterTires(minTemperatureC: -10, thicknessCm: -1.0f, pressureBar: 2.0f));
        }
    }
}
