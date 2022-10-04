namespace LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Entities;

public sealed class MappingSettings
{
    public bool UseSqlStylePropertiesNaming { get; set; }

    public bool AddPropertyTypeObjectNamePrefix { get; set; }

    public bool SuppressModelPostfix { get; set; }
}