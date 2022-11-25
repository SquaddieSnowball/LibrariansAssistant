using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibrariansAssistant.ServicesLayer.CommonServices.DataAnnotationModelValidationService;

public sealed class DataAnnotationModelValidationService : IDataAnnotationModelValidationService
{
    public void Validate<TModel>(TModel model)
    {
        if (model is null)
            throw new ArgumentNullException(nameof(model), "Model must not be null.");

        var validationContext = new ValidationContext(model);
        var validationResuls = new List<ValidationResult>();
        var errorMessages = new StringBuilder();

        if (Validator.TryValidateObject(model, validationContext, validationResuls, true) is false)
        {
            foreach (ValidationResult validationResult in validationResuls)
                errorMessages.AppendLine(validationResult.ErrorMessage);

            throw new ArgumentException(errorMessages.ToString());
        }
    }
}