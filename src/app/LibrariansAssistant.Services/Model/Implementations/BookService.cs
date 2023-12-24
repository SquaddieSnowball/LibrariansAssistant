using LibrariansAssistant.Domain.Models.Abstractions;
using LibrariansAssistant.Infrastructure.Repositories.Abstractions;
using LibrariansAssistant.Services.Common.Abstractions;
using LibrariansAssistant.Services.Model.Abstractions;

namespace LibrariansAssistant.Services.Model.Implementations;

/// <summary>
/// Represents the book service.
/// </summary>
public sealed class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IDataAnnotationsModelValidationService _dataAnnotationModelValidationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookService"/> class.
    /// </summary>
    /// <param name="bookRepository">Book repository for management.</param>
    /// <param name="dataAnnotationModelValidationService">Data Annotations 
    /// model validation service used to validate models.</param>
    public BookService(IBookRepository bookRepository,
        IDataAnnotationsModelValidationService dataAnnotationModelValidationService) =>
        (_bookRepository, _dataAnnotationModelValidationService) =
        (bookRepository, dataAnnotationModelValidationService);

    /// <summary>
    /// Adds a new book to the repository.
    /// </summary>
    /// <param name="book">Book to add to the repository.</param>
    public void BookAdd(IBookModel book)
    {
        _dataAnnotationModelValidationService.Validate(book);
        _bookRepository.BookAdd(book);
    }

    /// <summary>
    /// Gets all books from the repository.
    /// </summary>
    /// <returns>All books from the repository.</returns>
    public IEnumerable<IBookModel> BookGetAll() => _bookRepository.BookGetAll();

    /// <summary>
    /// Gets the book with the specified ID from the repository.
    /// </summary>
    /// <param name="bookId">Book ID.</param>
    /// <returns>Book with the specified ID.</returns>
    public IBookModel? BookGetById(int bookId) => _bookRepository.BookGetById(bookId);

    /// <summary>
    /// Updates an existing book in the repository.
    /// </summary>
    /// <param name="book">Existing book to update.</param>
    public void BookUpdate(IBookModel book)
    {
        _dataAnnotationModelValidationService.Validate(book);
        _bookRepository.BookUpdate(book);
    }

    /// <summary>
    /// Removes an existing book from the repository.
    /// </summary>
    /// <param name="bookId">Book ID.</param>
    public void BookDelete(int bookId) => _bookRepository.BookDelete(bookId);
}