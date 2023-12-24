using LibrariansAssistant.Domain.Models.Abstractions;
using LibrariansAssistant.Infrastructure.Repositories.Abstractions;
using LibrariansAssistant.Services.Common.Abstractions;
using LibrariansAssistant.Services.Model.Abstractions;

namespace LibrariansAssistant.Services.Model.Implementations;

/// <summary>
/// Represents the issuing service.
/// </summary>
public sealed class IssuingService : IIssuingService
{
    private readonly IIssuingRepository _issuingRepository;
    private readonly IDataAnnotationsModelValidationService _dataAnnotationModelValidationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="IssuingService"/> class.
    /// </summary>
    /// <param name="issuingRepository">Issuing repository for management.</param>
    /// <param name="dataAnnotationModelValidationService">Data Annotations 
    /// model validation service used to validate models.</param>
    public IssuingService(IIssuingRepository issuingRepository,
        IDataAnnotationsModelValidationService dataAnnotationModelValidationService) =>
        (_issuingRepository, _dataAnnotationModelValidationService) =
        (issuingRepository, dataAnnotationModelValidationService);

    /// <summary>
    /// Adds a new issuing to the repository.
    /// </summary>
    /// <param name="issuing">Issuing to add to the repository.</param>
    public void IssuingAdd(IIssuingModel issuing)
    {
        _dataAnnotationModelValidationService.Validate(issuing);
        _issuingRepository.IssuingAdd(issuing);
    }

    /// <summary>
    /// Gets all issuings from the repository.
    /// </summary>
    /// <returns>All issuings from the repository.</returns>
    public IEnumerable<IIssuingModel> IssuingGetAll() => _issuingRepository.IssuingGetAll();

    /// <summary>
    /// Gets the issuing with the specified ID from the repository.
    /// </summary>
    /// <param name="issuingId">Issuing ID.</param>
    /// <returns>Issuing with the specified ID.</returns>
    public IIssuingModel? IssuingGetById(int issuingId) => _issuingRepository.IssuingGetById(issuingId);

    /// <summary>
    /// Updates an existing issuing in the repository.
    /// </summary>
    /// <param name="issuing">Existing issuing to update.</param>
    public void IssuingUpdate(IIssuingModel issuing)
    {
        _dataAnnotationModelValidationService.Validate(issuing);
        _issuingRepository.IssuingUpdate(issuing);
    }

    /// <summary>
    /// Removes an existing issuing from the repository.
    /// </summary>
    /// <param name="issuingId">Issuing ID.</param>
    public void IssuingDelete(int issuingId) => _issuingRepository.IssuingDelete(issuingId);

    /// <summary>
    /// Finds the most active reader.
    /// </summary>
    /// <returns>The most active reader.</returns>
    public IReaderModel? ReaderMostActiveGet()
    {
        IEnumerable<IIssuingModel> issuings = _issuingRepository.IssuingGetAll();
        IReaderModel? reader = default;

        if (issuings.Any() is true)
        {
            reader = issuings
                .Select(i => i.Reader)
                .GroupBy(r => r.Id)
                .OrderByDescending(g => g.Count())
                .First()
                .First();
        }

        return reader;
    }

    /// <summary>
    /// Finds the most popular author.
    /// </summary>
    /// <returns>The most popular author.</returns>
    public IAuthorModel? AuthorMostPopularGet()
    {
        IEnumerable<IIssuingModel> issuings = _issuingRepository.IssuingGetAll();
        IAuthorModel? author = default;

        if (issuings.Any() is true)
        {
            author = issuings
                .Select(i => i.Book.Authors)
                .Aggregate((a1, a2) => a1.Concat(a2))
                .GroupBy(a => a.Id)
                .OrderByDescending(g => g.Count())
                .First()
                .First();
        }

        return author;
    }

    /// <summary>
    /// Finds the most popular book genre.
    /// </summary>
    /// <returns>The most popular book genre.</returns>
    public string? BookMostPopularGenreGet()
    {
        IEnumerable<IIssuingModel> issuings = _issuingRepository.IssuingGetAll();
        string? genre = default;

        if (issuings.Any() is true)
        {
            genre = issuings
                .Select(i => i.Book.Genre)
                .GroupBy(g => g)
                .OrderByDescending(g => g.Count())
                .First()
                .First();
        }

        return genre;
    }
}