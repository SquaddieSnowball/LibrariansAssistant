namespace LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;

/// <summary>
/// Provides CRUD methods for repositories.
/// </summary>
public interface IRepository : IAuthorRepository, IBookRepository, IIssuingRepository, IReaderRepository
{
    /// <summary>
    /// Initializes the repositories using the specified initialization string.
    /// </summary>
    /// <param name="initializationString">Initialization string.</param>
    void Initialize(string initializationString);
}