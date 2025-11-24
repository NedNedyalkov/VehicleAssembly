using VehicleAssembly.Abstractions;
using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Domain.Tires;

namespace VehicleAssembly.Domain.Vehicles
{
    public sealed record Car : Vehicle
    {
        public Tire Tire { get; private set; }

        internal Car(Manufacturer manufacturer, Tire tire, IVehicleShowInformationHandler? logger = null) : base(manufacturer, logger)
        {
            Tire = tire ?? throw new ArgumentNullException(paramName: nameof(tire));
        }

        internal Car(Manufacturer manufacturer, IVehicleShowInformationHandler? logger = null) : this(manufacturer, SummerTire.Default.Value, logger)
        {
        }

        public override void ShowInformation() => Logger.ShowInformation($"Driving a car from {Manufacturer} with {Tire}");
        public override string ToString() => $"Car: {Manufacturer}, Tire: {Tire}";
        public void ReplaceTires(Tire newTires) => Tire = newTires;
    }
}
