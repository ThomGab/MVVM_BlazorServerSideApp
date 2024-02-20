using WPC_App_BlazorServerSide.DTOs;

namespace WPC_App_BlazorServerSide.ViewModels
{
    public interface IMapModel
    {
        public List<CrimeDataResponse> MapPinData {  get; set; }
    }
}