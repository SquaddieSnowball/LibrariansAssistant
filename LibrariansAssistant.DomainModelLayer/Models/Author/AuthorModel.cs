using System.ComponentModel.DataAnnotations;

namespace LibrariansAssistant.DomainModelLayer.Models.Author;

public sealed class AuthorModel : IAuthorModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(25, ErrorMessage = "First name cannot exceed {1} characters.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed {1} characters.")]
    public string LastName { get; set; }

    [StringLength(25, ErrorMessage = "Patronymic cannot exceed {1} characters.")]
    public string? Patronymic { get; set; }

    public AuthorModel() =>
        (FirstName, LastName) = (string.Empty, string.Empty);

    public AuthorModel(string firstName, string lastName, string? patronymic) =>
        (FirstName, LastName, Patronymic) = (firstName, lastName, patronymic);
}