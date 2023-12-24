namespace LibrariansAssistant.Services.Common.Abstractions;

/// <summary>
/// Provides methods used to validate models using Data Annotations.
/// </summary>
public interface IDataAnnotationsModelValidationService
{
    /// <summary>
    /// Determines whether the specified model is valid.
    /// </summary>
    /// <typeparam name="TModel">Type of the model to validate.</typeparam>
    /// <param name="model">Model to validate.</param>
    void Validate<TModel>(TModel model);
}