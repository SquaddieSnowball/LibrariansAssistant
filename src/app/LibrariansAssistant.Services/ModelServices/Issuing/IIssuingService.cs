using LibrariansAssistant.Domain.Models.Author;
using LibrariansAssistant.Domain.Models.Reader;
using LibrariansAssistant.Infrastructure.Repositories.Interfaces;

namespace LibrariansAssistant.Services.ModelServices.Issuing;

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