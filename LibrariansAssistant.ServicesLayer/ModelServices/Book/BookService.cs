using LibrariansAssistant.DomainModelLayer.Models.Book;
using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;
using LibrariansAssistant.ServicesLayer.CommonServices.DataAnnotationModelValidationService;

namespace LibrariansAssistant.ServicesLayer.ModelServices.Book;

public sealed class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IDataAnnotationModelValidationService _dataAnnotationModelValidationService;

    public BookService(IBookRepository bookRepository,
        IDataAnnotationModelValidationService dataAnnotationModelValidationService) =>
        (_bookRepository, _dataAnnotationModelValidationService) =
        (bookRepository, dataAnnotationModelValidationService);

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