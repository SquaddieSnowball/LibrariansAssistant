using LibrariansAssistant.InfranstructureLayer.Repositories.Implementations;
using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;

namespace LibrariansAssistant.InfranstructureLayer.RepositoryFactories.Implementations;

public sealed class SqlServerRepositoryFactory : IRepositoryFactory
{
    private SqlServerRepository? _sqlServerRepository;

    public IRepository GetRepository() =>
        _sqlServerRepository ??= new SqlServerRepository();
}