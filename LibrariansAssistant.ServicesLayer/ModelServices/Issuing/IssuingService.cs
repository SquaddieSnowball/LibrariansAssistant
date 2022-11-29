using LibrariansAssistant.DomainModelLayer.Models.Author;
using LibrariansAssistant.DomainModelLayer.Models.Issuing;
using LibrariansAssistant.DomainModelLayer.Models.Reader;
using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;
using LibrariansAssistant.ServicesLayer.CommonServices.DataAnnotationModelValidationService;

namespace LibrariansAssistant.ServicesLayer.ModelServices.Issuing;

public sealed class IssuingService : IIssuingService
{
    private readonly IIssuingRepository _issuingRepository;
    private readonly IDataAnnotationModelValidationService _dataAnnotationModelValidationService;

    public IssuingService(IIssuingRepository issuingRepository,
        IDataAnnotationModelValidationService dataAnnotationModelValidationService) =>
        (_issuingRepository, _dataAnnotationModelValidationService) =
        (issuingRepository, dataAnnotationModelValidationService);

    public void IssuingAdd(IIssuingModel issuing)
    {
        try
        {
            _dataAnnotationModelValidationService.Validate(issuing);

            _issuingRepository.IssuingAdd(issuing);
        }
        catch
        {
            throw;
        }
    }

    public IEnumerable<IIssuingModel> IssuingGetAll()
    {
        try
        {
            return _issuingRepository.IssuingGetAll();
        }
        catch
        {
            throw;
        }
    }

    public IIssuingModel? IssuingGetById(int issuingId)
    {
        try
        {
            return _issuingRepository.IssuingGetById(issuingId);
        }
        catch
        {
            throw;
        }
    }

    public void IssuingUpdate(IIssuingModel issuing)
    {
        try
        {
            _dataAnnotationModelValidationService.Validate(issuing);

            _issuingRepository.IssuingUpdate(issuing);
        }
        catch
        {
            throw;
        }
    }

    public void IssuingDelete(int issuingId)
    {
        try
        {
            _issuingRepository.IssuingDelete(issuingId);
        }
        catch
        {
            throw;
        }
    }

    public IReaderModel? ReaderMostActiveGet()
    {
        IEnumerable<IIssuingModel> issuings;
        IReaderModel? reader = default;

        try
        {
            issuings = _issuingRepository.IssuingGetAll();
        }
        catch
        {
            throw;
        }

        if (issuings.Any() is true)
            reader = issuings
                .Select(i => i.Reader)
                .GroupBy(r => r.Id)
                .OrderByDescending(g => g.Count())
                .First()
                .First();

        return reader;
    }

    public IAuthorModel? AuthorMostPopularGet()
    {
        IEnumerable<IIssuingModel> issuings;
        IAuthorModel? author = default;

        try
        {
            issuings = _issuingRepository.IssuingGetAll();
        }
        catch
        {
            throw;
        }

        if (issuings.Any() is true)
            author = issuings
                .Select(i => i.Book.Authors)
                .Aggregate((a1, a2) => a1.Concat(a2))
                .GroupBy(a => a.Id)
                .OrderByDescending(g => g.Count())
                .First()
                .First();

        return author;
    }

    public string? BookMostPopularGenreGet()
    {
        IEnumerable<IIssuingModel> issuings;
        string? genre = default;

        try
        {
            issuings = _issuingRepository.IssuingGetAll();
        }
        catch
        {
            throw;
        }

        if (issuings.Any() is true)
            genre = issuings
                .Select(i => i.Book.Genre)
                .GroupBy(g => g)
                .OrderByDescending(g => g.Count())
                .First()
                .First();

        return genre;
    }
}