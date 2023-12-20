namespace LibrariansAssistant.Domain.Models.Abstractions;

/// <summary>
/// Represents the reader.
/// </summary>
public interface IReaderModel
{
    /// <summary>
    /// Gets or sets the ID of the reader.
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the reader.
    /// </summary>
    string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the reader.
    /// </summary>
    string LastName { get; set; }

    /// <summary>
    /// Gets or sets the reader's patronymic.
    /// </summary>
    string? Patronymic { get; set; }

    /// <summary>
    /// Gets or sets the gender of the reader.
    /// </summary>
    string Gender { get; set; }

    /// <summary>
    /// Gets or sets the date of birth of the reader.
    /// </summary>
    DateTime DateOfBirth { get; set; }
}