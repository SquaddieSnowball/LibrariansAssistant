using LibrariansAssistant.Dependencies;
using LibrariansAssistant.Validation.Resources.Attributes.ErrorMessages;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LibrariansAssistant.Validation.Attributes;

/// <summary>
/// Specifies that the data field should not be initialized with the default model.
/// </summary>
public sealed class NotDefaultModelAttribute : ValidationAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotDefaultModelAttribute"/> class.
    /// </summary>
    public NotDefaultModelAttribute() => SetUpValidation();

    /// <summary>
    /// Determines whether the specified value of the object is valid.
    /// </summary>
    /// <param name="value">The value of the object to validate.</param>
    /// <returns>true if the specified value is valid; otherwise false.</returns>
    public override bool IsValid(object? value)
    {
        if (value is null)
            return false;

        Type valueType = value.GetType();

        object? defaultValue =
            typeof(DependenciesContainer)
            .GetMethod("Resolve")!
            .MakeGenericMethod(valueType.GetInterface('I' + valueType.Name)!)
            .Invoke(null, new object?[] { null });

        foreach (PropertyInfo property in valueType.GetProperties())
            if (property.GetValue(value)?.Equals(property.GetValue(defaultValue)) is false)
                return true;

        return false;
    }

    private void SetUpValidation()
    {
        ErrorMessage = AttributesErrorMessages.NotDefaultModelAttribute;
    }
}