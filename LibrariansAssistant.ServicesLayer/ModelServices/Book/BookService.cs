using LibrariansAssistant.DomainModelLayer.Models.Book;
using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;
using LibrariansAssistant.ServicesLayer.CommonServices.DataAnnotationModelValidationService;

namespace LibrariansAssistant.ServicesLayer.ModelServices.Book;

/// <summary>
/// Represents the book service.
/// </summary>
public sealed class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IDataAnnotationModelValidationService _dataAnnotationModelValidationService;

    /// <summary>
    /// Initializes a new instance of the BookService class.
    /// </summary>
    /// <param name="bookRepository">Book repository for management.</param>
    /// <param name="dataAnnotationModelValidationService">Data Annotation 
    /// model validation service used to validate models.</param>
    public BookService(IBookRepository bookRepository,
        IDataAnnotationModelValidationService dataAnnotationModelValidationService) =>
        (_bookRepository, _dataAnnotationModelValidationService) =
        (bookRepository, dataAnnotationModelValidationService);

    /// <summary>
    /// Adds a new book to the repository.
    /// </summary>
    /// <param name="book">Book to add to the repository.</param>
    public void BookAdd(IBookModel book)
    {
        try
        {
            _dataAnnotationModelValidationService.Validate(book);

            _bookRepository.BookAdd(book);
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Gets all books from the repository.
    /// </summary>
    /// <returns>All books from the repository.</returns>
    public IEnumerable<IBookModel> BookGetAll()
    {
        try
        {
            return _bookRepository.BookGetAll();
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Gets the book with the specified ID from the repository.
    /// </summary>
    /// <param name="bookId">Book ID.</param>
    /// <returns>Book with the specified ID.</returns>
    public IBookModel? BookGetById(int bookId)
    {
        try
        {
            return _bookRepository.BookGetById(bookId);
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Updates an existing book in the repository.
    /// </summary>
    /// <param name="book">Existing book to update.</param>
    public void BookUpdate(IBookModel book)
    {
        try
        {
            _dataAnnotationModelValidationService.Validate(book);

            _bookRepository.BookUpdate(book);
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Removes an existing book from the repository.
    /// </summary>
    /// <param name="bookId">Book ID.</param>
    public void BookDelete(int bookId)
    {
        try
        {
            _bookRepository.BookDelete(bookId);
        }
        catch
        {
            throw;
        }
    }
}