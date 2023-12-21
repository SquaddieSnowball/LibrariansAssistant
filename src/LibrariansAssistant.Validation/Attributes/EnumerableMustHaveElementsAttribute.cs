using LibrariansAssistant.Validation.Resources.Attributes.ErrorMessages;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace LibrariansAssistant.Validation.Attributes;

/// <summary>
/// Specifies that the enumerated data field must have at least one element.
/// </summary>
public sealed class EnumerableMustHaveElementsAttribute : ValidationAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnumerableMustHaveElementsAttribute"/> class.
    /// </summary>
    public EnumerableMustHaveElementsAttribute() => SetUpValidation();

    /// <summary>
    /// Determines whether the specified value of the object is valid.
    /// </summary>
    /// <param name="value">The value of the object to validate.</param>
    /// <returns>true if the specified value is valid; otherwise false.</returns>
    public override bool IsValid(object? value) =>
        value is IEnumerable enumerable is true && enumerable.GetEnumerator().MoveNext() is true;

    private void SetUpValidation()
    {
        ErrorMessage = AttributesErrorMessages.EnumerableMustHaveElementsAttribute;
    }
}