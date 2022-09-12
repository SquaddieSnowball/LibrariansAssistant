namespace LibrariansAssistant.DomainModelLayer.Models.Author;

public sealed class AuthorModel : IAuthorModel
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Patronymic { get; set; }

    public AuthorModel(int id, string firstName, string lastName, string? patronymic) =>
        (Id, FirstName, LastName, Patronymic) = (id, firstName, lastName, patronymic);
}