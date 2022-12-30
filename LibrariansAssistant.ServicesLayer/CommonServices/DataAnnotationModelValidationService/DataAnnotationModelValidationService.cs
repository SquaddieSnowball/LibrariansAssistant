using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibrariansAssistant.ServicesLayer.CommonServices.DataAnnotationModelValidationService;

public sealed class DataAnnotationModelValidationService : IDataAnnotationModelValidationService
{
    public void Validate<TModel>(TModel model)
    {
        if (model is null)
            throw new ArgumentNullException(nameof(model), "Model must not be null.");

        ValidationContext validationContext = new(model);
        List<ValidationResult> validationResuls = new();
        StringBuilder errorMessages = new();

        if (Validator.TryValidateObject(model, validationContext, validationResuls, true) is false)
        {
            foreach (ValidationResult validationResult in validationResuls)
                _ = errorMessages.AppendLine(validationResult.ErrorMessage);

            throw new ArgumentException(errorMessages.ToString());
        }
    }
}