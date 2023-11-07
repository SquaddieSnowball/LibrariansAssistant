using LibrariansAssistant.Dependencies;
using LibrariansAssistant.Domain.Models.Author;
using LibrariansAssistant.Domain.Models.Book;
using LibrariansAssistant.Domain.Models.Issuing;
using LibrariansAssistant.Domain.Models.Reader;
using LibrariansAssistant.Infrastructure.Repositories.Interfaces;
using LibrariansAssistant.Presentation.ViewInterfaces.WinFormsViewInterfaces;
using LibrariansAssistant.Presentation.ViewServiceInterfaces;
using LibrariansAssistant.Services.CommonServices.DataAnnotationModelValidationService;
using LibrariansAssistant.Services.CommonServices.ReportGenerator;
using LibrariansAssistant.Services.CommonServices.ReportGenerator.Entities;
using LibrariansAssistant.Services.CommonServices.ReportGenerator.Implementations;
using LibrariansAssistant.Services.ModelServices.Author;
using LibrariansAssistant.Services.ModelServices.Book;
using LibrariansAssistant.Services.ModelServices.Issuing;
using LibrariansAssistant.Services.ModelServices.Reader;

namespace LibrariansAssistant.Presentation.Presenters.Implementations.WinFormsViewPresenters;

/// <summary>
/// Represents the presenter that controls the MainView.
/// </summary>
public sealed class MainViewPresenter : IPresenter
{
    private readonly IMainView _mainView;
    private readonly IRepository _repository;
    private readonly IMessageService _messageService;
    private readonly string _initializationString;
    private readonly IDataAnnotationModelValidationService _dataAnnotationModelValidationService =
        DependenciesContainer.Resolve<IDataAnnotationModelValidationService>()!;

    /// <summary>
    /// Initializes a new instance of the MainViewPresenter class.
    /// </summary>
    /// <param name="mainView">Instance of the MainView class to manage.</param>
    /// <param name="repository">Repository used to store view data.</param>
    /// <param name="messageService">Message service for passing messages to the view.</param>
    /// <param name="initializationString">Initialization string to initialize the repository.</param>
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

    /// <summary>
    /// Runs the view controlled by the current presenter.
    /// </summary>
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

        _mainView.IssuingsUpdatePeriodView += MainViewOnIssuingsUpdatePeriodView;
        _mainView.ReadersUpdatePeriodView += MainViewOnReadersUpdatePeriodView;

        _mainView.ReaderUpdateMostActiveView += MainViewOnReaderUpdateMostActiveView;
        _mainView.AuthorUpdateMostPopularView += MainViewOnAuthorUpdateMostPopularView;
        _mainView.BookUpdateMostPopularGenreView += MainViewOnBookUpdateMostPopularGenreView;

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

