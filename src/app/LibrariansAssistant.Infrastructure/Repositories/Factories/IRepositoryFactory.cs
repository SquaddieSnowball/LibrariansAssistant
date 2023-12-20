using LibrariansAssistant.Infrastructure.Repositories.Abstractions;

namespace LibrariansAssistant.Infrastructure.Repositories.Factories;

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