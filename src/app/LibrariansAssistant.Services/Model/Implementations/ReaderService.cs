using LibrariansAssistant.Domain.Models.Abstractions;
using LibrariansAssistant.Infrastructure.Repositories.Abstractions;
using LibrariansAssistant.Services.Common.Abstractions;
using LibrariansAssistant.Services.Model.Abstractions;

namespace LibrariansAssistant.Services.Model.Implementations;

/// <summary>
/// Represents the reader service.
/// </summary>
public sealed class ReaderService : IReaderService
{
    private readonly IReaderRepository _readerRepository;
    private readonly IDataAnnotationsModelValidationService _dataAnnotationModelValidationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReaderService"/> class.
    /// </summary>
    /// <param name="readerRepository">Reader repository for management.</param>
    /// <param name="dataAnnotationModelValidationService">Data Annotations 
    /// model validation service used to validate models.</param>
    public ReaderService(IReaderRepository readerRepository,
        IDataAnnotationsModelValidationService dataAnnotationModelValidationService) =>
        (_readerRepository, _dataAnnotationModelValidationService) =
        (readerRepository, dataAnnotationModelValidationService);

    /// <summary>
    /// Adds a new reader to the repository.
    /// </summary>
    /// <param name="reader">Reader to add to the repository.</param>
    public void ReaderAdd(IReaderModel reader)
    {
        _dataAnnotationModelValidationService.Validate(reader);
        _readerRepository.ReaderAdd(reader);
    }

    /// <summary>
    /// Gets all readers from the repository.
    /// </summary>
    /// <returns>All readers from the repository.</returns>
    public IEnumerable<IReaderModel> ReaderGetAll() => _readerRepository.ReaderGetAll();

    /// <summary>
    /// Gets the reader with the specified ID from the repository.
    /// </summary>
    /// <param name="readerId">Reader ID.</param>
    /// <returns>Reader with the specified ID.</returns>
    public IReaderModel? ReaderGetById(int readerId) => _readerRepository.ReaderGetById(readerId);

    /// <summary>
    /// Updates an existing reader in the repository.
    /// </summary>
    /// <param name="reader">Existing reader to update.</param>
    public void ReaderUpdate(IReaderModel reader)
    {
        _dataAnnotationModelValidationService.Validate(reader);
        _readerRepository.ReaderUpdate(reader);
    }

    /// <summary>
    /// Removes an existing reader from the repository.
    /// </summary>
    /// <param name="readerId">Reader ID.</param>
    public void ReaderDelete(int readerId) => _readerRepository.ReaderDelete(readerId);
}