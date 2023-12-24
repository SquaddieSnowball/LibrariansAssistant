using LibrariansAssistant.Infrastructure.Repositories.Abstractions;
using LibrariansAssistant.Infrastructure.Repositories.Implementations;

namespace LibrariansAssistant.Infrastructure.Repositories.Factories.Implementations;

/// <summary>
/// Represents the "SQL Server" repository factory.
/// </summary>
public sealed class SqlServerRepositoryFactory : IRepositoryFactory
{
    private readonly Lazy<SqlServerRepository> _sqlServerRepository = new();

    /// <summary>
    /// Creates a new repository instance.
    /// </summary>
    /// <returns>New repository instance.</returns>
    public IRepository GetRepository() => _sqlServerRepository.Value;
}