namespace LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;

public interface IRepository : IAuthorRepository, IBookRepository, IIssuingRepository, IReaderRepository
{
    void Initialize(string initializationString);
}