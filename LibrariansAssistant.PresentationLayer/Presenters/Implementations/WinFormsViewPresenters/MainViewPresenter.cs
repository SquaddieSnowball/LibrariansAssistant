using LibrariansAssistant.DependenciesLayer;
using LibrariansAssistant.DomainModelLayer.Models.Author;
using LibrariansAssistant.DomainModelLayer.Models.Book;
using LibrariansAssistant.DomainModelLayer.Models.Issuing;
using LibrariansAssistant.DomainModelLayer.Models.Reader;
using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;
using LibrariansAssistant.PresentationLayer.ViewInterfaces.WinFormsViewInterfaces;
using LibrariansAssistant.PresentationLayer.ViewServiceInterfaces;
using LibrariansAssistant.ServicesLayer.CommonServices.Interfaces;
using LibrariansAssistant.ServicesLayer.ModelServices.Author;
using LibrariansAssistant.ServicesLayer.ModelServices.Book;
using LibrariansAssistant.ServicesLayer.ModelServices.Issuing;
using LibrariansAssistant.ServicesLayer.ModelServices.Reader;

namespace LibrariansAssistant.PresentationLayer.Presenters.Implementations.WinFormsViewPresenters;

public sealed class MainViewPresenter : IPresenter
{
    private readonly IMainView _mainView;
    private readonly IRepository _repository;
    private readonly IMessageService _messageService;
    private readonly string _initializationString;
    private readonly IDataAnnotationModelValidationService _dataAnnotationModelValidationService =
        DependenciesContainer.Resolve<IDataAnnotationModelValidationService>()!;

    public MainViewPresenter(IMainView mainView, IRepository repository,
        IMessageService messageService, string initializationString)
    {
        (_mainView, _repository, _messageService, _initializationString) =
            (mainView, repository, messageService, initializationString);

        try
        {
            _repository.Initialize(_initializationString);
        }
        catch
        {
            throw;
        }

        SubscribeToViewEvents();
    }

    public void RunView() =>
        _mainView.Show();

    private void SubscribeToViewEvents()
    {
        _mainView.AuthorsUpdateNormalView += MainViewOnAuthorsUpdateNormalView;
        _mainView.BooksUpdateNormalView += MainViewOnBooksUpdateNormalView;
        _mainView.IssuingsUpdateNormalView += MainViewOnIssuingsUpdateNormalView;
        _mainView.ReadersUpdateNormalView += MainViewOnReadersUpdateNormalView;

        _mainView.AuthorUpdateEditView += MainViewOnAuthorUpdateEditView;
        _mainView.BookUpdateEditView += MainViewOnBookUpdateEditView;
        _mainView.IssuingUpdateEditView += MainViewOnIssuingUpdateEditView;
        _mainView.ReaderUpdateEditView += MainViewOnReaderUpdateEditView;

        _mainView.AuthorsUpdatePickView += MainViewOnAuthorsUpdatePickView;
        _mainView.BooksUpdatePickView += MainViewOnBooksUpdatePickView;
        _mainView.ReadersUpdatePickView += MainViewOnReadersUpdatePickView;

        _mainView.IssuingOpen += MainViewOnIssuingOpen;
        _mainView.IssuingClose += MainViewOnIssuingClose;
        _mainView.AuthorAdd += MainViewOnAuthorAdd;
        _mainView.BookAdd += MainViewOnBookAdd;
        _mainView.ReaderAdd += MainViewOnReaderAdd;

        _mainView.AuthorEdit += MainViewOnAuthorEdit;
        _mainView.BookEdit += MainViewOnBookEdit;
        _mainView.IssuingEdit += MainViewOnIssuingEdit;
        _mainView.ReaderEdit += MainViewOnReaderEdit;

        _mainView.AuthorRemove += MainViewOnAuthorRemove;
        _mainView.BookRemove += MainViewOnBookRemove;
        _mainView.IssuingRemove += MainViewOnIssuingRemove;
        _mainView.ReaderRemove += MainViewOnReaderRemove;
    }

