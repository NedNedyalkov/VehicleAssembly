using VehicleAssembly.Domain.Tires;

namespace VehicleAssembly.Factories
{
    public static class TiresFactory
    {
        public static bool TryCreateSummerTires(float pressureBar, float maxTemperatureC, out SummerTires? tires)
        {
            try
            {
                tires = new SummerTires(maxTemperatureC: maxTemperatureC, pressureBar: pressureBar);
            }
            catch (ArgumentException)
            {
                tires = null;
            }
            return tires is not null;
        }

        public static bool TryCreateWinterTires(float pressureBar, float minTemperatureC, float thicknessCm, out WinterTires? tires)
        {
            try
            {
                tires = new WinterTires(minTemperatureC: minTemperatureC, thicknessCm: thicknessCm, pressureBar: pressureBar);
            }
            catch (ArgumentException)
            {
                tires = null;
            }
            return tires is not null;
        }
    }
}
