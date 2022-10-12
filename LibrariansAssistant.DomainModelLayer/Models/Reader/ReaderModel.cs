namespace LibrariansAssistant.DomainModelLayer.Models.Reader;

public sealed class ReaderModel : IReaderModel
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Patronymic { get; set; }

    public string Gender { get; set; }

    public DateTime DateOfBirth { get; set; }

    public ReaderModel() =>
        (FirstName, LastName, Gender) = (string.Empty, string.Empty, string.Empty);

    public ReaderModel(string firstName, string lastName, string? patronymic,
        string gender, DateTime dateOfBirth) =>
        (FirstName, LastName, Patronymic, Gender, DateOfBirth) =
        (firstName, lastName, patronymic, gender, dateOfBirth);
}