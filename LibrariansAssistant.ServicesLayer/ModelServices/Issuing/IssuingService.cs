using LibrariansAssistant.DomainModelLayer.Models.Issuing;
using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;
using LibrariansAssistant.ServicesLayer.CommonServices.Interfaces;

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
}