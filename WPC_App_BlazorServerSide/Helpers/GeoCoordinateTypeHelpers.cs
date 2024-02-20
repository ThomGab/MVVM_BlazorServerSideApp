using WPC_App_BlazorServerSide.Validators.GeoCoordinateValidator;

namespace WPC_App_BlazorServerSide.Helpers
{
    public static class GeoCoordinateTypeHelpers
    {
        public static GeoCoordinateUIDataPreset GetUIPresets(this GeoCoordinateType type)
        {
            return type switch
            {
                GeoCoordinateType.Unintialized => throw new ArgumentOutOfRangeException($"{nameof(GeoCoordinateUIDataPreset)} should not be uninitialized"),
                GeoCoordinateType.Longitude => new GeoCoordinateUIDataPreset(180, -180, "Longitude"),
                GeoCoordinateType.Latitude => new GeoCoordinateUIDataPreset(90, -90, "Latitude"),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
