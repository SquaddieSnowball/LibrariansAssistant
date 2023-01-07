using LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Implementations;

namespace LibrariansAssistant.InfranstructureLayer.InfranstructureCreators.Implementations;

public sealed class SqlServerInfranstructureCreator : IInfranstructureCreator
{
    private SqlServerOrm? _sqlServerOrm;

    public bool? IsInfrastructureCreated { get; private set; }

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

    public void Create()
    {
        if (_sqlServerOrm is null)
            throw new InvalidOperationException("The operation could not be performed " +
                "because infranstructure creator has not been initialized.");

        if (IsInfrastructureCreated is false)
        {
            try
            {
                string[] batches =
                    Properties.Resources.SqlServerInfranstructureCreationScript
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