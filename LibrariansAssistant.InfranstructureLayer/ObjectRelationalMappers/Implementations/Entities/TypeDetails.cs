using System.Reflection;

namespace LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Implementations.Entities;

internal sealed class TypeDetails
{
    internal Type ObjectType { get; }

    internal bool IsEnumerable { get; }

    internal IEnumerable<PropertyDetails> PropertyDetails { get; } = Enumerable.Empty<PropertyDetails>();

    internal TypeDetails(Type type)
    {
        if ((type.IsGenericType is true) && (type.GetGenericTypeDefinition().Equals(typeof(IEnumerable<>)) is true))
        {
            ObjectType = type.GetGenericArguments()[0];
            IsEnumerable = true;
        }
        else
            ObjectType = type;

        foreach (PropertyInfo propertyInfo in ObjectType.GetProperties())
        {
            TypeDetails? typeDetails = null;

            if (IsSimpleType(propertyInfo.PropertyType) is false)
                typeDetails = new TypeDetails(propertyInfo.PropertyType);

            PropertyDetails = PropertyDetails.Append(new PropertyDetails(propertyInfo, typeDetails));
        }
    }

    private static bool IsSimpleType(Type type) =>
        (type.IsPrimitive is true) ||
        (type.Equals(typeof(decimal)) is true) ||
        (type.Equals(typeof(string)) is true) ||
        (type.Equals(typeof(DateTime)) is true) ||
        ((type.IsGenericType is true) && (type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) is true));
}