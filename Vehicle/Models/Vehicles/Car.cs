using Vehicle.Interfaces;
using Vehicle.Models.Tires;

namespace Vehicle.Models.Vehicles
{
    public sealed class Car : Vehicle
    {
        public ITire Tire { get; private set; }

        internal Car(ICarManufacturer manufacturer, ITire tire) : base(manufacturer)
        {
            Tire = tire ?? throw new ArgumentNullException(paramName: nameof(tire));
        }

        internal Car(ICarManufacturer manufacturer) : this(manufacturer, SummerTire.Default.Value)
        {
        }

        public override void ShowInformation() => Console.WriteLine($"Driving a car from {Manufacturer} with {Tire}");
        public override string ToString() => $"Car: {Manufacturer}, Tire: {Tire}";
        public void ReplaceTires(ITire newTires) => Tire = newTires;
    }
}
