using System.ComponentModel.DataAnnotations;

namespace LibrariansAssistant.DomainModelLayer.Models.Reader;

public sealed class ReaderModel : IReaderModel
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

    [Required(ErrorMessage = "Gender is required.")]
    [StringLength(25, ErrorMessage = "Gender cannot exceed {1} characters.")]
    public string Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public ReaderModel() =>
        (FirstName, LastName, Gender) = (string.Empty, string.Empty, string.Empty);

    public ReaderModel(string firstName, string lastName, string? patronymic,
        string gender, DateTime dateOfBirth) =>
        (FirstName, LastName, Patronymic, Gender, DateOfBirth) =
        (firstName, lastName, patronymic, gender, dateOfBirth);
}