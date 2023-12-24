using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace LibrariansAssistant.Domain.Validation.Attributes;

/// <summary>
/// Specifies that the enumerable data field must contain at least one element.
/// </summary>
internal sealed class EnumerableMustHaveElementsAttribute : ValidationAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EnumerableMustHaveElementsAttribute"/> class.
    /// </summary>
    public EnumerableMustHaveElementsAttribute() => SetUpValidation();

    /// <summary>
    /// Determines whether the specified value of the object is valid.
    /// </summary>
    /// <param name="value">The value of the object to validate.</param>
    /// <returns><see langword="true"/> if the specified value is valid; otherwise, <see langword="false"/>.</returns>
    public override bool IsValid(object? value) =>
        (value is IEnumerable enumerable) && (enumerable.GetEnumerator().MoveNext() is true);

    private void SetUpValidation()
    {
        ErrorMessage = "The enumerated data field must contain at least one element.";
    }
}