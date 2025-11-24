using VehicleAssembly.Abstractions;
using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Domain.Tires;

namespace VehicleAssembly.Domain.Vehicles
{
    public sealed record Car : Vehicle
    {
        public Tires.Tires Tires { get; private set; }

        internal Car(Manufacturer manufacturer, Tires.Tires tires, IVehicleShowInformationHandler? logger = null) : base(manufacturer, logger)
        {
            Tires = tires ?? throw new ArgumentNullException(paramName: nameof(tires));
        }

        internal Car(Manufacturer manufacturer, IVehicleShowInformationHandler? logger = null) : this(manufacturer, SummerTires.Default.Value, logger)
        {
        }

        public override void ShowInformation() => Logger.ShowInformation($"Driving a car from {Manufacturer} with {Tires}");
        public override string ToString() => $"Car: {Manufacturer}, Tire: {Tires}";
        public void ReplaceTires(Tires.Tires newTires) => Tires = newTires;
    }
}
