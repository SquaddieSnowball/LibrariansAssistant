using LibrariansAssistant.DomainModelLayer.Models.Reader;

namespace LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;

public interface IReaderRepository
{
    void ReaderAdd(IReaderModel reader);

    IEnumerable<IReaderModel> ReaderGetAll();

    IReaderModel? ReaderGetById(int readerId);

    void ReaderUpdate(IReaderModel reader);

    void ReaderDelete(int readerId);
}