using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Resources;
using WPC_App_BlazorServerSide.Helpers;

namespace WPC_App_BlazorServerSide.Validators.GeoCoordinateValidator
{
    /// <summary>
    /// TODO: Add resource string resx references into validator string.
    /// </summary>
    public class GlobalCoordinateValidator : ValidationAttribute
    {
        private GeoCoordinateUIDataPreset uiData;
        private readonly int maxDecimalPlaces;
        private readonly ResourceManager resourceManager;

        public GlobalCoordinateValidator(GeoCoordinateType coordinateType, ushort maxDecimalPlaces)
        {
            uiData = coordinateType.GetUIPresets();
            this.maxDecimalPlaces = maxDecimalPlaces;
            this.resourceManager = new ResourceManager("WPC_App_BlazorServerSide.Resource", Assembly.GetExecutingAssembly());            
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var allowedNumberStyles =
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowLeadingWhite |
                NumberStyles.AllowTrailingWhite |
                NumberStyles.AllowLeadingSign;

            var rawString = value?.ToString();
            double parsedValue;

            var validationErrorString = string.Empty;

            if (double.TryParse(rawString, allowedNumberStyles, CultureInfo.CurrentCulture, out parsedValue))
            {
                // Check if input is above max threshold, or below min threshold.
                if (parsedValue > uiData.max || parsedValue < uiData.min)
                {
                    validationErrorString = validationErrorString = string.Format(resourceManager.GetString("GeoValidatorOutOfRangeMessage"), uiData.friendlyName, uiData.max.ToString(), uiData.min.ToString()); ;
                }

                else
                {
                    var invariantDecimalCharacter = char.Parse(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
                    var invariantValueString = parsedValue.ToString(CultureInfo.InvariantCulture);
                    var hasDecimalPoint = invariantValueString.Count(c => c == invariantDecimalCharacter) == 1;

                    // Check if input has decimal point.
                    if (hasDecimalPoint == true)
                    {
                        var decimalComponents = invariantValueString.Split(invariantDecimalCharacter);

                        // Input has decimal point, but no decimal component
                        if (decimalComponents.Length == 1)
                        {
                            validationErrorString = resourceManager.GetString("GeoValidatorIncompleteDecimalMessage");
                        }

                        else
                        {
                            // Check if input has too many digits after decimal point.
                            var decimalDigitCount = decimalComponents[1].Length;

                            if (decimalDigitCount > maxDecimalPlaces)
                            {
                                validationErrorString = string.Format(resourceManager.GetString("GeoValidatorDecimalPrecisionExceededMessage"), maxDecimalPlaces);
                            }
                        }
                    }
                }
            }

            else
            {
                // Check if input is empty.
                if (string.IsNullOrWhiteSpace(rawString))
                {
                    validationErrorString = resourceManager.GetString("GeoValidatorMissingRequiredFieldMessage"); ;
                }
                else
                {
                    validationErrorString = resourceManager.GetString("GeoValidatorInvalidValue"); ;
                }
            }

            if (validationErrorString != string.Empty)
            {
                return new($"{validationErrorString}");
            }

            return ValidationResult.Success;
        }
    }
}
