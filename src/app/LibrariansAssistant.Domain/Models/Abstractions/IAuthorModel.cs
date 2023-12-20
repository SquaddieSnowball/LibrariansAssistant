namespace LibrariansAssistant.Domain.Models.Abstractions;

/// <summary>
/// Represents the author.
/// </summary>
public interface IAuthorModel
{
    /// <summary>
    /// Gets or sets the ID of the author.
    /// </summary>
    int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the author.
    /// </summary>
    string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the author.
    /// </summary>
    string LastName { get; set; }

    /// <summary>
    /// Gets or sets the author's patronymic.
    /// </summary>
    string? Patronymic { get; set; }
}