        _mainView.ExportDataText += MainViewOnExportDataText;
        _mainView.ExportDataExcel += MainViewOnExportDataExcel;
    }

    private void MainViewOnAuthorsUpdateNormalView(object? sender, EventArgs e)
    {
        _mainView.IsOperationSuccessful = false;

        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IAuthorModel> authors;

        try
        {
            authors = authorService.AuthorGetAll();
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

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

        _mainView.IsOperationSuccessful = true;
    }

    private void MainViewOnBooksUpdateNormalView(object? sender, EventArgs e)
    {
        _mainView.IsOperationSuccessful = false;

        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IBookModel> books;

        try
        {
            books = bookService.BookGetAll();
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

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

        _mainView.IsOperationSuccessful = true;
    }

    private void MainViewOnIssuingsUpdateNormalView(object? sender, EventArgs e)
    {
        _mainView.IsOperationSuccessful = false;

        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IIssuingModel> issuings;

        try
        {
            issuings = issuingService.IssuingGetAll();
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

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

        _mainView.IsOperationSuccessful = true;
    }

    private void MainViewOnReadersUpdateNormalView(object? sender, EventArgs e)
    {
        _mainView.IsOperationSuccessful = false;

        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IReaderModel> readers;

        try
        {
            readers = readerService.ReaderGetAll();
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

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

        _mainView.IsOperationSuccessful = true;
    }

    private void MainViewOnAuthorUpdateEditView(object? sender, int e)
    {
        _mainView.IsOperationSuccessful = false;

        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IAuthorModel? author;

        try
        {
            author = authorService.AuthorGetById(e);
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

        if (author is not null)
        {
            _mainView.VisibleDataEditView = new object?[]
            {
                author.Id,
                author.FirstName,
                author.LastName,
                author.Patronymic
            };

            _mainView.IsOperationSuccessful = true;
        }
        else
            _messageService.ShowError("This author is no longer exists.");
    }

    private void MainViewOnBookUpdateEditView(object? sender, int e)
    {
        _mainView.IsOperationSuccessful = false;

        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;

        IBookModel? book;

        try
        {
            book = bookService.BookGetById(e);
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

        if (book is not null)
        {
            _mainView.VisibleDataEditView = new object?[]
            {
                book.Id,
                book.Authors.Select(a => a.Id),
                book.Title,
                book.Genre
            };

            _mainView.IsOperationSuccessful = true;
        }
        else
            _messageService.ShowError("This book is no longer exists.");
    }

    private void MainViewOnIssuingUpdateEditView(object? sender, int e)
    {
        _mainView.IsOperationSuccessful = false;

        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;

        IIssuingModel? issuing;

        try
        {
            issuing = issuingService.IssuingGetById(e);
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

        if (issuing is not null)
        {
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

            _mainView.IsOperationSuccessful = true;
        }
        else
            _messageService.ShowError("This issuing is no longer exists.");
    }

    private void MainViewOnReaderUpdateEditView(object? sender, int e)
    {
        _mainView.IsOperationSuccessful = false;

        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;

        IReaderModel? reader;

        try
        {
            reader = readerService.ReaderGetById(e);
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

        if (reader is not null)
        {
            _mainView.VisibleDataEditView = new object?[]
            {
                reader.Id,
                reader.FirstName,
                reader.LastName,
                reader.Patronymic,
                reader.Gender,
                reader.DateOfBirth
            };

            _mainView.IsOperationSuccessful = true;
        }
        else
            _messageService.ShowError("This reader is no longer exists.");
    }

    private void MainViewOnAuthorsUpdatePickView(object? sender, EventArgs e)
    {
        _mainView.IsOperationSuccessful = false;

        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IAuthorModel> authors;

        try
        {
            authors = authorService.AuthorGetAll();
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

        _mainView.VisibleDataPickView = authors
            .Select(a => new KeyValuePair<int, string?>(
                a.Id,
                $"{a.LastName} {a.FirstName.First()}." +
                $"{((string.IsNullOrEmpty(a.Patronymic) is false) ? $" {a.Patronymic.First()}." : string.Empty)}"));

        _mainView.IsOperationSuccessful = true;
    }

    private void MainViewOnBooksUpdatePickView(object? sender, EventArgs e)
    {
        _mainView.IsOperationSuccessful = false;

        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IBookModel> books;

        try
        {
            books = bookService.BookGetAll();
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

        _mainView.VisibleDataPickView = books.Select(b => new KeyValuePair<int, string?>(b.Id, b.Title));

        _mainView.IsOperationSuccessful = true;
    }

    private void MainViewOnReadersUpdatePickView(object? sender, EventArgs e)
    {
        _mainView.IsOperationSuccessful = false;

        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;

        IEnumerable<IReaderModel> readers;

        try
        {
            readers = readerService.ReaderGetAll();
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

        _mainView.VisibleDataPickView = readers
            .Select(r => new KeyValuePair<int, string?>(
                r.Id,
                $"{r.LastName} {r.FirstName.First()}." +
                $"{((string.IsNullOrEmpty(r.Patronymic) is false) ? $" {r.Patronymic.First()}." : string.Empty)}"));

        _mainView.IsOperationSuccessful = true;
    }

    private void MainViewOnIssuingsUpdatePeriodView(object? sender, EventArgs e)
    {
        _mainView.IsOperationSuccessful = false;

        _mainView.VisibleDataPeriodView = new Dictionary<int, string>()
        {
            {3, "Take date"},
            {5, "Return date"}
        };

        _mainView.IsOperationSuccessful = true;
    }

    private void MainViewOnReadersUpdatePeriodView(object? sender, EventArgs e)
    {
        _mainView.IsOperationSuccessful = false;

        _mainView.VisibleDataPeriodView = new Dictionary<int, string>()
        {
            {5, "Date of birth"}
        };

        _mainView.IsOperationSuccessful = true;
    }

    private void MainViewOnReaderUpdateMostActiveView(object? sender, EventArgs e)
    {
        _mainView.IsOperationSuccessful = false;

        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;

        IReaderModel? reader;

        try
        {
            reader = issuingService.ReaderMostActiveGet();
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

        if (reader is not null)
        {
            _mainView.VisibleDataNormalView =
                new object[] { new
                {
                    reader.Id,
                    reader.FirstName,
                    reader.LastName,
                    reader.Patronymic,
                    reader.Gender,
                    reader.DateOfBirth
                }};

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
        else
        {
            _mainView.VisibleDataNormalView = null;
            _mainView.VisibleDataColumnHeadersNormalView = null;
        }

        _mainView.IsOperationSuccessful = true;
    }

    private void MainViewOnAuthorUpdateMostPopularView(object? sender, EventArgs e)
    {
        _mainView.IsOperationSuccessful = false;

        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;

        IAuthorModel? author;

        try
        {
            author = issuingService.AuthorMostPopularGet();
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

        if (author is not null)
        {
            _mainView.VisibleDataNormalView =
                new object[] { new
                {
                    author.Id,
                    author.FirstName,
                    author.LastName,
                    author.Patronymic
                }};

            _mainView.VisibleDataColumnHeadersNormalView = new string[]
            {
                "Id",
                "First name",
                "Last name",
                "Patronymic"
            };
        }
        else
        {
            _mainView.VisibleDataNormalView = null;
            _mainView.VisibleDataColumnHeadersNormalView = null;
        }

        _mainView.IsOperationSuccessful = true;
    }

    private void MainViewOnBookUpdateMostPopularGenreView(object? sender, EventArgs e)
    {
        _mainView.IsOperationSuccessful = false;

        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;

        string? genre;

        try
        {
            genre = issuingService.BookMostPopularGenreGet();
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

        if (genre is not null)
            _messageService.ShowMessage($"Most popular genre: \"{genre}\"");
        else
            _messageService.ShowMessage("The books were never taken.");

        _mainView.IsOperationSuccessful = true;
    }

    private void MainViewOnIssuingOpen(object? sender, IEnumerable<object?> e)
    {
        _mainView.IsOperationSuccessful = false;

        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;
        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;
        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;

        IIssuingModel issuing = DependenciesContainer.Resolve<IIssuingModel>()!;

        object?[] args = (e as object?[])!;

        IEnumerable<int> readerIds = (args[0] as IEnumerable<int>)!;
        IEnumerable<int> bookIds = (args[1] as IEnumerable<int>)!;

        if (readerIds.Any() is true)
        {
            IReaderModel? reader;

            try
            {
                reader = readerService.ReaderGetById(readerIds.First());
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);

                return;
            }

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
            IBookModel? book;

            try
            {
                book = bookService.BookGetById(bookIds.First());
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);

                return;
            }

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

            _mainView.IsOperationSuccessful = true;
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnIssuingClose(object? sender, IEnumerable<object?> e)
    {
        _mainView.IsOperationSuccessful = false;

        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;

        object?[] args = (e as object?[])!;

        IIssuingModel? issuing;

        try
        {
            issuing = issuingService.IssuingGetById((int)args[0]!);
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }

        if (issuing is not null)
        {
            issuing.Returned = true;
            issuing.ReturnDate = DateTime.Now;
            issuing.ReturnState = decimal.ToInt32((decimal)args[1]!);

            try
            {
                issuingService.IssuingUpdate(issuing);

                _mainView.IsOperationSuccessful = true;
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
        _mainView.IsOperationSuccessful = false;

        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IAuthorModel author = DependenciesContainer.Resolve<IAuthorModel>()!;

        object?[] args = (e as object?[])!;

        author.FirstName = (args[0] as string)!;
        author.LastName = (args[1] as string)!;
        author.Patronymic = args[2] as string;

        try
        {
            authorService.AuthorAdd(author);

            _mainView.IsOperationSuccessful = true;
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnBookAdd(object? sender, IEnumerable<object?> e)
    {
        _mainView.IsOperationSuccessful = false;

        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;
        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IBookModel book = DependenciesContainer.Resolve<IBookModel>()!;

        object?[] args = (e as object?[])!;

        List<IAuthorModel> authors = new();

        foreach (int authorId in (args[0] as IEnumerable<int>)!)
        {
            IAuthorModel? author;

            try
            {
                author = authorService.AuthorGetById(authorId);
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);

                return;
            }

            if (author is not null)
                authors.Add(author);
        }

        book.Authors = authors;
        book.Title = (args[1] as string)!;
        book.Genre = (args[2] as string)!;

        try
        {
            bookService.BookAdd(book);

            _mainView.IsOperationSuccessful = true;
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnReaderAdd(object? sender, IEnumerable<object?> e)
    {
        _mainView.IsOperationSuccessful = false;

        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;

        IReaderModel reader = DependenciesContainer.Resolve<IReaderModel>()!;

        object?[] args = (e as object?[])!;

        reader.FirstName = (args[0] as string)!;
        reader.LastName = (args[1] as string)!;
        reader.Patronymic = args[2] as string;
        reader.Gender = (args[3] as string)!;
        reader.DateOfBirth = (DateTime)args[4]!;

        try
        {
            readerService.ReaderAdd(reader);

            _mainView.IsOperationSuccessful = true;
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnAuthorEdit(object? sender, IEnumerable<object?> e)
    {
        _mainView.IsOperationSuccessful = false;

        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IAuthorModel author = DependenciesContainer.Resolve<IAuthorModel>()!;

        object?[] args = (e as object?[])!;

        author.Id = (int)args[0]!;
        author.FirstName = (args[1] as string)!;
        author.LastName = (args[2] as string)!;
        author.Patronymic = args[3] as string;

        try
        {
            authorService.AuthorUpdate(author);

            _mainView.IsOperationSuccessful = true;
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnBookEdit(object? sender, IEnumerable<object?> e)
    {
        _mainView.IsOperationSuccessful = false;

        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;
        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        IBookModel book = DependenciesContainer.Resolve<IBookModel>()!;

        object?[] args = (e as object?[])!;

        List<IAuthorModel> authors = new();

        foreach (int authorId in (args[1] as IEnumerable<int>)!)
        {
            IAuthorModel? author;

            try
            {
                author = authorService.AuthorGetById(authorId);
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);

                return;
            }

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

            _mainView.IsOperationSuccessful = true;
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnIssuingEdit(object? sender, IEnumerable<object?> e)
    {
        _mainView.IsOperationSuccessful = false;

        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;
        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;
        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;

        IIssuingModel issuing = DependenciesContainer.Resolve<IIssuingModel>()!;

        object?[] args = (e as object?[])!;

        IEnumerable<int> readerIds = (args[1] as IEnumerable<int>)!;
        IEnumerable<int> bookIds = (args[2] as IEnumerable<int>)!;

        if (readerIds.Any() is true)
        {
            IReaderModel? reader;

            try
            {
                reader = readerService.ReaderGetById(readerIds.First());
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);

                return;
            }

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
            IBookModel? book;

            try
            {
                book = bookService.BookGetById(bookIds.First());
            }
            catch (Exception ex)
            {
                _messageService.ShowError(ex.Message);

                return;
            }

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

            _mainView.IsOperationSuccessful = true;
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnReaderEdit(object? sender, IEnumerable<object?> e)
    {
        _mainView.IsOperationSuccessful = false;

        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;

        IReaderModel reader = DependenciesContainer.Resolve<IReaderModel>()!;

        object?[] args = (e as object?[])!;

        reader.Id = (int)args[0]!;
        reader.FirstName = (args[1] as string)!;
        reader.LastName = (args[2] as string)!;
        reader.Patronymic = args[3] as string;
        reader.Gender = (args[4] as string)!;
        reader.DateOfBirth = (DateTime)args[5]!;

        try
        {
            readerService.ReaderUpdate(reader);

            _mainView.IsOperationSuccessful = true;
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);
        }
    }

    private void MainViewOnAuthorRemove(object? sender, int e)
    {
        _mainView.IsOperationSuccessful = false;

        IAuthorService authorService =
            DependenciesContainer.Resolve<IAuthorService>(_repository, _dataAnnotationModelValidationService)!;

        try
        {
            authorService.AuthorDelete(e);

            _mainView.IsOperationSuccessful = true;
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }
    }

    private void MainViewOnBookRemove(object? sender, int e)
    {
        _mainView.IsOperationSuccessful = false;

        IBookService bookService =
            DependenciesContainer.Resolve<IBookService>(_repository, _dataAnnotationModelValidationService)!;

        try
        {
            bookService.BookDelete(e);

            _mainView.IsOperationSuccessful = true;
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }
    }

    private void MainViewOnIssuingRemove(object? sender, int e)
    {
        _mainView.IsOperationSuccessful = false;

        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(_repository, _dataAnnotationModelValidationService)!;

        try
        {
            issuingService.IssuingDelete(e);

            _mainView.IsOperationSuccessful = true;
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }
    }

    private void MainViewOnReaderRemove(object? sender, int e)
    {
        _mainView.IsOperationSuccessful = false;

        IReaderService readerService =
            DependenciesContainer.Resolve<IReaderService>(_repository, _dataAnnotationModelValidationService)!;

        try
        {
            readerService.ReaderDelete(e);

            _mainView.IsOperationSuccessful = true;
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }
    }

    private void MainViewOnExportDataText(object? sender, IEnumerable<object> e) =>
        ExportData("Text", e);

    private void MainViewOnExportDataExcel(object? sender, IEnumerable<object> e) =>
        ExportData("Excel", e);

    private void ExportData(string dataType, IEnumerable<object> data)
    {
        _mainView.IsOperationSuccessful = false;

        IReportGenerator reportGenerator = dataType switch
        {
            "Text" => new TextReportGenerator(),
            "Excel" => new ExcelReportGenerator(),
            _ => throw new NotImplementedException("This report generator has not yet been implemented."),
        };

        object[] args = (data as object[])!;

        string title = (args[0] as string)!;
        string[] columnHeaders = (args[1] as string[])!;
        string[,] exportData = (args[2] as string[,])!;
        string filePath = (args[3] as string)!;

        if (exportData.Length is 0)
        {
            _messageService.ShowError("There is no data to export.");

            return;
        }

        try
        {
            ReportDocument reportDocument = new(title, columnHeaders, exportData.GetLength(0), exportData.GetLength(1));

            for (var i = 0; i < reportDocument.RowCount; i++)
                for (var j = 0; j < reportDocument.ColumnCount; j++)
                    reportDocument[i, j] = exportData[i, j];

            reportGenerator.GenerateReport(filePath, reportDocument);

            _mainView.IsOperationSuccessful = true;
        }
        catch (Exception ex)
        {
            _messageService.ShowError(ex.Message);

            return;
        }
    }
}