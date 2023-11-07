using System.Reflection;

namespace LibrariansAssistant.Infrastructure.ObjectRelationalMappers.Implementations.Entities;

/// <summary>
/// Represents property details.
/// </summary>
internal sealed class PropertyDetails
{
    /// <summary>
    /// Gets an instance of the property's PropertyInfo object.
    /// </summary>
    internal PropertyInfo PropertyInfo { get; }

    /// <summary>
    /// Gets an instance of the property's TypeDetails object.
    /// </summary>
    internal TypeDetails? TypeDetails { get; }

    /// <summary>
    /// Gets a value indicating whether the property type is simple.
    /// </summary>
    internal bool IsSimpleType { get; }

    /// <summary>
    /// Gets the property name in Sql style.
    /// </summary>
    internal string SqlStyleName { get; }

    /// <summary>
    /// Initializes a new instance of the PropertyDetails class.
    /// </summary>
    /// <param name="propertyInfo">Instance of the property's PropertyInfo object.</param>
    /// <param name="typeDetails">Instance of the property's TypeDetails object.</param>
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