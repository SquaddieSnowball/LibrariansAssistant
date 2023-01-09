namespace LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Entities;

/// <summary>
/// Represents mapping settings.
/// </summary>
public sealed class MappingSettings
{
    /// <summary>
    /// Gets or sets a value indicating whether type property names should be converted to SQL style during data binding.
    /// </summary>
    public bool UseSqlStylePropertiesNaming { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to add property type object name prefix 
    /// to the property names during data binding to avoid ambiguous bindings.
    /// </summary>
    public bool AddPropertyTypeObjectNamePrefix { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to suppress "Model" ending in property name 
    /// while adding property type object name prefix during data binding.
    /// </summary>
    public bool SuppressModelInPrefix { get; set; }
}