using LibrariansAssistant.DomainModelLayer.Models.Author;

namespace LibrariansAssistant.DomainModelLayer.Models.Book;

public sealed class BookModel : IBookModel
{
    public int Id { get; set; }

    public IEnumerable<IAuthorModel> Authors { get; set; }

    public string Title { get; set; }

    public string Genre { get; set; }

    public BookModel() =>
        (Authors, Title, Genre) = (Enumerable.Empty<IAuthorModel>(), string.Empty, string.Empty);

    public BookModel(IEnumerable<IAuthorModel> authors, string title, string genre) =>
        (Authors, Title, Genre) = (authors, title, genre);
}