using WPC_App_BlazorServerSide.DTOs;

namespace WPC_App_BlazorServerSide.Models
{
    public class CrimeDataModel
    {
        public string? Category { get; init; }
        public Location? Location { get; init; }
        public string? Date { get; init; }
    }
}
