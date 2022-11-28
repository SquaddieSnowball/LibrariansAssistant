using LibrariansAssistant.DomainModelLayer.Models.Author;
using LibrariansAssistant.DomainModelLayer.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace LibrariansAssistant.DomainModelLayer.Models.Book;

public sealed class BookModel : IBookModel
{
    public int Id { get; set; }

    [EnumerableMustHaveElements(ErrorMessage = "Authors are required.")]
    public IEnumerable<IAuthorModel> Authors { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(50, ErrorMessage = "Title cannot exceed {1} characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Genre is required.")]
    [StringLength(25, ErrorMessage = "Genre cannot exceed {1} characters.")]
    public string Genre { get; set; }

    public BookModel() =>
        (Authors, Title, Genre) = (Enumerable.Empty<IAuthorModel>(), string.Empty, string.Empty);

    public BookModel(IEnumerable<IAuthorModel> authors, string title, string genre) =>
        (Authors, Title, Genre) = (authors, title, genre);
}