using Microsoft.AspNetCore.Components;
using WPC_App_BlazorServerSide.ViewModels;
using WPC_App_BlazorServerSide.Models;
using WPC_App_BlazorServerSide.Helpers;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace WPC_App_BlazorServerSide.Components
{
    public class MapLegendComponentBase : ComponentBase
    {
        [Parameter]
        public List<CrimeCategoryModel> CrimeCategories { get; set; } = new();
    }
}
