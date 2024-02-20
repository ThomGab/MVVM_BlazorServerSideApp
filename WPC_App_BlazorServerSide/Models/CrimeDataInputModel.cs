using WPC_App_BlazorServerSide.ViewModels;

namespace WPC_App_BlazorServerSide.Models
{
    public class CrimeDataInputModel : ICrimeDataInputModel
    {
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
