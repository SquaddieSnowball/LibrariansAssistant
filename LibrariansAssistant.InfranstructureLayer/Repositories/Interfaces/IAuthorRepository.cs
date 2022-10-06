using LibrariansAssistant.DomainModelLayer.Models.Author;

namespace LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;

public interface IAuthorRepository
{
    void AuthorAdd(IAuthorModel author);

    IEnumerable<IAuthorModel> AuthorGetAll();

    IAuthorModel? AuthorGetById(int authorId);

    void AuthorUpdate(IAuthorModel author);

    void AuthorDelete(int authorId);
}