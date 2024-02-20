using WPC_App_BlazorServerSide.Helpers;
using WPC_App_BlazorServerSide.Validators.GeoCoordinateValidator;

namespace WPC_App_BlazorServerSide.Components.Forms
{
    public class CrimeDataInputForm : ICrimeDataInputForm
    {
        [GlobalCoordinateValidator(GeoCoordinateType.Latitude, 6)]
        public string Latitude { get; set; } = string.Empty;

        [GlobalCoordinateValidator(GeoCoordinateType.Longitude, 6)]
        public string Longitude { get; set; } = string.Empty;

        public DateTime? Date { get; set; }
    }
}
