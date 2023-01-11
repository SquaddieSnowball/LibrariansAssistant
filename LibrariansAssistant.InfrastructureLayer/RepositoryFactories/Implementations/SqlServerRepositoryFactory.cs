using LibrariansAssistant.InfrastructureLayer.Repositories.Implementations;
using LibrariansAssistant.InfrastructureLayer.Repositories.Interfaces;

namespace LibrariansAssistant.InfrastructureLayer.RepositoryFactories.Implementations;

/// <summary>
/// Represents the Sql Server repository factory.
/// </summary>
public sealed class SqlServerRepositoryFactory : IRepositoryFactory
{
    private SqlServerRepository? _sqlServerRepository;

    /// <summary>
    /// Creates a new repository instance.
    /// </summary>
    /// <returns>New repository instance.</returns>
    public IRepository GetRepository() =>
        _sqlServerRepository ??= new SqlServerRepository();
}