namespace LibrariansAssistant.DomainModelLayer.Models.Reader;

public interface IReaderModel
{
    int Id { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }

    string? Patronymic { get; set; }

    string Gender { get; set; }

    DateTime DateOfBirth { get; set; }
}