using LibrariansAssistant.Domain.Models.Author;

namespace LibrariansAssistant.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Provides CRUD methods for the author repository.
/// </summary>
public interface IAuthorRepository
{
    /// <summary>
    /// Adds a new author to the repository.
    /// </summary>
    /// <param name="author">Author to add to the repository.</param>
    void AuthorAdd(IAuthorModel author);

    /// <summary>
    /// Gets all authors from the repository.
    /// </summary>
    /// <returns>All authors from the repository.</returns>
    IEnumerable<IAuthorModel> AuthorGetAll();

    /// <summary>
    /// Gets the author with the specified ID from the repository.
    /// </summary>
    /// <param name="authorId">Author ID.</param>
    /// <returns>Author with the specified ID.</returns>
    IAuthorModel? AuthorGetById(int authorId);

    /// <summary>
    /// Updates an existing author in the repository.
    /// </summary>
    /// <param name="author">Existing author to update.</param>
    void AuthorUpdate(IAuthorModel author);

    /// <summary>
    /// Removes an existing author from the repository.
    /// </summary>
    /// <param name="authorId">Author ID.</param>
    void AuthorDelete(int authorId);
}