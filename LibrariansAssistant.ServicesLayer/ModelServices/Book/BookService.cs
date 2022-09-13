using LibrariansAssistant.DomainModelLayer.Models.Book;

namespace LibrariansAssistant.ServicesLayer.ModelServices.Book;

public sealed class BookService : IBookService
{
    public void BookAdd(IBookModel book)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<IBookModel> BookGetAll()
    {
        throw new NotImplementedException();
    }

    public IBookModel BookGetById(int bookId)
    {
        throw new NotImplementedException();
    }

    public void BookUpdate(IBookModel book)
    {
        throw new NotImplementedException();
    }

    public void BookDelete(int bookId)
    {
        throw new NotImplementedException();
    }
}