using System.Reflection;

namespace LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Implementations.Entities;

internal sealed class PropertyDetails
{
    internal PropertyInfo PropertyInfo { get; }

    internal TypeDetails? TypeDetails { get; }

    internal bool IsSimpleType { get; }

    internal string SqlStyleName { get; }

    internal PropertyDetails(PropertyInfo propertyInfo, TypeDetails? typeDetails)
    {
        (PropertyInfo, TypeDetails) = (propertyInfo, typeDetails);

        if (typeDetails is null)
            IsSimpleType = true;

        SqlStyleName =
            string.Concat(propertyInfo.Name.Select(c => char.IsUpper(c) ? ("_" + c) : c.ToString()))
            .TrimStart('_')
            .ToLower();
    }
}