using LibrariansAssistant.DomainModelLayer.Models.Book;

namespace LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;

public interface IBookRepository
{
    void BookAdd(IBookModel book);

    IEnumerable<IBookModel> BookGetAll();

    IBookModel? BookGetById(int bookId);

    void BookUpdate(IBookModel book);

    void BookDelete(int bookId);
}