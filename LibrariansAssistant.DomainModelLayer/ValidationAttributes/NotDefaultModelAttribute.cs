using LibrariansAssistant.DependenciesLayer;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LibrariansAssistant.DomainModelLayer.ValidationAttributes;

internal sealed class NotDefaultModelAttribute : ValidationAttribute
{
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