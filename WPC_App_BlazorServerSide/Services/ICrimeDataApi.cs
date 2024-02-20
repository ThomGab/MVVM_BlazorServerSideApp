using Refit;
using WPC_App_BlazorServerSide.DTOs;

namespace WPC_App_BlazorServerSide.Services
{
    public interface ICrimeDataApi
    {
        [Get("/crimes-street/all-crime?lat={latitude}&lng={longitude}&date={date}")]
        Task<List<CrimeDataResponse>> GetCrimeData(string latitude, string longitude, string date);

        [Get("/crime-last-updated")]
        Task<LastUpdatedResponse> GetDateOfLastDataUpdate();

        [Get("/crime-categories")]
        Task<List<CrimeCategoryResponse>> GetCrimeCategories();
    }
}
