using LibrariansAssistant.Infrastructure.Repositories.Interfaces;

namespace LibrariansAssistant.Infrastructure.RepositoryFactories;

/// <summary>
/// Provides a factory method for creating repositories.
/// </summary>
public interface IRepositoryFactory
{
    /// <summary>
    /// Creates a new repository instance.
    /// </summary>
    /// <returns>New repository instance.</returns>
    IRepository GetRepository();
}