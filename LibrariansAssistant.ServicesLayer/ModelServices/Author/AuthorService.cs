using LibrariansAssistant.DomainModelLayer.Models.Author;
using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;
using LibrariansAssistant.ServicesLayer.CommonServices.DataAnnotationModelValidationService;

namespace LibrariansAssistant.ServicesLayer.ModelServices.Author;

public sealed class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IDataAnnotationModelValidationService _dataAnnotationModelValidationService;

    public AuthorService(IAuthorRepository authorRepository,
        IDataAnnotationModelValidationService dataAnnotationModelValidationService) =>
        (_authorRepository, _dataAnnotationModelValidationService) =
        (authorRepository, dataAnnotationModelValidationService);

    public void AuthorAdd(IAuthorModel author)
    {
        try
        {
            _dataAnnotationModelValidationService.Validate(author);

            _authorRepository.AuthorAdd(author);
        }
        catch
        {
            throw;
        }
    }

    public IEnumerable<IAuthorModel> AuthorGetAll()
    {
        try
        {
            return _authorRepository.AuthorGetAll();
        }
        catch
        {
            throw;
        }
    }

    public IAuthorModel? AuthorGetById(int authorId)
    {
        try
        {
            return _authorRepository.AuthorGetById(authorId);
        }
        catch
        {
            throw;
        }
    }

    public void AuthorUpdate(IAuthorModel author)
    {
        try
        {
            _dataAnnotationModelValidationService.Validate(author);

            _authorRepository.AuthorUpdate(author);
        }
        catch
        {
            throw;
        }
    }

    public void AuthorDelete(int authorId)
    {
        try
        {
            _authorRepository.AuthorDelete(authorId);
        }
        catch
        {
            throw;
        }
    }
}