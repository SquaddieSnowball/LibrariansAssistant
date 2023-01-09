using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;

namespace LibrariansAssistant.InfranstructureLayer.RepositoryFactories;

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