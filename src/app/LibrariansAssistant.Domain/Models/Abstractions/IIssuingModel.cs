namespace LibrariansAssistant.Domain.Models.Abstractions;

/// <summary>
/// Represents an issuing.
/// </summary>
public interface IIssuingModel
{
    /// <summary>
    /// Gets or sets the the ID of the issuing.
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// Gets or sets the reader.
    /// </summary>
    IReaderModel Reader { get; set; }

    /// <summary>
    /// Gets or sets the book.
    /// </summary>
    IBookModel Book { get; set; }

    /// <summary>
    /// Gets or sets the time the book was taken.
    /// </summary>
    DateTime TakeDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the book was returned.
    /// </summary>
    bool Returned { get; set; }

    /// <summary>
    /// Gets or sets the time the book was returned.
    /// </summary>
    DateTime? ReturnDate { get; set; }

    /// <summary>
    /// Gets or sets the return state of the book.
    /// </summary>
    int? ReturnState { get; set; }
}