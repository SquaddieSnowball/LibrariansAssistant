using LibrariansAssistant.DomainModelLayer.Models.Author;
using LibrariansAssistant.DomainModelLayer.Models.Reader;
using LibrariansAssistant.InfrastructureLayer.Repositories.Interfaces;

namespace LibrariansAssistant.ServicesLayer.ModelServices.Issuing;

/// <summary>
/// Provides methods for managing the issuing repository.
/// </summary>
public interface IIssuingService : IIssuingRepository
{
    /// <summary>
    /// Finds the most active reader.
    /// </summary>
    /// <returns>The most active reader.</returns>
    IReaderModel? ReaderMostActiveGet();

    /// <summary>
    /// Finds the most popular author.
    /// </summary>
    /// <returns>The most popular author.</returns>
    IAuthorModel? AuthorMostPopularGet();

    /// <summary>
    /// Finds the most popular book genre.
    /// </summary>
    /// <returns>The most popular book genre.</returns>
    public string? BookMostPopularGenreGet();
}