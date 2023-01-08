using System.ComponentModel.DataAnnotations;

namespace LibrariansAssistant.DomainModelLayer.Models.Reader;

/// <summary>
/// Represents the reader.
/// </summary>
public sealed class ReaderModel : IReaderModel
{
    /// <summary>
    /// Gets or sets the ID of the reader.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the reader.
    /// </summary>
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(25, ErrorMessage = "First name cannot exceed {1} characters.")]
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the reader.
    /// </summary>
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed {1} characters.")]
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the reader's patronymic.
    /// </summary>
    [StringLength(25, ErrorMessage = "Patronymic cannot exceed {1} characters.")]
    public string? Patronymic { get; set; }

    /// <summary>
    /// Gets or sets the gender of the reader.
    /// </summary>
    [Required(ErrorMessage = "Gender is required.")]
    [StringLength(25, ErrorMessage = "Gender cannot exceed {1} characters.")]
    public string Gender { get; set; }

    /// <summary>
    /// Gets or sets the date of birth of the reader.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Initializes a new instance of the ReaderModel class.
    /// </summary>
    public ReaderModel() =>
        (FirstName, LastName, Gender) = (string.Empty, string.Empty, string.Empty);

    /// <summary>
    /// Initializes a new instance of the AuthorModel class with the specified 
    /// first name, last name, patronymic, gender and date of birth.
    /// </summary>
    /// <param name="firstName">First name of the author.</param>
    /// <param name="lastName">Last name of the author.</param>
    /// <param name="patronymic">Author's patronymic.</param>
    /// <param name="gender">Gender of the reader.</param>
    /// <param name="dateOfBirth">Date of birth of the reader.</param>
    public ReaderModel(string firstName, string lastName, string? patronymic,
        string gender, DateTime dateOfBirth) =>
        (FirstName, LastName, Patronymic, Gender, DateOfBirth) =
        (firstName, lastName, patronymic, gender, dateOfBirth);
}