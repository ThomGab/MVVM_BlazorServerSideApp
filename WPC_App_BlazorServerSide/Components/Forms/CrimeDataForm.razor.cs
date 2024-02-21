using Blazorise;
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

        public bool DateTimeInputIsValid { get => dateTimeInputIsValid(); }

        private bool dateTimeInputIsValid()
        {
            if (string.IsNullOrWhiteSpace(InputModel.Date.ToString()))
            {
                return false;
            }

            else
            {
                if (InputModel.Date > ViewModel.LatestDataDate)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public bool SubmitDisabled(EditContext context)
        {
            var dateTimeInputIsInvalid = !DateTimeInputIsValid;
            var inputHasErrorMessages = context.GetValidationMessages().Any();
            var inputHasNullOrWhitespace = string.IsNullOrWhiteSpace(InputModel.Latitude) || string.IsNullOrWhiteSpace(InputModel.Longitude);

            return (inputHasErrorMessages || inputHasNullOrWhitespace || dateTimeInputIsInvalid);
        }
    }
}
