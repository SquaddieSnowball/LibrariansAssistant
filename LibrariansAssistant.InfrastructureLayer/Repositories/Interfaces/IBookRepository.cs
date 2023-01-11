using LibrariansAssistant.DomainModelLayer.Models.Book;

namespace LibrariansAssistant.InfrastructureLayer.Repositories.Interfaces;

/// <summary>
/// Provides CRUD methods for the book repository.
/// </summary>
public interface IBookRepository
{
    /// <summary>
    /// Adds a new book to the repository.
    /// </summary>
    /// <param name="book">Book to add to the repository.</param>
    void BookAdd(IBookModel book);

    /// <summary>
    /// Gets all books from the repository.
    /// </summary>
    /// <returns>All books from the repository.</returns>
    IEnumerable<IBookModel> BookGetAll();

    /// <summary>
    /// Gets the book with the specified ID from the repository.
    /// </summary>
    /// <param name="bookId">Book ID.</param>
    /// <returns>Book with the specified ID.</returns>
    IBookModel? BookGetById(int bookId);

    /// <summary>
    /// Updates an existing book in the repository.
    /// </summary>
    /// <param name="book">Existing book to update.</param>
    void BookUpdate(IBookModel book);

    /// <summary>
    /// Removes an existing book from the repository.
    /// </summary>
    /// <param name="bookId">Book ID.</param>
    void BookDelete(int bookId);
}