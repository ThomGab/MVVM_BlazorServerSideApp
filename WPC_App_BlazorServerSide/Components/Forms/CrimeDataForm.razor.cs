using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using WPC_App_BlazorServerSide.ViewModels;

namespace WPC_App_BlazorServerSide.Components.Forms
{
    public class CrimeDataFormBase : ComponentBase
    {
        [Inject]
        ICrimeData_ViewModel ViewModel { get; set; } = null!;

        public CrimeDataInputForm InputModel { get; set; } = new();

        public async Task ValidFormSubmitted(EditContext context)
        {
            await ViewModel.ValidFormSubmitted(InputModel);
        }

        public bool DateTimeInputIsValid { get; set; }

        public bool SubmitDisabled(EditContext context, bool dateTimeInputIsValid)
        {
            var inputHasErrorMessages = !context.GetValidationMessages().Any();
            var inputHasBeenModified = !context.IsModified();
            var inputHasNullOrWhitespace = !string.IsNullOrWhiteSpace(InputModel.Latitude) || !string.IsNullOrWhiteSpace(InputModel.Longitude);

            return (inputHasErrorMessages || inputHasBeenModified || inputHasNullOrWhitespace || !dateTimeInputIsValid);
        }
    }
}
