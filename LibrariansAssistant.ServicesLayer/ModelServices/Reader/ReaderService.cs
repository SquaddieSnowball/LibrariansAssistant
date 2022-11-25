using LibrariansAssistant.DomainModelLayer.Models.Reader;
using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;
using LibrariansAssistant.ServicesLayer.CommonServices.DataAnnotationModelValidationService;

namespace LibrariansAssistant.ServicesLayer.ModelServices.Reader;

public sealed class ReaderService : IReaderService
{
    private readonly IReaderRepository _readerRepository;
    private readonly IDataAnnotationModelValidationService _dataAnnotationModelValidationService;

    public ReaderService(IReaderRepository readerRepository,
        IDataAnnotationModelValidationService dataAnnotationModelValidationService) =>
        (_readerRepository, _dataAnnotationModelValidationService) =
        (readerRepository, dataAnnotationModelValidationService);

    public void ReaderAdd(IReaderModel reader)
    {
        try
        {
            _dataAnnotationModelValidationService.Validate(reader);

            _readerRepository.ReaderAdd(reader);
        }
        catch
        {
            throw;
        }
    }

    public IEnumerable<IReaderModel> ReaderGetAll()
    {
        try
        {
            return _readerRepository.ReaderGetAll();
        }
        catch
        {
            throw;
        }
    }

    public IReaderModel? ReaderGetById(int readerId)
    {
        try
        {
            return _readerRepository.ReaderGetById(readerId);
        }
        catch
        {
            throw;
        }
    }

    public void ReaderUpdate(IReaderModel reader)
    {
        try
        {
            _dataAnnotationModelValidationService.Validate(reader);

            _readerRepository.ReaderUpdate(reader);
        }
        catch
        {
            throw;
        }
    }

    public void ReaderDelete(int readerId)
    {
        try
        {
            _readerRepository.ReaderDelete(readerId);
        }
        catch
        {
            throw;
        }
    }
}