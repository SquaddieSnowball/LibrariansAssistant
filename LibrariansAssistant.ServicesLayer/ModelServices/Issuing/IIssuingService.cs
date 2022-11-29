using LibrariansAssistant.DomainModelLayer.Models.Author;
using LibrariansAssistant.DomainModelLayer.Models.Reader;
using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;

namespace LibrariansAssistant.ServicesLayer.ModelServices.Issuing;

public interface IIssuingService : IIssuingRepository
{
    IReaderModel? ReaderMostActiveGet();

    IAuthorModel? AuthorMostPopularGet();

    public string? BookMostPopularGenreGet();
}