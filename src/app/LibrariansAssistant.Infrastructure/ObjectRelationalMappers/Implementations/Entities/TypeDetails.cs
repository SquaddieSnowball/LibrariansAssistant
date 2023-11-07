using System.Reflection;

namespace LibrariansAssistant.Infrastructure.ObjectRelationalMappers.Implementations.Entities;

/// <summary>
/// Represents type details.
/// </summary>
internal sealed class TypeDetails
{
    /// <summary>
    /// Gets an instance of the property's Type object.
    /// </summary>
    internal Type ObjectType { get; }

    /// <summary>
    /// Gets a value indicating whether the property type is an enumerable.
    /// </summary>
    internal bool IsEnumerable { get; }

    /// <summary>
    /// Gets instances of the object's PropertyDetails objects.
    /// </summary>
    internal IEnumerable<PropertyDetails> PropertyDetails { get; } = Enumerable.Empty<PropertyDetails>();

    /// <summary>
    /// Initializes a new instance of the TypeDetails class.
    /// </summary>
    /// <param name="type">Type object.</param>
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