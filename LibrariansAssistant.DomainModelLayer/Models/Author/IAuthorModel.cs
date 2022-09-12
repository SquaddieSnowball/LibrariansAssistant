namespace LibrariansAssistant.DomainModelLayer.Models.Author;

public interface IAuthorModel
{
    int Id { get; set; }

    string FirstName { get; set; }

    string LastName { get; set; }

    string? Patronymic { get; set; }
}