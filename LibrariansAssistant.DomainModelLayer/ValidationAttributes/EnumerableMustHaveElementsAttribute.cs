using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace LibrariansAssistant.DomainModelLayer.ValidationAttributes;

internal sealed class EnumerableMustHaveElementsAttribute : ValidationAttribute
{
    public override bool IsValid(object? value) =>
        (value is IEnumerable enumerable is true) && (enumerable.GetEnumerator().MoveNext() is true);
}