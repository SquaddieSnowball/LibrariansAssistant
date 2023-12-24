using LibrariansAssistant.Infrastructure.Resources.Services.InfrastructureCreator;
using LibrariansAssistant.Infrastructure.Services.Abstractions;
using LibrariansAssistant.Infrastructure.Services.Implementations.ObjectRelationalMapper;

namespace LibrariansAssistant.Infrastructure.Services.Implementations.InfrastructureCreator;

/// <summary>
/// Represents the "SQL Server" infrastructure creator.
/// </summary>
public sealed class SqlServerInfrastructureCreator : IInfrastructureCreator
{
    private SqlServerObjectRelationalMapper? _sqlServerObjectRelationalMapper;

    /// <summary>
    /// Gets a value indicating whether the infrastructure has been created.
    /// </summary>
    public bool IsInfrastructureCreated
    {
        get
        {
            if (_sqlServerObjectRelationalMapper is null)
            {
                throw new InvalidOperationException("The operation could not be performed " +
                    "because infrastructure creator has not been initialized.");
            }

            int rowsAffected = _sqlServerObjectRelationalMapper
                .ExecuteScalar<int>
                (

                "SELECT COUNT(*) " +
                "FROM sys.databases " +
                "WHERE name = 'library';",

                default);

            return rowsAffected is not 0;
        }
    }

    /// <summary>
    /// Initializes the infrastructure creator.
    /// </summary>
    /// <param name="initializationString">Initialization string.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Initialize(string initializationString)
    {
        if (string.IsNullOrEmpty(initializationString) is true)
        {
            throw new ArgumentException("Initialization string must not be null or empty.",
                nameof(initializationString));
        }

        _sqlServerObjectRelationalMapper = new SqlServerObjectRelationalMapper(initializationString);
    }

    /// <summary>
    /// Creates infrastructure.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public void Create()
    {
        if (_sqlServerObjectRelationalMapper is null)
        {
            throw new InvalidOperationException("The operation could not be performed " +
                "because infrastructure creator has not been initialized.");
        }

        if (IsInfrastructureCreated is false)
        {
            string[] batches =
                InfrastructureCreationScripts.SqlServerInfrastructureCreationScript
                .Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

            _ = _sqlServerObjectRelationalMapper.ExecuteNonQueries(batches, default);
        }
    }
}