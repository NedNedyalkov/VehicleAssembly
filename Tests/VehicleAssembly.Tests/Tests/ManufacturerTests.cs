using System.Reflection;
using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Domain.Vehicles;

namespace VehicleAssembly.Tests.Tests
{
    [TestClass]
    public sealed class ManufacturerTests
    {
        [TestMethod]
        public void Manufacturer_MultipleCarsWithSameManufacturer_ShareTheSameManufacturer()
        {
            var toyota1 = new Car(Manufacturers.CarManufacturers[CarManufacturersEnum.Toyota]);
            var toyota2 = new Car(Manufacturers.CarManufacturers[CarManufacturersEnum.Toyota]);

            Assert.AreSame(toyota1.Manufacturer, toyota2.Manufacturer);
        }

        [TestMethod]
        public void Manufacturers_DontHavePublicConstructors()
        {
            var assembly = Assembly.GetAssembly(typeof(Manufacturer));
            Assert.IsNotNull(assembly, $"Could not get assembly containing {nameof(Manufacturer)}.");

            var manufacturerTypes = assembly.GetTypes().Where(t => typeof(Manufacturer).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            Assert.IsTrue(manufacturerTypes.Count() >= 1);

            foreach (var manufacturerType in manufacturerTypes)
            {
                var constructors = manufacturerType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                Assert.IsTrue(constructors.Length >= 1);

                foreach (var constructor in constructors)
                {
                    Assert.IsFalse(constructor.IsPublic, $"Manufacturer type {manufacturerType.FullName} has a non-private constructor.");
                }
            }
        }
    }
}
