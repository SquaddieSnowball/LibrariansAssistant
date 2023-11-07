using LibrariansAssistant.Dependencies;
using LibrariansAssistant.Domain.Models.Author;
using LibrariansAssistant.Domain.Models.Book;
using LibrariansAssistant.Domain.Models.Issuing;
using LibrariansAssistant.Domain.Models.Reader;
using LibrariansAssistant.Infrastructure.Repositories.Interfaces;

namespace LibrariansAssistant.Services.UnitTests.Mocks;

internal sealed class RepositoryMock : IRepository
{
    private readonly List<IAuthorModel> _authors = new();
    private readonly List<IBookModel> _books = new();
    private readonly List<IIssuingModel> _issuings = new();
    private readonly List<IReaderModel> _readers = new();

    public void Initialize(string initializationString)
    {
        IReaderModel reader1 = DependenciesContainer.Resolve<IReaderModel>("Ila", "Hodge", "W",
            "Female", new DateTime(1997, 2, 4))!;
        IReaderModel reader2 = DependenciesContainer.Resolve<IReaderModel>("Octavius", "Becker", "L",
            "Female", new DateTime(1985, 5, 30))!;
        IReaderModel reader3 = DependenciesContainer.Resolve<IReaderModel>("Hammett", "Mcneil", "V",
            "Male", new DateTime(1956, 5, 22))!;
        IReaderModel reader4 = DependenciesContainer.Resolve<IReaderModel>("Macaulay", "Harrison", "B",
            "Female", new DateTime(1957, 7, 26))!;
        IReaderModel reader5 = DependenciesContainer.Resolve<IReaderModel>("Clinton", "Alvarado", "Z",
            "Female", new DateTime(1963, 3, 24))!;

        reader1.Id = 1;
        reader2.Id = 2;
        reader3.Id = 3;
        reader4.Id = 4;
        reader5.Id = 5;

        _readers.Add(reader1);
        _readers.Add(reader2);
        _readers.Add(reader3);
        _readers.Add(reader4);
        _readers.Add(reader5);

        IAuthorModel author1 = DependenciesContainer.Resolve<IAuthorModel>("Phelan", "Price", "V")!;
        IAuthorModel author2 = DependenciesContainer.Resolve<IAuthorModel>("Abraham", "Chase", "F")!;
        IAuthorModel author3 = DependenciesContainer.Resolve<IAuthorModel>("Anne", "Fulton", "R")!;
        IAuthorModel author4 = DependenciesContainer.Resolve<IAuthorModel>("Bernard", "Davidson", "D")!;
        IAuthorModel author5 = DependenciesContainer.Resolve<IAuthorModel>("Jackson", "Taylor", "O")!;

        author1.Id = 1;
        author2.Id = 2;
        author3.Id = 3;
        author4.Id = 4;
        author5.Id = 5;

        _authors.Add(author1);
        _authors.Add(author2);
        _authors.Add(author3);
        _authors.Add(author4);
        _authors.Add(author5);

        IBookModel book1 = DependenciesContainer.Resolve<IBookModel>(new IAuthorModel[] { author1 },
            "Nulla magna, malesuada vel", "Vestibulum")!;
        IBookModel book2 = DependenciesContainer.Resolve<IBookModel>(new IAuthorModel[] { author2 },
            "Et nunc", "Eget lacus")!;
        IBookModel book3 = DependenciesContainer.Resolve<IBookModel>(new IAuthorModel[] { author3 },
            "Etiam laoreet, libero et", "Ipsum donec")!;
        IBookModel book4 = DependenciesContainer.Resolve<IBookModel>(new IAuthorModel[] { author4 },
            "Eget odio. Aliquam vulputate", "Tempor augue")!;
        IBookModel book5 = DependenciesContainer.Resolve<IBookModel>(new IAuthorModel[] { author5 },
            "Pede. Nunc", "Enim")!;

        book1.Id = 1;
        book2.Id = 2;
        book3.Id = 3;
        book4.Id = 4;
        book5.Id = 5;

        _books.Add(book1);
        _books.Add(book2);
        _books.Add(book3);
        _books.Add(book4);
        _books.Add(book5);

        IIssuingModel issuing1 = DependenciesContainer.Resolve<IIssuingModel>(reader1, book1,
            new DateTime(2020, 10, 8), true, new DateTime(2022, 8, 5), 28)!;
        IIssuingModel issuing2 = DependenciesContainer.Resolve<IIssuingModel>(reader2, book2,
            new DateTime(2021, 12, 5), true, new DateTime(2023, 1, 22), 61)!;
        IIssuingModel issuing3 = DependenciesContainer.Resolve<IIssuingModel>(reader3, book3,
            new DateTime(2021, 10, 31), true, new DateTime(2023, 1, 17), 55)!;
        IIssuingModel issuing4 = DependenciesContainer.Resolve<IIssuingModel>(reader4, book4,
            new DateTime(2021, 12, 3), true, new DateTime(2022, 7, 27), 18)!;
        IIssuingModel issuing5 = DependenciesContainer.Resolve<IIssuingModel>(reader5, book5,
            new DateTime(2022, 5, 19), true, new DateTime(2023, 1, 3), 91)!;

        issuing1.Id = 1;
        issuing2.Id = 2;
        issuing3.Id = 3;
        issuing4.Id = 4;
        issuing5.Id = 5;

        _issuings.Add(issuing1);
        _issuings.Add(issuing2);
        _issuings.Add(issuing3);
        _issuings.Add(issuing4);
        _issuings.Add(issuing5);
    }

    public void AuthorAdd(IAuthorModel author) => throw new NotImplementedException();

    public IEnumerable<IAuthorModel> AuthorGetAll() => _authors;

    public IAuthorModel? AuthorGetById(int authorId) => _authors.FirstOrDefault(a => a.Id == authorId);

    public void AuthorUpdate(IAuthorModel author) => throw new NotImplementedException();

    public void AuthorDelete(int authorId) => throw new NotImplementedException();

    public void BookAdd(IBookModel book) => throw new NotImplementedException();

    public IEnumerable<IBookModel> BookGetAll() => _books;

    public IBookModel? BookGetById(int bookId) => _books.FirstOrDefault(b => b.Id == bookId);

    public void BookUpdate(IBookModel book) => throw new NotImplementedException();

    public void BookDelete(int bookId) => throw new NotImplementedException();

    public void IssuingAdd(IIssuingModel issuing) => throw new NotImplementedException();

    public IEnumerable<IIssuingModel> IssuingGetAll() => _issuings;

    public IIssuingModel? IssuingGetById(int issuingId) => _issuings.FirstOrDefault(i => i.Id == issuingId);

    public void IssuingUpdate(IIssuingModel issuing) => throw new NotImplementedException();

    public void IssuingDelete(int issuingId) => throw new NotImplementedException();

    public void ReaderAdd(IReaderModel reader) => throw new NotImplementedException();

    public IEnumerable<IReaderModel> ReaderGetAll() => _readers;

    public IReaderModel? ReaderGetById(int readerId) => _readers.FirstOrDefault(r => r.Id == readerId);

    public void ReaderUpdate(IReaderModel reader) => throw new NotImplementedException();

    public void ReaderDelete(int readerId) => throw new NotImplementedException();
}