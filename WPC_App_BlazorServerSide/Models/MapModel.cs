using WPC_App_BlazorServerSide.DTOs;
using WPC_App_BlazorServerSide.ViewModels;

namespace WPC_App_BlazorServerSide.Models
{
    public class MapModel : IMapModel
    {
        public List<CrimeDataResponse> MapPinData { get; set; } = new ();
    }
}
