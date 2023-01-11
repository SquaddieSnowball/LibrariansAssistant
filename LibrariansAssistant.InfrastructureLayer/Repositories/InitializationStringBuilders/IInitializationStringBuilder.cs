namespace LibrariansAssistant.InfrastructureLayer.Repositories.InitializationStringBuilders;

/// <summary>
/// Provides methods for building initialization strings.
/// </summary>
public interface IInitializationStringBuilder
{
    /// <summary>
    /// Builds an initialization string.
    /// </summary>
    /// <returns>New initialization string.</returns>
    string Build();
}