using LibrariansAssistant.DomainModelLayer.Models.Issuing;

namespace LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;

/// <summary>
/// Provides CRUD methods for the issuing repository.
/// </summary>
public interface IIssuingRepository
{
    /// <summary>
    /// Adds a new issuing to the repository.
    /// </summary>
    /// <param name="issuing">Issuing to add to the repository.</param>
    void IssuingAdd(IIssuingModel issuing);

    /// <summary>
    /// Gets all issuings from the repository.
    /// </summary>
    /// <returns>All issuings from the repository.</returns>
    IEnumerable<IIssuingModel> IssuingGetAll();

    /// <summary>
    /// Gets the issuing with the specified ID from the repository.
    /// </summary>
    /// <param name="issuingId">Issuing ID.</param>
    /// <returns>Issuing with the specified ID.</returns>
    IIssuingModel? IssuingGetById(int issuingId);

    /// <summary>
    /// Updates an existing issuing in the repository.
    /// </summary>
    /// <param name="issuing">Existing issuing to update.</param>
    void IssuingUpdate(IIssuingModel issuing);

    /// <summary>
    /// Removes an existing issuing from the repository.
    /// </summary>
    /// <param name="issuingId">Issuing ID.</param>
    void IssuingDelete(int issuingId);
}