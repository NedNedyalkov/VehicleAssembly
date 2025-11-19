using Vehicle.Interfaces;
using Vehicle.Models.Tires;

namespace Vehicle.Models.Vehicles
{
    internal class Car(ICarManufacturer manufacturer, ITire tire) : Vehicle(manufacturer)
    {
        public ITire Tire { get; private set; } = tire ?? throw new ArgumentNullException(paramName: nameof(tire));

        internal Car(ICarManufacturer manufacturer) : this(manufacturer, new SummerTire())
        {
        }

        public override void ShowInformation() => Console.WriteLine($"Driving a car from {Manufacturer} with {Tire}");
        public override string ToString() => $"Car: {Manufacturer}, Tire: {Tire}";
        public void ReplaceTires(ITire newTires) => Tire = newTires;
    }
}
