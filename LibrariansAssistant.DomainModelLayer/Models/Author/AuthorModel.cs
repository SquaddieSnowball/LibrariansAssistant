namespace LibrariansAssistant.DomainModelLayer.Models.Author;

public sealed class AuthorModel : IAuthorModel
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Patronymic { get; set; }

    public AuthorModel() =>
        (FirstName, LastName) = (string.Empty, string.Empty);

    public AuthorModel(string firstName, string lastName, string? patronymic) =>
        (FirstName, LastName, Patronymic) = (firstName, lastName, patronymic);
}