using System.Reflection;

namespace VehicleAssembly.Tests
{
    [TestClass]
    public sealed class ManufacturerTests
    {
        [TestMethod]
        public void Manufacturer_MultipleCarsWithSameManufacturer_ShareTheSameManufacturer()
        {
            var car1 = new Car(CarManufacturers.Toyota.Value);
            var car2 = new Car(CarManufacturers.Toyota.Value);

            Assert.AreSame(car1.Manufacturer, car2.Manufacturer);
        }

        [TestMethod]
        public void Manufacturers_DontHaveNonPrivateConstructors()
        {
            var assembly = Assembly.GetAssembly(typeof(Manufacturer));
            Assert.IsNotNull(assembly, $"Could not get assembly containing {nameof(Manufacturer)}.");

            var manufacturerTypes = assembly.GetTypes().Where(t => typeof(Manufacturer).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            Assert.IsTrue(manufacturerTypes.Count() >= 1);

            foreach (var manufacturerType in manufacturerTypes)
            {
                var constructors = manufacturerType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                Assert.IsTrue(constructors.Count() >= 1);

                foreach (var constructor in constructors)
                {
                    Assert.IsTrue(constructor.IsPrivate, $"Manufacturer type {manufacturerType.FullName} has a non-private constructor.");
                }
            }
        }

        [TestMethod]
        public void Manufacturers_InstancePropertiesAreNotPublic()
        {
            var assembly = Assembly.GetAssembly(typeof(Manufacturer));
            Assert.IsNotNull(assembly, $"Could not get assembly containing {nameof(Manufacturer)}.");

            var manufacturerTypes = assembly.GetTypes().Where(t => typeof(Manufacturer).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            Assert.IsTrue(manufacturerTypes.Count() >= 1);

            foreach (var manufacturerType in manufacturerTypes)
            {
                var properties = manufacturerType.GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                Assert.IsTrue(properties.Count() >= 1);

                foreach (var property in properties)
                {
                    if (property.Name.Contains("Instance", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Assert.IsFalse(property.GetMethod?.IsPublic == true, $"Manufacturer type {manufacturerType.FullName} has a public instance property!");
                    }
                }
            }
        }
    }
}
