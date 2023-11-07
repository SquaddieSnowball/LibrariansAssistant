using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibrariansAssistant.Services.CommonServices.DataAnnotationModelValidationService;

/// <summary>
/// Represents the Data Annotation model validation service.
/// </summary>
public sealed class DataAnnotationModelValidationService : IDataAnnotationModelValidationService
{
    /// <summary>
    /// Determines whether the specified model is valid.
    /// </summary>
    /// <typeparam name="TModel">Type of the model to validate.</typeparam>
    /// <param name="model">Model to validate.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
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