    private void MainViewOnAuthorsUpdateNormalView(object? sender, EventArgs e)
    {
        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IAuthorModel> authors = authorService.AuthorGetAll();

        _mainView.VisibleDataNormalView = authors
            .Select(a => new
            {
                a.Id,
                a.FirstName,
                a.LastName,
                a.Patronymic
            })
            .Cast<object>();

        _mainView.VisibleDataColumnHeadersNormalView = new string[]
        {
            "Id",
            "First name",
            "Last name",
            "Patronymic"
        };
    }

    private void MainViewOnBooksUpdateNormalView(object? sender, EventArgs e)
    {
        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IBookModel> books = bookService.BookGetAll();

        _mainView.VisibleDataNormalView = books
            .Select(b => new
            {
                b.Id,
                Authors =
                    b.Authors.Select(a =>
                        $"{a.LastName} {a.FirstName.First()}." +
                        $"{((string.IsNullOrEmpty(a.Patronymic) is false) ? $" {a.Patronymic.First()}." : string.Empty)}")
                    .Aggregate((a1, a2) => a1 + " | " + a2),
                b.Title,
                b.Genre
            })
            .Cast<object>();

        _mainView.VisibleDataColumnHeadersNormalView = new string[]
        {
            "Id",
            "Authors",
            "Title",
            "Genre"
        };
    }

    private void MainViewOnIssuingsUpdateNormalView(object? sender, EventArgs e)
    {
        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IIssuingModel> issuings = issuingService.IssuingGetAll();

        _mainView.VisibleDataNormalView = issuings
            .Select(i => new
            {
                i.Id,
                Reader =
                    $"{i.Reader.LastName} {i.Reader.FirstName.First()}." +
                    $"{((string.IsNullOrEmpty(i.Reader.Patronymic) is false) ? $" {i.Reader.Patronymic.First()}." : string.Empty)}",
                i.Book.Title,
                i.TakeDate,
                i.Returned,
                i.ReturnDate,
                i.ReturnState
            })
            .Cast<object>();

        _mainView.VisibleDataColumnHeadersNormalView = new string[]
        {
            "Id",
            "Reader",
            "Book",
            "Take date",
            "Returned",
            "Return date",
            "Return state"
        };
    }

    private void MainViewOnReadersUpdateNormalView(object? sender, EventArgs e)
    {
        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IReaderModel> readers = readerService.ReaderGetAll();

        _mainView.VisibleDataNormalView = readers
            .Select(r => new
            {
                r.Id,
                r.FirstName,
                r.LastName,
                r.Patronymic,
                r.Gender,
                r.DateOfBirth
            })
            .Cast<object>();

        _mainView.VisibleDataColumnHeadersNormalView = new string[]
        {
            "Id",
            "First name",
            "Last name",
            "Patronymic",
            "Gender",
            "Date of birth"
        };
    }

    private void MainViewOnAuthorUpdateEditView(object? sender, int e)
    {
        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IAuthorModel? author = authorService.AuthorGetById(e);

        if (author is not null)
            _mainView.VisibleDataEditView = new object?[]
            {
                author.Id,
                author.FirstName,
                author.LastName,
                author.Patronymic
            };
        else
            _messageService.ShowError("This author is no longer exists.");
    }

    private void MainViewOnBookUpdateEditView(object? sender, int e)
    {
        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;

        IBookModel? book = bookService.BookGetById(e);

        if (book is not null)
            _mainView.VisibleDataEditView = new object?[]
            {
                book.Id,
                book.Authors.Select(a => a.Id),
                book.Title,
                book.Genre
            };
        else
            _messageService.ShowError("This book is no longer exists.");
    }

    private void MainViewOnIssuingUpdateEditView(object? sender, int e)
    {
        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;

        IIssuingModel? issuing = issuingService.IssuingGetById(e);

        if (issuing is not null)
            _mainView.VisibleDataEditView = new object?[]
            {
                issuing.Id,
                new int[]{ issuing.Reader.Id},
                new int[]{ issuing.Book.Id},
                issuing.TakeDate,
                issuing.Returned,
                issuing.ReturnDate,
                issuing.ReturnState
            };
        else
            _messageService.ShowError("This issuing is no longer exists.");
    }

