using VehicleAssembly.Domain.Tires;

namespace VehicleAssembly.Tests.Tests
{
    [TestClass]
    public class TireTests
    {
        [TestMethod]
        public void SummerTire_WithNegativePressure_ThrowsException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SummerTires(maxTemperatureC: 50, pressureBar: -1.0f));
        }

        [TestMethod]
        public void SummerTire_WithNegativeMaxTemperature_ThrowsException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SummerTires(maxTemperatureC: -5, pressureBar: 2.5f));
        }

        [TestMethod]
        public void WinterTire_WithNegativePressure_ThrowsException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new WinterTires(minTemperatureC: -10, thicknessCm: 3.0f, pressureBar: -2.0f));
        }

        [TestMethod]
        public void WinterTire_WithPositiveMinTemperature_ThrowsException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new WinterTires(minTemperatureC: 5, thicknessCm: 3.0f, pressureBar: 2.0f));
        }

        [TestMethod]
        public void WinterTire_WithNegativeThickness_ThrowsException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new WinterTires(minTemperatureC: -10, thicknessCm: -1.0f, pressureBar: 2.0f));
        }
    }
}
