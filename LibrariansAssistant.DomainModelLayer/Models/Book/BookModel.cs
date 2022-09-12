using LibrariansAssistant.DomainModelLayer.Models.Author;

namespace LibrariansAssistant.DomainModelLayer.Models.Book;

public sealed class BookModel : IBookModel
{
    public int Id { get; set; }

    public IEnumerable<IAuthorModel> Authors { get; set; }

    public string Title { get; set; }

    public string Genre { get; set; }

    public BookModel(int id, IEnumerable<IAuthorModel> authors, string title, string genre) =>
        (Id, Authors, Title, Genre) = (id, authors, title, genre);
}