using LibrariansAssistant.DomainModelLayer.Models.Author;

namespace LibrariansAssistant.DomainModelLayer.Models.Book;

public interface IBookModel
{
    int Id { get; set; }

    IEnumerable<IAuthorModel> Authors { get; set; }

    string Title { get; set; }

    string Genre { get; set; }
}