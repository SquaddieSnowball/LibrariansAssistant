using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LibrariansAssistant.Infrastructure.Services.Entities.ObjectRelationalMapper.SqlServerObjectRelationalMapper;

/// <summary>
/// Represents type details.
/// </summary>
internal sealed class TypeDetails
{
    private readonly List<PropertyDetails> _propertiesDetails = new();

    /// <summary>
    /// Gets an instance of the property's <see cref="Type"/> object.
    /// </summary>
    public Type ObjectType { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the property type is an enumerable.
    /// </summary>
    public bool IsEnumerable { get; private set; }

    /// <summary>
    /// Gets instances of the object's <see cref="PropertyDetails"/> objects.
    /// </summary>
    public IEnumerable<PropertyDetails> PropertiesDetails => _propertiesDetails;

    /// <summary>
    /// Initializes a new instance of the <see cref="TypeDetails"/> class.
    /// </summary>
    /// <param name="type"><see cref="Type"/> object.</param>
    public TypeDetails(Type type) => InitializeTypeDetails(type);

    [MemberNotNull(nameof(ObjectType))]
    private void InitializeTypeDetails(Type type)
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

            _propertiesDetails.Add(new PropertyDetails(propertyInfo, typeDetails));
        }
    }

    private static bool IsSimpleType(Type type) =>
        (type.IsPrimitive is true) ||
        (type.Equals(typeof(string)) is true) ||
        (type.Equals(typeof(decimal)) is true) ||
        (type.Equals(typeof(DateTime)) is true) ||
        ((type.IsGenericType is true) && (type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)) is true));
}