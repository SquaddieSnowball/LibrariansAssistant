using LibrariansAssistant.Infrastructure.ObjectRelationalMappers.Implementations;

namespace LibrariansAssistant.Infrastructure.InfrastructureCreators.Implementations;

/// <summary>
/// Represents the Sql Server infrastructure creator.
/// </summary>
public sealed class SqlServerInfrastructureCreator : IInfrastructureCreator
{
    private SqlServerOrm? _sqlServerOrm;

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
        if (string.IsNullOrEmpty(initializationString) is true)
            throw new ArgumentNullException(nameof(initializationString),
            "Initialization string must not be null or empty.");

        try
        {
            _sqlServerOrm = new SqlServerOrm(initializationString);

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
                    Properties.Resources.SqlServerInfrastructureCreationScript
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