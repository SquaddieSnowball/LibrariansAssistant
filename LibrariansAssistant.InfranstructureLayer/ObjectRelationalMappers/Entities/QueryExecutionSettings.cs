namespace LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Entities;

public sealed class QueryExecutionSettings
{
    public bool IsStoredProcedure { get; set; }

    public bool UseSqlStylePropertiesNaming { get; set; }

    public bool AddPropertyTypeObjectNamePrefix { get; set; }
}