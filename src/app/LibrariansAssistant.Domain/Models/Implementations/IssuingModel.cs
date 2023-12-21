using LibrariansAssistant.Dependencies;
using LibrariansAssistant.Domain.Models.Abstractions;
using LibrariansAssistant.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace LibrariansAssistant.Domain.Models.Implementations;

/// <summary>
/// Represents an issuing.
/// </summary>
public sealed class IssuingModel : IIssuingModel
{
    /// <summary>
    /// Gets or sets the the ID of the issuing.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the reader.
    /// </summary>
    [NotDefaultModel(ErrorMessage = "Reader is required.")]
    public IReaderModel Reader { get; set; }

    /// <summary>
    /// Gets or sets the book.
    /// </summary>
    [NotDefaultModel(ErrorMessage = "Book is required.")]
    public IBookModel Book { get; set; }

    /// <summary>
    /// Gets or sets the time the book was taken.
    /// </summary>
    public DateTime TakeDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the book was returned.
    /// </summary>
    public bool Returned { get; set; }

    /// <summary>
    /// Gets or sets the time the book was returned.
    /// </summary>
    public DateTime? ReturnDate { get; set; }

    /// <summary>
    /// Gets or sets the return state of the book.
    /// </summary>
    [Range(1, 100, ErrorMessage = "Return state must be in range of 1 to 100.")]
    public int? ReturnState { get; set; }

    /// <summary>
    /// Initializes a new instance of the IssuingModel class.
    /// </summary>
    public IssuingModel() =>
        (Reader, Book) =
        (DependenciesContainer.Resolve<IReaderModel>()!, DependenciesContainer.Resolve<IBookModel>()!);

    /// <summary>
    /// Initializes a new instance of the IssuingModel class with the specified 
    /// reader, book, take date, return status, return date and return state.
    /// </summary>
    /// <param name="reader">Reader.</param>
    /// <param name="book">Book.</param>
    /// <param name="takeDate">The time the book was taken.</param>
    /// <param name="returned">Value indicating whether the book was returned.</param>
    /// <param name="returnDate">The time the book was returned.</param>
    /// <param name="returnState">Return state of the book.</param>
    public IssuingModel(IReaderModel reader, IBookModel book, DateTime takeDate,
        bool returned, DateTime? returnDate, int? returnState) =>
        (Reader, Book, TakeDate, Returned, ReturnDate, ReturnState) =
        (reader, book, takeDate, returned, returnDate, returnState);
}