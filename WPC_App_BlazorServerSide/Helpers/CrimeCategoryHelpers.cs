using WPC_App_BlazorServerSide.Models;

namespace WPC_App_BlazorServerSide.Helpers
{
    public static class CrimeCategoryHelpers
    {
        private static string getColour(string? category)
        {
            return category switch
            {
                "anti-social-behaviour" => "#900C3F",
                "bicycle-theft" => "#FF5733",
                "burglary" => "#FFC300",
                "criminal-damage-arson" => "#A6FF00",
                "drugs" => "#00FF46",
                "other-theft" => "#00FFE0",
                "possession-of-weapons" => "#00FFE0",
                "public-order" => "#8B00FF",
                "robbery" => "#473949",
                "shoplifting" => "#E400FF",
                "theft-from-the-person" => "#37A295",
                "vehicle-crime" => "#418078",
                "violent-crime" => "#808041",
                "other-crime" => "#B6B62E",
                _ => "#7D7D7C"
            };
        }

        static public string GetColour(this CrimeDataModel crimeData)
        {
            return getColour(crimeData.Category);
        }

        static public string GetColour(this CrimeCategoryModel legendItem)
        {
            return getColour(legendItem.CategoryCode);
        }
    }
}
