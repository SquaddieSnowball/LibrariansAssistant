using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LibrariansAssistant.Infrastructure.Services.Entities.ObjectRelationalMapper.SqlServerObjectRelationalMapper;

/// <summary>
/// Represents property details.
/// </summary>
internal sealed class PropertyDetails
{
    /// <summary>
    /// Gets an instance of the property's <see cref="System.Reflection.PropertyInfo"/> object.
    /// </summary>
    public PropertyInfo PropertyInfo { get; }

    /// <summary>
    /// Gets an instance of the property's <see cref="SqlServerObjectRelationalMapper.TypeDetails"/> object.
    /// </summary>
    public TypeDetails? TypeDetails { get; }

    /// <summary>
    /// Gets a value indicating whether the property type is simple.
    /// </summary>
    public bool IsSimpleType { get; private set; }

    /// <summary>
    /// Gets the property name in SQL style.
    /// </summary>
    public string SqlStyleName { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyDetails"/> class.
    /// </summary>
    /// <param name="propertyInfo">Instance of the property's 
    /// <see cref="System.Reflection.PropertyInfo"/> object.</param>
    /// <param name="typeDetails">Instance of the property's 
    /// <see cref="SqlServerObjectRelationalMapper.TypeDetails"/> object.</param>
    public PropertyDetails(PropertyInfo propertyInfo, TypeDetails? typeDetails)
    {
        (PropertyInfo, TypeDetails) = (propertyInfo, typeDetails);

        InitializePropertyDetails();
    }

    [MemberNotNull(nameof(SqlStyleName))]
    private void InitializePropertyDetails()
    {
        if (TypeDetails is null)
            IsSimpleType = true;

        SqlStyleName =
            string.Concat(PropertyInfo.Name.Select(c => (char.IsUpper(c) is true) ? $"_{c}" : c.ToString()))
            .TrimStart('_')
            .ToLower();
    }
}