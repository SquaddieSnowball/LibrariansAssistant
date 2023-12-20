namespace LibrariansAssistant.Infrastructure.Services.Abstractions;

/// <summary>
/// Provides methods for building initialization strings.
/// </summary>
public interface IRepositoryInitializationStringBuilder
{
    /// <summary>
    /// Builds an initialization string.
    /// </summary>
    /// <returns>New initialization string.</returns>
    string Build();
}