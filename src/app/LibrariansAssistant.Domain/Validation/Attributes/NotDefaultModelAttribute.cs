using LibrariansAssistant.Dependencies;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LibrariansAssistant.Domain.Validation.Attributes;

/// <summary>
/// Specifies that the data field should not be initialized with the default model.
/// </summary>
internal sealed class NotDefaultModelAttribute : ValidationAttribute
{
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
}