using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace LibrariansAssistant.DomainModelLayer.ValidationAttributes;

/// <summary>
/// Specifies that the enumerated data field must have at least one element.
/// </summary>
internal sealed class EnumerableMustHaveElementsAttribute : ValidationAttribute
{
    /// <summary>
    /// Determines whether the specified value of the object is valid.
    /// </summary>
    /// <param name="value">The value of the object to validate.</param>
    /// <returns>true if the specified value is valid; otherwise false.</returns>
    public override bool IsValid(object? value) =>
        (value is IEnumerable enumerable is true) && (enumerable.GetEnumerator().MoveNext() is true);
}