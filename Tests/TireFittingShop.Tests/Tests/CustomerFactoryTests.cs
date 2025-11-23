using TireFittingShop.Domain;
using TireFittingShop.Services;
using TireFittingShop.Tests.Utilities;

namespace TireFittingShop.Tests.Tests
{
    [TestClass]
    public partial class CustomerFactoryTests
    {
        [TestMethod]
        public void CustomerFactory_Create_ReturnsACustomer()
        {
            var randomProvider = new SystemRandomProvider();
            var factory = new RandomCustomerFactory(randomProvider);

            var customer = factory.Create();

            Assert.IsInstanceOfType<Customer>(customer);
        }

        [TestMethod]
        public void CustomerFactory_CreateACoupleOfCustomers_ReturnsDifferentCustomers()
        {
            var randomProvider = new SystemRandomProvider();
            var factory = new RandomCustomerFactory(randomProvider);

            var customer1 = factory.Create();
            var customer2 = factory.Create();

            Assert.IsInstanceOfType<Customer>(customer1);
            Assert.IsInstanceOfType<Customer>(customer2);
            Assert.AreNotEqual(customer1.Id, customer2.Id);
        }

        [TestMethod]
        public void CustomerFactory_CreateACoupleOfCustomersWithSameRandom_ReturnsDifferentCustomersWithSameVehicles()
        {
            var randomProvider = new FixedRandomProvider(0);
            var factory = new RandomCustomerFactory(randomProvider);

            var customer1 = factory.Create();
            var customer2 = factory.Create();

            Assert.IsInstanceOfType<Customer>(customer1);
            Assert.IsInstanceOfType<Customer>(customer2);
            Assert.AreNotEqual(customer1.Id, customer2.Id);
            Assert.AreSame(customer1.Vehicle, customer2.Vehicle);
            Assert.AreEqual(customer1.Vehicle, customer2.Vehicle);
        }
    }
}
