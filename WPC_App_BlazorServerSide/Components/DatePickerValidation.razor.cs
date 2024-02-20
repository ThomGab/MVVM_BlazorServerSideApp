using Microsoft.AspNetCore.Components;
using System.Numerics;
using WPC_App_BlazorServerSide.Components.Forms;
using WPC_App_BlazorServerSide.ViewModels;

namespace WPC_App_BlazorServerSide.Components
{
    public class DateTimePickerValidationBase : ComponentBase
    {
        [Inject]
        public ICrimeData_ViewModel ViewModel { get; set; } = null!;

        [Parameter]
        public bool IsValid { get; set; } = false;

        [Parameter]
        public CrimeDataInputForm Form { get; set; }
    }
}
