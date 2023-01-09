namespace LibrariansAssistant.InfranstructureLayer.InfranstructureCreators;

/// <summary>
/// Provides methods for creating infrastructure.
/// </summary>
public interface IInfranstructureCreator
{
    /// <summary>
    /// Gets a value indicating whether the infrastructure has been created.
    /// </summary>
    public bool? IsInfrastructureCreated { get; }

    /// <summary>
    /// Initializes the infrastructure creator.
    /// </summary>
    /// <param name="initializationString">Initialization string.</param>
    void Initialize(string initializationString);

    /// <summary>
    /// Creates infrastructure.
    /// </summary>
    void Create();
}