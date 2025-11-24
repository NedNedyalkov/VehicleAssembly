using VehicleAssembly.Domain.Tires;

namespace VehicleAssembly.Factories
{
    public static class TireFactory
    {
        public static bool TryCreateSummerTire(float pressureBar, float maxTemperatureC, out SummerTires? tires)
        {
            try
            {
                tires = new SummerTires(maxTemperatureC: maxTemperatureC, pressureBar: pressureBar);
            }
            catch
            {
                tires = null;
            }
            return tires is not null;
        }

        public static bool TryCreateWinterTire(float pressureBar, float minTemperatureC, float thicknessCm, out WinterTires? tires)
        {
            try
            {
                tires = new WinterTires(minTemperatureC: minTemperatureC, thicknessCm: thicknessCm, pressureBar: pressureBar);
            }
            catch
            {
                tires = null;
            }
            return tires is not null;
        }
    }
}
