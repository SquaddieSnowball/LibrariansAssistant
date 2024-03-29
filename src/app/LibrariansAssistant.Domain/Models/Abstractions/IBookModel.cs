﻿namespace LibrariansAssistant.Domain.Models.Abstractions;

/// <summary>
/// Represents a book.
/// </summary>
public interface IBookModel
{
    /// <summary>
    /// Gets or sets the ID of the book.
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// Gets or sets the authors of the book.
    /// </summary>
    IEnumerable<IAuthorModel> Authors { get; set; }

    /// <summary>
    /// Gets or sets the title of the book.
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets the genre of the book.
    /// </summary>
    string Genre { get; set; }
}