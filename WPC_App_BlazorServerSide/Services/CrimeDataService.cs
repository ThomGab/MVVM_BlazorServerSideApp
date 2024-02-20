using System.Globalization;
using WPC_App_BlazorServerSide.Models;

namespace WPC_App_BlazorServerSide.Services
{
    /// <summary>
    /// Need to add null checking and processing.
    /// </summary>

    public class CrimeDataService : ICrimeDataService
    {
        private readonly ICrimeDataApi crimeDataApi;

        public CrimeDataService(ICrimeDataApi crimeDataApi)
        {
            this.crimeDataApi = crimeDataApi;
        }

        public async Task<IReadOnlyList<CrimeCategoryModel>> GetAllCategories()
        {
            var response = await this.crimeDataApi.GetCrimeCategories();
            var result = new List<CrimeCategoryModel>();

            foreach (var category in response)
            {
                result.Add(new CrimeCategoryModel { CategoryCode = category.url, CategoryFriendly = category.name });
            }

            return result;
        }

        public async Task<List<CrimeDataModel>> GetCrimeData(string latitude, string longitude, DateTime date)
        {
            // api only takes date string of format 2021-05 (yyyy-MM)
            var dateFormattedString = date.ToString("yyyy-MM", CultureInfo.InvariantCulture);

            var response = await this.crimeDataApi.GetCrimeData(latitude, longitude, dateFormattedString);
            var result = new List<CrimeDataModel>();

            foreach (var crimeData in response)
            {
                result.Add(new CrimeDataModel { Category = crimeData.Category, Location = crimeData.Location, Date = crimeData.Date });
            }

            return result;
        }

        public async Task<CrimeDataLastUpdatedModel> GetLastUpdatedDate()
        {
            var response = await this.crimeDataApi.GetDateOfLastDataUpdate();
            var result = new CrimeDataLastUpdatedModel() { Date = response.Date };

            return result;
        }
    }
}
