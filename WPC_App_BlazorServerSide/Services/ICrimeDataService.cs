using WPC_App_BlazorServerSide.Models;

namespace WPC_App_BlazorServerSide.Services
{
    public interface ICrimeDataService
    {
        public Task<IReadOnlyList<CrimeCategoryModel>> GetAllCategories();

        public Task<List<CrimeDataModel>> GetCrimeData(string latitude, string longitude, DateTime date);

        public Task<CrimeDataLastUpdatedModel> GetLastUpdatedDate();
    }
}
