using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Factories;

namespace TireFittingShop.Tests.Tests
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void Customer_Creation_SetsPropertiesCorrectly()
        {
            var customerId = 1;
            var success = VehicleFactory.TryCreateVehicle(CarManufacturersEnum.Toyota, out var vehicle);
            Assert.IsTrue(success);

            var customer = new Domain.Customer(customerId, vehicle!);

            Assert.AreEqual(customerId, customer.Id);
            Assert.AreEqual(vehicle, customer.Vehicle);
        }

        [TestMethod]
        public void Customer_Creation_NullVehicle_ThrowsArgumentNullException()
        {
            var customerId = 1;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var customer = new Domain.Customer(customerId, null!);
            });
        }
    }
}
