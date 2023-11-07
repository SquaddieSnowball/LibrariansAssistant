using LibrariansAssistant.Domain.Models.Reader;

namespace LibrariansAssistant.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Provides CRUD methods for the reader repository.
/// </summary>
public interface IReaderRepository
{
    /// <summary>
    /// Adds a new reader to the repository.
    /// </summary>
    /// <param name="reader">Reader to add to the repository.</param>
    void ReaderAdd(IReaderModel reader);

    /// <summary>
    /// Gets all readers from the repository.
    /// </summary>
    /// <returns>All readers from the repository.</returns>
    IEnumerable<IReaderModel> ReaderGetAll();

    /// <summary>
    /// Gets the reader with the specified ID from the repository.
    /// </summary>
    /// <param name="readerId">Reader ID.</param>
    /// <returns>Reader with the specified ID.</returns>
    IReaderModel? ReaderGetById(int readerId);

    /// <summary>
    /// Updates an existing reader in the repository.
    /// </summary>
    /// <param name="reader">Existing reader to update.</param>
    void ReaderUpdate(IReaderModel reader);

    /// <summary>
    /// Removes an existing reader from the repository.
    /// </summary>
    /// <param name="readerId">Reader ID.</param>
    void ReaderDelete(int readerId);
}