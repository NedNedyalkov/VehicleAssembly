using VehicleAssembly.Abstractions;
using VehicleAssembly.Domain.Manufacturers;
using VehicleAssembly.Domain.Tires;

namespace VehicleAssembly.Domain.Vehicles
{
    public sealed record Car : Vehicle
    {
        public Tires.Tires Tires { get; private set; }

        internal Car(Manufacturer manufacturer, Tires.Tires tires, IVehicleShowInformationHandler? showInfoHandler = null)
            : base(manufacturer, showInfoHandler)
        {
            Tires = tires ?? throw new ArgumentNullException(paramName: nameof(tires));
        }

        internal Car(Manufacturer manufacturer, IVehicleShowInformationHandler? showInfoHandler = null)
            : this(manufacturer, SummerTires.Default.Value, showInfoHandler)
        {
        }

        public override string GetInformation() => $"Car from {Manufacturer} with {Tires}";
        public override string ToString() => $"Car: {Manufacturer}, Tires: {Tires}";
        public void ReplaceTires(Tires.Tires newTires) => Tires = newTires;
    }
}