    private void MainViewOnReaderUpdateEditView(object? sender, int e)
    {
        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;

        IReaderModel? reader = readerService.ReaderGetById(e);

        if (reader is not null)
            _mainView.VisibleDataEditView = new object?[]
            {
                reader.Id,
                reader.FirstName,
                reader.LastName,
                reader.Patronymic,
                reader.Gender,
                reader.DateOfBirth
            };
        else
            _messageService.ShowError("This reader is no longer exists.");
    }

    private void MainViewOnAuthorsUpdatePickView(object? sender, EventArgs e)
    {
        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IAuthorModel> authors = authorService.AuthorGetAll();

        _mainView.VisibleDataPickView = authors
            .Select(a => new KeyValuePair<int, string?>(
                a.Id,
                $"{a.LastName} {a.FirstName.First()}." +
                $"{((string.IsNullOrEmpty(a.Patronymic) is false) ? $" {a.Patronymic.First()}." : string.Empty)}"));
    }

    private void MainViewOnBooksUpdatePickView(object? sender, EventArgs e)
    {
        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IBookModel> books = bookService.BookGetAll();

        _mainView.VisibleDataPickView = books.Select(b => new KeyValuePair<int, string?>(b.Id, b.Title));
    }

    private void MainViewOnReadersUpdatePickView(object? sender, EventArgs e)
    {
        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IReaderModel> readers = readerService.ReaderGetAll();

        _mainView.VisibleDataPickView = readers
            .Select(r => new KeyValuePair<int, string?>(
                r.Id,
                $"{r.LastName} {r.FirstName.First()}." +
                $"{((string.IsNullOrEmpty(r.Patronymic) is false) ? $" {r.Patronymic.First()}." : string.Empty)}"));
    }

