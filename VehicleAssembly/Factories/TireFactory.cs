using VehicleAssembly.Domain.Tires;

namespace VehicleAssembly.Domain.Vehicles
{
    public static class TireFactory
    {
        public static bool TryCreateSummerTire(float pressureBar, float maxTemperatureC, out SummerTire? tire)
        {
            try
            {
                tire = new SummerTire(maxTemperatureC: maxTemperatureC, pressureBar: pressureBar);
            }
            catch
            {
                tire = null;
            }
            return tire is not null;
        }

        public static bool TryCreateWinterTire(float pressureBar, float minTemperatureC, float thicknessCm, out WinterTire? tire)
        {
            try
            {
                tire = new WinterTire(minTemperatureC: minTemperatureC, thicknessCm: thicknessCm, pressureBar: pressureBar);
            }
            catch
            {
                tire = null;
            }
            return tire is not null;
        }
    }
}
