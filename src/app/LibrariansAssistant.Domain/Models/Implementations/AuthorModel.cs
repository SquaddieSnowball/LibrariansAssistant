using System.ComponentModel.DataAnnotations;
using LibrariansAssistant.Domain.Models.Abstractions;

namespace LibrariansAssistant.Domain.Models.Implementations;

/// <summary>
/// Represents the author.
/// </summary>
public sealed class AuthorModel : IAuthorModel
{
    /// <summary>
    /// Gets or sets the ID of the author.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the author.
    /// </summary>
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(25, ErrorMessage = "First name cannot exceed {1} characters.")]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the author.
    /// </summary>
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed {1} characters.")]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the author's patronymic.
    /// </summary>
    [StringLength(25, ErrorMessage = "Patronymic cannot exceed {1} characters.")]
    public string? Patronymic { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorModel"/> class.
    /// </summary>
    public AuthorModel() => (FirstName, LastName) = (default!, default!);

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorModel"/> class 
    /// with the specified first name, last name, and patronymic.
    /// </summary>
    /// <param name="firstName">First name of the author.</param>
    /// <param name="lastName">Last name of the author.</param>
    /// <param name="patronymic">Author's patronymic.</param>
    public AuthorModel(string firstName, string lastName, string? patronymic) =>
        (FirstName, LastName, Patronymic) =
        (firstName, lastName, patronymic);
}