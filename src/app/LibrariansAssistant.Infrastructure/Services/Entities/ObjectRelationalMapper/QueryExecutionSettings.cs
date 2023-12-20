namespace LibrariansAssistant.Infrastructure.Services.Entities.ObjectRelationalMapper;

/// <summary>
/// Represents query execution settings.
/// </summary>
public sealed class QueryExecutionSettings
{
    /// <summary>
    /// Gets or sets a value indicating whether the query is a stored procedure.
    /// </summary>
    public bool IsStoredProcedure { get; set; }
}