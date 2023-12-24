using LibrariansAssistant.Domain.Models.Abstractions;
using LibrariansAssistant.Infrastructure.Repositories.Abstractions;
using LibrariansAssistant.Services.Common.Abstractions;
using LibrariansAssistant.Services.Model.Abstractions;

namespace LibrariansAssistant.Services.Model.Implementations;

/// <summary>
/// Represents the author service.
/// </summary>
public sealed class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IDataAnnotationsModelValidationService _dataAnnotationModelValidationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorService"/> class.
    /// </summary>
    /// <param name="authorRepository">Author repository for management.</param>
    /// <param name="dataAnnotationModelValidationService">Data Annotations 
    /// model validation service used to validate models.</param>
    public AuthorService(IAuthorRepository authorRepository,
        IDataAnnotationsModelValidationService dataAnnotationModelValidationService) =>
        (_authorRepository, _dataAnnotationModelValidationService) =
        (authorRepository, dataAnnotationModelValidationService);

    /// <summary>
    /// Adds a new author to the repository.
    /// </summary>
    /// <param name="author">Author to add to the repository.</param>
    public void AuthorAdd(IAuthorModel author)
    {
        _dataAnnotationModelValidationService.Validate(author);
        _authorRepository.AuthorAdd(author);
    }

    /// <summary>
    /// Gets all authors from the repository.
    /// </summary>
    /// <returns>All authors from the repository.</returns>
    public IEnumerable<IAuthorModel> AuthorGetAll() => _authorRepository.AuthorGetAll();

    /// <summary>
    /// Gets the author with the specified ID from the repository.
    /// </summary>
    /// <param name="authorId">Author ID.</param>
    /// <returns>Author with the specified ID.</returns>
    public IAuthorModel? AuthorGetById(int authorId) => _authorRepository.AuthorGetById(authorId);

    /// <summary>
    /// Updates an existing author in the repository.
    /// </summary>
    /// <param name="author">Existing author to update.</param>
    public void AuthorUpdate(IAuthorModel author)
    {
        _dataAnnotationModelValidationService.Validate(author);
        _authorRepository.AuthorUpdate(author);
    }

    /// <summary>
    /// Removes an existing author from the repository.
    /// </summary>
    /// <param name="authorId">Author ID.</param>
    public void AuthorDelete(int authorId) => _authorRepository.AuthorDelete(authorId);
}