    private void MainViewOnIssuingOpen(object? sender, IEnumerable<object?> e)
    {
        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;
        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;
        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;

        IIssuingModel issuing = DependenciesContainer.Resolve<IIssuingModel>()!;

        var args = (e as object?[])!;

        var readerIds = (args[0] as IEnumerable<int>)!;
        var bookIds = (args[1] as IEnumerable<int>)!;

        if (readerIds.Any() is true)
        {
            IReaderModel? reader = readerService.ReaderGetById(readerIds.First());

            if (reader is not null)
                issuing.Reader = reader;
            else
            {
                _messageService.ShowError("This reader is no longer exists.");

                return;
            }
        }

        if (bookIds.Any() is true)
        {
            IBookModel? book = bookService.BookGetById(bookIds.First());

            if (book is not null)
                issuing.Book = book;
            else
            {
                _messageService.ShowError("This book is no longer exists.");

                return;
            }
        }

        issuing.TakeDate = DateTime.Now;

        try
        {
            issuingService.IssuingAdd(issuing);
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnIssuingClose(object? sender, IEnumerable<object?> e)
    {
        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;

        var args = (e as object?[])!;

        IIssuingModel? issuing = issuingService.IssuingGetById((int)args[0]!);

        if (issuing is not null)
        {
            issuing.Returned = true;
            issuing.ReturnDate = DateTime.Now;
            issuing.ReturnState = decimal.ToInt32((decimal)args[1]!);

            try
            {
                issuingService.IssuingUpdate(issuing);
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);
            }
        }
        else
            _messageService.ShowError("This issuing is no longer exists.");
    }

    private void MainViewOnAuthorAdd(object? sender, IEnumerable<object?> e)
    {
        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IAuthorModel author = DependenciesContainer.Resolve<IAuthorModel>()!;

        var args = (e as object?[])!;

        author.FirstName = (args[0] as string)!;
        author.LastName = (args[1] as string)!;
        author.Patronymic = args[2] as string;

        try
        {
            authorService.AuthorAdd(author);
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnBookAdd(object? sender, IEnumerable<object?> e)
    {
        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;
        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IBookModel book = DependenciesContainer.Resolve<IBookModel>()!;

        var args = (e as object?[])!;

        var authors = new List<IAuthorModel>();

        foreach (int authorId in (args[0] as IEnumerable<int>)!)
        {
            IAuthorModel? author = authorService.AuthorGetById(authorId);

            if (author is not null)
                authors.Add(author);
        }

        book.Authors = authors;
        book.Title = (args[1] as string)!;
        book.Genre = (args[2] as string)!;

        try
        {
            bookService.BookAdd(book);
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnReaderAdd(object? sender, IEnumerable<object?> e)
    {
        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;

        IReaderModel reader = DependenciesContainer.Resolve<IReaderModel>()!;

        var args = (e as object?[])!;

        reader.FirstName = (args[0] as string)!;
        reader.LastName = (args[1] as string)!;
        reader.Patronymic = args[2] as string;
        reader.Gender = (args[3] as string)!;
        reader.DateOfBirth = (DateTime)args[4]!;

        try
        {
            readerService.ReaderAdd(reader);
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnAuthorEdit(object? sender, IEnumerable<object?> e)
    {
        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IAuthorModel author = DependenciesContainer.Resolve<IAuthorModel>()!;

        var args = (e as object?[])!;

        author.Id = (int)args[0]!;
        author.FirstName = (args[1] as string)!;
        author.LastName = (args[2] as string)!;
        author.Patronymic = args[3] as string;

        try
        {
            authorService.AuthorUpdate(author);
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnBookEdit(object? sender, IEnumerable<object?> e)
    {
        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;
        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IBookModel book = DependenciesContainer.Resolve<IBookModel>()!;

        var args = (e as object?[])!;

        var authors = new List<IAuthorModel>();

        foreach (int authorId in (args[1] as IEnumerable<int>)!)
        {
            IAuthorModel? author = authorService.AuthorGetById(authorId);

            if (author is not null)
                authors.Add(author);
        }

        book.Id = (int)args[0]!;
        book.Authors = authors;
        book.Title = (args[2] as string)!;
        book.Genre = (args[3] as string)!;

        try
        {
            bookService.BookUpdate(book);
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnIssuingEdit(object? sender, IEnumerable<object?> e)
    {
        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;
        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;
        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;

        IIssuingModel issuing = DependenciesContainer.Resolve<IIssuingModel>()!;

        var args = (e as object?[])!;

        var readerIds = (args[1] as IEnumerable<int>)!;
        var bookIds = (args[2] as IEnumerable<int>)!;

        if (readerIds.Any() is true)
        {
            IReaderModel? reader = readerService.ReaderGetById(readerIds.First());

            if (reader is not null)
                issuing.Reader = reader;
            else
            {
                _messageService.ShowError("This reader is no longer exists.");

                return;
            }
        }

        if (bookIds.Any() is true)
        {
            IBookModel? book = bookService.BookGetById(bookIds.First());

            if (book is not null)
                issuing.Book = book;
            else
            {
                _messageService.ShowError("This book is no longer exists.");

                return;
            }
        }

        issuing.Id = (int)args[0]!;
        issuing.TakeDate = (DateTime)args[3]!;
        issuing.Returned = (bool)args[4]!;
        issuing.ReturnDate = (DateTime)args[5]!;
        issuing.ReturnState = decimal.ToInt32((decimal)args[6]!);

        try
        {
            issuingService.IssuingUpdate(issuing);
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnReaderEdit(object? sender, IEnumerable<object?> e)
    {
        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;

        IReaderModel reader = DependenciesContainer.Resolve<IReaderModel>()!;

        var args = (e as object?[])!;

        reader.Id = (int)args[0]!;
        reader.FirstName = (args[1] as string)!;
        reader.LastName = (args[2] as string)!;
        reader.Patronymic = args[3] as string;
        reader.Gender = (args[4] as string)!;
        reader.DateOfBirth = (DateTime)args[5]!;

        try
        {
            readerService.ReaderUpdate(reader);
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnAuthorRemove(object? sender, int e)
    {
        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        authorService.AuthorDelete(e);
    }

    private void MainViewOnBookRemove(object? sender, int e)
    {
        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;

        bookService.BookDelete(e);
    }

    private void MainViewOnIssuingRemove(object? sender, int e)
    {
        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;

        issuingService.IssuingDelete(e);
    }

    private void MainViewOnReaderRemove(object? sender, int e)
    {
        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;

        readerService.ReaderDelete(e);
    }
}