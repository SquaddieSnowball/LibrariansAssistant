using LibrariansAssistant.Domain.Models.Abstractions;
using LibrariansAssistant.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace LibrariansAssistant.Domain.Models.Implementations;

/// <summary>
/// Represents a book.
/// </summary>
public sealed class BookModel : IBookModel
{
    /// <summary>
    /// Gets or sets the ID of the book.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the authors of the book.
    /// </summary>
    [EnumerableMustHaveElements(ErrorMessage = "Authors are required.")]
    public IEnumerable<IAuthorModel> Authors { get; set; }

    /// <summary>
    /// Gets or sets the title of the book.
    /// </summary>
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(50, ErrorMessage = "Title cannot exceed {1} characters.")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the genre of the book.
    /// </summary>
    [Required(ErrorMessage = "Genre is required.")]
    [StringLength(25, ErrorMessage = "Genre cannot exceed {1} characters.")]
    public string Genre { get; set; }

    /// <summary>
    /// Initializes a new instance of the BookModel class.
    /// </summary>
    public BookModel() =>
        (Authors, Title, Genre) = (Enumerable.Empty<IAuthorModel>(), string.Empty, string.Empty);

    /// <summary>
    /// Initializes a new instance of the BookModel class with the specified authors, title, and genre.
    /// </summary>
    /// <param name="authors">Authors of the book.</param>
    /// <param name="title">Title of the book.</param>
    /// <param name="genre">Genre of the book.</param>
    public BookModel(IEnumerable<IAuthorModel> authors, string title, string genre) =>
        (Authors, Title, Genre) = (authors, title, genre);
}