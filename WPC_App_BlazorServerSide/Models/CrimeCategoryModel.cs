using WPC_App_BlazorServerSide.DTOs;

namespace WPC_App_BlazorServerSide.Models
{
    public class CrimeCategoryModel
    {
        public string CategoryCode { get; init; } = string.Empty;

        public string CategoryFriendly { get; init; } = string.Empty;

        public bool IsSelected { get; set; } = true;
    }
}
