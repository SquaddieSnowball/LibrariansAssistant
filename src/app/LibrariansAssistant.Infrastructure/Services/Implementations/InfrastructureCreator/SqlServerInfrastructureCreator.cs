using LibrariansAssistant.Infrastructure.Resources.Services.InfrastructureCreator;
using LibrariansAssistant.Infrastructure.Services.Abstractions;
using LibrariansAssistant.Infrastructure.Services.Implementations.ObjectRelationalMapper;
using LibrariansAssistant.Validation.Helpers;

namespace LibrariansAssistant.Infrastructure.Services.Implementations.InfrastructureCreator;

/// <summary>
/// Represents the Sql Server infrastructure creator.
/// </summary>
public sealed class SqlServerInfrastructureCreator : IInfrastructureCreator
{
    private SqlServerObjectRelationalMapper? _sqlServerOrm;

    /// <summary>
    /// Gets a value indicating whether the infrastructure has been created.
    /// </summary>
    public bool? IsInfrastructureCreated { get; private set; }

    /// <summary>
    /// Initializes the infrastructure creator.
    /// </summary>
    /// <param name="initializationString">Initialization string.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Initialize(string initializationString)
    {
        Verify.NotNullOrEmpty(initializationString);

        try
        {
            _sqlServerOrm = new SqlServerObjectRelationalMapper(initializationString);

            int rowsAffected = _sqlServerOrm.ExecuteScalar<int>
                (

                "SELECT COUNT(*) " +
                "FROM sys.databases " +
                "WHERE name = 'library';",

                default

                );

            IsInfrastructureCreated = rowsAffected is not 0;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Creates infrastructure.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public void Create()
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because infrastructure creator has not been initialized.");

        if (IsInfrastructureCreated is false)
        {
            try
            {
                string[] batches =
                    InfrastructureCreationScripts.SqlServerInfrastructureCreationScript
                    .Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

                _ = _sqlServerOrm.ExecuteNonQueries(batches, default);
            }
            catch
            {
                throw;
            }
        }
    }
}