using LibrariansAssistant.DomainModelLayer.Models.Reader;
using LibrariansAssistant.InfrastructureLayer.Repositories.Interfaces;
using LibrariansAssistant.ServicesLayer.CommonServices.DataAnnotationModelValidationService;

namespace LibrariansAssistant.ServicesLayer.ModelServices.Reader;

/// <summary>
/// Represents the reader service.
/// </summary>
public sealed class ReaderService : IReaderService
{
    private readonly IReaderRepository _readerRepository;
    private readonly IDataAnnotationModelValidationService _dataAnnotationModelValidationService;

    /// <summary>
    /// Initializes a new instance of the ReaderService class.
    /// </summary>
    /// <param name="readerRepository">Reader repository for management.</param>
    /// <param name="dataAnnotationModelValidationService">Data Annotation 
    /// model validation service used to validate models.</param>
    public ReaderService(IReaderRepository readerRepository,
        IDataAnnotationModelValidationService dataAnnotationModelValidationService) =>
        (_readerRepository, _dataAnnotationModelValidationService) =
        (readerRepository, dataAnnotationModelValidationService);

    /// <summary>
    /// Adds a new reader to the repository.
    /// </summary>
    /// <param name="reader">Reader to add to the repository.</param>
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

    /// <summary>
    /// Gets all readers from the repository.
    /// </summary>
    /// <returns>All readers from the repository.</returns>
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

    /// <summary>
    /// Gets the reader with the specified ID from the repository.
    /// </summary>
    /// <param name="readerId">Reader ID.</param>
    /// <returns>Reader with the specified ID.</returns>
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

    /// <summary>
    /// Updates an existing reader in the repository.
    /// </summary>
    /// <param name="reader">Existing reader to update.</param>
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

    /// <summary>
    /// Removes an existing reader from the repository.
    /// </summary>
    /// <param name="readerId">Reader ID.</param>
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